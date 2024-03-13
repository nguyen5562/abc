using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ShopPet.Models
{
    public class LoaiSanPham
    {
        [Key]
        public int MaLoaiSP { get; set; }
        public string TenLoaiSP { get; set; }
        public ICollection<SanPham> SanPhams { get; set; }

    }
}
