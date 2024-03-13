using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoPet.Repository;
using ShopPet.Models;
using ShopPet.Models.ViewModels;
using ShopPet.Repository;
using X.PagedList;

namespace ShopPet.Controllers
{
    public class CategoryController : Controller
    {
		private readonly DataContext _dataContext;
		public CategoryController(DataContext dataContext)
		{
			_dataContext = dataContext;
		}
		public async Task<IActionResult> Index(string searchTen, List<string> lsp, List<string> ncc, List<string> loai, decimal minban, decimal maxban, int page = 1, int pageSize = 6)
		{
			List<CartItemModel> cartItems = HttpContext.Session.GetJson<List<CartItemModel>>("Cart") ?? new List<CartItemModel>();
			int tongSoLuong = cartItems.Sum(item => item.soLuong);
			ViewBag.TongSoLuong = tongSoLuong;
			ViewBag.ListLoaiSp = await _dataContext.LoaiSanPhams.ToListAsync();
			ViewBag.ListNcc = await _dataContext.NhaCungCaps.ToListAsync();

			var query = from sp in _dataContext.SanPhams
						join nc in _dataContext.NhaCungCaps on sp.NhaCungCapId equals nc.MaNCC
						join ls in _dataContext.LoaiSanPhams on sp.LoaiSanPhamId equals ls.MaLoaiSP
						select new ProductViewModel()
						{
							MaSP = sp.MaSP,
							TenSP = sp.TenSP,
							MoTa = sp.MoTa,
							GiongLoai = sp.GiongLoai,
							GiaBan = sp.GiaBan,
							Anh = sp.Anh,
							SoLuongTon = sp.SoLuongTon,
							MaLSP = ls.MaLoaiSP,
							LoaiSanPham = ls.TenLoaiSP,
							MaNCC = nc.MaNCC,
							NhaCungCap = nc.TenNCC
						};

			if (!string.IsNullOrEmpty(searchTen))
			{
				query = query.Where(s => s.TenSP.Contains(searchTen));
			}

            if (lsp != null && lsp.Any())
            {
                query = query.Where(s => lsp.Contains(s.LoaiSanPham));
            }

            if (ncc != null && ncc.Any())
            {
                query = query.Where(s => ncc.Contains(s.NhaCungCap));
            }

            if (loai != null && loai.Any())
            {
                query = query.Where(s => loai.Contains(s.GiongLoai));
            }

            if (maxban != 0 || minban != 0)
			{
				query = query.Where(item => item.GiaBan >= minban && item.GiaBan <= maxban);
			}

			var totalItemCount = await query.CountAsync();
			var model = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
			var pagedList = new StaticPagedList<ProductViewModel>(model, page, pageSize, totalItemCount);

			ViewBag.PageStartItem = (page - 1) * pageSize + 1;
			ViewBag.PageEndItem = Math.Min(page * pageSize, totalItemCount);
			ViewBag.Page = page;
			ViewBag.TotalItemCount = totalItemCount;
			ViewBag.searchTen = searchTen;
			ViewBag.lsp = lsp;
			ViewBag.ncc = ncc;
			ViewBag.loai = loai;
			ViewBag.minban = minban;
			ViewBag.maxban = maxban;

			return View(pagedList);
		}
	}
}
