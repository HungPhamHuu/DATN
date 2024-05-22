using AppData.DB_Context;
using AppData.Entities.Models;
using AppData.Entities.ViewModels;
using AppData.Repositories.ProductDetail;
using AppData.Repositories.SaleDetail;
using AppData.Repositories.SaleRes;
using AppData.Repositories.ViewModel;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleChiTietController : ControllerBase
    {
        // GET: api/<SaleChiTietController>
        private readonly ISaleRepository _saleRepository;
        private readonly ISaleDetailRepository _saleDetailRepository;
        private readonly SaleChiTietVm _saleChiTietVm;
        private readonly SPCTViewModel _spctViewModel;
        private readonly ApplicationDbContext _context;
        private readonly IProductDetailRepository _spct;

        public SaleChiTietController(ApplicationDbContext applicationDbContext)
        {
            _saleRepository = new SaleRepository(applicationDbContext);
            _saleChiTietVm = new SaleChiTietVm();
            _spct = new ProductDetailRepository(applicationDbContext);
            _spctViewModel = new SPCTViewModel();
            _saleDetailRepository = new SaleDetailRepository(applicationDbContext);
        }
        [HttpGet("[action]")]
        public List<SaleChiTietViewModel> DanhSach()
        {
            return _saleChiTietVm.DanhSachSanPhamSale();
        }
        [HttpGet("[action]")]
        public List<SaleChiTietViewModel> DanhSachKh()
        {
            return _saleChiTietVm.DanhSachSanPhamSale().Where(c => c.TrangThai == 1).ToList();
        }
        [HttpGet("[action]")]
        public SaleChiTietViewModel SanphamChiTiettheoID(Guid id)
        {
            return _saleChiTietVm.SanPhamSaleTheoID(id);
        }
        [HttpGet("[action]")]
        public SaleChiTiet SaleChiTiettheoID(Guid id)
        {
            return _saleDetailRepository.GetSaleDetailById(id);
        }
        [HttpGet("[action]")]
        public List<SaleChiTietViewModel> SanPhamSaleTheoTenSPAdmin(string tensp)
        {
            return _saleChiTietVm.SanPhamSaleTheoTenSP(tensp);
        }

        [HttpGet("[action]")]
        public List<SaleChiTietViewModel> SanPhamSaleTheoTenSPKH(string tensp)
        {
            try
            {
                List<SaleChiTietViewModel> lst = _saleChiTietVm.SanPhamSaleTheoTenSP(tensp).Where(c => c.TrangThai == 1).ToList();
                if (lst.Count() == 0)
                {
                    return null;
                }
                else
                {
                    return lst;
                }
            }
            catch
            {
                return null;
            }


        }

        [HttpGet("[action]")]
        public SaleChiTietViewModel SanPhamSaleTheoTenSPMauSizeThuongHieuTheLoai(string tensp, string mau, string size, string thuonghieu, string theloai)
        {
            return _saleChiTietVm.SanPhamSaleTheoTenSPMauSizeThuongHieuTheLoai(tensp, mau, size, thuonghieu, theloai);
        }
        [HttpGet("[action]")]
        public List<SaleChiTietViewModel> SanPhamSaleTheoTenSpThuongHieuTheLoai(string tensp, string thuonghieu, string theloai)
        {
            return _saleChiTietVm.SanPhamSaleTheoTenSpThuongHieuTheLoai(tensp, thuonghieu, theloai);
        }
        [HttpGet("[action]")]
        public List<SaleChiTietViewModel> SanPhamSaleTruyVanMau(string tensp, string size, string thuonghieu, string theloai)
        {
            return _saleChiTietVm.SanPhamSaleTruyVanMau(tensp, size, thuonghieu, theloai);
        }
        [HttpGet("[action]")]
        public List<SaleChiTietViewModel> SanPhamSaleTruyVanSize(string tensp, string mau, string thuonghieu, string theloai)
        {
            return _saleChiTietVm.SanPhamSaleTruyVanSize(tensp, mau, thuonghieu, theloai);
        }
        [HttpGet("[action]")]
        public List<SaleChiTietViewModel> LocSanPhamSaleTheoMauSizeThuongHieuLoaiSanPhamAdmin(string? mau, string? size, string? thuonghieu, string? loaisanpham)
        {
            return _saleChiTietVm.LocSanPhamSaleTheoMauSizeThuongHieuLoaiSanPham(mau, size, thuonghieu, loaisanpham);
        }
        [HttpGet("[action]")]
        public List<SaleChiTietViewModel> LocSanPhamSaleTheoMauSizeThuongHieuLoaiSanPhamKH(string? mau, string? size, string? thuonghieu, string? loaisanpham)
        {
            return _saleChiTietVm.LocSanPhamSaleTheoMauSizeThuongHieuLoaiSanPham(mau, size, thuonghieu, loaisanpham).Where(c => c.TrangThai == 1).ToList();
        }
        [HttpGet("[action]")]
        public List<SaleChiTietViewModel> LocTheoNgay(DateTime ngaybatdau, DateTime ngayketthuc)
        {
            return _saleChiTietVm.LocTheoNgay(ngaybatdau, ngayketthuc);
        }
        [HttpGet("[action]")]
        public List<SaleChiTietViewModel> LocTheoGia(decimal giamax)
        {
            return _saleChiTietVm.LocTheoGia(giamax).Where(c => c.TrangThai == 1).ToList();
        }

        [HttpPost("[action]")]
        public string ThemSaleChiTiet(Guid idsale, Guid IDSPCT, int soluong)
        {
            try
            {
                SaleChiTiet salechitiet = new SaleChiTiet()
                {
                    ID = Guid.NewGuid(),
                    IDSale = idsale,
                    IDSPCT = IDSPCT,
                    SoLuong = soluong,
                    TrangThai = 1
                };
                return _saleDetailRepository.CreateSaleDetail(salechitiet);
            }
            catch (Exception ex)
            {
                return "Thất bại.";
            }
        }

        [HttpPut("[action]")]
        public string CapNhatSaleChiTiet(Guid id, Guid IDSPCT, int soluong, int trangthai)
        {
            try
            {
                SaleChiTiet salect = _saleDetailRepository.GetAllSaleDetail().FirstOrDefault(c => c.ID == id);
                if (salect == null)
                {
                    return "Thất bại.";
                }
                else
                {
                    SanPhamChiTiet spct = _spct.GetId(IDSPCT).Result;
                    if (soluong > spct.SoLuong)
                    {
                        return "Số lượng vượt quá so với sản phẩm tồn";
                    }
                    else if (soluong <= spct.SoLuong && soluong > 0)
                    {
                        spct.SoLuong = spct.SoLuong - soluong;
                        if (spct.SoLuong == 0)
                        {
                            spct.TrangThai = 0;
                        }
                        _spct.CapNhatSanPham(spct);

                        salect.IDSPCT = IDSPCT;
                        salect.SoLuong = soluong + salect.SoLuong;
                        salect.TrangThai = trangthai;

                        return _saleDetailRepository.UpdateSaleDetail(salect);
                    }
                    else
                    {
                        return "Số lượng phải lớn hơn 1";
                    }
                }
            }
            catch (Exception ex)
            {
                return "Lỗi.";
            }
        }
        [HttpPut("[action]")]
        public string HetSale(Guid id)
        {
            try
            {
                SaleChiTiet salect = _saleDetailRepository.GetAllSaleDetail().FirstOrDefault(c => c.ID == id);
                if (salect == null)
                {
                    return "Thất bại.";
                }
                else
                {
                    return _saleDetailRepository.DeleteSaleDetail(id);
                }

            }
            catch (Exception ex)
            {
                return "Thất bại.";
            }
        }
        [HttpPut("[action]")]
        public string BatDauSale(Guid id)
        {
            try
            {
                SaleChiTiet salect = _saleDetailRepository.GetAllSaleDetail().FirstOrDefault(c => c.ID == id);
                if (salect == null)
                {
                    return "Thất bại.";
                }
                else
                {
                    return _saleDetailRepository.BatDauSale(id);
                }

            }
            catch (Exception ex)
            {
                return "Thất bại.";
            }
        }
        [HttpDelete("[action]")]
        public string XoaCungSaleChiTiet(Guid id)
        {
            try
            {
                SaleChiTiet salect = _saleDetailRepository.GetAllSaleDetail().FirstOrDefault(c => c.ID == id);
                if (salect == null)
                {
                    return "Thất bại.";
                }
                else
                {
                    return _saleDetailRepository.XoaCung(id);
                }

            }
            catch (Exception ex)
            {
                return "Thất bại.";
            }
        }
        [HttpGet("[action]")]
        public List<SaleChiTietViewModel> SanPhamSaleTheoLoaisanpham(string loaisp)
        {
            try
            {
                List<SaleChiTietViewModel> lst = _saleChiTietVm.SanPhamSaleTheoLoaisanpham(loaisp).Where(c => c.TrangThai == 1).ToList();
                if (lst.Count() == 0)
                {
                    return null;
                }
                else
                {
                    return lst;
                }
            }
            catch
            {
                return null;
            }


        }
        [HttpGet("[action]")]
        public List<SaleChiTietViewModel> SanPhamSaleTheoThuonghieu(string thuonghieu)
        {
            try
            {
                List<SaleChiTietViewModel> lst = _saleChiTietVm.SanPhamSaleTheoThuonghieu(thuonghieu).Where(c => c.TrangThai == 1).ToList();
                if (lst.Count() == 0)
                {
                    return null;
                }
                else
                {
                    return lst;
                }
            }
            catch
            {
                return null;
            }


        }
    }
}
