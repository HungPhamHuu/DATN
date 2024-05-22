using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Entities.Models
{
    public class Voucher
    {
        [Key]
        public Guid ID { get; set; }
        public string MaVoucher {  get; set; }
        public DateTime NgayTao { get; set; }
        public DateTime? NgayBatDau {  get; set; }
        public DateTime? NgayKetThuc {  get; set; }
        public decimal GiaTriVoucher { get; set; }
        public decimal DieuKienMin { get; set; }
        public decimal DieuKienMax { get; set; }
        public int SoLuong { get; set; }
        public string? MoTa {  get; set; }
        public int TrangThai { get; set; }
        public virtual IEnumerable<VoucherChiTiet> VoucherChiTiets { get; set; }

    }
}
