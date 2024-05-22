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
    public class SanPhamChiTietConfiguration : IEntityTypeConfiguration<SanPhamChiTiet>
    {
       
        public void Configure(EntityTypeBuilder<SanPhamChiTiet> builder)
        {
            builder.ToTable("SanPhamChiTiet");
            builder.HasKey(x => x.ID);
            builder.Property(x => x.MaSPCT).HasColumnType("nvarchar(100)");
            builder.Property(x => x.LoaiSanPham).HasColumnType("nvarchar(100)");
            builder.Property(x => x.SoLuong).HasColumnType("int");
            builder.Property(x => x.GiaBan).HasColumnType("decimal");
            builder.Property(x => x.MoTa).HasColumnType("nvarchar(max)");
            builder.Property(x => x.TrangThai).HasColumnType("int");
            builder.HasOne(x => x.SanPham).WithMany(x => x.SanPhamChiTiets).HasForeignKey(x => x.IDSP);
            //builder.HasOne(x => x.Anh).WithMany(x => x.sanPhamChiTiets).HasForeignKey(x => x.IdAnh);
            builder.HasOne(x => x.MauSac).WithMany(x => x.SanPhamChiTiets).HasForeignKey(x => x.IDMauSac);
            builder.HasOne(x => x.Size).WithMany(x => x.SanPhamChiTiets).HasForeignKey(x => x.IDSize);
            builder.HasOne(x => x.ThuongHieu).WithMany(x => x.SanPhamChiTiets).HasForeignKey(x => x.IDThuongHieu);
            

        }
    }
}
