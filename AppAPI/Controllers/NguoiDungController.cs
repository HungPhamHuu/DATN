using AppData.DB_Context;
using AppData.Entities.Models;
using AppData.Repositories.Roles;
using AppData.Repositories.Users;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AppAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class NguoiDungController : ControllerBase
    {
        public readonly IUserRepository _inguoidung;
        public readonly IRoleRepository _iChucvu;
        public readonly ApplicationDbContext _context;
        public NguoiDungController()
        {
            _iChucvu = new RoleRepository();
            _inguoidung = new UserRepository();
        }
        // GET: api/<NguoiDungController>
        [HttpPost("[action]")]
        public string DangNhap(string emai, string matkhau)
        {
            return _inguoidung.DangNhap(emai, matkhau);
        }

        // GET api/<DangNhapController>/5
        [HttpPost("[action]")]
        public string DangKyKhachHang(string name, DateTime ngaysinh, string anh, string sdt, string email, string mk, string quan, string tinh, string xa, string diachi)
        {
            if (DateTime.Now.Year - ngaysinh.Year < 18)
            {
                return "Yêu cầu khách hàng trên 18 tuổi.";
            }
            else
            {
                NguoiDung nguoiDung = new NguoiDung()
                {
                    Id = Guid.NewGuid(),
                    IdChucVu = _iChucvu.DanhSachChucVu().FirstOrDefault(c => c.TenChucVu == "Khách hàng").Id,
                    TenNguoiDung = name,
                    NgaySinh = ngaysinh,
                    Anh = anh,
                    SDT = sdt,
                    Email = email,
                    MatKhau = mk,
                    PhuongXa = xa,
                    TinhThanh = tinh,
                    DiaChi = diachi,
                    QuanHuyen = quan,
                    TrangThai = 1
                };
                return _inguoidung.CreateKhachHang(nguoiDung);
            }
        }

        // POST api/<DangNhapController>
        [HttpPost("[action]")]
        public string DangKyNhanVien(Guid IdChucVu, string name, DateTime ngaysinh, string sdt, string email, string mk)
        {
            if (DateTime.Now.Year - ngaysinh.Year < 18)
            {
                return "Yêu cầu khách hàng trên 18 tuổi.";
            }
            else
            {
                NguoiDung nguoiDung = new NguoiDung()
                {
                    Id = Guid.NewGuid(),
                    IdChucVu = IdChucVu,
                    TenNguoiDung = name,
                    NgaySinh = ngaysinh,
                    Anh = "",
                    SDT = sdt,
                    Email = email,
                    MatKhau = mk,
                    PhuongXa = "",
                    TinhThanh = "",
                    DiaChi = "",
                    QuanHuyen = "",
                    TrangThai = 1
                };
                return _inguoidung.CreateNhanVien(nguoiDung);
            }
        }
        [HttpGet("[action]")]
        public IEnumerable<ChucVu> DanhSachChucVu()
        {
            return _iChucvu.DanhSachChucVu();
        }

        // GET api/<NguoiDungController>/5
        [HttpPost("[action]")]
        public bool ThemMoiChucVu(ChucVu chucVu)
        {
            return _iChucvu.ThemMoiChucVu(chucVu);
        }
        // POST api/<NguoiDungController>
        //Nguoi Dung
        [HttpGet("[action]")]
        public IEnumerable<NguoiDung> DanhSachNguoiDung()
        {
            return _inguoidung.DanhSachNguoiDung();
        }
        [HttpGet("[action]")]
        public IEnumerable<NguoiDung> lstNguoiDungTheoTen(string TenNguoiDung)
        {
            return _inguoidung.lstNguoiDungTheoTen(TenNguoiDung);
        }
        [HttpGet("[action]")]
        public IEnumerable<NguoiDung> lstNguoiDungTheoSDT(string sdt)
        {
            return _inguoidung.lstNguoiDungTheoSDT(sdt);
        }
        [HttpGet("[action]")]
        public List<NguoiDung> lstNguoiDungTheoEmai(string email)
        {
            return _inguoidung.lstNguoiDungTheoEmai(email);
        }
        [HttpGet("[action]")]
        public NguoiDung NguoiDungTheoId(Guid idnguoidung)
        {
            return _inguoidung.NguoiDungTheoId(idnguoidung);
        }

        // GET api/<NguoiDungController>/5
        [HttpPut("[action]")]
        public string ChinhSuaNguoiDung(Guid idnguoidung , string name , DateTime? Ngaysinh, string? anh , string? sdt , string? email, string? quanhuyen, string? tinhthanh, string? phuongxa, string? diachi)
        {
            if (DateTime.Now.Year - Ngaysinh.Value.Year < 18)
            {
                return "Yêu cầu khách hàng trên 18 tuổi.";
            }
            else
            {
                var nguoidung = _inguoidung.lstnguoidung().FirstOrDefault(C => C.Id == idnguoidung);
                nguoidung.TenNguoiDung = name;
                nguoidung.NgaySinh = Ngaysinh;
                nguoidung.Anh = anh;
                nguoidung.SDT = sdt;
                nguoidung.Email = email;
                nguoidung.QuanHuyen = quanhuyen;
                nguoidung.TinhThanh = tinhthanh;
                nguoidung.PhuongXa = phuongxa;
                nguoidung.DiaChi = diachi;
                return _inguoidung.ChinhSuaThongTin(nguoidung);
            }

        }
        [HttpPut("[action]")]
        public string DoiMatKhau(Guid idnguoidung , string matkhau)
        {
            try
            {
                var nguoidung = _inguoidung.lstnguoidung().FirstOrDefault(C => C.Id == idnguoidung);
                if(nguoidung == null)
                {
                    return "Thất bại.";
                } else
                {
                    nguoidung.MatKhau = matkhau;
                    return _inguoidung.ChinhSuaThongTin(nguoidung);
                }
            } catch (Exception e)
            {
                return "Lỗi.";
            }
         
        }

        [HttpPut("[action]")]
        public string TatHoatDong(Guid IdNguoiDung)
        {
            return _inguoidung.TatHoatDong(IdNguoiDung);
        }
        [HttpPut("[action]")]
        public string KichHoatTaiKhoan(Guid IdNguoiDung)
        {
            return _inguoidung.KichHoat(IdNguoiDung);
        }
        [HttpDelete("[action]")]
        public bool Xoa(string email)
        {
            try
            {
                NguoiDung user = _context.nguoidung.FirstOrDefault(c => c.Email == email);
                _context.nguoidung.Remove(user);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        [HttpGet("[action]")]
        public List<NguoiDung> DanhSachNhanVien()
        {
            try
            {
                return _inguoidung.DanhSachNhanVien();
            } catch
            {
                return null;
            }
        }
        [HttpGet("[action]")]
        public List<NguoiDung> TimKiemNhanVienTheoTuKhoa(string tukhoa)
        {
            try
            {
                return _inguoidung.TimKiemNhanVienTheoTuKhoa(tukhoa);
            }
            catch
            {
                return null;
            }
        }
        [HttpGet("[action]")]
        public List<NguoiDung> TimKiemTheoChucVu(Guid idChucVu)
        {
            try
            {
                return _inguoidung.TimKiemTheoChucVu(idChucVu);
            }
            catch
            {
                return null;
            }
        }


    }
}