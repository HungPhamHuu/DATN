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
    public class HoaDonConfiguration : IEntityTypeConfiguration<HoaDon>
    {
        public void Configure(EntityTypeBuilder<HoaDon> builder)
        {
            builder.ToTable("HoaDon");
            builder.HasKey(x => x.ID);
            builder.Property(x => x.MaHoaDon).HasColumnType("nvarchar(100)");
            builder.Property(x => x.NgayTao).HasColumnType("DateTime");
            builder.Property(x => x.NgayNhanHang).HasColumnType("DateTime");
            builder.Property(x => x.NgayThanhToan).HasColumnType("DateTime");
            builder.Property(x => x.TongSoLuong).HasColumnType("int");
            builder.Property(x => x.TongTien).HasColumnType("decimal");
            builder.Property(x => x.TienShip).HasColumnType("decimal");
            builder.Property(x => x.TienGiamGia).HasColumnType("decimal");
            builder.Property(x => x.ThanhTien).HasColumnType("decimal");
            builder.Property(x => x.TenKhachHang).HasColumnType("nvarchar(100)");
            builder.Property(x => x.SDTKhachHang).HasColumnType("nvarchar(100)");
            builder.Property(x => x.DiaChi).HasColumnType("nvarchar(100)");
            builder.Property(x => x.GhiChu).HasColumnType("nvarchar(max)").IsRequired(false);
            builder.Property(x => x.TrangThai).HasColumnType("int");
            builder.HasOne(x => x.NguoiDung).WithMany(x => x.HoaDons).HasForeignKey(x => x.IDNguoiDung).OnDelete(DeleteBehavior.Restrict); ;
            builder.HasOne(x => x.VoucherChiTiet).WithMany(x => x.HoaDons).HasForeignKey(x => x.IDVoucherChiTiet).OnDelete(DeleteBehavior.Restrict).IsRequired(false);
            builder.HasOne(x => x.ThanhToan).WithMany(x => x.hoaDons).HasForeignKey(x => x.IDHinhThucThanhToan).OnDelete(DeleteBehavior.Restrict); 
        }
    }
}
