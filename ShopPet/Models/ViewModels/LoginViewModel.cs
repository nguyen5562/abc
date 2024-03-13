using System.ComponentModel.DataAnnotations;

namespace ShopPet.Models.ViewModels
{
	public class LoginViewModel
	{
		[Required(ErrorMessage = "Tên đăng nhập không được để trống!")]
		[StringLength(50, ErrorMessage = "Vui lòng nhập đầy đủ họ va tên !")]
		public string UserName { get; set; }
		[DataType(DataType.Password), Required(ErrorMessage = "Vui lòng nhập mật khẩu của bạn !")]
		public string Password { get; set; }
		public string ReturnUrl { get; set; }
	}
}
