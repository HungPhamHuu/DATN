using AppData.DB_Context;
using AppData.Entities.Models;
using AppData.Entities.ViewModels;
using AppData.Repositories.Comment;
using AppData.Repositories.Users;
using AppData.Repositories.ViewModel;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BinhLuanController : ControllerBase
    {
        // GET: api/<BinhLuanController>
        public readonly IBinhLuanRepository _ibinhluan;
        public readonly ApplicationDbContext _context;
        public readonly IUserRepository userRepository;
        public readonly SPCTViewModel _spctViewModel;
        public readonly BinhLuanVM binhLuanVM;
        public BinhLuanController()
        {
            _context = new ApplicationDbContext();
            _spctViewModel = new SPCTViewModel();
            _ibinhluan = new BinhLuanRepository(_context);
            userRepository = new UserRepository();
            binhLuanVM = new BinhLuanVM();
        }
        [HttpGet("[action]")]
        public IEnumerable<BinhLuan> DanhSachBinhLuan()
        {
            return _ibinhluan.DanhSachBinhLuan();
        }
        [HttpGet("[action]")]
        public IEnumerable<BinhLuanViewModel> GetAll()
        {
            return binhLuanVM.GetAll();
        }
        [HttpGet("[action]")]
        public IEnumerable<BinhLuanViewModel> DanhSachBinhLuanTheoTen(string tensp)
        {
            return binhLuanVM.GetAllTenSanPham(tensp);
        }

        [HttpGet("[action]")]
        public IEnumerable<BinhLuan> DanhSachBinhLuanTheoIdSPCT(Guid ID)
        {
            return _ibinhluan.DanhSachBinhLuanTheoIdSPCT(ID);
        }

        [HttpGet("[action]")]
        public IEnumerable<BinhLuan> DanhSachBinhLuanTheoIdSPCTSale(Guid IDspsale)
        {
            return _ibinhluan.DanhSachBinhLuanTheoIdSPCTSale(IDspsale);
        }
        [HttpGet("[action]")]
        public IEnumerable<BinhLuan> DanhSachBinhLuanTheoTenSPCT()
        {
            return _ibinhluan.DanhSachBinhLuan();
        }

        // GET api/<BinhLuanController>/5
        [HttpPost("[action]")]
        public string TaoBinhLuanMoi(Guid idsp, Guid idnguoidung, string Danhgiasanpham, string? anh, string? noidung)
        {
            try
            {
                var sanpham = _spctViewModel.DanhSachSanPhamHoanThien().FirstOrDefault(c => c.ID == idsp);

                if (GetAll().Any(c => c.IDNguoiDung == idnguoidung && c.TenSanPham.ToLower() == sanpham.TenSP.ToLower()))
                {
                    return "Sản phẩm bạn đã bình luận.";
                }
                else
                {
                    BinhLuan cmt = new BinhLuan()
                    {
                        ID = Guid.NewGuid(),
                        IDSpCt = idsp,
                        IDNguoiDung = idnguoidung,
                        DanhGiaSanPham = Danhgiasanpham,
                        HinhAnh = anh,
                        NgayTao = DateTime.Now,
                        NoiDung = noidung,
                        TrangThai = 1,
                    };
                    return _ibinhluan.ThemMoiBinhLuan(cmt);
                }
            }
            catch (Exception ex)
            {
                return "Lỗi: " + ex.Message;
            }

        }

        // POST api/<BinhLuanController>
        [HttpPost("[action]")]
        public string ChinhSuaBinhLuan(Guid idbinhluan, string? DanhGia, string? anh, string? NoiDung)
        {
            try
            {
                var cmt = DanhSachBinhLuan().FirstOrDefault(c => c.ID == idbinhluan);
                cmt.DanhGiaSanPham = DanhGia;
                cmt.HinhAnh = anh;
                cmt.NoiDung = NoiDung;
                return _ibinhluan.ChinhSuaBinhLuan(cmt);
            }
            catch (Exception ex)
            {
                return "Lỗi: " + ex.Message;
            }
        }

        // PUT api/<BinhLuanController>/5
        [HttpPut("[action]")]
        public string XoaBinhLuan(Guid id)
        {
            return _ibinhluan.XoaBinhLuan(id);
        }
    }
}
