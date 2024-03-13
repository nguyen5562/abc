using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShoPet.Repository;
using ShopPet.Extensions;
using ShopPet.Models;
using ShopPet.Models.ViewModels;
using ShopPet.Repository;

namespace ShopPet.Controllers
{
    public class AccountController : Controller
    {
		private readonly UserManager<AppUserModel> _userManager;
		private readonly SignInManager<AppUserModel> _signInManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly CheckRole _checkRole;

		public AccountController(SignInManager<AppUserModel> signInManager, UserManager<AppUserModel> userManager, RoleManager<AppRole> roleManager, CheckRole checkRole)
		{
			_signInManager = signInManager;
			_userManager = userManager;
			_roleManager = roleManager;
            _checkRole = checkRole;
		}

		public IActionResult Login(string returnUrl)
        {
			List<CartItemModel> cartItems = HttpContext.Session.GetJson<List<CartItemModel>>("Cart") ?? new List<CartItemModel>();
			int tongSoLuong = cartItems.Sum(item => item.soLuong);
			ViewBag.TongSoLuong = tongSoLuong;
			return View(new LoginViewModel { ReturnUrl=returnUrl});
        }

		[HttpPost]
		public async Task<IActionResult> Login(LoginViewModel loginVN)

		{
			if (ModelState.IsValid)
			{
				Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(loginVN.UserName, loginVN.Password, false, false);
				if (result.Succeeded)
				{
                    var user = await _userManager.FindByNameAsync(loginVN.UserName);
                    string username = user.UserName;
                    
                    if (await _checkRole.CheckAccessAdminByUser(username))
                    {
						return Redirect("/Admin");
                    }
                    else
					{
						return Redirect("/");
					}
                }
				ModelState.AddModelError("","Invalid Username and Password");
			}	
		    return View();
		}

		public IActionResult Create()
		{
			List<CartItemModel> cartItems = HttpContext.Session.GetJson<List<CartItemModel>>("Cart") ?? new List<CartItemModel>();
			int tongSoLuong = cartItems.Sum(item => item.soLuong);
			ViewBag.TongSoLuong = tongSoLuong;
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create(UserViewModel user)
		{
			if (ModelState.IsValid)
			{
				
				AppUserModel newuser = new AppUserModel
				{
					UserName = user.UserName,
					Email = user.Email,
					
				};

				IdentityResult result =  await _userManager.CreateAsync(newuser,user.Password);

				if (result.Succeeded)
				{
                    //var roleExists = await _roleManager.RoleExistsAsync("User");

                    //if (!roleExists)
                    //{
                    //    var createRoleResult = await _roleManager.CreateAsync(new AppRole("User"));

                    //    if (!createRoleResult.Succeeded)
                    //    {
                    //        // Xử lý lỗi khi tạo vai trò không thành công
                    //        foreach (var error in createRoleResult.Errors)
                    //        {
                    //            ModelState.AddModelError("", $"Lỗi tạo vai trò: {error.Description}");
                    //        }

                    //        // Trả về hoặc xử lý lỗi theo ý bạn
                    //        return View(user);
                    //    }
                    //}
                    IdentityResult roleResult = await _userManager.AddToRoleAsync(newuser, "User");

                    if (roleResult.Succeeded)
                    {
                        TempData["success"] = "Tạo user và vai trò thành công!";
                        return RedirectToAction("Login", "Account");
                    }
                    else
                    {
                        // Xử lý lỗi khi thêm người dùng vào vai trò
                        foreach (var error in roleResult.Errors)
                        {
                            ModelState.AddModelError("", $"Lỗi thêm người dùng vào vai trò: {error.Description}");
                        }
                    }
                }
                else
                {
                    // Xử lý lỗi khi tạo người dùng
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(user);
		}
        public async Task<IActionResult> Logout(string returnUrl = "/")
        {
          
                // Đăng xuất người dùng
                await _signInManager.SignOutAsync();

            // Chuyển hướng về trang returnUrl
            return Redirect(returnUrl);
        }
    }
}
