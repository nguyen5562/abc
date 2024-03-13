using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopPet.Models
{
    public class AppUserGroup
    {
        [StringLength(450)]
        [Key]
        [Column(Order = 1)]
        public string UserId { get; set; }

        [Key]
        [Column(Order = 2)]
        public int GroupId { get; set; }

        [ForeignKey("UserId")]
        public virtual AppUserModel AppUserModel { get; set; }

        [ForeignKey("GroupId")]
        public virtual AppGroup AppGroup { get; set; }
    }
}
