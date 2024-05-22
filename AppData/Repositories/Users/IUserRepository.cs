using AppCommon.RepositoryAsync;
using AppData.Entities.Models;

namespace AppData.Repositories.Users;

public interface IUserRepository 
{
    public string DangNhap(string email, string matkhau);
    public string CreateKhachHang(NguoiDung nguoidung);
    public string CreateNhanVien(NguoiDung nguoidung);
    public List<NguoiDung> DanhSachNguoiDung();
    public List<NguoiDung> lstNguoiDungTheoSDT(string sdt);
    public List<NguoiDung> lstNguoiDungTheoEmai(string email);
    public List<NguoiDung> lstNguoiDungTheoTen(string tennguoidung);
    public NguoiDung NguoiDungTheoId(Guid id);
    public string ChinhSuaThongTin(NguoiDung nguoiDung);
    public string TatHoatDong(Guid id);
    public string KichHoat(Guid id);
    public List<NguoiDung> lstnguoidung();
    public List<NguoiDung> DanhSachNhanVien();
    public List<NguoiDung> TimKiemNhanVienTheoTuKhoa(string tukhoa);
    public List<NguoiDung> TimKiemTheoChucVu(Guid idChucVu);
}