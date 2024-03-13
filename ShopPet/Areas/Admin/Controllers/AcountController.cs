using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using ShopPet.Areas.Models.ViewModel;
using ShopPet.Models;
using ShopPet.Models.ViewModels;
using ShopPet.Repository;
using X.PagedList;

namespace ShopPet.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AcountController : Controller
    {
        private readonly UserManager<AppUserModel> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly SignInManager<AppUserModel> _signInManager;
        private readonly DataContext _context;

        public AcountController(UserManager<AppUserModel> userManager, DataContext context, RoleManager<AppRole> roleManager, SignInManager<AppUserModel> signInManager)
        {
            _userManager = userManager;
            _context = context;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        public async Task<ActionResult> Index(int page = 1, int pageSize = 5)
        {
            //var items = await _context.Users.ToListAsync();
            //return View(items);

            var query = (from user in _context.Users
                         select new AdminViewModel
                         {
                             Id = user.Id,
                             UserName = user.UserName,
                             Email = user.Email,
                             Password = user.PasswordHash,
                             Role = (from userRole in _context.UserRoles
                                     where userRole.UserId == user.Id
                                     join role in _context.Roles on userRole.RoleId equals role.Id
                                     select role.Name).ToList()
                         }).ToList();


            var totalItemCount = query.Count();
            var model = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            var pagedList = new StaticPagedList<AdminViewModel>(model, page, pageSize, totalItemCount);
            ViewBag.PageStartItem = (page - 1) * pageSize + 1;
            ViewBag.PageEndItem = Math.Min(page * pageSize, totalItemCount);
            ViewBag.TotalItemCount = totalItemCount;
            ViewBag.Page = page;
            return View(pagedList);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            var model = new EditUserViewModel
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Role = (from userRole in _context.UserRoles
                        where userRole.UserId == user.Id
                        join role in _context.Roles on userRole.RoleId equals role.Id
                        select role.Name).ToList()
            };

            ViewBag.Role = new SelectList(_roleManager.Roles.ToList(), "Name", "Name");

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(model.Id);

                if (user == null)
                {
                    return NotFound();
                }

                user.UserName = model.UserName;
                user.Email = model.Email;

                // Xóa tất cả quyền hiện tại của người dùng
                var currentRoles = await _userManager.GetRolesAsync(user).ConfigureAwait(true);
                await _userManager.RemoveFromRolesAsync(user, currentRoles).ConfigureAwait(true);

                // Thêm lại quyền mới
                if (model.Role != null && model.Role.Any())
                {
                    foreach (var roleName in model.Role)
                    {
                        await _userManager.AddToRoleAsync(user, roleName).ConfigureAwait(true);
                    }
                }

                // Cập nhật thông tin người dùng
                var result = await _userManager.UpdateAsync(user).ConfigureAwait(true);

                if (result.Succeeded)
                {
                    if (user.UserName == User.Identity.Name)
                    {
                        await _signInManager.SignOutAsync();
                        return Redirect("/");
                    }
                    else return RedirectToAction("Index", "Acount");
                }

                // Xử lý lỗi khi cập nhật thông tin người dùng không thành công
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // Nếu ModelState không hợp lệ, hiển thị lại form với thông báo lỗi
            ViewBag.Role = new SelectList(_roleManager.Roles.ToList(), "Name", "Name");

            return View(model);
        }

        public ActionResult Create()
        {
            ViewBag.Role = new SelectList(_roleManager.Roles.ToList(), "Name", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AdminViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new AppUserModel
                {
                    UserName = model.UserName,
                    Email = model.Email,
                };

                // Tạo mới người dùng
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    // Thêm quyền cho người dùng
                    if (model.Role != null && model.Role.Any())
                    {
                        foreach (var roleName in model.Role)
                        {
                            // Gán quyền cho người dùng
                            await _userManager.AddToRoleAsync(user, roleName);
                        }
                    }

                    // Điều hướng hoặc hiển thị thông báo thành công tùy thuộc vào yêu cầu của bạn
                    return RedirectToAction("Index", "Acount");
                }

                // Xử lý lỗi khi tạo mới người dùng không thành công
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            ViewBag.Role = new SelectList(_roleManager.Roles.ToList(), "Name", "Name");
            // Nếu ModelState không hợp lệ, hiển thị lại form với thông báo lỗi
            return View(model);
        }

        public async Task<IActionResult> Delete(string id)
        {
            // Tìm vai trò dựa trên tên
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                // Vai trò không tồn tại, xử lý tương ứng (ví dụ: trả về 404)
                return NotFound();
            }

            // Xóa vai trò
            var result = await _userManager.DeleteAsync(user);

            if (result.Succeeded)
            {
                // Xóa thành công, thực hiện các xử lý khác nếu cần
                return RedirectToAction("Index"); // Chuyển hướng đến trang danh sách vai trò hoặc trang khác
            }
            else
            {
                // Xóa không thành công, xử lý lỗi nếu cần
                return View("Error");
            }
        }

        public async Task<IActionResult> Logout(string returnUrl = "/")
        {
            await _signInManager.SignOutAsync();
            return Redirect(returnUrl);
        }
    }
}