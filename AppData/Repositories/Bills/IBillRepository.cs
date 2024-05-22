using AppData.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Repositories.Bills;

public interface IBillRepository
{
    bool Ceate(HoaDon hoaDon);
    bool Update(HoaDon hoaDon, Guid id);
    Task<bool> Delete(Guid id);
    public List<HoaDon> GetAll();
    public List<HoaDon> DanhSachTheoTenvaSDT(string tukhoa);
    public List<HoaDon> DanhSachHoaDonChoTheoTenvaSDT(string tukhoa);
    public List<HoaDon> DanhSachTheoTrangThai(int trangthai);
    public List<HoaDon> ListTrangThaiHoaDon();
    public List<HoaDon> GetAllByIdNguoiDung(Guid idnguoidung);
    public HoaDon LayHoaDonChoMoiNhat();
    public HoaDon HoaDonTaoMoiNhat(string sdtkhachhang);
    Task<HoaDon> GetId(Guid id);
    Task<bool> GetMa(string ma);
    string HuyHoaDon(Guid id);
    string XacNhanHoaDon(Guid id);
    string DangGiao(Guid id);
    string DaNhan(Guid id);
    string HoanHang(Guid id);
    string XacNhanHoanHang(Guid id);
}
