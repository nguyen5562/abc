using Microsoft.AspNetCore.Identity;

namespace ShopPet.Models
{
    public class AppRole : IdentityRole
    {
        public bool AccessAdmin { get; set; }
        public bool AccessUser { get; set; }
        public bool ViewProduct { get; set; }
        public bool AddProduct { get; set; }
        public bool EditProduct { get; set; }
        public bool DeleteProduct { get; set; }
        public bool ViewSupplier { get; set; }
        public bool AddSupplier { get; set; }
        public bool EditSupplier { get; set; }
        public bool DeleteSupplier { get; set; }
        public bool ViewCategory { get; set; }
        public bool AddCategory { get; set; }
        public bool EditCategory { get; set; }
        public bool DeleteCategory { get; set; }
        public bool ViewOrder { get; set; }
        public bool EditOrder { get; set; }
        public bool ViewRole { get; set; }
        public bool AddRole { get; set; }
        public bool EditRole { get; set; }
        public bool DeleteRole { get; set; }
        public bool ViewAccount { get; set; }
        public bool AddAccount { get; set; }
        public bool EditAccount { get; set; }
        public bool DeleteAccount { get; set; }
        public bool ViewGroup { get; set; }
        public bool AddGroup { get; set; }
        public bool EditGroup { get; set; }
        public bool DeleteGroup { get; set; }
    }
}
