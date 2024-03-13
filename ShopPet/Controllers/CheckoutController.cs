using Microsoft.AspNetCore.Mvc;
using ShoPet.Repository;
using ShopPet.Models;
using ShopPet.Repository;
using System.Security.Claims;
using PayPal.Core;
using PayPal.v1.Payments;
using BraintreeHttp;
using Newtonsoft.Json;

namespace ShopPet.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly string _clientId;
        private readonly string _secretKey;
        private readonly double TyGia = 10000;

        public CheckoutController(DataContext dataContext, IConfiguration config)
        {
            _dataContext = dataContext;
            _clientId = config["PaypalSettings:ClientId"];
            _secretKey = config["PaypalSettings:SecretKey"];
        }

        public IActionResult Index()
        {
            List<CartItemModel> cartItems = HttpContext.Session.GetJson<List<CartItemModel>>("Cart") ?? new List<CartItemModel>();
            int tongSoLuong = cartItems.Sum(item => item.soLuong);
            ViewBag.TongSoLuong = tongSoLuong;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Checkout()
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);

            if (userEmail == null)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                List<CartItemModel> cartItems = HttpContext.Session.GetJson<List<CartItemModel>>("Cart") ?? new List<CartItemModel>();
                if (cartItems.Count == 0)
                {
                    TempData["error"] = "Giỏ hàng của bạn đang trống. Vui lòng thêm sản phẩm vào giỏ hàng trước khi thanh toán.";
                    return RedirectToAction("Index", "Cart");
                }
                var orderCode = Guid.NewGuid().ToString();
                var orderItem = new HoaDon
                {
                    NgayTao = DateTime.Now,
                    OrderCode = orderCode,
                    UserName = userEmail,
                    SĐT = Request.Form["SĐT"],
                    Note = Request.Form["Note"],
                    TrangThai = 1,
                    ThanhToan = 0,
                    DiaChi = Request.Form["DiaChi"]
                };
                _dataContext.Add(orderItem);
                _dataContext.SaveChanges();


                foreach (var cart in cartItems)
                {
                    var orderDetail = new ChiTietHoaDon();
                    orderDetail.HoaDonId = orderItem.MaHD;
                    orderDetail.UserName = userEmail;
                    orderDetail.OrderCode = orderCode;
                    orderDetail.SanPhamId = cart.MaSP;
                    orderDetail.Price = (decimal)cart.thanhTien;
                    orderDetail.Quantity = cart.soLuong;
                    var product = _dataContext.SanPhams.Find(cart.MaSP);
                    if (product != null)
                    {
                        product.SoLuongTon -= cart.soLuong;
                    }
                    _dataContext.Add(orderDetail);
                    _dataContext.SaveChanges();
                }
                HttpContext.Session.Remove("Cart");
                TempData["success"] = "Thanh toán thành công, vui lòng chờ duyệt đơn hàng";
                return RedirectToAction("Index", "Cart");
            }
            return View();
        }

        public async Task<IActionResult> PaypalCheckout()
        {
            var paypalOrderId = DateTime.Now.Ticks;
            var hostname = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}";

			var userEmail = User.FindFirstValue(ClaimTypes.Email);

            if (userEmail == null)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                List<CartItemModel> cartItems = HttpContext.Session.GetJson<List<CartItemModel>>("Cart") ?? new List<CartItemModel>();
                if (cartItems.Count == 0)
                {
                    TempData["error"] = "Giỏ hàng của bạn đang trống. Vui lòng thêm sản phẩm vào giỏ hàng trước khi thanh toán.";
                    return RedirectToAction("Index", "Cart");
                }

                var environment = new SandboxEnvironment(_clientId, _secretKey);
                var client = new PayPalHttpClient(environment);

                #region Create Paypal Order
                var itemList = new ItemList()
                {
                    Items = new List<Item>()
                };
                var total = Math.Round(cartItems.Sum(p => p.thanhTien) / TyGia, 2);
                foreach (var item in cartItems)
                {
                    itemList.Items.Add(new Item()
                    {
                        Name = item.TenSP,
                        Currency = "USD",
                        Price = Math.Round(item.donGia / TyGia, 2).ToString(),
                        Quantity = item.soLuong.ToString(),
                        Sku = "sku",
                        Tax = "0"
                    });
                }
                #endregion

                var payment = new Payment()
                {
                    Intent = "sale",
                    Transactions = new List<Transaction>()
                    {
                        new Transaction()
                        {
                            Amount = new Amount()
                            {
                                Total = total.ToString(),
                                Currency = "USD",
                                Details = new AmountDetails
                                {
                                    Tax = "0",
                                    Shipping = "0",
                                    Subtotal = total.ToString()
                                }
                            },
                            ItemList = itemList,
                            Description = $"Invoice #{paypalOrderId}",
                            InvoiceNumber = paypalOrderId.ToString()
                        }
                    },
                    RedirectUrls = new RedirectUrls()
                    {
                        CancelUrl = $"{hostname}/Checkout/CheckoutFail",
                        ReturnUrl = $"{hostname}/Checkout/CheckoutSuccess"
                    },
                    Payer = new Payer()
                    {
                        PaymentMethod = "paypal"
                    }
                };

                PaymentCreateRequest request = new PaymentCreateRequest();
                request.RequestBody(payment);

                try
                {
                    var response = await client.Execute(request);
                    var statusCode = response.StatusCode;
                    Payment result = response.Result<Payment>();

                    var links = result.Links.GetEnumerator();
                    string paypalRedirectUrl = null;
                    while (links.MoveNext())
                    {
                        LinkDescriptionObject lnk = links.Current;
                        if (lnk.Rel.ToLower().Trim().Equals("approval_url"))
                        {
                            //saving the payapalredirect URL to which user will be redirected for payment  
                            paypalRedirectUrl = lnk.Href;
                        }
                    }

                    return Redirect(paypalRedirectUrl);
                }
                catch (HttpException httpException)
                {
                    var statusCode = httpException.StatusCode;
                    var debugId = httpException.Headers.GetValues("PayPal-Debug-Id").FirstOrDefault();

                    //Process when Checkout with Paypal fails
                    return Redirect("/Checkout/CheckoutFail");
                }
            }
        }

        public IActionResult CheckoutFail()
        {
            TempData["success"] = "Chưa thanh toán thành công, vui lòng thanh toán lại";
            return RedirectToAction("Index", "Cart");
        }

        public async Task<IActionResult> CheckoutSuccess()
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);

            List<CartItemModel> cartItems = HttpContext.Session.GetJson<List<CartItemModel>>("Cart") ?? new List<CartItemModel>();
            var orderCode = Guid.NewGuid().ToString();
            var orderItem = new HoaDon
            {
                NgayTao = DateTime.Now,
                OrderCode = orderCode,
                UserName = userEmail,
                SĐT = "",
                Note = "",
                TrangThai = 1,
                ThanhToan = 1,
                DiaChi = ""
            };
            _dataContext.Add(orderItem);
            _dataContext.SaveChanges();

            foreach (var cart in cartItems)
            {
                var orderDetail = new ChiTietHoaDon();
                orderDetail.HoaDonId = orderItem.MaHD;
                orderDetail.UserName = userEmail;
                orderDetail.OrderCode = orderCode;
                orderDetail.SanPhamId = cart.MaSP;
                orderDetail.Price = (decimal)cart.thanhTien;
                orderDetail.Quantity = cart.soLuong;
                var product = _dataContext.SanPhams.Find(cart.MaSP);
                if (product != null)
                {
                    product.SoLuongTon -= cart.soLuong;
                }
                _dataContext.Add(orderDetail);
                _dataContext.SaveChanges();
            }

            HttpContext.Session.Remove("Cart");
            TempData["success"] = "Thanh toán thành công, vui lòng chờ duyệt đơn hàng";
            return RedirectToAction("Index", "Cart");
        }
    }
}
