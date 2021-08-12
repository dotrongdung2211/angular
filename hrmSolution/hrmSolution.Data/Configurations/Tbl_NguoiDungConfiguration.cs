using hrmSolution.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace hrmSolution.Data.Configurations
{
    public class Tbl_NguoiDungConfiguration : IEntityTypeConfiguration<Tbl_NguoiDung>
    {
        public void Configure(EntityTypeBuilder<Tbl_NguoiDung> builder)
        {
            builder.ToTable("Tbl_NguoiDung");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                .IsRequired()
                .HasColumnName("id")
                .ValueGeneratedNever();

            builder.Property(c => c.C_Code)
                .IsRequired()
                .HasColumnName("c_Code")
                .IsUnicode(false);

            builder.HasIndex(c => c.C_Code)
                .IsUnique();

            builder.Property(c => c.C_Email)
                .IsRequired()
                .HasColumnName("c_Email")
                .IsUnicode(false);

            builder.Property(c => c.C_MatKhau)
               .IsRequired()
               .HasColumnName("c_MatKhau")
               .IsUnicode(false);

            builder.Property(c => c.C_HoVaTen)
               .IsRequired()
               .HasColumnName("c_HoVaTen")
               .IsUnicode();

            builder.Property(c => c.C_SoDienThoai)
               .IsRequired()
               .HasColumnName("c_SoDienThoai")
               .IsUnicode(false);

            builder.Property(c => c.C_NgayTao)
              .IsRequired()
              .HasColumnName("c_NgayTao")
              .HasColumnType("datetime");

            builder.Property(c => c.C_Avatar)
               .IsRequired()
               .HasColumnName("c_Avatar")
               .IsUnicode(false);

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
                .WithMany(c => c.Tbl_NguoiDungs)
                .HasForeignKey("Fk_DanhMucTenQuyen")
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
