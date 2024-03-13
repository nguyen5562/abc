using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoPet.Repository;
using ShopPet.Extensions;
using ShopPet.Models;
using ShopPet.Repository;
using X.PagedList;

namespace ShopPet.Controllers
{
    public class HistoryController : Controller
    {
        private readonly UserManager<AppUserModel> _userManager;
        private readonly DataContext _dataContext;
        public HistoryController(DataContext dataContext, UserManager<AppUserModel> userManager)
        {
            _dataContext = dataContext;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index(  int page = 1, int pageSize = 10)
        {
            

            var user = await _userManager.GetUserAsync(User);

            // Kiểm tra nếu user không null và có thuộc tính Email
            if (user != null && !string.IsNullOrEmpty(user.Email))
            {
				// Sử dụng Email trong truy vấn
				var query = _dataContext.HoaDons.Where(hd => hd.UserName == user.Email).OrderByDescending(hd => hd.NgayTao);
				List<CartItemModel> cartItems = HttpContext.Session.GetJson<List<CartItemModel>>("Cart") ?? new List<CartItemModel>();
                int tongSoLuong = cartItems.Sum(item => item.soLuong);
                ViewBag.TongSoLuong = tongSoLuong;              
                var model = query.Skip((page - 1) * pageSize).Take(pageSize).ToList();
                var totalItemCount = query.Count();
                var pagedList = new StaticPagedList<HoaDon>(model, page, pageSize, totalItemCount);
                ViewBag.PageStartItem = (page - 1) * pageSize + 1;
                ViewBag.PageEndItem = Math.Min(page * pageSize, totalItemCount);
                ViewBag.Page = page;
                ViewBag.TotalItemCount = totalItemCount;
                /*ViewBag.lsp = lsp;*/
                return View(pagedList);
            }
            else
            {
                // Xử lý khi không có thông tin về người dùng
                return RedirectToAction("Login", "Account"); // Hoặc chuyển hướng đến trang login
            }
        }
        public IActionResult Details(int id)
        {
            var orderDetailList = _dataContext.ChiTietHoaDons
                .Where(o => o.HoaDonId == id)
                .Join(
                    _dataContext.SanPhams,
                    orderDetail => orderDetail.SanPhamId,
                    product => product.MaSP,
                    (orderDetail, product) => new ChiTietHoaDon
                    {
                        SanPham = product,
                        Quantity = orderDetail.Quantity,
                        Price = orderDetail.Price,
                        // Add other properties as needed
                    })
                .ToList();

            return PartialView("_OrderDetailPartialView", orderDetailList);
        }
        public IActionResult Huy(int id, int page)
        {
            var hoadon = _dataContext.HoaDons.FirstOrDefault(hd => hd.MaHD == id);

            if (hoadon == null)
            {
                TempData["error"] = "Không tìm thấy đơn hàng.";
                return RedirectToAction("Index", page);
            }

            hoadon.TrangThai = 4;
            _dataContext.Update(hoadon);
            _dataContext.SaveChanges();

            var chiTietHoaDons = _dataContext.ChiTietHoaDons.Where(ct => ct.HoaDonId == id).ToList();
            foreach (var chiTiet in chiTietHoaDons)
            {
                var product = _dataContext.SanPhams.Find(chiTiet.SanPhamId);

                if (product != null)
                {
                    product.SoLuongTon += chiTiet.Quantity; 
                    _dataContext.Update(product);
                }
            }

            _dataContext.SaveChanges(); 

            return RedirectToAction("Index", page);
        }
    }
}
