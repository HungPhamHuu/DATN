using AppData.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppData.Entities.Configuration
{
    public class BinhLuanConfiguration : IEntityTypeConfiguration<BinhLuan>
    {
        public void Configure(EntityTypeBuilder<BinhLuan> builder)
        {
            builder.ToTable(nameof(BinhLuan));
            builder.HasKey(x => x.ID);
            builder.Property(x => x.NgayTao).HasColumnType("DateTime");
            builder.Property(x => x.DanhGiaSanPham).HasColumnType("nvarchar(100)");
            builder.Property(x => x.NoiDung).HasColumnType("nvarchar(max)");
            builder.Property(x => x.HinhAnh).HasColumnType("nvarchar(100)");
            builder.Property(x => x.TrangThai).HasColumnType("int");
            builder.HasOne(x => x.SanPhamChiTiet).WithMany(x => x.BinhLuan).HasForeignKey(x => x.IDSpCt).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.NguoiDung).WithMany(x => x.BinhLuans).HasForeignKey(x => x.IDNguoiDung).OnDelete(DeleteBehavior.Restrict);
        }
    }
}