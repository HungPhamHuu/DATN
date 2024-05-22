using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppCommon.Implements;

namespace AppData.Entities.Models
{
    public class NguoiDung 
    {
        [Key]
        public Guid Id { get; set; }
        public string TenNguoiDung { get; set; }
        public DateTime? NgaySinh { get; set; }
        public Guid IdChucVu { get; set; }
        public string? Anh { get; set; }
        [MaxLength(10)]
        public string? SDT { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        public string MatKhau { get; set; }
        public string? QuanHuyen { get; set; }
        public string? TinhThanh { get; set; }
        public string? PhuongXa { get; set; }
        public string? DiaChi { get; set; }
        public int TrangThai { get; set; }
        public virtual ChucVu ChucVus { get; set; }
        public virtual IEnumerable<HoaDon> HoaDons { get; set; }
        public virtual IEnumerable<BinhLuan> BinhLuans { get; set; }
        public virtual IEnumerable<GioHang> GioHang { get; set; }
        public virtual IEnumerable<VoucherChiTiet> VoucherChiTiets { get; set; }
    }
}