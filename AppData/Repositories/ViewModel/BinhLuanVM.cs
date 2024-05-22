using AppData.DB_Context;
using AppData.Entities.ViewModels;
using AppData.Repositories.Comment;
using AppData.Repositories.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Repositories.ViewModel
{
    public class BinhLuanVM
    {
        private readonly ApplicationDbContext _context;
        private readonly IBinhLuanRepository _ibinhluan;
        private readonly SPCTViewModel _sanphamct;
        private readonly IUserRepository userRepository;
        private readonly SaleChiTietVm saleChiTietVm;
        public BinhLuanVM()
        {
            _context = new ApplicationDbContext();
            _ibinhluan = new BinhLuanRepository(_context);
            userRepository = new UserRepository();
            _sanphamct = new SPCTViewModel();
            saleChiTietVm = new SaleChiTietVm();
        }

        public List<BinhLuanViewModel> GetAll()
        {
            var lst = from a in _ibinhluan.DanhSachBinhLuan()
                      join b in _sanphamct.DanhSachSanPhamHoanThien() on a.IDSpCt equals b.ID
                      join c in userRepository.DanhSachNguoiDung() on a.IDNguoiDung equals c.Id
                      select new BinhLuanViewModel
                      {
                          ID = a.ID,
                          IDSpCt = a.IDSpCt,
                          IDNguoiDung = a.IDNguoiDung,
                          TenNguoiDung = c.TenNguoiDung,
                          TenSanPham = b.TenSP,
                          DanhGiaSanPham = a.DanhGiaSanPham,
                          HinhAnh = a.HinhAnh,
                          NgayTao = a.NgayTao,
                          NoiDung = a.NoiDung,
                          TrangThai = a.TrangThai,
                      };
            return lst.ToList();
        }
        public List<BinhLuanViewModel> GetAllTenSanPham(string tensp)
        {
            return GetAll().Where(c => c.TenSanPham.ToLower().Trim() == tensp.ToLower().Trim()).ToList();
        }
    }
}
