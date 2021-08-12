using hrmSolution.Data.Configurations;
using hrmSolution.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace hrmSolution.Data.EF
{
    public class HrmDbContext : DbContext
    {
        public HrmDbContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new Tbl_NguoiDungConfiguration());
            modelBuilder.ApplyConfiguration(new Tbl_DanhMucTenQuyenConfiguration());
            modelBuilder.ApplyConfiguration(new Tbl_DanhMucManHinhConfiguration());
            modelBuilder.ApplyConfiguration(new Tbl_DanhMucChucNangConfiguration());
            modelBuilder.ApplyConfiguration(new Tbl_ChiTietQuyenTheoManHinhConfiguration());

            //base.OnModelCreating(modelBuilder);
            // tao data fake
            modelBuilder.Entity<Tbl_DanhMucTenQuyen>().HasData(
                new Tbl_DanhMucTenQuyen()
                {
                    Id = Guid.Parse("5EF507D6-48FF-419D-BFFA-07291275379E"),
                    C_Code = "ROLE001",
                    C_TenQuyen = "Admin",
                    C_NgayTao = DateTime.Now,
                    C_TrangThai = 2,
                    C_IsDeleted = 2,
                }
            );
            modelBuilder.Entity<Tbl_NguoiDung>().HasData(
                new Tbl_NguoiDung()
                {
                    Id = Guid.NewGuid(),
                    C_Code = "ACC0001",
                    C_Email = "admin@gmail.com",
                    C_MatKhau = "admin",
                    C_HoVaTen = "Tao La Admin",
                    C_NgayTao =  DateTime.Now,
                    C_SoDienThoai = "0974084154",
                    C_Avatar = "avatarpath",
                    C_TrangThai = 2,
                    C_IsDeleted = 2,
                    Fk_DanhMucTenQuyen = Guid.Parse("5EF507D6-48FF-419D-BFFA-07291275379E"),
                }
            );
        }

        public DbSet<Tbl_NguoiDung> Tbl_NguoiDung { set; get; }
        public DbSet<Tbl_DanhMucTenQuyen> Tbl_DanhMucTenQuyen { set; get; }
        public DbSet<Tbl_DanhMucChucNang> Tbl_DanhMucChucNang { set; get; }
        public DbSet<Tbl_DanhMucManHinh> Tbl_DanhMucManHinh { set; get; }
        public DbSet<Tbl_ChiTietQuyenTheoManHinh> Tbl_ChiTietQuyenTheoManHinh { set; get; }


    }
}
