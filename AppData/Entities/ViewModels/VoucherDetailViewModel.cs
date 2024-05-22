using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Entities.ViewModels
{
    public class VoucherDetailViewModel
    {
        public Guid ID { get; set; }
        public Guid? IDVoucher { get; set; }
        public Guid? IDNguoiDung { get; set; }
        public string MaVoucher { get; set; }
        public DateTime? NgayBatDau { get; set; }
        public DateTime? NgayKetThuc { get; set; }
        public decimal GiaTriVoucher { get; set; }
        public decimal DieuKienMin { get; set; }
        public decimal DieuKienMax { get; set; }
        public int SoLuong { get; set; }
        public string? MoTa { get; set; }
        public int? TrangThai { get; set; }
    }
}
