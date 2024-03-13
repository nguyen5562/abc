using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoPet.Repository;
using ShopPet.Models;
using ShopPet.Repository;
using System.Diagnostics;
using X.PagedList;
using static System.Reflection.Metadata.BlobBuilder;

namespace ShopPet.Controllers
{
    public class HomeController : Controller
    {
		private readonly DataContext _dataContext;
		private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, DataContext context)
        {
            _logger = logger;
            _dataContext = context;
        }

		public IActionResult Index(int page = 1, int pageSize =6)
		{
			List<CartItemModel> cartItems = HttpContext.Session.GetJson<List<CartItemModel>>("Cart") ?? new List<CartItemModel>();
			int tongSoLuong = cartItems.Sum(item => item.soLuong);
			ViewBag.TongSoLuong = tongSoLuong;
			var query = _dataContext.SanPhams.OrderByDescending(sp => sp.MaSP);//sắp xếp giảm dần
			var model = query.Skip((page - 1) * pageSize).Take(pageSize).ToList();
			var totalItemCount = query.Count();
			var pagedList = new StaticPagedList<SanPham>(model, page, pageSize, totalItemCount);
			ViewBag.PageStartItem = (page - 1) * pageSize + 1;
			ViewBag.PageEndItem = Math.Min(page * pageSize, totalItemCount);
			ViewBag.Page = page;
			ViewBag.TotalItemCount = totalItemCount;
			/*ViewBag.lsp = lsp;*/
			return View(pagedList);
		}

		public IActionResult Privacy(int statuscode)
        {
            return View();
        }

		[ResponseCache(Duration =0,Location =ResponseCacheLocation.None,NoStore =true)]
		public IActionResult Error(int statuscode)
		{
			if(statuscode == 404)
			{
				return View("NotFound");
			}	
			return View(new ErrorViewModel { RequestId=Activity.Current?.Id??HttpContext.TraceIdentifier});
		}

		public async Task<IActionResult> Search(string TenSP, int page = 1, int pageSize = 6)
		{
			if (!String.IsNullOrEmpty(TenSP))
			{
				TenSP = TenSP.ToLower();
				var query = _dataContext.SanPhams.Where(b => b.TenSP.ToLower().Contains(TenSP));
				if (query.Count() == 0)
				{
					return RedirectToAction("thongBaoRong", "SanPham");
				}
				var model = query.Skip((page - 1) * pageSize).Take(pageSize).ToList();


				var totalItemCount = query.Count();
				var pagedList = new StaticPagedList<SanPham>(model, page, pageSize, totalItemCount);
				ViewBag.PageStartItem = (page - 1) * pageSize + 1;
				ViewBag.PageEndItem = Math.Min(page * pageSize, totalItemCount);
				ViewBag.Page = page;
				return View(pagedList);
			}
			return View();
		}

	}
}