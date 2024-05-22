using AppData.DB_Context;
using AppData.Entities.Models;
using AppData.Entities.ViewModels;
using AppData.Repositories.BillDetails;
using AppData.Repositories.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace AppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HoaDonCTController : Controller
    {
        private readonly IBillDetailRepository _billDtRepo;
        private readonly SPCTViewModel _viewModel;
        private readonly HoaDonChiTietVm hoaDonChiTietVm;
        private readonly SaleChiTietVm saleChiTietVm;
        public HoaDonCTController(ApplicationDbContext dbContext)
        {
            _billDtRepo = new BillDetailRepository(dbContext);
            _viewModel = new SPCTViewModel();
            saleChiTietVm = new SaleChiTietVm();
            hoaDonChiTietVm = new HoaDonChiTietVm(dbContext);
        }

        [HttpGet("[action]")]
        public async Task<ActionResult> Index()
        {
            var lst = _billDtRepo.GetAll();
            return Ok(lst);
        }
        [HttpGet("[action]")]
        public List<HoaDonChiTietViewModel> DanhSachHDChiTietTheoIDhd(Guid idhd)
        {
            return hoaDonChiTietVm.DanhSachHoaDonTheoIDHD(idhd);
        }
        [HttpPost("[action]")]
        public string Create(Guid idhd, Guid idspct, int soluong)
        {
            try
            {
                var sanpham = _viewModel.DanhSachSanPhamHoanThien().FirstOrDefault(c => c.ID == idspct);
                if (sanpham != null)
                {
                    if (soluong > sanpham.SoLuong)
                    {
                        return "Số lượng sản phẩm hiện tại còn " + sanpham.SoLuong;
                    }
                    else if (soluong <= 0)
                    {
                        return "Yêu cầu ít nhất 1 sản phẩm.";
                    }
                    else
                    {
                        var lsthdct = hoaDonChiTietVm.DanhSachHoaDonTheoIDHD(idhd);
                        if (lsthdct.Any(c => c.IDSPCT == idspct) == true)
                        {
                            var hdct = _billDtRepo.GetByIdBill(idhd).FirstOrDefault(c => c.IDSPCT == idspct);
                            if ((soluong + hdct.SoLuong) > 0 && (soluong + hdct.SoLuong) > sanpham.SoLuong)
                            {
                                return "Tổng số lượng trong hoá đơn đã vượt quá số lượng tồn";
                            }
                            else
                            {
                                hdct.SoLuong = soluong + hdct.SoLuong;
                            }
                            _billDtRepo.Update(hdct, hdct.ID);
                            return "Thêm thành công";
                        }
                        else
                        {
                            HoaDonChiTiet hdCT = new HoaDonChiTiet();
                            hdCT.ID = Guid.NewGuid();
                            hdCT.IDHoaDon = idhd;
                            hdCT.IDSPCT = idspct;
                            hdCT.IDSaleCT = null;
                            hdCT.SoLuong = soluong;
                            hdCT.Gia = sanpham.GiaBan;
                            _billDtRepo.Create(hdCT);
                            return "Thêm Thành Công.";
                        }
                    }
                }
                else
                {
                    var sanphamsale = saleChiTietVm.DanhSachSanPhamSale().FirstOrDefault(c => c.IDSalechitiet == idspct);
                    if (soluong > sanphamsale.SoLuong)
                    {
                        return "Số lượng sản phẩm hiện tại còn " + sanphamsale.SoLuong;
                    }
                    else if (soluong <= 0)
                    {
                        return "Yêu cầu ít nhất 1 sản phẩm.";
                    }
                    else
                    {
                        var lsthdct = hoaDonChiTietVm.DanhSachHoaDonTheoIDHD(idhd);
                        if (lsthdct.Any(c => c.IDSaleCT == idspct) == true)
                        {
                            var hdct = _billDtRepo.GetByIdBill(idhd).FirstOrDefault(c => c.IDSaleCT == idspct);
                            if ((soluong + hdct.SoLuong) > 0 && (soluong + hdct.SoLuong) > sanphamsale.SoLuong)
                            {
                                return "Tổng số lượng trong hoá đơn đã vượt quá số lượng tồn";
                            }
                            else
                            {
                                hdct.SoLuong = soluong + hdct.SoLuong;
                            }
                            _billDtRepo.Update(hdct, hdct.ID);
                            return "Thêm thành công";
                        }
                        else
                        {
                            HoaDonChiTiet hdCT = new HoaDonChiTiet();
                            hdCT.ID = Guid.NewGuid();
                            hdCT.IDHoaDon = idhd;
                            hdCT.IDSPCT = null;
                            hdCT.IDSaleCT = idspct;
                            hdCT.SoLuong = soluong;
                            hdCT.Gia = sanphamsale.GiaGiam;
                            _billDtRepo.Create(hdCT);
                            return "Thêm Thành Công.";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return "Lỗi: " + ex.Message;
            }

        }

        [HttpPut("id")]
        public async Task<ActionResult> Update(HoaDonChiTiet hdCT)
        {
            if (hdCT != null)
            {
                if (await _billDtRepo.Update(hdCT, hdCT.ID))
                {
                    return Ok("Sửa Thành Công");
                }
                return BadRequest("Sửa không thành công");
            }
            else { return BadRequest("Sửa không thành công"); }
        }

        [HttpPut("[action]")]
        public string CapNhatSoLuongHDCT(Guid IDhdct, int soluong)
        {
            var hdct = _billDtRepo.GetAll().FirstOrDefault(c => c.ID == IDhdct);
            int soluongton = 0 ;
            if (hdct.IDSaleCT == null)
            {
                var spct = _viewModel.DanhSachSanPhamHoanThien().FirstOrDefault(c => c.ID == hdct.IDSPCT);
                soluongton = spct.SoLuong;

            } else
            {
                var spct = saleChiTietVm.DanhSachSanPhamSale().FirstOrDefault(c => c.IDSalechitiet == hdct.IDSaleCT);
                soluongton = spct.SoLuong;
            }
            if (soluong > soluongton)
            {
                return "Số lượng sản phẩm hiện tại còn " + soluongton;
            }
            else if (soluong <= 0)
            {
                return "Yêu cầu ít nhất 1 sản phẩm.";
            }
            else
            {
                if (hdct != null)
                {
                    hdct.SoLuong = soluong;
                    if (_billDtRepo.Update(hdct, hdct.ID).Result == true)
                    {
                        return "Cập nhật số lượng thành công";
                    }
                    else
                    {
                        return "Sửa thất bại.";
                    }
                }
                else
                {
                    return "Sửa không thành công";
                }
            }

        }

        [HttpDelete("id")]
        public async Task<ActionResult> Delete(Guid id)
        {
            if (await _billDtRepo.Delete(id))
            {
                return Ok("Xóa Thành Công.");
            }
            else { return BadRequest("Xóa Thất Bại."); }
        }

        [HttpGet("id")]
        public async Task<ActionResult> GetId(Guid id)
        {
            var gId = await _billDtRepo.GetId(id);
            return Ok(gId);
        }
        [HttpGet("[action]")]
        public List<HoaDonChiTiet> DanhSachHoaDonChiTietTheoIDHoaDon(Guid idHoaDon)
        {
            return _billDtRepo.GetByIdBill(idHoaDon);
        }
    }
}
