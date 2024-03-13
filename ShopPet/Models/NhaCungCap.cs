using System.ComponentModel.DataAnnotations;

namespace ShopPet.Models
{
    public class NhaCungCap
    {
        [Key]
        public int MaNCC { get; set; }
        public string TenNCC { get; set; }

    }
}
