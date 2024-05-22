using DataAnnotationsExtensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Entities.Models
{
    public class HoaDon
    {
        [Key]
        public Guid ID { get; set; }
        public Guid IDNguoiDung { get; set; }
        public Guid? IDVoucherChiTiet { get; set; }
        public Guid? IDHinhThucThanhToan { get; set; }
        [Required]
        public string MaHoaDon {  get; set; }
        public DateTime NgayTao { get; set; }
        [Required]
        public string? TenKhachHang { get; set; }
        [Required]
        public string? SDTKhachHang { get; set; }
        [Required]
        public string? DiaChi { get; set; }
        public int TongSoLuong {  get; set; }
        public decimal ThanhTien { get; set; }
        public decimal TienShip {  get; set; }
        public decimal TienGiamGia { get; set; }
        public decimal TongTien {  get; set; }
        public DateTime? NgayThanhToan { get; set; }
        public DateTime? NgayNhanHang { get; set; }
        public string? GhiChu { get; set; }
        public int TrangThai { get; set; }
        public virtual NguoiDung NguoiDung { get; set;}
        public virtual VoucherChiTiet VoucherChiTiet { get; set; }
        public virtual ThanhToan ThanhToan { get; set; }
        public virtual IEnumerable<HoaDonChiTiet> HoaDonChiTiets { get; set; }
    }
}
