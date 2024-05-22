using AppData.DB_Context;
using AppData.Entities.Models;
using AppData.Repositories.BillDetails;
using AppData.Repositories.Bills;
using AppData.Repositories.CartDetail;
using AppData.Repositories.HinhThucThanhToan;
using AppData.Repositories.ProductDetail;
using AppData.Repositories.SaleDetail;
using AppData.Repositories.VoucherDetails;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DatHangController : ControllerBase
    {
        private readonly IBillRepository _ibill;
        private readonly IBillDetailRepository _ibillDetail;
        private readonly IVoucherDetailRepository _ivoucherdetail;
        private readonly ICartDetailRepository cartDetailRepository;
        private readonly ApplicationDbContext _context;
        private readonly IHinhThucThanhToanRes hinhThucThanhToanRes;
        private readonly IProductDetailRepository _spct;
        private readonly SaleDetailRepository _salechitiet;
        public DatHangController(ApplicationDbContext context)
        {
            _context = context;
            _ibill = new BillRepository(context);
            _ibillDetail = new BillDetailRepository(context);
            hinhThucThanhToanRes = new HinhThucThanhToanRes();
            cartDetailRepository = new CartDetailRepository(context);
            _ivoucherdetail = new VoucherDetailRepository(context);
            _spct = new ProductDetailRepository(_context);
            _salechitiet = new SaleDetailRepository(_context);

        }
        // GET: api/<DatHangController>
        [HttpPost("[action]")]
        public string DatHangOnline(Guid IdNguoiDung, Guid? idvoucher, Guid? hinhthucthanhtoan, string tenkhachang, string sdtkh, string diachi, int tongsoluong, decimal thanhtien, decimal tienship, decimal tiengiamgia, DateTime NgayThanhToan, DateTime ngaynhanhang, string? GhiChu)
        {
            try
            {
                var tenhinhthucthanhtoan = hinhThucThanhToanRes.DanhSachThanhToan().FirstOrDefault(c => c.ID == hinhthucthanhtoan).HinhThucThanhToan;
                HoaDon hoaDon = new HoaDon();
                hoaDon.ID = Guid.NewGuid();
                hoaDon.IDNguoiDung = IdNguoiDung;
                hoaDon.IDVoucherChiTiet = idvoucher == null ? null : _ivoucherdetail.GetAllVoucherDetail().FirstOrDefault(c => c.ID == idvoucher).ID;
                hoaDon.IDHinhThucThanhToan = hinhthucthanhtoan == null ? null : hinhThucThanhToanRes.DanhSachThanhToan().FirstOrDefault(c => c.ID == hinhthucthanhtoan).ID;
                hoaDon.MaHoaDon = Convert.ToString(hoaDon.ID).Substring(0, 8).ToUpper();
                hoaDon.NgayTao = DateTime.Now;
                hoaDon.TenKhachHang = tenkhachang;
                hoaDon.SDTKhachHang = sdtkh;
                hoaDon.DiaChi = diachi;
                hoaDon.TongSoLuong = tongsoluong;
                hoaDon.ThanhTien = thanhtien;
                hoaDon.TienShip = tienship;
                hoaDon.TienGiamGia = tiengiamgia;
                hoaDon.TongTien = hoaDon.ThanhTien + hoaDon.TienShip - hoaDon.TienGiamGia;
                hoaDon.NgayThanhToan = NgayThanhToan;
                hoaDon.NgayNhanHang = ngaynhanhang;
                hoaDon.GhiChu = GhiChu == null ? null : GhiChu;
                if (tenhinhthucthanhtoan == "Thanh toán online")
                {
                    hoaDon.TrangThai = 3; // Đã thanh toán chưa giao hàng
                }
                else
                {
                    hoaDon.TrangThai = 1; // đang xử lý
                }

                _ibill.Ceate(hoaDon);
               
                var soluong = cartDetailRepository.GetCartDetailByIdGioHang(IdNguoiDung).Count();
                foreach (var cartdetail in cartDetailRepository.GetCartDetailByIdGioHang(IdNguoiDung))
                {
                    HoaDonChiTiet billDetail = new HoaDonChiTiet();
                    billDetail.IDHoaDon = hoaDon.ID;
                    billDetail.ID = Guid.NewGuid();
                    billDetail.Gia = cartdetail.Gia;
                    billDetail.SoLuong = cartdetail.SoLuong;
                    if (cartdetail.IDSaleCT == null)
                    {
                        billDetail.IDSaleCT = null;
                        billDetail.IDSPCT = cartdetail.IDSPCT;
                        SanPhamChiTiet spct = _spct.GetAll().FirstOrDefault(c => c.ID == cartdetail.IDSPCT);
                        spct.SoLuong = spct.SoLuong - cartdetail.SoLuong;
                        if (spct.SoLuong == 0)
                        {
                            spct.TrangThai = 0;
                        }
                        _spct.CapNhatSanPham(spct);
                    }
                    else if (cartdetail.IDSPCT == null)
                    {
                        billDetail.IDSaleCT = cartdetail.IDSaleCT;
                        billDetail.IDSPCT = null;
                        SaleChiTiet salechitiet = _salechitiet.GetAllSaleDetail().FirstOrDefault(c => c.ID == cartdetail.IDSaleCT);
                        salechitiet.SoLuong = salechitiet.SoLuong - cartdetail.SoLuong;
                        if (salechitiet.SoLuong == 0)
                        {
                            salechitiet.TrangThai = 0;
                        }
                        _salechitiet.UpdateSaleDetail(salechitiet);
                    }
                    _ibillDetail.Create(billDetail);
                    cartDetailRepository.DeleteCartDetail(cartdetail.ID);
                }
                var hoadonchitiet = _ibillDetail.GetByIdBill(hoaDon.ID);
                HoaDon bill = _ibill.GetAll().FirstOrDefault(c => c.ID == hoaDon.ID);
                bill.TongSoLuong = hoadonchitiet.Sum(c => c.SoLuong);
                bill.ThanhTien = hoadonchitiet.Sum(c => c.SoLuong * c.Gia);
                bill.TongTien = bill.ThanhTien + hoaDon.TienShip - hoaDon.TienGiamGia;
                _ibill.Update(bill, bill.ID);
                return "Đơn hàng đang chờ xử lý";
            }
            catch (Exception ex)
            {
                return "Lỗi: " + ex.Message;
            }
        }
        [HttpGet("[action]")]
        public List<HoaDon> DanhSachHoaDonCho()
        {
            return _ibill.GetAll().Where(c => c.TrangThai == 0).OrderByDescending(c => c.NgayTao).ToList();
        }
        [HttpGet("[action]")]
        public List<HoaDon> DanhSachHoaDonChoTheoNgayHomNay()
        {
            return _ibill.GetAll().Where(c => c.TrangThai == 0 && c.NgayTao.Date == DateTime.Now.Date).OrderByDescending(c => c.NgayTao).ToList();
        }

        [HttpPost("[action]")]
        public string TaoHoaDonTaiQuay(HoaDon hd)
        {
            try
            {
                if (hd == null)
                {
                    return "Tạo hóa đơn thất bại";
                }
                else
                {
                    //NguoiDung nguoidung = 
                    //Check ID nguoiDung

                    HoaDon hoaDon = new HoaDon();

                    hoaDon.ID = Guid.NewGuid();
                    hoaDon.IDNguoiDung = hd.IDNguoiDung;
                    hoaDon.IDVoucherChiTiet = _ivoucherdetail.GetAllVoucherDetail().FirstOrDefault(c => c.ID == hd.IDVoucherChiTiet).ID == hd.IDVoucherChiTiet ? hd.IDVoucherChiTiet : null;
                    hoaDon.MaHoaDon = Convert.ToString(hoaDon.ID).Substring(0, 8).ToUpper();
                    hoaDon.NgayTao = DateTime.Now;
                    hoaDon.TenKhachHang = hd.TenKhachHang == null ? "" : hd.TenKhachHang;
                    hoaDon.SDTKhachHang = hd.SDTKhachHang == null ? "" : hd.SDTKhachHang;
                    hoaDon.DiaChi = hd.DiaChi == null ? "" : hd.DiaChi;
                    hoaDon.TongSoLuong = 0;
                    hoaDon.ThanhTien = 0;
                    hoaDon.TienShip = 0;
                    hoaDon.TienGiamGia = 0;
                    hoaDon.TongTien = 0;

                    hoaDon.NgayThanhToan = hd.NgayThanhToan == null ? null : hd.NgayThanhToan;
                    hoaDon.NgayNhanHang = hd.NgayNhanHang == null ? null : hd.NgayNhanHang;
                    hoaDon.GhiChu = hd.GhiChu == null ? null : hd.GhiChu;
                    hoaDon.TrangThai = 2; // HoaDonCho
                    _ibill.Ceate(hoaDon);
                    return "Đơn hàng đang chờ xử lý";
                }
            }
            catch (Exception ex)
            {
                return "Lỗi: " + ex.Message;
            }
        }

        [HttpPost("[action]")]
        public string ThemSanPhamVaoHoaDonChiTietTaiQuay(HoaDonChiTiet hdct)
        {
            try
            {
                if (_ibillDetail.GetAll().Any(c => c.IDHoaDon == hdct.IDHoaDon))
                {
                    if (hdct.IDSPCT == null)
                    {
                        HoaDonChiTiet hoaDonChiTiet = new HoaDonChiTiet()
                        {
                            ID = Guid.NewGuid(),
                            IDHoaDon = hdct.IDHoaDon,
                            IDSaleCT = hdct.IDSaleCT,
                            IDSPCT = null,
                            SoLuong = hdct.SoLuong,
                            Gia = hdct.Gia,
                        };
                        _ibillDetail.Create(hoaDonChiTiet);
                        return "Thêm thành công.";
                    }
                    else
                    {
                        HoaDonChiTiet hoaDonChiTiet = new HoaDonChiTiet()
                        {
                            ID = Guid.NewGuid(),
                            IDHoaDon = hdct.IDHoaDon,
                            IDSaleCT = null,
                            IDSPCT = hdct.IDSPCT,
                            SoLuong = hdct.SoLuong,
                            Gia = hdct.Gia,
                        };
                        _ibillDetail.Create(hoaDonChiTiet);
                        return "Thêm thành công.";
                    }

                }
                {
                    return "Yêu cầu bạn tạo hóa đơn.";
                }
            }
            catch (Exception ex)
            {
                return "Lỗi: " + ex.Message;
            }

        }
        [HttpPut("[action]")]
        public string CapNhatSoLuongSanPhamVaoHoaDonChiTietTaiQuay(HoaDonChiTiet hdct, int soluong)
        {
            try
            {
                if (_ibillDetail.GetAll().Any(c => c.IDHoaDon == hdct.IDHoaDon))
                {
                    HoaDonChiTiet hoaDonChiTiet = _ibillDetail.GetId(hdct.ID).Result;
                    hoaDonChiTiet.SoLuong = soluong;
                    _ibillDetail.Update(hoaDonChiTiet, hoaDonChiTiet.ID);
                    return "Cập nhật số lượng thành công.";
                }
                else
                {
                    return "Yêu cầu bạn tạo hóa đơn.";
                }
            }
            catch (Exception ex)
            {
                return "Lỗi: " + ex.Message;
            }
        }

        [HttpPost("[action]")]
        public string CapNhatHoaDonTaiQuay(HoaDon hd)
        {
            try
            {
                if (hd == null)
                {
                    return "Cập nhật hóa đơn thất bại";
                }
                else
                {
                    HoaDon hoaDon = _ibill.GetAll().FirstOrDefault(c => c.ID == hd.ID);
                    hoaDon.IDVoucherChiTiet = _ivoucherdetail.GetAllVoucherDetail().FirstOrDefault(c => c.ID == hd.IDVoucherChiTiet).ID == hd.IDVoucherChiTiet ? hd.IDVoucherChiTiet : null;
                    hoaDon.MaHoaDon = Convert.ToString(hoaDon.ID).Substring(0, 8).ToUpper();
                    hoaDon.NgayTao = DateTime.Now;
                    hoaDon.TenKhachHang = hd.TenKhachHang == null ? "" : hd.TenKhachHang;
                    hoaDon.SDTKhachHang = hd.SDTKhachHang == null ? "" : hd.SDTKhachHang;
                    hoaDon.DiaChi = hd.DiaChi == null ? "" : hd.DiaChi;
                    hoaDon.TongSoLuong = 0;
                    hoaDon.ThanhTien = 0;
                    hoaDon.TienShip = 0;
                    hoaDon.TienGiamGia = 0;
                    hoaDon.TongTien = 0;

                    hoaDon.NgayThanhToan = hd.NgayThanhToan == null ? null : hd.NgayThanhToan;
                    hoaDon.NgayNhanHang = hd.NgayNhanHang == null ? null : hd.NgayNhanHang;
                    hoaDon.GhiChu = hd.GhiChu == null ? null : hd.GhiChu;
                    hoaDon.TrangThai = 0; // HoaDonCho
                    _ibill.Ceate(hoaDon);
                    return "Cập nhật thành công";
                }
            }
            catch (Exception ex)
            {
                return "Lỗi: " + ex.Message;
            }
        }

        [HttpPut("[action]")]
        public string ThanhToanHoaDonTaiQuay(Guid id , Guid idNguoiDung , Guid idthanhtoan)
        {
            try
            {
                if (id == null)
                {
                    return "Không có hóa đơn.";
                }
                else
                {
                    var hd = _ibill.GetAll().FirstOrDefault(c => c.ID == id);
                    var hoaDonChiTiet = _ibillDetail.GetByIdBill(hd.ID);
                    foreach(var item in hoaDonChiTiet)
                    {
                        var sanphamchitiet = _spct.GetAll().FirstOrDefault(c => c.ID == item.IDSPCT);
                        if(sanphamchitiet != null)
                        {
                            sanphamchitiet.SoLuong = sanphamchitiet.SoLuong - item.SoLuong;
                            if (sanphamchitiet.SoLuong == 0)
                            {
                                sanphamchitiet.TrangThai = 0;
                            }
                            _spct.CapNhatSanPham(sanphamchitiet);
                        } else
                        {
                            var sanphamsale = _salechitiet.GetAllSaleDetail().FirstOrDefault(c => c.ID == item.IDSaleCT);
                            sanphamsale.SoLuong = sanphamsale.SoLuong - item.SoLuong;
                            if(sanphamsale.SoLuong == 0)
                            {
                                sanphamsale.TrangThai = 0;
                            }
                            _salechitiet.UpdateSaleDetail(sanphamsale);
                        }
                    }
                    hd.NgayThanhToan = DateTime.Now;
                    hd.IDHinhThucThanhToan = idthanhtoan;
                    hd.IDNguoiDung = idNguoiDung;
                    hd.NgayNhanHang = DateTime.Now;
                    hd.TrangThai = 4;
                    _ibill.Update(hd, id);
                    return "Thanh toán thành công";
                }
            }
            catch (Exception ex)
            {
                return "Lỗi: " + ex.Message;
            }
        }
    }
}
