using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Entities.ViewModels.GioHangCTVM
{
    public class GioHangCTRequestVM
    {
        public Guid Id { get; set; }
        public Guid IdGioHang {  get; set; }
        public Guid IDSPCT { get; set; }
        public int SoLuong { get; set; }
    }
}
