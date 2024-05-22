using AppData.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Entities.ViewModels
{
    public class SaleChiTietViewModel
    {
        public Guid? IDSalechitiet { get; set; }
        public Guid? IDSale { get; set; }
        public Guid? IdSanPhamCT { get; set; }
        public string? MaSale { get ; set; }
        public string? TenSP { get;set; }
        public string? Size { get;set; }
        public string? Mau { get;set; }
        public string? ThuongHieu { get; set; }
        public string? TheLoai { get; set; }
        public int SoLuong { get;set; }
        public decimal GiaGoc { get; set; }
        public List<Anh> lstAnhSanPham { get; set; }
        public decimal PhanTramGiam { get; set; }
        public decimal GiaGiam { get; set; }
        public DateTime? NgayBatDau { get; set; }
        public DateTime? NgayKetThuc { get; set; }
        public string? MoTa { get ; set; }
        public int? TrangThai { get; set; }

    }
}
