﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ShopPet.Repository;

#nullable disable

namespace ShopPet.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20231201075849_newtable")]
    partial class newtable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Roles");

                    b.HasDiscriminator<string>("Discriminator").HasValue("IdentityRole");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("RoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("UserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.ToTable("UserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("UserTokens");
                });

            modelBuilder.Entity("ShopPet.Models.AppGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("Name")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.HasKey("Id");

                    b.ToTable("ApplicationGroups");
                });

            modelBuilder.Entity("ShopPet.Models.AppRoleGroup", b =>
                {
                    b.Property<string>("RoleId")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)")
                        .HasColumnOrder(2);

                    b.Property<int>("GroupId")
                        .HasColumnType("int")
                        .HasColumnOrder(1);

                    b.HasKey("RoleId", "GroupId");

                    b.HasIndex("GroupId");

                    b.ToTable("ApplicationRoleGroups");
                });

            modelBuilder.Entity("ShopPet.Models.AppUserGroup", b =>
                {
                    b.Property<string>("UserId")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)")
                        .HasColumnOrder(1);

                    b.Property<int>("GroupId")
                        .HasColumnType("int")
                        .HasColumnOrder(2);

                    b.HasKey("UserId", "GroupId");

                    b.HasIndex("GroupId");

                    b.ToTable("AppUserGroups");
                });

            modelBuilder.Entity("ShopPet.Models.AppUserModel", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DiaChi")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("GioiTinh")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTime>("NgaySinh")
                        .HasColumnType("datetime2");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TaiKhoan")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ShopPet.Models.ChiTietHoaDon", b =>
                {
                    b.Property<int>("MaCT")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MaCT"));

                    b.Property<int>("HoaDonId")
                        .HasColumnType("int");

                    b.Property<string>("OrderCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int>("SanPhamId")
                        .HasColumnType("int");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MaCT");

                    b.HasIndex("HoaDonId");

                    b.HasIndex("SanPhamId");

                    b.ToTable("ChiTietHoaDons");
                });

            modelBuilder.Entity("ShopPet.Models.HoaDon", b =>
                {
                    b.Property<int>("MaHD")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MaHD"));

                    b.Property<string>("DiaChi")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("NgayTao")
                        .HasColumnType("datetime2");

                    b.Property<string>("Note")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OrderCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SĐT")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TrangThai")
                        .HasColumnType("int");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MaHD");

                    b.ToTable("HoaDons");
                });

            modelBuilder.Entity("ShopPet.Models.LoaiSanPham", b =>
                {
                    b.Property<int>("MaLoaiSP")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MaLoaiSP"));

                    b.Property<string>("TenLoaiSP")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MaLoaiSP");

                    b.ToTable("LoaiSanPhams");
                });

            modelBuilder.Entity("ShopPet.Models.NhaCungCap", b =>
                {
                    b.Property<int>("MaNCC")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MaNCC"));

                    b.Property<string>("DiaChi")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SĐT")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TenNCC")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MaNCC");

                    b.ToTable("NhaCungCaps");
                });

            modelBuilder.Entity("ShopPet.Models.SanPham", b =>
                {
                    b.Property<int>("MaSP")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MaSP"));

                    b.Property<string>("Anh")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("GiaBan")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("GiaNhap")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("GiongLoai")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("LoaiSanPhamId")
                        .HasColumnType("int");

                    b.Property<string>("MoTa")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NhaCungCapId")
                        .HasColumnType("int");

                    b.Property<int>("SoLuongTon")
                        .HasColumnType("int");

                    b.Property<string>("TenSP")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MaSP");

                    b.HasIndex("LoaiSanPhamId");

                    b.HasIndex("NhaCungCapId");

                    b.ToTable("SanPhams");
                });

            modelBuilder.Entity("ShopPet.Models.AppRole", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityRole");

                    b.Property<string>("Description")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.HasDiscriminator().HasValue("AppRole");
                });

            modelBuilder.Entity("ShopPet.Models.AppRoleGroup", b =>
                {
                    b.HasOne("ShopPet.Models.AppGroup", "AppGroup")
                        .WithMany()
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ShopPet.Models.AppRole", "AppRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AppGroup");

                    b.Navigation("AppRole");
                });

            modelBuilder.Entity("ShopPet.Models.AppUserGroup", b =>
                {
                    b.HasOne("ShopPet.Models.AppGroup", "AppGroup")
                        .WithMany()
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ShopPet.Models.AppUserModel", "AppUserModel")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AppGroup");

                    b.Navigation("AppUserModel");
                });

            modelBuilder.Entity("ShopPet.Models.ChiTietHoaDon", b =>
                {
                    b.HasOne("ShopPet.Models.HoaDon", "HoaDon")
                        .WithMany("ChiTietHoaDons")
                        .HasForeignKey("HoaDonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ShopPet.Models.SanPham", "SanPham")
                        .WithMany("ChiTietHoaDons")
                        .HasForeignKey("SanPhamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("HoaDon");

                    b.Navigation("SanPham");
                });

            modelBuilder.Entity("ShopPet.Models.SanPham", b =>
                {
                    b.HasOne("ShopPet.Models.LoaiSanPham", "LoaiSanPham")
                        .WithMany("SanPhams")
                        .HasForeignKey("LoaiSanPhamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ShopPet.Models.NhaCungCap", "NhaCungCap")
                        .WithMany()
                        .HasForeignKey("NhaCungCapId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("LoaiSanPham");

                    b.Navigation("NhaCungCap");
                });

            modelBuilder.Entity("ShopPet.Models.HoaDon", b =>
                {
                    b.Navigation("ChiTietHoaDons");
                });

            modelBuilder.Entity("ShopPet.Models.LoaiSanPham", b =>
                {
                    b.Navigation("SanPhams");
                });

            modelBuilder.Entity("ShopPet.Models.SanPham", b =>
                {
                    b.Navigation("ChiTietHoaDons");
                });
#pragma warning restore 612, 618
        }
    }
}
