using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Entities.ViewModels.GioHangCTVM
{
    public class GioHangCTVM
    {
        public Guid ID { get; set; }
        public Guid IDGioHang { get; set; }
        public Guid? IDSaleCT { get; set; }
        public Guid? IDSPCT { get; set; }
        public string? TenSP { get; set; }
        public int SoLuong { get; set; }
        public decimal Gia { get; set; }
        public decimal GiaGoc { get; set; }
        public string? DuongDanAnh { get ; set; }
        public string? Size { get; set; }
        public string? MauSac {  get; set; }
        public string? ThuongHieu { get; set; }
        public string? LoaiSanPham { get; set; }

        public decimal? ThanhTien { get; set; }
    }
}
