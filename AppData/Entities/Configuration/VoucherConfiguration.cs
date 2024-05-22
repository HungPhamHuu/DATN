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
    public class VoucherConfiguration : IEntityTypeConfiguration<Voucher>
    {
        public void Configure(EntityTypeBuilder<Voucher> builder)
        {
            builder.ToTable("Voucher");
            builder.HasKey(x => x.ID);
            builder.Property(x => x.MaVoucher).HasColumnType("nvarchar(100)").IsRequired();
            builder.Property(x => x.NgayTao).HasColumnType("DateTime");
            builder.Property(x => x.NgayBatDau).HasColumnType("DateTime");
            builder.Property(x => x.NgayKetThuc).HasColumnType("DateTime");
            builder.Property(x => x.GiaTriVoucher).HasColumnType("Decimal");
            builder.Property(x => x.DieuKienMin).HasColumnType("Decimal");
            builder.Property(x => x.DieuKienMax).HasColumnType("Decimal");
            builder.Property(x => x.SoLuong).HasColumnType("int");
            builder.Property(x => x.MoTa).HasColumnType("nvarchar(max)"); ;
            builder.Property(x => x.TrangThai).HasColumnType("int");
        }
    }
}
