using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Entities.Models
{
    public class AnhSanPham
    {
        public Guid IdSanPhamChiTiet { get; set; }
        public Guid Idanh { get; set; }
        public Anh Anh { get; set; }
        public SanPhamChiTiet SanPhamChiTiet { get; set; }
    }
}
