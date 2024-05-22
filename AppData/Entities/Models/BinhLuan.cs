using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Entities.Models
{
    public class BinhLuan
    {
        [Key]
        public Guid ID { get; set; }
        public Guid IDSpCt { get; set; }
        public string? DanhGiaSanPham { get; set; }
        public string? HinhAnh { get; set; }
        public Guid IDNguoiDung { get; set; }
        public DateTime? NgayTao { get; set; }
        public string? NoiDung { get; set; }
        public int TrangThai { get; set; }

        public virtual SanPhamChiTiet SanPhamChiTiet { get; set; }
        public virtual NguoiDung NguoiDung { get; set; }

    }
}
