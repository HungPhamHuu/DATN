using AppData.DB_Context;
using AppData.Entities.Models;
using AppData.Entities.ViewModels;
using AppData.Repositories.Roles;
using AppData.Repositories.Users;
using AppView.Models;
using AppData.Session;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos.Serialization.HybridRow;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Drawing;
using System.Net.Http;
using System.Security.Claims;
using System.Text;

namespace AppView.Areas.Admin.Controllers
{
    public class BanHangTaiQuayController : Controller
    {
        private readonly ILogger<BanHangTaiQuayController> _logger;
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;
        private readonly ApplicationDbContext _context;
        private readonly IRoleRepository roleRepository;
        private readonly IUserRepository userRepository;
        private readonly Utils _utils;

        public BanHangTaiQuayController(ILogger<BanHangTaiQuayController> logger, IConfiguration configuration, Utils utils)
        {
            _logger = logger;
            _configuration = configuration;
            _utils = utils;
            _userRepository = new UserRepository();
            _httpClient = new HttpClient();
            _context = new ApplicationDbContext();
            roleRepository = new RoleRepository();
            userRepository = new UserRepository();
            _configuration = configuration;
        }
        // GET: BanHangTaiQuay
        public ActionResult BanHang()
        {

            return View();
        }

        [HttpGet]
        public ActionResult SanPhamChiTiet(Guid idspct)
        {
            string urlapi = $"https://localhost:7265/api/SanPhamChiTiet/SanPhamHoanThienByID?id={idspct}";
            var reponse = _httpClient.GetAsync(urlapi).Result;
            string apiData = reponse.Content.ReadAsStringAsync().Result;
            SanPhamChiTietViewModel DanhsachKetQua = JsonConvert.DeserializeObject<SanPhamChiTietViewModel>(apiData);
            return View(DanhsachKetQua);
        }

        [HttpGet]
        public ActionResult DanhSachSaleJson()
        {
            try
            {
                var url = $"https://localhost:7265/api/SaleChiTiet/DanhSachKh";
                var res = _httpClient.GetAsync(url).Result;
                var data = res.Content.ReadAsStringAsync().Result;
                List<SaleChiTietViewModel> lst = JsonConvert.DeserializeObject<List<SaleChiTietViewModel>>(data);
                return Json(lst);
            }
            catch
            {
                return Json(null);
            }
        }

        [HttpGet]
        public ActionResult SanPhamSaleChiTiet(Guid idspsale)
        {
            var url = $"https://localhost:7265/api/SaleChiTiet/SanphamChiTiettheoID?id={idspsale}";
            var res = _httpClient.GetAsync(url).Result;
            var data = res.Content.ReadAsStringAsync().Result;
            SaleChiTietViewModel sale = JsonConvert.DeserializeObject<SaleChiTietViewModel>(data);
            return View(sale);
        }

        [HttpGet]
        public ActionResult DanhSachSanPhamHoanThien()
        {
            try
            {
                string apiUrl = "https://localhost:7265/api/SanPhamChiTiet/DanhSachSanPhamHoanThien";
                var reponse = _httpClient.GetAsync(apiUrl).Result;
                string apiData = reponse.Content.ReadAsStringAsync().Result;
                List<SanPhamChiTietViewModel> spct = JsonConvert.DeserializeObject<List<SanPhamChiTietViewModel>>(apiData);
                var uniqueSpct = spct.GroupBy(c => c.TenSP)
                         .Select(group => group.First())
                         .ToList();

                return Json(uniqueSpct);
            } catch
            {
                return Json(null);
            }
        }

        [HttpGet]
        [HttpPost]
        public ActionResult TimKiemHoaDonCho([FromBody] string tukhoa)
        {
            try
            {
                string apiUrl = $"https://localhost:7265/api/HoaDon/TimKiemHoaDonCho?tukhoa={tukhoa}";
                var reponse = _httpClient.GetAsync(apiUrl).Result;
                string apiData = reponse.Content.ReadAsStringAsync().Result;
                List<HoaDon> lsthoadoncho = JsonConvert.DeserializeObject<List<HoaDon>>(apiData);
                return Json(lsthoadoncho);
            } catch
            {
                return Json(null);
            }
           
        }


        [HttpGet]
        public ActionResult DanhSachHoaDonCho()
        {
            try
            {
                string apiUrl = "https://localhost:7265/api/HoaDon/DanhSachHoaDonCho";
                var reponse = _httpClient.GetAsync(apiUrl).Result;
                string apiData = reponse.Content.ReadAsStringAsync().Result;
                List<HoaDon> lsthoadoncho = JsonConvert.DeserializeObject<List<HoaDon>>(apiData);
                return Json(lsthoadoncho);
            } catch
            {
                return Json(null);
            }
           
        }

        [HttpGet]
        public ActionResult LayHoaDonChoMoiNhat()
        {
            try
            {
                string apiUrl = "https://localhost:7265/api/HoaDon/LayHoaDonChoMoiNhat";
                var reponse = _httpClient.GetAsync(apiUrl).Result;
                string apiData = reponse.Content.ReadAsStringAsync().Result;
                HoaDon hoadoncho = JsonConvert.DeserializeObject<HoaDon>(apiData);
                return Json(hoadoncho);
            } catch
            {
                return Json(null);
            }
           
        }
        [HttpGet]
        [HttpPost]
        public string NhanEmail([FromBody] string email)
        {
            HttpContext.Session.Remove("email");
            HttpContext.Session.SetString("email", email);
            return email;
        }

        [HttpGet]
        [HttpPost]
        public ActionResult TaoHoaDonCho([FromBody] HoaDon hoaDon)
        {
            ThongBao tb = new ThongBao();
            try
            {
                string email = HttpContext.Session.GetString("email");
                string apiURL = $"https://localhost:7265/api/HoaDon/TaoHoaDonCho?tenkh={hoaDon.TenKhachHang}&sdt={hoaDon.SDTKhachHang}&email={email}";
                var obj = JsonConvert.SerializeObject(hoaDon);
                StringContent content = new StringContent(obj, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _httpClient.PostAsync(apiURL, content).Result;
                if (response.IsSuccessStatusCode)
                {
                    var mess = response.Content.ReadAsStringAsync().Result;
                    tb.message = mess;
                    return Json(tb);
                }
                else
                {
                    tb.message = "Thất bại";
                    return Json(tb);
                }
            } catch
            {
                tb.message = "Lỗi";
                return Json(tb);
            }
           
        }
        [HttpGet]
        [HttpPost]
        public ActionResult HoaDonChiTiet([FromBody] HoaDon hoaDon)
        {
            // Lõio hóa đơn tại quầy
            try
            {
                string hoadon = $"https://localhost:7265/api/HoaDon/id?id={hoaDon.ID}";
                var response = _httpClient.GetAsync(hoadon).Result;
                string apiData = response.Content.ReadAsStringAsync().Result;
                HoaDon result = JsonConvert.DeserializeObject<HoaDon>(apiData);
                return Json(result);
            }
            catch (Exception ex)
            {
                return Json(null);
            }

        }
        [HttpGet]
        [HttpPost]
        public ActionResult LstHoaDonChiTiet([FromBody] HoaDon hoaDon)
        {
            try
            {
                string urlapi = $"https://localhost:7265/api/HoaDonCT/DanhSachHDChiTietTheoIDhd?idhd={hoaDon.ID}";
            var response = _httpClient.GetAsync(urlapi).Result;
            string apiData = response.Content.ReadAsStringAsync().Result;
            List<HoaDonChiTietViewModel> result = JsonConvert.DeserializeObject<List<HoaDonChiTietViewModel>>(apiData);
            return Json(result);
            }
            catch
            {
                return Json(null);
            }

        }

        [HttpGet]
        [HttpPost]
        public ActionResult CapNhatSoLuongHDCT([FromBody] HoaDonChiTiet hdct)
        {
            ThongBao tb = new ThongBao();
            try
            {
                string url = $"https://localhost:7265/api/HoaDonCT/CapNhatSoLuongHDCT?IDhdct={hdct.ID}&soluong={hdct.SoLuong}";
                var obj = JsonConvert.SerializeObject(hdct);
                StringContent content = new StringContent(obj, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _httpClient.PutAsync(url, content).Result;
                if (response.IsSuccessStatusCode)
                {
                    var mess = response.Content.ReadAsStringAsync().Result;
                    tb.message = mess;
                    return Json(tb);
                }
                else
                {
                    tb.message = "Thất bại.";
                    return Json(tb);
                }
            } catch
            {
                tb.message = "Lỗi";
                return Json(tb);
            }
            
        }
        [HttpGet]
        [HttpPost]
        public ActionResult XoaSanPhamTrongHoaDonChiTiet([FromBody] Guid id)
        {
            ThongBao tb = new ThongBao();
            try
            {
                string url = $"https://localhost:7265/api/HoaDonCT/id?id={id}";
                HttpResponseMessage response = _httpClient.DeleteAsync(url).Result;
                if (response.IsSuccessStatusCode)
                {
                    var mess = response.Content.ReadAsStringAsync().Result;
                    tb.message = mess;
                    return Json(tb);
                }
                else
                {
                    tb.message = "Thất bại";
                    return Json(tb);
                }
            } catch
            {
                tb.message = "Lỗi";
                return Json(tb);
            }
         
        }
        [HttpGet]
        [HttpPost]
        public ActionResult NhanIDhD([FromBody] string idhd)
        {
            try
            {
                var nhanidhd = idhd;
                HttpContext.Session.Remove("NhanIdHd");
                HttpContext.Session.SetString("NhanIdHd", nhanidhd);
                ThongBao tb = new ThongBao();
                tb.message = nhanidhd;
                return Json(tb);
            } catch
            {
                return Json(null);
            }
           
        }
        [HttpGet]
        public string TraIDHoaDon()
        {
            var idhd = HttpContext.Session.GetString("NhanIdHd");
            return idhd;
        }
        [HttpGet]
        [HttpPost]
        public string NhanIDSPCT([FromBody] string idspct)
        {
            var nhanidspct = idspct;
            HttpContext.Session.Remove("NhanIDSPCT");
            HttpContext.Session.SetString("NhanIDSPCT", nhanidspct);
            return nhanidspct;
        }
        [HttpGet]
        [HttpPost]
        public ActionResult ThemSanPhamVaoHDCT([FromBody] HoaDonChiTiet hdct)
        {
            ThongBao tb = new ThongBao();
            try
            {
                var idhd = HttpContext.Session.GetString("NhanIdHd");
                var nhanidspct = HttpContext.Session.GetString("NhanIDSPCT");
                string url = $"https://localhost:7265/api/HoaDonCT/Create?idhd={idhd}&idspct={hdct.IDSPCT}&soluong={hdct.SoLuong}";
                var obj = JsonConvert.SerializeObject(hdct);
                StringContent content = new StringContent(obj, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _httpClient.PostAsync(url, content).Result;
                if (response.IsSuccessStatusCode)
                {
                    var mess = response.Content.ReadAsStringAsync().Result;
                    tb.message = mess;
                    return Json(tb);
                }
                else
                {
                    tb.message = "Thất bại";
                    return Json(tb);
                }
            } catch
            {
                tb.message = "Lỗi";
                return Json(tb);
            }
            
        }
        [HttpGet]
        [HttpPost]
        public string NhanIDthanhToan([FromBody] string Idthanhtoan)
        {
            HttpContext.Session.Remove("IdThanhToan");
            HttpContext.Session.SetString("IdThanhToan", Idthanhtoan);
            return Idthanhtoan;
        }


        [HttpGet]
        [HttpPost]
        public ActionResult HoaDonChoXuLy([FromBody] HoaDon hoaDon)
        {
            ThongBao tb = new ThongBao();

            try
            {
                var user = HttpContext.User;
                var email = user.FindFirstValue(ClaimTypes.Email);
                var id = _userRepository.lstnguoidung().FirstOrDefault(c => c.Email == email).Id;
                var idthanhtoan = HttpContext.Session.GetString("IdThanhToan");
                string url = $"https://localhost:7265/api/DatHang/ThanhToanHoaDonTaiQuay?id={hoaDon.ID}&idNguoiDung={id}&idthanhtoan={idthanhtoan}";
                var obj = JsonConvert.SerializeObject(hoaDon);
                StringContent content = new StringContent(obj, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _httpClient.PutAsync(url, content).Result;
                if (response.IsSuccessStatusCode)
                {
                    var mess = response.Content.ReadAsStringAsync().Result;
                    tb.message = mess;
                    return Json(tb);
                }
                else
                {
                    tb.message = "Thất bại.";
                    return Json(tb);
                }
            } catch
            {
                tb.message = "Lỗi.";
                return Json(tb);
            }
        }
        [HttpGet]
        [HttpPost]
        public ActionResult ThemNguoiDungMoi([FromBody] NguoiDung nguoidung)
        {
            ThongBao tb = new ThongBao();
            try
            {
                NguoiDung nguoiDung = new NguoiDung()
                {
                    Id = Guid.NewGuid(),
                    IdChucVu = roleRepository.DanhSachChucVu().FirstOrDefault(c => c.TenChucVu == "Khách hàng").Id,
                    TenNguoiDung = nguoidung.TenNguoiDung,
                    NgaySinh = DateTime.Now,
                    Anh = "",
                    SDT = nguoidung.SDT,
                    Email = nguoidung.Email,
                    MatKhau = "nam@1234",
                    PhuongXa = "",
                    TinhThanh = "",
                    DiaChi = "",
                    QuanHuyen = "",
                    TrangThai = 1
                };
                string message = userRepository.CreateKhachHang(nguoiDung);
                if (message == "Email đã đăng ký. Mời bạn đăng ký lại.")
                {
                    tb.message = message;
                    return Json(tb);
                }
                else if (message == "SDT đã đăng ký. Mời bạn đăng ký lại.")
                {
                    tb.message = message;
                    return Json(tb);
                }
                else if (message == "Đăng ký khách hàng thất bại.")
                {
                    tb.message = message;
                    return Json(tb);
                }
                else
                {
                    // Tạo giỏ hàng 
                    GioHang gh = new GioHang()
                    {
                        ID = Guid.NewGuid(),
                        IDNguoiDung = nguoiDung.Id,
                    };
                    _context.gioHangs.Add(gh);
                    _context.SaveChanges();
                    tb.message = message;
                    return Json(tb);
                }
            } catch
            {
                tb.message = "Lỗi.";
                return Json(tb);
            }
     
        }
        public ActionResult ThanhToanOnline()
        {
            string vnp_Returnurl = _configuration["VnpaySettings:vnp_Returnurl"]; //URL nhan ket qua tra ve 
            string vnp_Url = _configuration["VnpaySettings:vnp_Url"]; //URL thanh toan cua VNPAY 
            string vnp_TmnCode = _configuration["VnpaySettings:vnp_TmnCode"]; //Ma định danh merchant kết nối (Terminal Id)
            string vnp_HashSecret = _configuration["VnpaySettings:vnp_HashSecret"]; //Secret Key
            string ipAddress = _utils.GetIpAddress();
            long randomString = DateTime.Now.Ticks;
            //Get payment input

            HoaDon hd = HttpContext.Session.GetObjectFromJson<HoaDon>("HoaDon");
            var Amount = Convert.ToInt32(hd.TongTien); //Convert.ToInt32(hoaDon.ThanhTien + hoaDon.TienShip - hoaDon.TienGiamGia);
            DateTime ngayMoi = DateTime.Now;
            VnPayLibrary vnpay = new VnPayLibrary();

            vnpay.AddRequestData("vnp_Version", VnPayLibrary.VERSION);
            vnpay.AddRequestData("vnp_Command", "pay");
            vnpay.AddRequestData("vnp_TmnCode", vnp_TmnCode);
            vnpay.AddRequestData("vnp_Amount", (Amount * 100).ToString()); //Số tiền thanh toán. Số tiền không mang các ký tự phân tách thập phân, phần nghìn, ký tự tiền tệ. Để gửi số tiền thanh toán là 100,000 VND (một trăm nghìn VNĐ) thì merchant cần nhân thêm 100 lần (khử phần thập phân), sau đó gửi sang VNPAY là: 10000000
                                                                           //string idbank = HttpContext.Session.GetString("idbank");
            vnpay.AddRequestData("vnp_BankCode", "VNBANK");
            vnpay.AddRequestData("vnp_CreateDate", ngayMoi.ToString("yyyyMMddHHmmss"));
            vnpay.AddRequestData("vnp_CurrCode", "VND");
            vnpay.AddRequestData("vnp_IpAddr", ipAddress);

            vnpay.AddRequestData("vnp_Locale", "vn");

            vnpay.AddRequestData("vnp_OrderInfo", "Thanh toan don hang:" + randomString);
            vnpay.AddRequestData("vnp_OrderType", "other"); //default value: other

            vnpay.AddRequestData("vnp_ReturnUrl", vnp_Returnurl);
            vnpay.AddRequestData("vnp_TxnRef", randomString.ToString()); // Mã tham chiếu của giao dịch tại hệ thống của merchant. Mã này là duy nhất dùng để phân biệt các đơn hàng gửi sang VNPAY. Không được trùng lặp trong ngày

            //HttpContext.Session.Remove("randomString");
            //HttpContext.Session.SetString("randomString", randomString);
            //Add Params of 2.1.0 Version
            //Billing

            string paymentUrl = vnpay.CreateRequestUrl(vnp_Url, vnp_HashSecret);
            _logger.LogInformation("VNPAY URL: {0}", paymentUrl);
            return Redirect(paymentUrl);
        }
    }
}
