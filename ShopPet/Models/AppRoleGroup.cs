using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopPet.Models
{
    public class AppRoleGroup
    {
        [Key]
        [Column(Order = 1)]
        public int GroupId { get; set; }

        
        [Key]
        [Column(Order = 2)]
        [StringLength(450)]
        public string RoleId { get; set; }

        [ForeignKey("RoleId")]
        public virtual AppRole AppRole { get; set; }

        [ForeignKey("GroupId")]
        public virtual AppGroup AppGroup { get; set; }
    }
}
