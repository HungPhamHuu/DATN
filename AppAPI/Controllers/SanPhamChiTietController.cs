using AppData.DB_Context;
using AppData.Entities.Models;
using AppData.Entities.ViewModels;
using AppData.Repositories.Bills;
using AppData.Repositories.Product;
using AppData.Repositories.ProductDetail;
using AppData.Repositories.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using System.Net.Http.Headers;

namespace AppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SanPhamChiTietController : ControllerBase
    {
        private readonly IProductDetailRepository _productDetailRepo;
        private readonly ApplicationDbContext _context;
        private readonly SPCTViewModel _spctviewmodel;
        public SanPhamChiTietController(ApplicationDbContext context)
        {
            _productDetailRepo = new ProductDetailRepository(context);
            _context = context;
            _spctviewmodel = new SPCTViewModel();
        }
        [HttpGet("[action]")]
        public List<SanPhamChiTiet> DanhSach()
        {
            return _productDetailRepo.GetAll(); ;
        }
        [HttpGet("[action]")]
        public List<SanPhamChiTietViewModel> DanhSachSanPhamHoanThien()
        {
            return _spctviewmodel.DanhSachSanPhamHoanThien();
        }
        [HttpGet("[action]")]
        public List<SanPhamChiTietViewModel> DanhSachSanPhamHoanThienTheoTenTheLoaiHang()
        {
            return _spctviewmodel.DanhSachSanPhamHoanThienTheoTenTheLoaiHang();
        }
        [HttpGet("[action]")]
        public SanPhamChiTietViewModel SanPhamHoanThienByID(Guid id)
        {
            return _spctviewmodel.SanPhamHoanThienTheoId(id);
        }
        [HttpGet("[action]")]
        public SanPhamChiTietViewModel SanPhamHoanThienTheotenmausize(string ten, string mau, string size, string loaisp, string tenth)
        {
            return _spctviewmodel.SanPhamHoanThienTheotenmausize(ten, mau, size, loaisp, tenth);
        }


        [HttpGet("[action]")]
        public List<SanPhamChiTietViewModel> DanhSachSanPhamHoanThienTheoMau(string Tensp, string MauSac, string loaisp, string tenth)
        {
            return _spctviewmodel.DanhSachSanPhamHoanThienTheoMau(Tensp, MauSac, loaisp, tenth);
        }

        [HttpGet("[action]")]
        public List<SanPhamChiTietViewModel> DanhSachSanPhamHoanThienTheoSize(string tensp, string size, string loaisp, string tenth)
        {
            return _spctviewmodel.DanhSachSanPhamHoanThienTheoSize(tensp, size, loaisp, tenth);
        }

        [HttpGet("[action]")]
        public List<SanPhamChiTietViewModel> DanhSachSanPhamHoanThienTheoTen(string tensp)
        {
            return _spctviewmodel.DanhSachSanPhamHoanThienTheoTen(tensp);
        }
        [HttpGet("[action]")]
        public List<SanPhamChiTietViewModel> LayDanhSachSanPhamHoanThienTheoTen(string tensp, string thuonghieu, string loaisp)
        {
            return _spctviewmodel.LayDanhSachSanPhamHoanThienTheoTen(tensp, thuonghieu, loaisp);
        }
        [HttpGet("[action]")]
        public List<SanPhamChiTietViewModel> LocSanPhamTheoMauSizeThuongHieuLoaiSanPham(string? mau, string? size, string? thuonghieu, string? loaisanpham)
        {
            return _spctviewmodel.LocSanPhamTheoMauSizeThuongHieuLoaiSanPham(mau, size, thuonghieu, loaisanpham);
        }
        [HttpGet("[action]")]
        public List<SanPhamChiTietViewModel> DanhSachTheLoaiSanPham()
        {
            return _spctviewmodel.DanhSachTheLoaiSanPham();
        }
        [HttpGet("[action]")]
        public List<SanPhamChiTietViewModel> LocTheoLoaiSanPham(string loaisanpham)
        {
            return _spctviewmodel.LocTheoLoaiSanPham(loaisanpham);
        }
        [HttpGet("[action]")]
        public List<SanPhamChiTietViewModel> DanhSachThuongHieu()
        {
            return _spctviewmodel.DanhSachThuongHieu();
        }
        [HttpGet("[action]")]
        public List<SanPhamChiTietViewModel> LocTheoThuongHieu(string thuonghieu)
        {
            return _spctviewmodel.LocTheoThuongHieu(thuonghieu);
        }
        [HttpGet("[action]")]
        public List<SanPhamChiTietViewModel> DanhSachGiaCuaSanPham()
        {
            return _spctviewmodel.DanhSachGiaCuaSanPham();
        }
        [HttpGet("[action]")]
        public List<SanPhamChiTietViewModel> LocTheoGia(decimal giamin, decimal giamax)
        {
            return _spctviewmodel.LocTheoGia(giamin, giamax);
        }
        [HttpGet("[action]")]
        public List<SanPhamChiTietViewModel> DanhSachSize()
        {
            return _spctviewmodel.DanhSachSize();
        }
        [HttpGet("[action]")]
        public List<SanPhamChiTietViewModel> LocTheoSize(string size)
        {
            return _spctviewmodel.LoctheoSize(size);
        }
        [HttpGet("GetProductDetails")]
        public IActionResult GetProductDetails()
        {
            try
            {
                var productDetails = _productDetailRepo.GetProductDetails();
                return Ok(productDetails);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpPut("[action]/{id}")]
        public async Task<ActionResult<bool>> Update(SanPhamChiTiet spct)
        {
            var spctobject = await _productDetailRepo.GetId(spct.ID);
            if (spctobject != null)
            {
                return await _productDetailRepo.Update(spct);
            }
            else
            {
                return false;

            }
        }

        [HttpPost("[action]")]
        public bool Create(SanPhamChiTiet sanPham)
        {

            return _productDetailRepo.Create(sanPham);
        }
        [HttpPut("[action]/{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            if (await _productDetailRepo.Delete(id))
            {
                return Ok("Xóa thành công");
            }
            else return BadRequest("Xóa thất bại");
        }
        [HttpPut("[action]")]
        public  bool ConHang(Guid id)
        {
            return _productDetailRepo.ConHang(id);
        }

        [HttpGet("id")]
        public async Task<ActionResult> GetById(Guid id)
        {
            var hd = await _productDetailRepo.GetId(id);
            return Ok(hd);
        }
        [HttpPost("[action]")]
        public string ThemMoiSanPhamChiTiet(Guid idsp, Guid idsize, Guid idmausac, Guid idthuonghieu, string masp, string loaisanpham, int soluong, decimal giaban, string mota)
        {
            SanPhamChiTiet spct = new SanPhamChiTiet()
            {
                ID = Guid.NewGuid(),
                IDSP = idsp,
                IDMauSac = idmausac,
                IDThuongHieu = idthuonghieu,
                IDSize = idsize,
                MaSPCT = masp,
                SoLuong = soluong,
                GiaBan = giaban,
                LoaiSanPham = loaisanpham,
                MoTa = mota,
                TrangThai = 1
            };
            return _productDetailRepo.ThemSanPham(spct);
        }
        [HttpPost("[action]")]
        public string ChinhSuaSanPham(Guid id, Guid idsp, Guid idsize, Guid idmausac, Guid idthuonghieu, string masp, string loaisanpham, int soluong, decimal giaban, string mota, int trangthai)
        {
            SanPhamChiTiet spct = _productDetailRepo.GetAll().FirstOrDefault(c => c.ID == id);
            if (spct == null)
            {
                return "Không có sản phẩm";
            }
            else
            {
                spct.IDSP = idsp;
                spct.IDMauSac = idmausac;
                spct.IDThuongHieu = idthuonghieu;
                spct.IDSize = idsize;
                spct.MaSPCT = masp;
                spct.SoLuong = soluong;
                spct.GiaBan = giaban;
                spct.LoaiSanPham = loaisanpham;
                spct.MoTa = mota;
                spct.TrangThai = trangthai;

                return _productDetailRepo.CapNhatSanPham(spct);
            }
        }
        [HttpDelete("[action]/{id}")]
        public bool XoaCung(Guid id)
        {
           return _productDetailRepo.XoaCung(id);
        }
    }
}
