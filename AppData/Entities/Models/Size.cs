using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace AppData.Entities.Models
{
    public class Size
    {
        [Key]
        public Guid ID { get; set; }
        public string SizeNumber { get; set; }
        public virtual IEnumerable<SanPhamChiTiet> SanPhamChiTiets { get; set; }
    }
}