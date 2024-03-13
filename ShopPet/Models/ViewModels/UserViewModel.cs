using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace ShopPet.Models.ViewModels
{
    public class UserViewModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Tên đăng nhập không được để trống!")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập email của bạn !")]
        [EmailAddress(ErrorMessage = "Địa chỉ email không hợp lệ !")]
        public string Email { get; set; }

        [DataType(DataType.Password), Required(ErrorMessage = "Vui lòng nhập mật khẩu của bạn !")]
        public string Password { get; set; }

        /*[Required(ErrorMessage = "Vui lòng chọn quyền của tài khoản !")]
        public List<string> Role { get; set; }*/



        /*public Gender GioiTinh { get; set; }
		[Required(ErrorMessage = "Vui lòng nhập số điện thoại của bạn!")]
		[DataType(DataType.PhoneNumber)]//kiẻu dữ liệu cho thuộc tính số điện thoại 
		[RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Số điện thoại không hợp lệ")]
		public string PhoneNumber { get; set; }
		[Required(ErrorMessage = "Bạn ở đâu ???")]
		[StringLength(50, ErrorMessage = "Địa chỉ nhà dài dữ vậy, sao kiếm ?")]
		public string DiaChi { get; set; }
		[Required(ErrorMessage = "Ngày sinh không được bỏ trống")]
		public string NgaySinh { get; set; }
		[Required(ErrorMessage = "Tài khoản không được bỏ trống")]
		[StringLength(15, ErrorMessage = "Tài khoản không quá 15 kí tự")]
		public string TaiKhoan { get; set; }*/
    }
    public enum Gender
    {
        Nam,
        Nữ
    }
}
