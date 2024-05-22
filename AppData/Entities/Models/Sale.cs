using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Entities.Models
{
    public class Sale
    {
        [Key]
        public Guid? ID { get; set; }
        public string? MaSale { get; set; }
        public DateTime? NgayBatDau { get; set; }
        public DateTime? NgayKetThuc { get; set; }
        public int TrangThai { get; set; }
        public decimal PhanTramGiam { get; set; }
        public virtual IEnumerable<SaleChiTiet> SaleChiTiets { get; set; }

    }
}
