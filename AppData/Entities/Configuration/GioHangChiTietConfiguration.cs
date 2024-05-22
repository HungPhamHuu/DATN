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
    public class GioHangChiTietConfiguration : IEntityTypeConfiguration<GioHangChiTiet>
    {
        public void Configure(EntityTypeBuilder<GioHangChiTiet> builder)
        {
            builder.ToTable("GioHangChiTiet");
            builder.HasKey(x => x.ID);
            builder.Property(x => x.SoLuong).HasColumnType("int");
            builder.Property(x => x.Gia).HasColumnType("decimal");
            builder.HasOne(x => x.GioHang).WithMany(x => x.GioHangChiTiets).HasForeignKey(x => x.IDGioHang).OnDelete(DeleteBehavior.Restrict); 
            builder.HasOne(x => x.SanPhamChiTiet).WithMany(x => x.GioHangChiTiets).HasForeignKey(x => x.IDSPCT).OnDelete(DeleteBehavior.Restrict).IsRequired(false);
            builder.HasOne(x => x.SaleChiTiet).WithMany(x => x.GioHangChiTiets).HasForeignKey(x => x.IDSaleCT).OnDelete(DeleteBehavior.Restrict).IsRequired(false);
        }
    }
}
