using AppData.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Entities.ViewModels
{
    public class SanPhamChiTietViewModel
    {
        public Guid ID { get; set; }
        public string? TenSP { get; set; }
        public string? Size { get; set; }
        public string? TenThuongHieu { get; set; }
        public string? MauSac { get; set; }
        public string MaSPCT { get; set; }
        public string? LoaiSanPham { get; set; }
        public int SoLuong { get; set; }
        public decimal GiaBan { get; set; }
        public string? MoTa { get; set; }
        public byte[]? QrImage { get; set; }
        public int? TrangThai { get; set; }
        public List<Anh> lstAnhSanPham { get; set; }
    }
}
