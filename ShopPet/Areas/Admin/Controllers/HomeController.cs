using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopPet.Areas.Models.ViewModel;
using ShopPet.Models;
using ShopPet.Models.ViewModels;
using ShopPet.Repository;

namespace ShopPet.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {

        private readonly DataContext _dataContext;
        public HomeController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        [Area("Admin")]
        public async Task<IActionResult> Index()
        {
            var soLuongHoaDon = await _dataContext.HoaDons.CountAsync();
            var query = from role in _dataContext.Roles
                        join userRole in _dataContext.UserRoles on role.Id equals userRole.RoleId
                        where role.Name == "user"
                        select new { role, userRole };
            var soluongkhachang = await query.CountAsync();
            var doanhThu = await _dataContext.ChiTietHoaDons.SumAsync(x => x.Price);
            var result = (from cthd in _dataContext.ChiTietHoaDons
                          join sp in _dataContext.SanPhams on cthd.SanPhamId equals sp.MaSP
                          join hd in _dataContext.HoaDons on cthd.HoaDonId equals hd.MaHD
                          group new { cthd, sp } by new { sp.MaSP, sp.TenSP, sp.MoTa, sp.GiongLoai, sp.GiaBan, sp.GiaNhap, sp.Anh, sp.SoLuongTon } into grouped
                          orderby grouped.Sum(x => x.cthd.Quantity) descending
                          select new ProductViewModel()
                          {
                              MaSP = grouped.Key.MaSP,
                              TenSP = grouped.Key.TenSP,
                              MoTa = grouped.Key.MoTa,
                              GiongLoai = grouped.Key.GiongLoai,
                              GiaBan = grouped.Key.GiaBan,
                              GiaNhap = grouped.Key.GiaNhap,
                              Anh = grouped.Key.Anh,
                              SoLuongTon = grouped.Key.SoLuongTon,
                              TongSoLuong = grouped.Sum(x => x.cthd.Quantity),
                          })
               .Take(5)
               .ToList();
            ViewBag.SoLuongHoaDon = soLuongHoaDon;
            ViewBag.soluongkhachang = soluongkhachang;
            ViewBag.doanhthu = doanhThu;

            return View(result);

        }
        public async Task<IActionResult> DoanhThu()
        {
            var doanhThuTheoThang = await (from hd in _dataContext.HoaDons
                                           join cthd in _dataContext.ChiTietHoaDons on hd.MaHD equals cthd.HoaDonId
                                           group new { hd, cthd } by new { Thang = hd.NgayTao.Month, Nam = hd.NgayTao.Year } into grouped
                                           select new ProductViewModel
                                           {
                                               Thang = grouped.Key.Thang,
                                               Nam = grouped.Key.Nam,
                                               TongDoanhThu = grouped.Sum(x => x.cthd.Price * x.cthd.Quantity),
                                           })
                                .ToListAsync();
            return View(doanhThuTheoThang);
        }
    }
}
