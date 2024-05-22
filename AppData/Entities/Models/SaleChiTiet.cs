using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Entities.Models
{
    public class SaleChiTiet
    {
        [Key]
        public Guid? ID { get; set; }
        public Guid? IDSale { get; set; }
        public Guid? IDSPCT { get; set; }
        public int SoLuong { get; set; }
        public int TrangThai { get; set; }
        public virtual Sale Sale { get; set; }
        public virtual SanPhamChiTiet SanPhamChiTiet { get; set; }
        public virtual IEnumerable<GioHangChiTiet>? GioHangChiTiets { get; set; }
        public virtual IEnumerable<HoaDonChiTiet>? HoaDonChiTiets { get; set; }
    }
}
