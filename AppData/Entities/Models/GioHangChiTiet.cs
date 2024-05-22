using DataAnnotationsExtensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Entities.Models
{
    public class GioHangChiTiet
    {
        [Key]
        public Guid ID { get; set; }
        [Required]
        public Guid IDGioHang {  get; set; }
        public Guid? IDSPCT { get; set; }
        public Guid? IDSaleCT { get; set; }
        [Min(1, ErrorMessage = "Số lượng lớn hơn bằng 1.")]
        public int SoLuong { get; set; }
        public decimal Gia { get; set;}
        public virtual GioHang GioHang { get; set; }
        public virtual SanPhamChiTiet SanPhamChiTiet { get; set; }
        public virtual SaleChiTiet SaleChiTiet { get; set; }
    }
}
