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
    public class SaleChiTietConfiguration : IEntityTypeConfiguration<SaleChiTiet>
    {
        public void Configure(EntityTypeBuilder<SaleChiTiet> builder)
        {
            builder.ToTable("SaleChiTiet");
            builder.HasKey(x => x.ID);
            builder.HasOne(x => x.Sale).WithMany(x => x.SaleChiTiets).HasForeignKey(x => x.IDSale);
            builder.HasOne(x => x.SanPhamChiTiet).WithMany(x => x.SaleChiTiets).HasForeignKey(x => x.IDSPCT);
        }
    }
}
