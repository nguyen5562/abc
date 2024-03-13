using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopPet.Models;
using ShopPet.Models.ViewModels;
using ShopPet.Repository;
using X.PagedList;

namespace ShopPet.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OrderController : Controller
    {
        private readonly DataContext _dataContext;
        public OrderController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<IActionResult> Index(string email, string diachi, string sdt, DateTime beginDate = default, DateTime endDate = default, int page1 = 1, int page2 = 1, int page3 = 1, int page4 = 1, int pageSize = 5, int pageId = 1)
        {
            //var query = await _dataContext.HoaDons.ToListAsync();
            ////var model = query.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            //var totalItemCount = query.Count();
            ////var pagedList = new StaticPagedList<HoaDon>(model, page, pageSize, totalItemCount);
            //var pagedList = query.ToPagedList();
            //ViewBag.PageStartItem = (page - 1) * pageSize + 1;
            //ViewBag.PageEndItem = Math.Min(page * pageSize, totalItemCount);
            //ViewBag.TotalItemCount = totalItemCount;
            //ViewBag.Page = page;
            //return View(pagedList);
            if (beginDate == default)
            {
                beginDate = DateTime.MinValue.Date;
            }

            if (endDate == default)
            {
                endDate = DateTime.MaxValue.Date;
            }

            // Tổng
            var query = await _dataContext.HoaDons.ToListAsync();
            //var pagedList = query.ToPagedList(page, pageSize);

            // Tìm kiếm
            if (!string.IsNullOrEmpty(email))
                query = query.Where(o => o.UserName.Contains(email, StringComparison.OrdinalIgnoreCase)).ToList();

            if (!string.IsNullOrEmpty(diachi))
                query = query.Where(h => h.DiaChi.Contains(diachi, StringComparison.OrdinalIgnoreCase)).ToList();

            if (!string.IsNullOrEmpty(sdt))
                query = query.Where(h => h.SĐT.Contains(sdt, StringComparison.OrdinalIgnoreCase)).ToList();

            if (beginDate < endDate)
                query = query.Where(h => h.NgayTao > beginDate && h.NgayTao < endDate).ToList();

            var totalItemCount = query.Count();
            ViewBag.TotalItemCount = totalItemCount;
            //ViewBag.PagedList = pagedList;


            // Chờ xác nhận
            var query1 = query.Where(o => o.TrangThai == 1);
            var pagedList1 = query1.ToPagedList(page1, pageSize);
            var totalItemCount1 = query1.Count();
            ViewBag.PageStartItem1 = (page1 - 1) * pageSize + 1;
            ViewBag.PageEndItem1 = Math.Min(page1 * pageSize, totalItemCount1);
            ViewBag.TotalItemCount1 = totalItemCount1;
            ViewBag.PagedList1 = pagedList1;

            // Đang giao hàng
            var query2 = query.Where(o => o.TrangThai == 2);
            var pagedList2 = query2.ToPagedList(page2, pageSize);
            var totalItemCount2 = query2.Count();
            ViewBag.PageStartItem2 = (page2 - 1) * pageSize + 1;
            ViewBag.PageEndItem2 = Math.Min(page2 * pageSize, totalItemCount2);
            ViewBag.TotalItemCount2 = totalItemCount2;
            ViewBag.PagedList2 = pagedList2;

            // Đã giao hàng
            var query3 = query.Where(o => o.TrangThai == 3);
            var pagedList3 = query3.ToPagedList(page3, pageSize);
            var totalItemCount3 = query3.Count();
            ViewBag.PageStartItem3 = (page3 - 1) * pageSize + 1;
            ViewBag.PageEndItem3 = Math.Min(page3 * pageSize, totalItemCount3);
            ViewBag.TotalItemCount3 = totalItemCount3;
            ViewBag.PagedList3 = pagedList3;

            // Đã hủy
            var query4 = query.Where(o => o.TrangThai == 4);
            var pagedList4 = query4.ToPagedList(page4, pageSize);
            var totalItemCount4 = query4.Count();
            ViewBag.PageStartItem4 = (page4 - 1) * pageSize + 1;
            ViewBag.PageEndItem4 = Math.Min(page4 * pageSize, totalItemCount4);
            ViewBag.TotalItemCount4 = totalItemCount4;
            ViewBag.PagedList4 = pagedList4;

            ViewBag.email = email;
            ViewBag.diachi = diachi;
            ViewBag.sdt = sdt;
            ViewBag.beginDate = beginDate;
            ViewBag.endDate = endDate;
            ViewBag.Page1 = page1;
            ViewBag.Page2 = page2;
            ViewBag.Page3 = page3;
            ViewBag.Page4 = page4;
            ViewBag.PageId = pageId;

            return View();
        }

        public IActionResult Xacnhan(int id, int page)
        {
            var hoadon = _dataContext.HoaDons.FirstOrDefault(hd => hd.MaHD == id);
            hoadon.TrangThai = 2;
            _dataContext.Update(hoadon);
            _dataContext.SaveChanges();
            return RedirectToAction("Index", page);
        }

        public IActionResult Vanchuyen(int id, int page)
        {
            var hoadon = _dataContext.HoaDons.FirstOrDefault(hd => hd.MaHD == id);
            hoadon.TrangThai = 3;
            _dataContext.Update(hoadon);
            _dataContext.SaveChanges();
            return RedirectToAction("Index", page);
        }
        public IActionResult Huy(int id, int page)
        {
            var hoadon = _dataContext.HoaDons.FirstOrDefault(hd => hd.MaHD == id);
            hoadon.TrangThai = 4;
            _dataContext.Update(hoadon);
            _dataContext.SaveChanges();
            return RedirectToAction("Index", page);
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
	}
}
