using Microsoft.AspNetCore.Mvc;
using ShoPet.Repository;
using ShopPet.Models;
using ShopPet.Repository;
using System.Drawing;
using static System.Reflection.Metadata.BlobBuilder;

namespace ShopPet.Controllers
{
    public class ProductController : Controller
    {
		private readonly DataContext _dataContext;
		public ProductController(DataContext dataContext)
		{
			_dataContext = dataContext;
		}
		public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Details(int MaSP)
        {
			List<CartItemModel> cartItems = HttpContext.Session.GetJson<List<CartItemModel>>("Cart") ?? new List<CartItemModel>();
			int tongSoLuong = cartItems.Sum(item => item.soLuong);
			ViewBag.TongSoLuong = tongSoLuong;
			if (MaSP==null)return RedirectToAction("Index");
            var productById=_dataContext.SanPhams.Where(p=>p.MaSP==MaSP).FirstOrDefault();
            return View(productById);
        }
      
    }
}
