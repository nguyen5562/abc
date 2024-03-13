using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ShopPet.Models
{
	public class ChiTietHoaDon
	{
		[Key]  
		
		public int MaCT { get; set; }
        public string OrderCode { get; set; }
        public string UserName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
		public int SanPhamId { get; set; }
        public SanPham SanPham { get; set; }
        public int HoaDonId { get; set; }
        public HoaDon HoaDon { get; set; }

	}
	public class OrderDetailEntityConfiguration : IEntityTypeConfiguration<ChiTietHoaDon>
	{
		public void Configure(EntityTypeBuilder<ChiTietHoaDon> builder)
		{
			builder.HasOne(x => x.HoaDon).WithMany(x => x.ChiTietHoaDons).HasForeignKey(x => x.HoaDonId);
			builder.HasOne(x => x.SanPham).WithMany(x => x.ChiTietHoaDons).HasForeignKey(x => x.SanPhamId);
		}
	}
}
