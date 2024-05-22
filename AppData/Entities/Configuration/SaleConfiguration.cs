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
    public class SaleConfiguration : IEntityTypeConfiguration<Sale>
    {
        public void Configure(EntityTypeBuilder<Sale> builder)
        {
            builder.ToTable("Sale");
            builder.HasKey(x => x.ID);
            builder.Property(x => x.MaSale).HasColumnType("nvarchar(100)").IsRequired();
            builder.Property(x => x.NgayBatDau).HasColumnType("DateTime");
            builder.Property(x => x.NgayKetThuc).HasColumnType("DateTime");
            builder.Property(x => x.PhanTramGiam).HasColumnType("int");
            builder.Property(x => x.TrangThai).HasColumnType("int");
        }
    }
}
