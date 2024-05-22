using AppData.DB_Context;
using AppData.Entities.Models;
using AppData.Repositories.BillDetails;
using AppData.Repositories.Bills;
using AppData.Repositories.CartDetail;
using AppData.Repositories.HinhThucThanhToan;
using AppData.Repositories.Roles;
using AppData.Repositories.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using System.Net.Http.Headers;

namespace AppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HoaDonController : ControllerBase
    {
        private readonly IBillRepository _billRepo;
        private readonly IBillDetailRepository billDetailRepository;
        private readonly IUserRepository userRepository;
        public readonly IRoleRepository _iChucvu;
        public readonly IHinhThucThanhToanRes hinhThucThanhToanRes;
        public HoaDonController(ApplicationDbContext dbContext)
        {
            _billRepo = new BillRepository(dbContext);
            userRepository = new UserRepository();
            billDetailRepository = new BillDetailRepository(dbContext);
            _iChucvu = new RoleRepository();
            hinhThucThanhToanRes = new HinhThucThanhToanRes();
        }
        [HttpGet("[action]")]
        public ActionResult Index()
        {
            var listBill = _billRepo.GetAll();
            return Ok(listBill);
        }
        [HttpGet("[action]")]
        public List<HoaDon> GetAllHoaDonTheoIDNguoiDung(Guid idnguoidung)

        {
            return _billRepo.GetAllByIdNguoiDung(idnguoidung);
        }
        [HttpGet("[action]")]
        public List<HoaDon> GetAllHoaDonTrangThaiTheoIDNguoiDung(Guid idnguoidung, int trangthai)

        {
            return _billRepo.GetAllByIdNguoiDung(idnguoidung).Where(c => c.TrangThai == trangthai).ToList();
        }

        [HttpPut("[action]")]
        public string Updatehoadon(Guid idhd)
        {
            try
            {
                var hoadonct = billDetailRepository.GetAll().Where(c => c.IDHoaDon == idhd).ToList();
                var hd = _billRepo.GetAll().FirstOrDefault(c => c.ID == idhd);
                if (hd == null)
                {
                    return "Lỗi.";  
                } else
                {
                    hd.TongSoLuong = hoadonct.Sum(c => c.SoLuong);
                    hd.ThanhTien = hoadonct.Sum(c => c.SoLuong * c.Gia);
                    hd.TongTien = hoadonct.Sum(c => c.SoLuong * c.Gia) + hd.TienShip - hd.TienGiamGia;
                    _billRepo.Update(hd, idhd);
                    if(_billRepo.Update(hd, idhd) == true)
                    {
                        return "Cập nhật thành công.";
                    } else
                    {
                        return "Cập nhật thất bại.";
                    }
                }
            } catch (Exception ex)
            {
                return "Lỗi: " + ex.Message;
            }
        }
        [HttpPut("[action]")]
        public string UpdateThongTinKhachHangTrongHD(Guid idhd , string tenkhach , string diachi , string sdt)
        {
            try
            {
                var hd = _billRepo.GetAll().FirstOrDefault(c => c.ID == idhd);
                if (hd == null)
                {
                    return "Lỗi.";
                }
                else
                {
                    hd.TenKhachHang = tenkhach;
                    hd.DiaChi = diachi;
                    hd.SDTKhachHang = sdt;
                    _billRepo.Update(hd, idhd);
                    if (_billRepo.Update(hd, idhd) == true)
                    {
                        return "Cập nhật thành công.";
                    }
                    else
                    {
                        return "Cập nhật thất bại.";
                    }
                }
            }
            catch (Exception ex)
            {
                return "Lỗi: " + ex.Message;
            }
        }

        [HttpGet("[action]")]
        public HoaDon HoaDonTaoMoiNhat(string sdt)
        {
            return _billRepo.HoaDonTaoMoiNhat(sdt);
        }

        [HttpGet("[action]")]
        public List<HoaDon> DanhSachTheoTenvaSDT(string tukhoa)

        {
            return _billRepo.DanhSachTheoTenvaSDT(tukhoa);
        }
        [HttpGet("[action]")]
        public List<HoaDon> DanhSachTheoTrangThai(int trangthai)

        {
            return _billRepo.DanhSachTheoTrangThai(trangthai);
        }
        [HttpGet("[action]")]
        public List<HoaDon> ListTrangThaiHoaDon()

        {
            return _billRepo.ListTrangThaiHoaDon();
        }

        [HttpGet("[action]")]
        public List<HoaDon> TimKiemHoaDonCho(string tukhoa)
        {
            try
            {
                return _billRepo.DanhSachHoaDonChoTheoTenvaSDT(tukhoa);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [HttpGet("[action]")]
        public List<HoaDon> DanhSachHoaDonCho()
        {
            try
            {
                return _billRepo.GetAll().Where(c => c.TrangThai == 2).OrderByDescending(c => c.NgayTao).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpGet("[action]")]
        public HoaDon LayHoaDonChoMoiNhat()
        {
            try
            {
                return _billRepo.LayHoaDonChoMoiNhat();
            }catch
            {
                return null;
            }
        }
        [HttpPost("[action]")]
        public bool TaoHoaDonCho(string tenkh, string sdt, string email)
        {
            try
            {
                var nguoidung = userRepository.lstnguoidung().FirstOrDefault(c => c.SDT == sdt && c.TenNguoiDung == tenkh && c.Email == email);
                NguoiDung user = new NguoiDung();
               
                var httt = hinhThucThanhToanRes.DanhSachThanhToan().FirstOrDefault(c => c.HinhThucThanhToan == "Thanh toán khi nhận hàng");

                if (nguoidung == null)
                {
                    var andanh = userRepository.lstnguoidung().FirstOrDefault(c => c.Email == "khachvanglai@gmail.com");  
                    HoaDon hoaDon = new HoaDon();
                    hoaDon.ID = Guid.NewGuid();
                    hoaDon.IDNguoiDung = andanh.Id;
                    hoaDon.IDVoucherChiTiet = null;
                    hoaDon.IDHinhThucThanhToan = httt.ID;
                    hoaDon.MaHoaDon = Convert.ToString(hoaDon.ID).Substring(0, 8).ToUpper();
                    hoaDon.NgayTao = DateTime.Now;
                    hoaDon.TenKhachHang = tenkh;
                    hoaDon.SDTKhachHang = sdt;
                    hoaDon.TongSoLuong = 0;
                    hoaDon.ThanhTien = 0;
                    hoaDon.TienShip = 0;
                    hoaDon.TienGiamGia = 0;
                    hoaDon.DiaChi = "";
                    hoaDon.TongTien = hoaDon.ThanhTien + hoaDon.TienShip - hoaDon.TienGiamGia;
                    hoaDon.NgayThanhToan = DateTime.Now;
                    hoaDon.NgayNhanHang = DateTime.Now;
                    hoaDon.TrangThai = 2; // Hóa đơn chờ

                    return _billRepo.Ceate(hoaDon);
                }
                else
                {
                    HoaDon hoaDon = new HoaDon();
                    hoaDon.ID = Guid.NewGuid();
                    hoaDon.IDNguoiDung = nguoidung.Id;
                    hoaDon.IDVoucherChiTiet = null;
                    hoaDon.IDHinhThucThanhToan = httt.ID;
                    hoaDon.MaHoaDon = Convert.ToString(hoaDon.ID).Substring(0, 8).ToUpper();
                    hoaDon.NgayTao = DateTime.Now;
                    hoaDon.TenKhachHang = tenkh;
                    hoaDon.DiaChi = "";
                    hoaDon.SDTKhachHang = sdt;
                    hoaDon.TongSoLuong = 0;
                    hoaDon.ThanhTien = 0;
                    hoaDon.TienShip = 0;
                    hoaDon.TienGiamGia = 0;
                    hoaDon.TongTien = hoaDon.ThanhTien + hoaDon.TienShip - hoaDon.TienGiamGia;
                    hoaDon.NgayThanhToan = DateTime.Now;
                    hoaDon.NgayNhanHang = DateTime.Now;
                    hoaDon.TrangThai = 2; // Hoa đơn chờ

                    return _billRepo.Ceate(hoaDon);
                }
            }

            catch (Exception ex)
            {
                return false;
            }
        }
        [HttpPost("[action]")]
        public async Task<ActionResult> Create(HoaDon hoaDon)
        {
            if (await _billRepo.GetMa(hoaDon.MaHoaDon))
            {
                return NotFound("Mã đã tồn tại");
            }
            else if (_billRepo.Ceate(hoaDon))
            {
                return Ok("Thêm thành công");
            }
            return BadRequest("Thêm không thành công");
        }

        [HttpPut("id")]
        public async Task<ActionResult> Update(HoaDon hoaDon)
        {
            if (hoaDon != null)
            {
                if (_billRepo.Update(hoaDon, hoaDon.ID))
                {
                    return Ok("Sửa thành công");
                }
                return BadRequest("Sửa không thành công");
            }
            else
            {
                return BadRequest("Sửa không thành công");
            }
        }

        [HttpDelete("id")]
        public async Task<ActionResult> Delete(Guid id)
        {
            if (await _billRepo.Delete(id))
            {
                return Ok("Xóa thành công");
            }
            else return BadRequest("Xóa thất bại");
        }

        [HttpGet("id")]
        public async Task<ActionResult> GetById(Guid id)
        {
            var hd = await _billRepo.GetId(id);
            return Ok(hd);
        }

        [HttpGet("[action]")]
        public async Task<ActionResult> GetByMa(string ma)
        {
            var maHd = await _billRepo.GetMa(ma);
            return Ok(maHd);
        }

        [HttpPut("[action]")]
        public string HuyDonHang(Guid id)
        {
            return  _billRepo.HuyHoaDon(id);
        }

        [HttpPut("[action]")]
        public string DaNhan(Guid id)
        {
            return _billRepo.DaNhan(id);
        }

        [HttpPut("[action]")]
        public string DangGiao(Guid id)
        {
            return _billRepo.DangGiao(id);
        }
        [HttpPut("[action]")]
        public string XacNhan(Guid id)
        {
            return _billRepo.XacNhanHoaDon(id);
        }
        [HttpPut("[action]")]
        public string HoanHang(Guid id)
        {
            return _billRepo.HoanHang(id);
        }
        [HttpPut("[action]")]
        public string XacNhanHoanHang(Guid id)
        {
            return _billRepo.XacNhanHoanHang(id);
        }
        //Thông kê 

    }
}
