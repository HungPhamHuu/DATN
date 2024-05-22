using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Entities.Models
{
    public class ThanhToan
    {
        [Key]
        public Guid ID { get; set; }
        public string HinhThucThanhToan { get; set; }
        public int TrangThai { get; set; }
        public IEnumerable<HoaDon> hoaDons { get; set; }
    }
}
