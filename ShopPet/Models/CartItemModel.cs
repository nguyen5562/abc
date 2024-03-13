using System.Drawing;

namespace ShopPet.Models
{
	public class CartItemModel
	{
		public int MaSP { get; set; }
		public string TenSP { get; set; }
		public string Anh { get; set; }
		public double donGia { get; set; }      
		public int soLuong { get; set; }
		public double thanhTien
		{
			get { return soLuong * donGia; }
		}
		public CartItemModel()
		{

		}
		// Khởi tạo giỏ hàng
		public CartItemModel(SanPham sanpham)
		{
			MaSP = sanpham.MaSP;
			/*SanPham sanPham = db.SanPhams.Single(sp => sp.MaSp == maSP);*/
			TenSP = sanpham.TenSP;
			Anh = sanpham.Anh;
			donGia = double.Parse(sanpham.GiaBan.ToString());
			soLuong = 1;
		}
	}
}
