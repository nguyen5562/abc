using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using ShopPet.Models;
using ShopPet.Repository;

namespace ShopPet.Extensions
{
    public class CheckRole
    {
		private readonly UserManager<AppUserModel> _userManager;
		private readonly RoleManager<AppRole> _roleManager;

		public CheckRole(UserManager<AppUserModel> userManager, RoleManager<AppRole> roleManager)
		{
			_userManager = userManager;
			_roleManager = roleManager;
		}

		public async Task<bool> CheckAccessAdminByRole(string roleName)
		{
			var role = await _roleManager.FindByNameAsync(roleName);
			return role != null && role.AccessAdmin;
		}

		public async Task<bool> CheckAccessUserByRole(string roleName)
		{
			var role = await _roleManager.FindByNameAsync(roleName);
			return role != null && role.AccessUser;
		}

		// Product
		public async Task<bool> CheckViewProductByRole(string roleName)
		{
			var role = await _roleManager.FindByNameAsync(roleName);
			return role != null && role.ViewProduct;
		}

        public async Task<bool> CheckAddProductByRole(string roleName)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            return role != null && role.AddProduct;
        }

        public async Task<bool> CheckEditProductByRole(string roleName)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            return role != null && role.EditProduct;
        }

        public async Task<bool> CheckDeleteProductByRole(string roleName)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            return role != null && role.DeleteProduct;
        }

        // Supplier
        public async Task<bool> CheckViewSupplierByRole(string roleName)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            return role != null && role.ViewSupplier;
        }

        public async Task<bool> CheckAddSupplierByRole(string roleName)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            return role != null && role.AddSupplier;
        }

        public async Task<bool> CheckEditSupplierByRole(string roleName)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            return role != null && role.EditSupplier;
        }

        public async Task<bool> CheckDeleteSupplierByRole(string roleName)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            return role != null && role.DeleteSupplier;
        }

        // Category
        public async Task<bool> CheckViewCategoryByRole(string roleName)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            return role != null && role.ViewCategory;
        }

        public async Task<bool> CheckAddCategoryByRole(string roleName)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            return role != null && role.AddCategory;
        }

        public async Task<bool> CheckEditCategoryByRole(string roleName)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            return role != null && role.EditCategory;
        }

        public async Task<bool> CheckDeleteCategoryByRole(string roleName)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            return role != null && role.DeleteCategory;
        }

        // Order
        public async Task<bool> CheckViewOrderByRole(string roleName)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            return role != null && role.ViewOrder;
        }

        public async Task<bool> CheckEditOrderByRole(string roleName)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            return role != null && role.EditOrder;
        }

        // Role
        public async Task<bool> CheckViewRoleByRole(string roleName)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            return role != null && role.ViewRole;
        }

        public async Task<bool> CheckAddRoleByRole(string roleName)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            return role != null && role.AddRole;
        }

        public async Task<bool> CheckEditRoleByRole(string roleName)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            return role != null && role.EditRole;
        }

        public async Task<bool> CheckDeleteRoleByRole(string roleName)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            return role != null && role.DeleteRole;
        }

        // Account
        public async Task<bool> CheckViewAccountByRole(string roleName)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            return role != null && role.ViewAccount;
        }

        public async Task<bool> CheckAddAccountByRole(string roleName)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            return role != null && role.AddAccount;
        }

        public async Task<bool> CheckEditAccountByRole(string roleName)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            return role != null && role.EditAccount;
        }

        public async Task<bool> CheckDeleteAccountByRole(string roleName)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            return role != null && role.DeleteAccount;
        }

        // CheckByUser
        // Truy cập admin
        public async Task<bool> CheckAccessAdminByUser(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            var roleList = await _userManager.GetRolesAsync(user);

            foreach (var role in roleList)
                if (await CheckAccessAdminByRole(role)) return true;

            return false;
        }

        // Truy cập user
		public async Task<bool> CheckAccessUserByUser(string username)
		{
			var user = await _userManager.FindByNameAsync(username);
			var roleList = await _userManager.GetRolesAsync(user);

			foreach (var role in roleList)
				if (await CheckAccessUserByRole(role)) return true;

			return false;
		}

		// Product
		public async Task<bool> CheckViewProductByUser(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            var roleList = await _userManager.GetRolesAsync(user);

            foreach (var role in roleList)
                if (await CheckViewProductByRole(role)) return true;

            return false;
        }

        public async Task<bool> CheckAddProductByUser(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            var roleList = await _userManager.GetRolesAsync(user);

            foreach (var role in roleList)
                if (await CheckAddProductByRole(role)) return true;

            return false;
        }

        public async Task<bool> CheckEditProductByUser(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            var roleList = await _userManager.GetRolesAsync(user);

            foreach (var role in roleList)
                if (await CheckEditProductByRole(role)) return true;

            return false;
        }

        public async Task<bool> CheckDeleteProductByUser(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            var roleList = await _userManager.GetRolesAsync(user);

            foreach (var role in roleList)
                if (await CheckDeleteProductByRole(role)) return true;

            return false;
        }

        // Supplier
        public async Task<bool> CheckViewSupplierByUser(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            var roleList = await _userManager.GetRolesAsync(user);

            foreach (var role in roleList)
                if (await CheckViewSupplierByRole(role)) return true;

            return false;
        }

        public async Task<bool> CheckAddSupplierByUser(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            var roleList = await _userManager.GetRolesAsync(user);

            foreach (var role in roleList)
                if (await CheckAddSupplierByRole(role)) return true;

            return false;
        }

        public async Task<bool> CheckEditSupplierByUser(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            var roleList = await _userManager.GetRolesAsync(user);

            foreach (var role in roleList)
                if (await CheckEditSupplierByRole(role)) return true;

            return false;
        }

        public async Task<bool> CheckDeleteSupplierByUser(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            var roleList = await _userManager.GetRolesAsync(user);

            foreach (var role in roleList)
                if (await CheckDeleteSupplierByRole(role)) return true;

            return false;
        }

        // Category
        public async Task<bool> CheckViewCategoryByUser(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            var roleList = await _userManager.GetRolesAsync(user);

            foreach (var role in roleList)
                if (await CheckViewCategoryByRole(role)) return true;

            return false;
        }

        public async Task<bool> CheckAddCategoryByUser(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            var roleList = await _userManager.GetRolesAsync(user);

            foreach (var role in roleList)
                if (await CheckAddCategoryByRole(role)) return true;

            return false;
        }

        public async Task<bool> CheckEditCategoryByUser(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            var roleList = await _userManager.GetRolesAsync(user);

            foreach (var role in roleList)
                if (await CheckEditCategoryByRole(role)) return true;

            return false;
        }

        public async Task<bool> CheckDeleteCategoryByUser(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            var roleList = await _userManager.GetRolesAsync(user);

            foreach (var role in roleList)
                if (await CheckDeleteCategoryByRole(role)) return true;

            return false;
        }

        // Order
        public async Task<bool> CheckViewOrderByUser(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            var roleList = await _userManager.GetRolesAsync(user);

            foreach (var role in roleList)
                if (await CheckViewOrderByRole(role)) return true;

            return false;
        }

        public async Task<bool> CheckEditOrderByUser(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            var roleList = await _userManager.GetRolesAsync(user);

            foreach (var role in roleList)
                if (await CheckEditOrderByRole(role)) return true;

            return false;
        }

        // Role
        public async Task<bool> CheckViewRoleByUser(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            var roleList = await _userManager.GetRolesAsync(user);

            foreach (var role in roleList)
                if (await CheckViewRoleByRole(role)) return true;

            return false;
        }

        public async Task<bool> CheckAddRoleByUser(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            var roleList = await _userManager.GetRolesAsync(user);

            foreach (var role in roleList)
                if (await CheckAddRoleByRole(role)) return true;

            return false;
        }

        public async Task<bool> CheckEditRoleByUser(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            var roleList = await _userManager.GetRolesAsync(user);

            foreach (var role in roleList)
                if (await CheckEditRoleByRole(role)) return true;

            return false;
        }

        public async Task<bool> CheckDeleteRoleByUser(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            var roleList = await _userManager.GetRolesAsync(user);

            foreach (var role in roleList)
                if (await CheckDeleteRoleByRole(role)) return true;

            return false;
        }

        // Account
        public async Task<bool> CheckViewAccountByUser(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            var roleList = await _userManager.GetRolesAsync(user);

            foreach (var role in roleList)
                if (await CheckViewAccountByRole(role)) return true;

            return false;
        }

        public async Task<bool> CheckAddAccountByUser(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            var roleList = await _userManager.GetRolesAsync(user);

            foreach (var role in roleList)
                if (await CheckAddAccountByRole(role)) return true;

            return false;
        }

        public async Task<bool> CheckEditAccountByUser(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            var roleList = await _userManager.GetRolesAsync(user);

            foreach (var role in roleList)
                if (await CheckEditAccountByRole(role)) return true;

            return false;
        }

        public async Task<bool> CheckDeleteAccountByUser(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            var roleList = await _userManager.GetRolesAsync(user);

            foreach (var role in roleList)
                if (await CheckDeleteAccountByRole(role)) return true;

            return false;
        }
    }
}
