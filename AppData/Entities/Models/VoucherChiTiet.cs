using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Entities.Models
{
    public class VoucherChiTiet
    {
        [Key]
        public Guid ID { get; set; }
        public Guid? IDVoucher {  get; set; }
        public Guid? IDNguoiDung { get; set; }
        public int SoLuong { get; set; }
        public int? TrangThai { get; set; }
        public virtual NguoiDung NguoiDung { get; set; }
        public virtual Voucher Voucher { get; set; }
        public virtual IEnumerable<HoaDon> HoaDons { get; set; }
    }
}
