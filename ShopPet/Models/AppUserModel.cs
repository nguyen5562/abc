using Microsoft.AspNetCore.Identity;

namespace ShopPet.Models
{
    public class AppUserModel : IdentityUser
    {
        public DateTime NgaySinh { get; set; }
        public string GioiTinh { get; set; }
        public string TaiKhoan { get; set; }
        public string DiaChi { get; set; }
    }
}