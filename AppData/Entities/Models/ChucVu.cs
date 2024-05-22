using System.ComponentModel.DataAnnotations;

using AppCommon.Implements;


namespace AppData.Entities.Models
{
    public class ChucVu 
    {
        [Key]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Mời nhập giá trị.")]
        public string TenChucVu { get; set; }
        public int TrangThai { get; set; }
        public virtual IEnumerable<NguoiDung> NguoiDung { get; set; }
    }
}