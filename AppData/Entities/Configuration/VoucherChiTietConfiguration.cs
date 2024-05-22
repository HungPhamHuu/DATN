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
    public class VoucherChiTietConfiguration : IEntityTypeConfiguration<VoucherChiTiet>
    {
        public void Configure(EntityTypeBuilder<VoucherChiTiet> builder)
        {
            builder.ToTable("VoucherChiTiet");
            builder.HasKey(x => x.ID);
            builder.Property(x => x.SoLuong).HasColumnType("int");
            builder.Property(x => x.TrangThai).HasColumnType("int");
            builder.HasOne(x => x.Voucher).WithMany(x => x.VoucherChiTiets).HasForeignKey(x => x.IDVoucher);
            builder.HasOne(x => x.NguoiDung).WithMany(x => x.VoucherChiTiets).HasForeignKey(x => x.IDNguoiDung);
        }
    }
}
