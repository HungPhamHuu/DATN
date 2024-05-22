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
    public class GioHangConfiguration : IEntityTypeConfiguration<GioHang>
    {

        public void Configure(EntityTypeBuilder<GioHang> builder)
        {
            builder.ToTable("GioHang");
            builder.HasKey(x => x.ID);
            builder.HasOne(x => x.NguoiDung).WithMany(x => x.GioHang).HasForeignKey(x => x.IDNguoiDung).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
