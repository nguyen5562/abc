using Microsoft.AspNetCore.Mvc;

using ShoPet.Repository;
using ShopPet.Models;
using ShopPet.Models.ViewModels;
using ShopPet.Repository;

namespace ShopPet.Controllers
{
	public class CartController : Controller
	{
		private readonly DataContext _dataContext;
		public CartController(DataContext dataContext)
		{
			_dataContext = dataContext;
		}
		public IActionResult Index()
		{
			List<CartItemModel> cartItems = HttpContext.Session.GetJson<List<CartItemModel>>("Cart") ?? new List<CartItemModel>();
			int tongSoLuong = cartItems.Sum(item => item.soLuong);
			ViewBag.TongSoLuong = tongSoLuong;
			CartItemViewModel cartItemVM = new()
			{
				CartItems = cartItems,
				GrandTotal = cartItems.Sum(x => x.soLuong * x.donGia),

			};		
			return View(cartItemVM);
		}
		
		public IActionResult Checkout()
		{
			return View("~/View/Checkout/Index.cshtml");
		}
		public async Task<IActionResult> Add(int maSP)
		{
			SanPham sanpham = await _dataContext.SanPhams.FindAsync(maSP);
			List<CartItemModel> cartItem = HttpContext.Session.GetJson<List<CartItemModel>>("Cart") ?? new List<CartItemModel>();
			CartItemModel cartItems = cartItem.Where(c => c.MaSP == maSP).FirstOrDefault();

			if (cartItems == null)
			{
				cartItem.Add(new CartItemModel(sanpham));
			}
			else
			{
				cartItems.soLuong += 1;
			}
			HttpContext.Session.SetJson("Cart", cartItem);
			TempData["success"] = "Thêm vào giỏ hàng thành công";
			return Redirect(Request.Headers["Referer"].ToString());
		}
		public async Task<IActionResult> UpdateCart(int maSP, int newQuantity)
		{
			// Kiểm tra sản phẩm có tồn tại trong cơ sở dữ liệu hay không. Nếu không, xử lý lỗi.
			SanPham product = await _dataContext.SanPhams.FindAsync(maSP);
			if (product == null)
			{
				TempData["error"] = "Sản phẩm không tồn tại";
				return RedirectToAction("Error"); // Chuyển hướng đến trang thông báo lỗi
			}

			List<CartItemModel> cartItems = HttpContext.Session.GetJson<List<CartItemModel>>("Cart") ?? new List<CartItemModel>();
			CartItemModel cartItem = cartItems.FirstOrDefault(c => c.MaSP == maSP);

			if (cartItem != null)
			{
				if (newQuantity > 0)
				{
					if (newQuantity <= product.SoLuongTon)
					{
						cartItem.soLuong = newQuantity;
						TempData["success"] = "Cập nhật giỏ hàng thành công";
					}
					else
					{
						cartItem.soLuong = 1;
						TempData["ErrorMessage"] = $"Sản phẩm {cartItem.TenSP} chỉ còn {product.SoLuongTon} sản phẩm";
					}
				}
				else
				{
					TempData["ErrorMessage"] = "Số lượng không hợp lệ.";
				}
			}
			HttpContext.Session.SetJson("Cart", cartItems);
			
			return RedirectToAction(""); 
		}
		public async Task<IActionResult> Decrease(int maSP)
		{
			List<CartItemModel> cart = HttpContext.Session.GetJson<List<CartItemModel>>("Cart");
			CartItemModel cartItem = cart.Where(c => c.MaSP == maSP).FirstOrDefault();

			if (cartItem != null)
			{
				if (cartItem.soLuong > 1) 
				{
					cartItem.soLuong--; 
				}
				else
				{
					cart.RemoveAll(p => p.MaSP == maSP);
				}

				if (cart.Count == 0)
				{
					HttpContext.Session.Remove("Cart");
				}
				else
				{
					HttpContext.Session.SetJson("Cart", cart);
				}
			}
			TempData["success"] = "Bớt  một giỏ hàng thành công";
			return RedirectToAction("Index"); // Sửa từ "Redirect" thành "RedirectToAction"
		}
		public async Task<IActionResult> Increase(int maSP)
		{
			List<CartItemModel> cart = HttpContext.Session.GetJson<List<CartItemModel>>("Cart");
			CartItemModel cartItem = cart.Where(c => c.MaSP == maSP).FirstOrDefault();

			if (cartItem != null)
			{
				if (cartItem.soLuong >= 1)
				{
					cartItem.soLuong++; // Tăng số lượng
				}
				else
				{
					cart.RemoveAll(p => p.MaSP == maSP); // Xóa sản phẩm khỏi giỏ hàng nếu số lượng = 1
				}

				if (cart.Count == 0)
				{
					HttpContext.Session.Remove("Cart");
				}
				else
				{
					HttpContext.Session.SetJson("Cart", cart);
				}
			}
			TempData["success"] = "Thêm một giỏ hàng thành công";
			return RedirectToAction("Index"); // Redirect về action "Index"
		}
		public async Task<IActionResult> Remove(int maSP)
		{
			List<CartItemModel> cart = HttpContext.Session.GetJson<List<CartItemModel>>("Cart");
			cart.RemoveAll(c => c.MaSP == maSP);

			if (cart.Count == 0)
			{
				HttpContext.Session.Remove("Cart");
			}
			else
			{
				HttpContext.Session.SetJson("Cart", cart);
			}
			TempData["success"] = "xóa một giỏ hàng thành công";
			return RedirectToAction("Index"); // Redirect về action "Index"
		}
		public async Task<IActionResult> Clear(int maSP)
		{
			HttpContext.Session.Remove("Cart");
			TempData["success"] = "xóa tất cảt giỏ hàng thành công";
			return RedirectToAction("Index");
		}
	}
}
