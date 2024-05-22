using AppCommon.RepositoryAsync;
using AppData.DB_Context;
using AppData.Entities.Models;
using AppData.Entities.ViewModels.GioHangVM;
using AppData.Repositories.Cart;
using AppData.Repositories.Roles;
using Microsoft.EntityFrameworkCore;

namespace AppData.Repositories.Users;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;
    private readonly IRoleRepository _chucVures;
    private readonly ICartRepository _cart;
    public UserRepository()
    {
        _context = new ApplicationDbContext();
        _cart = new CartRepository(_context);
        _chucVures = new RoleRepository();
    }

    public string ChinhSuaThongTin(NguoiDung nguoiDung)
    {
        try
        {
            if (nguoiDung == null)
            {
                return "Chỉnh sửa thông tin thất bại.";
            }
            else
            {
                _context.nguoidung.Update(nguoiDung);
                _context.SaveChanges();
                return "Chỉnh sửa thông tin thành công.";
            }
        }
        catch (Exception ex)
        {
            return "Lỗi.";
        }
    }

    public string CreateKhachHang(NguoiDung nguoidung)
    {
        try
        {
            if (nguoidung == null)
            {
                return "Đăng ký khách hàng thất bại.";
            }
            else if (lstnguoidung().Any(c => c.Email == nguoidung.Email))
            {
                return "Email đã đăng ký. Mời bạn đăng ký lại.";
            }
            else if (lstnguoidung().Any(c => c.SDT == nguoidung.SDT))
            {
                return "SDT đã đăng ký. Mời bạn đăng ký lại.";
            }
            else
            {
                GioHang gioHangVM = new GioHang()
                {
                    ID = Guid.NewGuid(),
                    IDNguoiDung = nguoidung.Id,
                };
                _context.gioHangs.Add(gioHangVM);
                _context.nguoidung.Add(nguoidung);
                _context.SaveChanges();
                return "Đăng ký khách hàng thành công.";
            }
        }
        catch (Exception ex)
        {
            return "Lỗi: " + ex.Message;
        }
    }

    public string CreateNhanVien(NguoiDung nguoidung)
    {
        try
        {
            if (nguoidung == null)
            {
                return "Đăng ký nhân viên thất bại.";
            }
            else if (lstnguoidung().Any(c => c.Email == nguoidung.Email))
            {
                return "Email đã đăng ký. Mời bạn đăng ký lại.";
            }
            else if (lstnguoidung().Any(c => c.SDT == nguoidung.SDT))
            {
                return "SDT đã đăng ký. Mời bạn đăng ký lại.";
            }
            else
            {
                GioHang gioHangVM = new GioHang()
                {
                    ID = Guid.NewGuid(),
                    IDNguoiDung = nguoidung.Id,
                };
                _context.gioHangs.Add(gioHangVM);
                _context.nguoidung.Add(nguoidung);
                _context.SaveChanges();
                return "Đăng ký nhân viên thành công.";
            }
        }
        catch (Exception ex)
        {
            return "Lỗi: " + ex.Message;
        }
    }

    public List<NguoiDung> lstnguoidung()
    {
        var lstnguoidung = _context.nguoidung.ToList();
        return lstnguoidung;
    }
    public string DangNhap(string email, string matkhau)
    {
        try
        {
            if (lstnguoidung().Any(c => c.Email == email))
            {
                var user = lstnguoidung().FirstOrDefault(c => c.Email == email);
                if (user.TrangThai == 0)
                {
                    return "Tài khoản đã bị khóa liên hệ với Admin.";
                }
                else
                {
                    if (user.MatKhau == matkhau)
                    {
                        var idcv = lstnguoidung().FirstOrDefault(c => c.Email == email && matkhau == matkhau).IdChucVu;
                        var tencv = _chucVures.DanhSachChucVu().FirstOrDefault(c => c.Id == idcv).TenChucVu;
                        if (tencv == "Khách hàng")
                        {
                            return "Khách hàng đăng nhập thành công.";
                        }
                        else if (tencv == "Quản lý")
                        {
                            return "Quản lý đăng nhập thành công.";
                        }
                        else
                        {
                            return "Nhân viên đăng nhập thành công.";
                        }
                    }
                    else
                    {
                        return "Sai thông tin đăng nhập. Mời đăng nhập lại.";
                    }
                }
            }
            else
            {
                return "Tài khoản chưa được đăng ký. Vui lòng đăng ký tài khoản.";
            }

        }
        catch (Exception ex)
        {
            return "Lỗi: " + ex.Message;
        }
    }
    public NguoiDung NguoiDungTheoId(Guid id)
    {
        try
        {
            return _context.nguoidung.FirstOrDefault(c => c.Id == id);
        }
        catch (Exception ex)
        {
            return null;
        }
    }
    public List<NguoiDung> lstNguoiDungTheoTen(string tennguoidung)
    {
        try
        {
            return _context.nguoidung.Where(c => c.TenNguoiDung.Contains(tennguoidung) || c.Email.Contains(tennguoidung) || c.SDT.Contains(tennguoidung)).ToList();
        }
        catch (Exception ex)
        {
            return null;
        }
    }
    public List<NguoiDung> DanhSachNguoiDung()
    {
        try
        {
            var idchucvukh = _chucVures.DanhSachChucVu().FirstOrDefault(c => c.TenChucVu == "Khách hàng").Id;
            return lstnguoidung().Where(c => c.IdChucVu == idchucvukh).ToList();
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public string KichHoat(Guid id)
    {
        try
        {
            var nguoidung = lstnguoidung().FirstOrDefault(C => C.Id == id);
            nguoidung.TrangThai = 1;
            _context.nguoidung.Update(nguoidung);
            _context.SaveChanges();
            return "Kíck hoạt thành công.";
        }
        catch (Exception ex)
        {
            return "Lỗi: " + ex.Message;
        }
    }

    public string TatHoatDong(Guid id)
    {
        try
        {
            var nguoidung = lstnguoidung().FirstOrDefault(C => C.Id == id);
            nguoidung.TrangThai = 0;
            _context.nguoidung.Update(nguoidung);
            _context.SaveChanges();
            return "Tắt hoạt động thành công.";
        }
        catch (Exception ex)
        {
            return "Lỗi: " + ex.Message;
        }
    }
    public List<NguoiDung> lstNguoiDungTheoSDT(string sdt)
    {
        try
        {
            return lstnguoidung().Where(C => C.SDT.Contains(sdt)).ToList();
        }
        catch (Exception ex)
        {
            return null;
        }
    }
    public List<NguoiDung> lstNguoiDungTheoEmai(string email)
    {
        try
        {
            return lstnguoidung().Where(C => C.Email.ToLower() == email.ToLower()).ToList();
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public List<NguoiDung> DanhSachNhanVien()
    {
        try
        {
            var idchucvukh = _chucVures.DanhSachChucVu().FirstOrDefault(c => c.TenChucVu == "Khách hàng").Id;
            var lst = lstnguoidung().Where(c => c.IdChucVu != idchucvukh).ToList();
            return lst;
        } catch
        {
            return null;
        }
       

    }
    public List<NguoiDung> TimKiemNhanVienTheoTuKhoa(string tukhoa)
    {
        try
        {
            var lst = DanhSachNhanVien().Where(c => c.TenNguoiDung.ToLower().Contains(tukhoa.ToLower()) || c.Email.ToLower().Contains(tukhoa.ToLower()) || c.SDT.ToLower().Contains(tukhoa.ToLower())).ToList();
            return lst;
        }
        catch
        {
            return null;
        }
    }
    public List<NguoiDung> TimKiemTheoChucVu(Guid idChucVu)
    {
        try
        {
            var lst = DanhSachNhanVien().Where(c => c.IdChucVu == idChucVu).ToList();
            return lst;
        }
        catch
        {
            return null;
        }
    }
}