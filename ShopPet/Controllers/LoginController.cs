using Microsoft.AspNetCore.Mvc;

namespace ShopPet.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
