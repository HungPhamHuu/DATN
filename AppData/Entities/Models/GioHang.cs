using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Entities.Models
{
    public class GioHang
    {
        [Key]
        public Guid ID { get; set; }
        [Required]
        public Guid IDNguoiDung { get; set; }
        public virtual NguoiDung NguoiDung { get; set; }
        public virtual IEnumerable<GioHangChiTiet> GioHangChiTiets { get; set; }
    }
}
