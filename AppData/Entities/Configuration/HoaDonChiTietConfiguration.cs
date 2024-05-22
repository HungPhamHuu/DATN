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
    public class HoaDonChiTietConfiguration : IEntityTypeConfiguration<HoaDonChiTiet>
    {
        public void Configure(EntityTypeBuilder<HoaDonChiTiet> builder)
        {
            builder.ToTable("HoaDonChiTiet");
            builder.HasKey(x => x.ID);
            builder.Property(x => x.SoLuong).HasColumnType("int");
            builder.Property(x => x.Gia).HasColumnType("decimal");
            builder.HasOne(x => x.HoaDon).WithMany(x => x.HoaDonChiTiets).HasForeignKey(x => x.IDHoaDon).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.SanPhamChiTiet).WithMany(x => x.HoaDonChiTiets).HasForeignKey(x => x.IDSPCT).OnDelete(DeleteBehavior.Restrict).IsRequired(false);
            builder.HasOne(x => x.SaleChiTiet).WithMany(x => x.HoaDonChiTiets).HasForeignKey(x => x.IDSaleCT).OnDelete(DeleteBehavior.Restrict).IsRequired(false);
        }
    }
}
