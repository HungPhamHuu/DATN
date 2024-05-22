using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Entities.ViewModels
{
    public class HoaDonChiTietViewModel
    {
        public Guid ID { get; set; }
        public Guid IDHoaDon { get; set; }
        public Guid? IDSaleCT { get; set; }
        public Guid? IDSPCT { get; set; }
        public string? TenSP { get; set; }
        public int SoLuong { get; set; }
        public decimal Gia { get; set; }
        public string? image { get; set; }
        public string? Size { get; set; }
        public string? MauSac { get; set; }

        public decimal ThanhTien { get; set; }
    }
}
