using AppData.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace AppData.Entities.Configuration
{
    public class NguoiDungConfiguration : IEntityTypeConfiguration<NguoiDung>
    {
        public void Configure(EntityTypeBuilder<NguoiDung> builder)
        {
            builder.ToTable("NguoiDung");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Email).HasColumnType("varchar(225)");
            builder.Property(x => x.SDT).HasColumnType("varchar(10)").IsRequired();
            builder.Property(x => x.TenNguoiDung).HasColumnType("nvarchar(100)");
            builder.Property(x => x.Anh).HasColumnType("nvarchar(100)").IsRequired(false);
            builder.Property(x => x.MatKhau).HasColumnType("nvarchar(100)");
            builder.Property(x => x.PhuongXa).HasColumnType("nvarchar(255)");
            builder.Property(x => x.QuanHuyen).HasColumnType("nvarchar(255)");
            builder.Property(x => x.DiaChi).HasColumnType("nvarchar(max)");
            builder.Property(x => x.TinhThanh).HasColumnType("nvarchar(255)");
            builder.Property(x => x.TrangThai).HasColumnType("int");
            builder.Property(x => x.NgaySinh).HasColumnType("DateTime");
            builder.HasOne(c => c.ChucVus).WithMany(c => c.NguoiDung).HasForeignKey(c => c.IdChucVu);
        }
    }
}