using System.ComponentModel.DataAnnotations;

namespace ShopPet.Models
{
    public class HoaDon
    {
        [Key]
        public int MaHD { get; set; }
        public string OrderCode { get; set; }
        public string UserName { get; set; }
        public DateTime NgayTao { get; set; }
		public int TrangThai { get; set; }
		[Required(ErrorMessage = "Vui lòng nhập số điện thoại")]
		[RegularExpression(@"^0[0-9]{9}$", ErrorMessage = "Số điện thoại phải có 10 chữ số,có số 0 ở đầu")]
        public string SĐT { get; set; }
		[Required(ErrorMessage = "Vui lòng nhập địa chỉ")]
		public string DiaChi { get; set; }
        public string Note { get; set; }

        public int ThanhToan { get; set; }
		public ICollection<ChiTietHoaDon> ChiTietHoaDons { get; set; }
	}
}
