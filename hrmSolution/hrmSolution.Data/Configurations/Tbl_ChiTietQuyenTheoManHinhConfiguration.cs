using System;
using System.Collections.Generic;
using System.Text;
using hrmSolution.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace hrmSolution.Data.Configurations
{
    public class Tbl_ChiTietQuyenTheoManHinhConfiguration : IEntityTypeConfiguration<Tbl_ChiTietQuyenTheoManHinh>
    {
        public void Configure(EntityTypeBuilder<Tbl_ChiTietQuyenTheoManHinh> builder)
        {
            builder.ToTable("Tbl_ChiTietQuyenTheoManHinh");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                .IsRequired()
                .HasColumnName("id")
                .ValueGeneratedNever();

            builder.Property(c => c.C_NgayTao)
              .IsRequired()
              .HasColumnName("c_NgayTao")
              .HasColumnType("datetime");

            builder.Property(c => c.C_TrangThai)
               .IsRequired()
               .HasColumnName("c_TrangThai")
               .HasColumnType("int");

            builder.Property(c => c.C_IsDeleted)
               .IsRequired()
               .HasColumnName("c_IsDeleted")
               .HasColumnType("int")
               .HasDefaultValue(2);


            builder.HasOne(c => c.Tbl_DanhMucTenQuyen)
                .WithMany(c => c.Tbl_ChiTietQuyenTheoManHinhs)
                .HasForeignKey("Fk_DanhMucTenQuyen")
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(c => c.Tbl_DanhMucChucNang)
               .WithMany(c => c.Tbl_ChiTietQuyenTheoManHinhs)
               .HasForeignKey("Fk_DanhMucChucNang")
               .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(c => c.Tbl_DanhMucManHinh)
               .WithMany(c => c.Tbl_ChiTietQuyenTheoManHinhs)
               .HasForeignKey("Fk_DanhMucManHinh")
               .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
