using AppData.DB_Context;
using AppData.Entities.Models;
using AppData.Entities.ViewModels;
using AppData.Repositories.ViewModel;
using AppData.Repositories.VoucherDetails;
using AppData.Repositories.Vouchers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VoucherController : ControllerBase
    {
        IVoucherRepository<Voucher> _vcRepo;
        private readonly VoucherChiTietVM _voucherChiTietVM;
        ApplicationDbContext _context = new ApplicationDbContext();
        private readonly IVoucherDetailRepository voucherDetailRepository;
        private readonly HoaDonChiTietVm hoaDonChiTietVm;

        public VoucherController(ApplicationDbContext context)
        {
            IVoucherRepository<Voucher> repo = new VoucherRepository<Voucher>(context, context.voucher);
            hoaDonChiTietVm = new HoaDonChiTietVm(context);
            _voucherChiTietVM = new VoucherChiTietVM();
            voucherDetailRepository = new VoucherDetailRepository(context);
            _vcRepo = repo;
        }
        [HttpGet("[action]")]
        public IEnumerable<Voucher> DanhSach()
        {
            try
            {
                return _vcRepo.GetAllVoucher();
            }
            catch
            {
                return null;
            }
            
        }
        [HttpPost("[action]")]
        public bool CreateVC(string ma, DateTime NgayBatDau, DateTime NgayKetThuc, decimal GiaTriVoucher, decimal DKmin, decimal Dkmax, int SoLuong, string moTa, int trangThai)
        {
            try
            {
                if (_vcRepo.GetAllVoucher().Any(c => c.MaVoucher.ToUpper().Trim() == ma.ToLower().Trim()))
                {
                    return false;
                }
                else
                {
                    Voucher voucher = new Voucher();
                    voucher.ID = Guid.NewGuid();
                    voucher.MaVoucher = ma.ToUpper();
                    voucher.NgayTao = DateTime.Now;
                    voucher.NgayBatDau = NgayBatDau;
                    voucher.NgayKetThuc = NgayKetThuc;
                    voucher.GiaTriVoucher = GiaTriVoucher;
                    voucher.DieuKienMin = DKmin;
                    voucher.DieuKienMax = Dkmax;
                    voucher.SoLuong = SoLuong;
                    voucher.MoTa = moTa;
                    voucher.TrangThai = trangThai;
                    return _vcRepo.CreateVoucher(voucher);
                }
            } catch
            {
                return false;
            }
           
        }
        [HttpPut("[action]")]
        public bool UpdateVC(Guid id, DateTime NgayBatDau, DateTime NgayKetThuc, decimal GiaTriVoucher, decimal DieuKienMin, decimal DieuKienMax, int SoLuong, string MoTa, int TrangThai)
        {
            try
            {
                var voucher = _vcRepo.GetAllVoucher().FirstOrDefault(c => c.ID == id);
                voucher.NgayBatDau = NgayBatDau;
                voucher.NgayKetThuc = NgayKetThuc;
                voucher.GiaTriVoucher = GiaTriVoucher;
                voucher.DieuKienMin = DieuKienMin;
                voucher.DieuKienMax = DieuKienMax;
                voucher.SoLuong = SoLuong;
                voucher.MoTa = MoTa;
                voucher.TrangThai = TrangThai;
                return _vcRepo.UpdateVoucher(voucher);
            }
            catch
            {
                return false;
            }
          
        }

        [HttpDelete("[action]")]
        public bool DeleteVC(Guid id)
        {
            try
            {
                var voucher = _context.voucher.First(c => c.ID == id);
                return _vcRepo.DeleteVoucher(voucher);
            }
            catch
            {
                return false;
            }
            
        }

        [HttpGet("getByid-voucher/{id}")]
        public IActionResult GetById(Guid id)
        {
            try
            {
                var voucher = _vcRepo.GetById(id);
                if (voucher == null)
                {
                    return NotFound();
                }
                return Ok(voucher);
            }
            catch
            {
                return null;
            }
        
        }

        [HttpGet("[action]")]
        public List<VoucherChiTiet> DanhSachVoucherChiTiet()
        {
            try
            {
                return voucherDetailRepository.GetAllVoucherDetail();
            }
            catch
            {
                return null;
            }
           
        }
        [HttpGet("[action]")]
        public List<VoucherChiTiet> DanhSachVoucherChiTietTheoIDnguoiDung(Guid idNguoiDung)
        {
            try
            {
                return voucherDetailRepository.GetAllVoucherByIDNguoiDung(idNguoiDung);
            }
            catch
            {
                return null;
            }
            
        }
        [HttpGet("[action]")]
        public VoucherChiTiet VoucherChiTietTheoIDnguoiDungvaIdSale(Guid idNguoiDung , Guid idsale)
        {
            try
            {
                return voucherDetailRepository.GetAllVoucherByIDNguoiDung(idNguoiDung).FirstOrDefault(c => c.IDVoucher == idsale);
            }
            catch
            {
                return null;
            }

        }
        [HttpGet("[action]")]
        public List<HoaDonChiTietViewModel> DanhSachVoucherChiTietTheoIDhd(Guid idhd)
        {
            try
            {
                return hoaDonChiTietVm.DanhSachHoaDonTheoIDHD(idhd);
            }
            catch
            {
                return null;
            }
            
        }
        [HttpPost("[action]")]
        public bool CreateVoucherChiTiet(Guid IDVoucher, Guid IDnguoiDung, int soluong)
        {
            try
            {
                VoucherChiTiet voucherchitiet = new VoucherChiTiet()
                {
                    ID = Guid.NewGuid(),
                    IDVoucher = IDVoucher,
                    IDNguoiDung = IDnguoiDung,
                    SoLuong = soluong,
                    TrangThai = 1,
                };
                var voucher = _vcRepo.GetById(IDVoucher);
                voucher.SoLuong = voucher.SoLuong - 1;
                if (voucher.SoLuong == 0)
                {
                    voucher.SoLuong = 0;
                }
                _vcRepo.UpdateVoucher(voucher);

                return voucherDetailRepository.CreateVoucherDetail(voucherchitiet);
               
            }
            catch
            {
                return false;
            }
        
        }
        [HttpPut("[action]")]
        public bool UpdateVoucherChiTiet(Guid id, int soluong, int trangThai)
        {
            try
            {
                VoucherChiTiet voucherchitiet = voucherDetailRepository.GetAllVoucherDetail().FirstOrDefault(c => c.ID == id);
                voucherchitiet.SoLuong = soluong;
                voucherchitiet.TrangThai = trangThai;
                return voucherDetailRepository.UpdateVoucherDetail(voucherchitiet);
            }
            catch
            {
                return false;
            }
          
        }

        [HttpGet("[action]")]
        public List<VoucherDetailViewModel> DanhSachVoucherVM()
        {
            try
            {
                return _voucherChiTietVM.DanhSachVoucherChiTiet();
            }
            catch
            {
                return null;
            }
            
        }

        [HttpGet("[action]")]
        public List<VoucherDetailViewModel> DanhSachVoucherVMByIdNguoiDung(Guid idnguoidung)
        {
            try
            {
                return _voucherChiTietVM.DanhSachVoucherChiTietTheoIDNguoiDung(idnguoidung);
            }
            catch
            {
                return null;
            }
            
        }

        [HttpGet("[action]")]
        public List<VoucherDetailViewModel> DanhSachVoucherVMByIdNguoiDungChuaSuDung(Guid idnguoidung)
        {
            try
            {
                return _voucherChiTietVM.DanhSachVoucherChiTietTheoIDNguoiDung(idnguoidung).Where(c => c.TrangThai == 1).ToList();
            }
            catch
            {
                return null;
            }
            
        }
        [HttpGet("[action]")]
        public VoucherDetailViewModel VoucherVMTheoID(Guid IDnguoidung, Guid idvoucherdetail)
        {
            try
            {
                return _voucherChiTietVM.DanhSachVoucherChiTietByID(IDnguoidung, idvoucherdetail);
            }
            catch
            {
                return null;
            }
           
        }
        [HttpGet("[action]")]
        public VoucherDetailViewModel DanhSachVMVoucherTheoMa(Guid idnguoidung, string ma)
        {
            try
            {
                return _voucherChiTietVM.DanhSachVoucherChiTietTheoMaVoucher(idnguoidung, ma);
            }
            catch
            {
                return null;
            }
           
        }
    }

}

