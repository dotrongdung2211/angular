﻿using System;
using System.Collections.Generic;
using System.Text;
using hrmSolution.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace hrmSolution.Data.Configurations
{
    public class Tbl_DanhMucManHinhConfiguration : IEntityTypeConfiguration<Tbl_DanhMucManHinh>
    {
        public void Configure(EntityTypeBuilder<Tbl_DanhMucManHinh> builder)
        {
            builder.ToTable("Tbl_DanhMucManHinh");

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

            builder.Property(c => c.C_TenManHinh)
               .IsRequired()
               .HasColumnName("c_TenManHinh")
               .IsUnicode();

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
        }
    }
}
