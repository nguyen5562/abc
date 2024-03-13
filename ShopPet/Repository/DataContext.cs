using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ShopPet.Models;

namespace ShopPet.Repository
{

    public class DataContext : IdentityDbContext<AppUserModel, AppRole, string>
    {
        public DbSet<SanPham> SanPhams { get; set; }
        public DbSet<LoaiSanPham> LoaiSanPhams { get; set; }
        public DbSet<NhaCungCap> NhaCungCaps { get; set; }
        public DbSet<HoaDon> HoaDons { get; set; }
        public DbSet<ChiTietHoaDon> ChiTietHoaDons { get; set; }
        public DbSet<AppGroup> AppGroups { get; set; }
        public DbSet<AppRoleGroup> AppRoleGroups { get; set; }
        public DbSet<AppUserGroup> AppUserGroups { get; set; }
        public DbSet<Claims> Claims { get; set; }
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DataContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppRoleGroup>()
                        .HasKey(e => new { e.RoleId, e.GroupId });

            modelBuilder.Entity<AppUserGroup>()
                        .HasKey(e => new { e.UserId, e.GroupId });

            modelBuilder.Entity<IdentityUserLogin<string>>()
                .HasKey(e => new { e.LoginProvider, e.ProviderKey });

            modelBuilder.Entity<IdentityUserRole<string>>()
                .HasKey(e => new { e.UserId, e.RoleId });

            modelBuilder.Entity<IdentityUserToken<string>>()
                .HasKey(e => new { e.UserId, e.LoginProvider, e.Name });
        }
    }

}
