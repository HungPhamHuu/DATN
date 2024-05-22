using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Entities.Models
{
    public class Anh
    {
        [Key]
        public Guid ID { get; set; }
        public string? DuongDan { get; set; }
        public virtual IEnumerable<SanPhamChiTiet> SanPhamChiTiets { get; set; }
    }
}
