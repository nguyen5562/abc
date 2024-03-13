using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopPet.Models;
using ShopPet.Repository;
using X.PagedList;


namespace ShopPet.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RoleController : Controller
	{
		private readonly RoleManager<AppRole> _roleManager;
		private readonly DataContext _context;

        public RoleController(RoleManager<AppRole> roleManager, DataContext context)
        {
            _roleManager = roleManager;
            _context = context;
        }

        public async Task<ActionResult> Index(int page = 1, int pageSize = 5)
        {
            var query = await _context.Roles.ToListAsync();

            var totalItemCount = query.Count();
            var model = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            var pagedList = new StaticPagedList<AppRole>(model, page, pageSize, totalItemCount);
            ViewBag.PageStartItem = (page - 1) * pageSize + 1;
            ViewBag.PageEndItem = Math.Min(page * pageSize, totalItemCount);
            ViewBag.TotalItemCount = totalItemCount;
            ViewBag.Page = page;
            return View(pagedList);
        }

        public IActionResult Edit(string id)
        {
            var role = _roleManager.FindByIdAsync(id).Result;

            if (role == null)
            {
                // Xử lý khi không tìm thấy role
                return NotFound();
            }

            return View(role);
        }

        // Action để xử lý khi submit form chỉnh sửa role
        [HttpPost]
        public IActionResult Edit(AppRole model)
        {
            if (ModelState.IsValid)
            {
                var role = _roleManager.FindByIdAsync(model.Id).Result;

                if (role == null)
                {
                    // Xử lý khi không tìm thấy role
                    return NotFound();
                }

                // Cập nhật các thuộc tính role từ model
                role.AccessAdmin = model.AccessAdmin;
                role.AccessUser = model.AccessUser;

                role.ViewProduct = model.ViewProduct;
                role.AddProduct = model.AddProduct;
                role.EditProduct = model.EditProduct;
                role.DeleteProduct = model.DeleteProduct;

                role.ViewSupplier = model.ViewSupplier;
                role.AddSupplier = model.AddSupplier;
                role.EditSupplier = model.EditSupplier;
                role.DeleteSupplier = model.DeleteSupplier;

                role.ViewCategory = model.ViewCategory;
                role.AddCategory = model.AddCategory;
                role.EditCategory = model.EditCategory;
                role.DeleteCategory = model.DeleteCategory;

                role.ViewOrder = model.ViewOrder;
                role.EditOrder = model.EditOrder;

                role.ViewRole = model.ViewRole;
                role.AddRole = model.AddRole;
                role.EditRole = model.EditRole;
                role.DeleteRole = model.DeleteRole;

                role.ViewAccount = model.ViewAccount;
                role.AddAccount = model.AddAccount;
                role.EditAccount = model.EditAccount;
                role.DeleteAccount = model.DeleteAccount;

                role.ViewGroup = model.ViewGroup;
                role.AddGroup = model.AddGroup;
                role.EditGroup = model.EditGroup;
                role.DeleteGroup = model.DeleteGroup;

                // Lưu thay đổi vào database
                var result = _roleManager.UpdateAsync(role).Result;

                if (result.Succeeded)
                {
                    // Xử lý khi chỉnh sửa role thành công
                    return RedirectToAction("Index", "Role"); // Chuyển hướng về trang chủ hoặc trang danh sách role
                }
                else
                {
                    // Xử lý khi có lỗi trong quá trình chỉnh sửa role
                    ModelState.AddModelError("", "Có lỗi xảy ra khi chỉnh sửa role.");
                }
            }

            // Nếu ModelState không hợp lệ, hiển thị lại form với dữ liệu nhập và thông báo lỗi
            return View(model);
        }

        public ActionResult Create() 
		{
			return View();
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(AppRole model)
        {
            if (ModelState.IsValid)
            {
                await _roleManager.CreateAsync(model);
                return RedirectToAction("Index");
            }

            return View(model);
        }

        public async Task<IActionResult> Delete(string id)
        {
            // Tìm vai trò dựa trên tên
            var role = await _roleManager.FindByIdAsync(id);

            if (role == null)
            {
                // Vai trò không tồn tại, xử lý tương ứng (ví dụ: trả về 404)
                return NotFound();
            }

            // Xóa vai trò
            var result = await _roleManager.DeleteAsync(role);

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
    }
}