using System.Reflection.Emit;
using System.Reflection;
using AppData.Entities.Models;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace AppData.DB_Context
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(){ }
        //
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options){}
      

        public DbSet<Anh> anhs { get; set; }
        public DbSet<AnhSanPham> anhSanPhams { get; set; }
        public DbSet<BinhLuan> binhLuans { get; set; }
        public DbSet<ThuongHieu> thuonghieu { get; set; }
        public DbSet<GioHang> gioHangs { get; set; }
        public DbSet<GioHangChiTiet> gioHangChiTiets { get; set; }
        public DbSet<ThanhToan> thanhtoans { get; set; }
        public DbSet<HoaDon> hoaDons { get; set; }
        public DbSet<HoaDonChiTiet> hoaDonChiTiets { get; set; }
        public DbSet<MauSac> mauSacs { get; set; }
        public DbSet<SanPham> sanPhams { get; set; }
        public DbSet<SanPhamChiTiet> sanPhamChiTiets { get; set; }
        public DbSet<Size> sizes { get; set; }
        public DbSet<ThuongHieu> thuongHieus { get; set; }
        public DbSet<Voucher> voucher { get; set; }
        public DbSet<VoucherChiTiet> voucherDetail { get; set; }
        public DbSet<NguoiDung> nguoidung { get; set; }
        public DbSet<ChucVu> chucvu { get; set; }
        public DbSet<Sale> sales { get; set; }
        public DbSet<SaleChiTiet> saleChiTiets { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            //optionsBuilder.UseSqlServer(@"Data Source=QUYNHTRANG\SQLEXPRESS;Initial Catalog=TestDATN;User ID=trangntq;Password=trang130603");

            //optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-PTN36F3\SQLEXPRESS;Initial Catalog=DatnTest;Integrated Security=True");
            //optionsBuilder.UseSqlServer(@"Data Source=WINDOWS-10\MSSQLSERVER01;Initial Catalog=DATN;Integrated Security=True;");
            optionsBuilder.UseSqlServer(@"Data Source=WINDOWS-10\MSSQLSERVER01;Initial Catalog=DATN;Integrated Security=True;");

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}