using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Entities.Models
{
    public class ThuongHieu
    {
        [Key]
        public Guid ID { get; set; }
        public string TenThuongHieu { get; set; }
        public virtual IEnumerable<SanPhamChiTiet> SanPhamChiTiets { get; set; }
    }
}
