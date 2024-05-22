using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Entities.Models
{
    public class HoaDonChiTiet
    {
        [Key]
        public Guid ID { get; set; }
        public Guid IDHoaDon {  get; set; }
        public Guid? IDSPCT { get; set; }
        public Guid? IDSaleCT { get; set; }
        public int SoLuong {  get; set; }
        public decimal Gia { get; set; }
        public virtual HoaDon HoaDon { get; set; }
        public virtual SanPhamChiTiet SanPhamChiTiet { get; set; }
        public virtual SaleChiTiet SaleChiTiet { get; set; }
    }
}
