using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Entities.ViewModels
{
    public class BinhLuanViewModel
    {
        public Guid ID { get; set; }
        public Guid IDSpCt { get; set; }
        public string TenSanPham { get; set; }
        public string? DanhGiaSanPham { get; set; }
        public string? HinhAnh { get; set; }
        public Guid IDNguoiDung { get; set; }
        public string TenNguoiDung { get;set; }
        public DateTime? NgayTao { get; set; }
        public string? NoiDung { get; set; }
        public int TrangThai { get; set; }
    }
}
