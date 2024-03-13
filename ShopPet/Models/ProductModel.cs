using ShopPet.Repository.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopPet.Models
{
    public class ProductModel
    {
        public int MaSP { get; set; }
        public string TenSP { get; set; }
        public string MoTa { get; set; }
        public species GiongLoai { get; set; }
        public decimal GiaBan { get; set; }
        public decimal GiaNhap { get; set; }
        public string Anh { get; set; }
        public int SoLuongTon { get; set; }
        public string TenLoaiSP { get; set; }
        public LoaiSanPham LoaiSanPham { get; set; }
        public string TenNCC { get; set; }
        public NhaCungCap NhaCungCap { get; set; }
        [NotMapped]
        [FileExtension]
        public IFormFile ImageUpload { get; set; }
    }

    public enum species
    {
        Chó,
        Mèo
    }
}
