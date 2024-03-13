using System.ComponentModel.DataAnnotations;

namespace ShopPet.Models
{
    public class Claims
    {
        [Key]
        public int Id { get; set; }
        public string ClaimName { get; set; }
    }
}
