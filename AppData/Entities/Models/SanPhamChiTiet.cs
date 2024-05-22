using System.ComponentModel.DataAnnotations;

using DataAnnotationsExtensions;

namespace AppData.Entities.Models
{
    public class SanPhamChiTiet
    {
        [Key]
        public Guid ID { get; set; }
        public Guid? IDSP { get; set; }
        public Guid? IDSize { get; set; }
        public Guid? IDMauSac { get; set; }
        public Guid? IDThuongHieu { get; set; }
        public List<AnhSanPham>? lstAnhSanPham { get; set; }
        //public Guid? IDAnh {  get; set; }
        [Required]
        public string MaSPCT { get; set; }
        [Min(0, ErrorMessage = "Mời bạn nhập giá trị.")]
        public int SoLuong { get; set; }
        [Min(0, ErrorMessage = "Mời bạn nhập giá trị.")]
        public decimal GiaBan { get; set; }
        public string? LoaiSanPham { get; set; }
        public string? MoTa { get; set; }
        public byte[]? QrImage { get; set; }
        public int? TrangThai { get; set; }

        public virtual SanPham? SanPham { get; set; }
        public virtual MauSac? MauSac { get; set; }
        public virtual Size? Size { get; set; }
        public virtual ThuongHieu? ThuongHieu { get; set; }
        public List<Anh>? ListAnh { get; set; }

        public virtual IEnumerable<BinhLuan>? BinhLuan { get; set; }
        public virtual IEnumerable<GioHangChiTiet>? GioHangChiTiets { get; set; }
        public virtual IEnumerable<HoaDonChiTiet>? HoaDonChiTiets { get; set; }
        public virtual IEnumerable<SaleChiTiet> SaleChiTiets { get; set; }

    }
}
