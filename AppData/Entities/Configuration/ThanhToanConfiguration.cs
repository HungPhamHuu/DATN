using AppData.Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Entities.Configuration
{
    public class ThanhToanConfiguration : IEntityTypeConfiguration<ThanhToan>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<ThanhToan> builder)
        {
            builder.HasKey(x => x.ID);
            builder.Property(x => x.HinhThucThanhToan).HasColumnType("nvarchar(225)");
            builder.Property(x => x.TrangThai).HasColumnType("int");
        }
    }
}
