using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopPet.Models
{
    public class SanPham
    {
        [Key]
        public int MaSP { get; set; }
        public string TenSP { get; set; }
        public string MoTa { get; set; }
        public decimal GiaBan { get; set; }
        public decimal GiaNhap { get; set; }
        public string Anh { get; set; }
        public int SoLuongTon { get; set; }
        public int LoaiSanPhamId { get; set; }
        public LoaiSanPham LoaiSanPham { get; set; }
        public int NhaCungCapId { get; set; }
        public NhaCungCap NhaCungCap { get; set; }
		public ICollection<ChiTietHoaDon> ChiTietHoaDons { get; set; }
	}

	public class CommentEntityConfiguration : IEntityTypeConfiguration<SanPham>
	{
		public void Configure(EntityTypeBuilder<SanPham> builder)
		{
			builder.ToTable(nameof(SanPham));
			builder.HasOne(x => x.LoaiSanPham).WithMany(x => x.SanPhams).HasForeignKey(x => x.LoaiSanPhamId);
			builder.HasOne(x => x.NhaCungCap).WithOne().HasForeignKey<SanPham>(x => x.NhaCungCapId);
		}
	}
}
