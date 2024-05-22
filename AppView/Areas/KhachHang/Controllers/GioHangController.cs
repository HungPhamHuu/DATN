using AppData.Entities.ViewModels;
using Microsoft.AspNetCore.Http;
using AppData.Session;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using AppData.Entities.ViewModels.GioHangVM;
using System.Text;
using AppData.Entities.ViewModels.GioHangCTVM;
using System.Security.Claims;
using AppData.Repositories.Users;
using AppData.Repositories.CartDetail;
using AppData.DB_Context;
using AppData.Repositories.ProductDetail;
using AppData.Repositories.Product;
using AppData.Entities.Models;
using System.Net.Http;
using System.Net.Mail;
using AppData.Repositories.ViewModel;
using AppData.Repositories.HinhThucThanhToan;
using AppView.Models;
using AppView.Controllers;
using System;
using Microsoft.Azure.Cosmos.Serialization.HybridRow;

namespace AppView.Areas.KhachHang.Controllers
{
    public class GioHangController : Controller
    {
        private readonly ILogger<GioHangController> _logger;
        private readonly IUserRepository _userRepository;
        private readonly ApplicationDbContext _context;
        private readonly ICartDetailRepository cartDetailRepository;
        private readonly IProductDetailRepository productDetailRepository;
        private readonly IProductRepository productRepository;
        private readonly SaleChiTietVm saleChiTiet;
        private readonly IHinhThucThanhToanRes _hinhthuc;
        private readonly IConfiguration _configuration;
        private readonly Utils _utils;

        public GioHangController(ILogger<GioHangController> logger, ApplicationDbContext context, IConfiguration configuration, Utils utils)
        {
            _logger = logger;
            _userRepository = new UserRepository();
            cartDetailRepository = new CartDetailRepository(context);
            productDetailRepository = new ProductDetailRepository(context);
            productRepository = new ProductRepository(context);
            saleChiTiet = new SaleChiTietVm();
            _hinhthuc = new HinhThucThanhToanRes();
            _configuration = configuration;
            _utils = utils;
        }

        HttpClient _client = new HttpClient();


        [HttpGet]
        [HttpPost]
        public IActionResult GioHangChiTiet()
        {
            List<ThanhPho> laytinh = Lst_Provinces();
            var user = HttpContext.User;
            var email = user.FindFirstValue(ClaimTypes.Email);
            if (email == null)
            {
                return RedirectToAction("DanhNhap", "DangNhap");
            }
            else
            {
                var id = _userRepository.lstnguoidung().FirstOrDefault(c => c.Email == email).Id;
                string apiUrl = $"https://localhost:7265/api/GioHangChiTiet/GioHangChitietTheoIDNguoiDung?idnguoidung={id}";
                var response = _client.GetAsync(apiUrl).Result;
                string apiData = response.Content.ReadAsStringAsync().Result;
                List<GioHangCTVM> result = JsonConvert.DeserializeObject<List<GioHangCTVM>>(apiData);
                var soluong = result.Count();
                HttpContext.Session.Remove("TienShip");
                HttpContext.Session.Remove("soluong");
                HttpContext.Session.SetInt32("soluong", soluong);
                return View(result);
            }
        }

        [HttpGet]
        public ActionResult Soluong()
        {
            try
            {
                var user = HttpContext.User;
                var email = user.FindFirstValue(ClaimTypes.Email);
                if (email == null)
                {
                    return RedirectToAction("DanhNhap", "DangNhap");
                }
                else
                {
                    var id = _userRepository.lstnguoidung().FirstOrDefault(c => c.Email == email).Id;
                    string apiUrl = $"https://localhost:7265/api/GioHangChiTiet/GioHangChitietTheoIDNguoiDung?idnguoidung={id}";
                    var response = _client.GetAsync(apiUrl).Result;
                    string apiData = response.Content.ReadAsStringAsync().Result;
                    List<GioHangCTVM> result = JsonConvert.DeserializeObject<List<GioHangCTVM>>(apiData);
                    var soluong = result.Count();
                    return Json(soluong);
                }
            } catch
            {
                return Json(0);
            }

        }

        [HttpGet]
        public IActionResult GioHangChiTietJson()
        {
            try
            {
                var user = HttpContext.User;
                var email = user.FindFirstValue(ClaimTypes.Email);
                if (email == null)
                {
                    return RedirectToAction("DanhNhap", "DangNhap");
                }
                else
                {
                    var id = _userRepository.lstnguoidung().FirstOrDefault(c => c.Email == email).Id;
                    string apiUrl = $"https://localhost:7265/api/GioHangChiTiet/GioHangChitietTheoIDNguoiDung?idnguoidung={id}";
                    var response = _client.GetAsync(apiUrl).Result;
                    string apiData = response.Content.ReadAsStringAsync().Result;
                    List<GioHangCTVM> result = JsonConvert.DeserializeObject<List<GioHangCTVM>>(apiData);
                    return Json(result);
                }
            } catch
            {
                return Json(null);
            }
            
        }
        public double Trongluong()
        {
            try
            {
                double trongluong = 0;
                double TongTrongLuong = 0;
                var user = HttpContext.User;
                var email = user.FindFirstValue(ClaimTypes.Email);
                if (email == null)
                {
                    return 0;
                }
                else
                {
                    var id = _userRepository.lstnguoidung().FirstOrDefault(c => c.Email == email).Id;
                    string apiUrl = $"https://localhost:7265/api/GioHangChiTiet/GioHangChitietTheoIDNguoiDung?idnguoidung={id}";
                    var httpClient = new HttpClient();
                    var response = httpClient.GetAsync(apiUrl).Result;
                    string apiData = response.Content.ReadAsStringAsync().Result;
                    var result = JsonConvert.DeserializeObject<List<GioHangCTVM>>(apiData);
                    foreach (var item in result)
                    {
                        if (item.IDSPCT != null)
                        {
                            var idsp = productDetailRepository.GetAll().FirstOrDefault(c => c.ID == item.IDSPCT).IDSP;
                            var sanpham = productRepository.GetAll().Result.FirstOrDefault(c => c.ID == idsp);
                            trongluong = (double)item.SoLuong * sanpham.KhoiLuong;
                            TongTrongLuong += trongluong;
                        }
                        else
                        {
                            var sale = saleChiTiet.DanhSachSanPhamSale().FirstOrDefault(c => c.IDSalechitiet == item.IDSaleCT);
                            var idspct = productDetailRepository.GetAll().FirstOrDefault(c => c.ID == sale.IdSanPhamCT).IDSP;
                            var sanpham = productRepository.GetAll().Result.FirstOrDefault(c => c.ID == idspct);
                            trongluong = (double)item.SoLuong * sanpham.KhoiLuong;
                            TongTrongLuong += trongluong;
                        }

                    }
                    return TongTrongLuong;
                }
            } catch
            {
                return 0;
            }
            
        }
        public double TongTien()
        {
            try
            {
                double TongTien = 0;
                var user = HttpContext.User;
                var email = user.FindFirstValue(ClaimTypes.Email);
                if (email == null)
                {
                    return 0;
                }
                else
                {
                    var id = _userRepository.lstnguoidung().FirstOrDefault(c => c.Email == email).Id;
                    string apiUrl = $"https://localhost:7265/api/GioHangChiTiet/GioHangChitietTheoIDNguoiDung?idnguoidung={id}";
                    var httpClient = new HttpClient();
                    var response = httpClient.GetAsync(apiUrl).Result;
                    string apiData = response.Content.ReadAsStringAsync().Result;
                    var result = JsonConvert.DeserializeObject<List<GioHangCTVM>>(apiData);
                    TongTien = (double)result.Sum(c => c.ThanhTien);
                    return TongTien;
                }
            } catch
            {
                return 0;
            }
            
        }

        [HttpPost]
        public List<ThanhPho> Lst_Provinces()
        {
            string token = "be8ee160-008a-11ee-a281-3aa62a37e0a5";
            _client.DefaultRequestHeaders.Add("token", token);
            List<ThanhPho> thongtintp = new List<ThanhPho>();
            var urlTinhThanhPho = "https://online-gateway.ghn.vn/shiip/public-api/master-data/province";
            HttpResponseMessage response = _client.GetAsync(urlTinhThanhPho).Result;
            // lấy ID tỉnh
            if (response.IsSuccessStatusCode)
            {
                var provinces = response.Content.ReadAsStringAsync().Result; ;

                JObject jsonObject = JObject.Parse(provinces);

                // Lấy danh sách tỉnh/thành phố từ đối tượng JSON
                JArray provinceArray = (JArray)jsonObject["data"];


                if (provinceArray != null)
                {
                    foreach (JObject province in provinceArray)
                    {
                        ThanhPho lstThanhPho = new ThanhPho();
                        lstThanhPho.ProvinceID = Convert.ToInt32(province["ProvinceID"]);
                        lstThanhPho.ProvinceName = Convert.ToString(province["ProvinceName"]);
                        thongtintp.Add(lstThanhPho);
                    }
                }
                ViewBag.thongtin = thongtintp;
            }
            HttpContext.Session.Remove("ThanhPho");
            HttpContext.Session.Set("ThanhPho", thongtintp.OrderBy(c => c.ProvinceName));
            return thongtintp;
        }
        [HttpGet]
        public ActionResult Lst_ThanhPho()
        {
            var ThanhPho = HttpContext.Session.GetString("ThanhPho");
            return Json(new { ThanhPho });
        }
        [HttpGet]
        [HttpPost]
        public ActionResult NhanIDTinh([FromBody] int IdThanhPho)
        {
            List<QuanHuyen> thongtinquanhuyen = new List<QuanHuyen>();
            string token = "be8ee160-008a-11ee-a281-3aa62a37e0a5";
            _client.DefaultRequestHeaders.Add("token", token);
            string url = $"https://online-gateway.ghn.vn/shiip/public-api/master-data/district?province_id={IdThanhPho}";
            HttpResponseMessage response1 = _client.GetAsync(url).Result;
            if (response1.IsSuccessStatusCode)
            {
                var districts = response1.Content.ReadAsStringAsync().Result;

                JObject jsonObject1 = JObject.Parse(districts);

                // Lấy danh sách tỉnh/thành phố từ đối tượng JSON
                JArray districtArray = (JArray)jsonObject1["data"];


                if (districtArray != null)
                {
                    foreach (JObject district in districtArray)
                    {
                        QuanHuyen LstQuanHuyen = new QuanHuyen();
                        LstQuanHuyen.DistrictId = Convert.ToInt32(district["DistrictID"]);
                        LstQuanHuyen.DistrictName = Convert.ToString(district["DistrictName"]);
                        thongtinquanhuyen.Add(LstQuanHuyen);
                    }
                }
            }

            HttpContext.Session.Set("Thongtinquanhuyen", thongtinquanhuyen.OrderBy(c => c.DistrictName));

            HttpContext.Session.Remove("IdThanhPho");
            HttpContext.Session.SetInt32("IdThanhPho", IdThanhPho);
            return Json(thongtinquanhuyen);
        }

        [HttpGet]
        public IActionResult LayQuanHuyen()
        {
            var thongTinQuanHuyen = HttpContext.Session.GetString("Thongtinquanhuyen");

            // Trả về dữ liệu dưới dạng JSON
            return Json(new { thongTinQuanHuyen });
        }

        [HttpPost]
        public IActionResult NhanIDHuyen(int IdQuanHuyen)
        {
            List<PhuongXa> LstPhuongXa = new List<PhuongXa>();
            string token = "be8ee160-008a-11ee-a281-3aa62a37e0a5";
            _client.DefaultRequestHeaders.Add("token", token);
            string url = $"https://online-gateway.ghn.vn/shiip/public-api/master-data/ward?district_id={IdQuanHuyen}";
            HttpResponseMessage response1 = _client.GetAsync(url).Result;
            if (response1.IsSuccessStatusCode)
            {
                var districts = response1.Content.ReadAsStringAsync().Result;

                JObject jsonObject1 = JObject.Parse(districts);

                // Lấy danh sách tỉnh/thành phố từ đối tượng JSON
                JArray WardCodeArray = (JArray)jsonObject1["data"];

                if (WardCodeArray != null)
                {
                    foreach (JObject Ward in WardCodeArray)
                    {
                        PhuongXa phuongxa = new PhuongXa();
                        phuongxa.WardCode = Convert.ToString(Ward["WardCode"]);
                        phuongxa.WardName = Convert.ToString(Ward["WardName"]);
                        LstPhuongXa.Add(phuongxa);
                    }
                }
            }

            HttpContext.Session.Set("XaPhuong", LstPhuongXa.OrderBy(c => c.WardName));

            HttpContext.Session.Remove("IDQuanHuyen");
            HttpContext.Session.SetInt32("IDQuanHuyen", IdQuanHuyen);
            return Json(LstPhuongXa);
        }

        [HttpGet]
        public IActionResult GetAllXaPhuong()
        {
            var ThongTinXaPhuong = HttpContext.Session.GetString("XaPhuong");

            // Trả về dữ liệu dưới dạng JSON
            return Json(new { ThongTinXaPhuong });
        }
        
        [HttpPost]
        [HttpGet]
        public IActionResult TinhTienShip(int WardCode)
        {
            var tienship = 0;
            try
            {
                var tongtien = TongTien();
                var IdQuanHuyen = HttpContext.Session.GetInt32("IDQuanHuyen");
                List<DichVuChuyenPhat> lstdichvu = new List<DichVuChuyenPhat>();
                // ChonDichVụ
                string token = "be8ee160-008a-11ee-a281-3aa62a37e0a5";
                _client.DefaultRequestHeaders.Add("token", token);
                string urldichvu = $"https://online-gateway.ghn.vn/shiip/public-api/v2/shipping-order/available-services?shop_id=3689187&from_district=3440&to_district={IdQuanHuyen}";
                HttpResponseMessage response1 = _client.GetAsync(urldichvu).Result;
                if (response1.IsSuccessStatusCode)
                {
                    var Services = response1.Content.ReadAsStringAsync().Result;

                    JObject jsonObject = JObject.Parse(Services);

                    // Lấy danh sách tỉnh/thành phố từ đối tượng JSON
                    JArray ServicesArray = (JArray)jsonObject["data"];
                    if (ServicesArray != null)
                    {
                        foreach (JObject service in ServicesArray)
                        {
                            DichVuChuyenPhat dichvucchuyenphat = new DichVuChuyenPhat();
                            dichvucchuyenphat.ServiceId = Convert.ToInt32(service["service_id"]);
                            dichvucchuyenphat.ServiceName = Convert.ToString(service["short_name"]);
                            lstdichvu.Add(dichvucchuyenphat);
                        }
                    }
                }

                int idservice = lstdichvu.FirstOrDefault(c => c.ServiceName == "Chuyển phát thương mại điện tử").ServiceId;
                var trongLuong = Trongluong();
                string urltinhphi = $"https://online-gateway.ghn.vn/shiip/public-api/v2/shipping-order/fee?service_id={idservice}&insurance_value={tongtien}&from_district_id=3440&to_district_id={IdQuanHuyen}&to_ward_code={WardCode}&height=60&length=60&weight={trongLuong}&width=60";
                HttpResponseMessage httpResponseMessage = _client.GetAsync(urltinhphi).Result;
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    var ship = httpResponseMessage.Content.ReadAsStringAsync().Result;

                    JObject ships = JObject.Parse(ship);


                    JToken dataToken = ships["data"];
                    if (dataToken is JArray)
                    {
                        JArray districtArray = (JArray)dataToken;
                        // Tiếp tục xử lý với districtArray
                    }
                    else if (dataToken is JObject)
                    {
                        JObject districtObject = (JObject)dataToken;
                        // Tiếp tục xử lý nếu cần thiết
                        tienship = Convert.ToInt32(districtObject["total"]);
                    }
                }
                HttpContext.Session.Remove("TienShip");
                HttpContext.Session.SetInt32("TienShip", tienship);
                return Json(tienship);
            } catch
            {
                return Json(tienship);
            }
            
        }
        [HttpGet]
        public IActionResult HienThiTienShip()
        {
            var tienship = HttpContext.Session.GetInt32("TienShip");
            return Json(new { tienship });
        }

        [HttpGet]
        public async Task<IActionResult> ShowAllCart()
        {
            string apiUrl = "https://localhost:7265/api/GioHang/get";
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(apiUrl);

            string apiData = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<GioHangVM>>(apiData);
            return View(result);
        }

        [HttpGet]
        public IActionResult CreateCart()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCart(GioHangVM gioHangVM)
        {
            string apiUrl = "https://localhost:7265/api/GioHang/add";
            var httpClient = new HttpClient();
            var content = new StringContent(JsonConvert.SerializeObject(gioHangVM), Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync(apiUrl, content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("ShowAllCart");
            }
            else
            {
                return View(gioHangVM);
            }
        }
        public string NhanIDThanhToan([FromBody] string id)
        {
            HttpContext.Session.Remove("idbank");

            HttpContext.Session.SetString("idbank", id);
            return id;
        }

        [HttpPost]
        public ActionResult TaoHoaDon([FromBody] HoaDon hd)
        {
            ThongBao tb = new ThongBao();
            try
            {
                string urlapi = "";
                var user = HttpContext.User;
                var email = user.FindFirstValue(ClaimTypes.Email);
                Guid? idhinhthucthanhtoan ;
                if(hd.IDHinhThucThanhToan == null)
                {
                    idhinhthucthanhtoan = _hinhthuc.DanhSachThanhToan().FirstOrDefault(c => c.HinhThucThanhToan == "Thanh toán khi nhận hàng").ID;
                } else
                {
                    idhinhthucthanhtoan = hd.IDHinhThucThanhToan;
                }
               // var hinhthucthanhtoan = _hinhthuc.DanhSachThanhToan().FirstOrDefault(c => c.ID == hd.IDHinhThucThanhToan).HinhThucThanhToan;
                var id = _userRepository.lstnguoidung().FirstOrDefault(c => c.Email == email).Id;
                string datetime = Convert.ToString(DateTime.Now);
                DateTime ngayChuyenDoi;
                string ngayMoi = "";
                if (DateTime.TryParseExact(datetime, "dd/MM/yyyy HH:mm:ss", null, System.Globalization.DateTimeStyles.None, out ngayChuyenDoi))
                {
                    ngayMoi = ngayChuyenDoi.ToString("yyyy/MM/dd");
                }
                if (hd.IDVoucherChiTiet == null)
                {
                    urlapi = $"https://localhost:7265/api/DatHang/DatHangOnline?IdNguoiDung={id}&hinhthucthanhtoan={idhinhthucthanhtoan}&tenkhachang={hd.TenKhachHang}&sdtkh={hd.SDTKhachHang}&diachi={hd.DiaChi}&tongsoluong={hd.TongSoLuong}&thanhtien={hd.ThanhTien}&tienship={hd.TienShip}&tiengiamgia={hd.TienGiamGia}&NgayThanhToan={ngayMoi}&ngaynhanhang={ngayMoi}&GhiChu={hd.GhiChu}";
                }
                else
                {
                    urlapi = $"https://localhost:7265/api/DatHang/DatHangOnline?IdNguoiDung={id}&idvoucher={hd.IDVoucherChiTiet}&hinhthucthanhtoan={idhinhthucthanhtoan}&tenkhachang={hd.TenKhachHang}&sdtkh={hd.SDTKhachHang}&diachi={hd.DiaChi}&tongsoluong={hd.TongSoLuong}&thanhtien={hd.ThanhTien}&tienship={hd.TienShip}&tiengiamgia={hd.TienGiamGia}&NgayThanhToan={ngayMoi}&ngaynhanhang={ngayMoi}&GhiChu={hd.GhiChu}";
                }

                var json = JsonConvert.SerializeObject(hd);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage message = _client.PostAsync(urlapi, content).Result;
                if (message.IsSuccessStatusCode)
                {
                    var mess = message.Content.ReadAsStringAsync().Result;
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
        public ActionResult NhanHoaDon([FromBody] HoaDon hd)
        {
            try
            {
                HttpContext.Session.Remove("HoaDon");
                HttpContext.Session.SetObjectAsJson("HoaDon", hd);
                return Json(hd);
            } catch
            {
                return Json(null);
            }
        }
       
        [HttpGet]
        [HttpPost]
        public ActionResult ThanhToanOnLine()
        {

            string vnp_Returnurl = _configuration["VnpaySettings:vnp_Returnurl"]; //URL nhan ket qua tra ve 
            string vnp_Url = _configuration["VnpaySettings:vnp_Url"]; //URL thanh toan cua VNPAY 
            string vnp_TmnCode = _configuration["VnpaySettings:vnp_TmnCode"]; //Ma định danh merchant kết nối (Terminal Id)
            string vnp_HashSecret = _configuration["VnpaySettings:vnp_HashSecret"]; //Secret Key
            string ipAddress = _utils.GetIpAddress();
            long randomString = DateTime.Now.Ticks;
            //Get payment input

            HoaDon hoaDon = HttpContext.Session.GetObjectFromJson<HoaDon>("HoaDon");
            var Amount = Convert.ToInt32(hoaDon.ThanhTien + hoaDon.TienShip - hoaDon.TienGiamGia);
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


            string paymentUrl = vnpay.CreateRequestUrl(vnp_Url, vnp_HashSecret);
            _logger.LogInformation("VNPAY URL: {0}", paymentUrl);
            return Redirect(paymentUrl);

        }

        [HttpGet]
        [HttpPost]
        public ActionResult KetQuaThanhToan()
        {
            _logger.LogInformation("Begin VNPAY Return, URL={0}", HttpContext.Request.Path);
            HoaDon hd = HttpContext.Session.GetObjectFromJson<HoaDon>("HoaDon");

            if (Request.Query.Count > 0)
            {
                string vnp_HashSecret = _configuration["VnpaySettings:vnp_HashSecret"]; //Chuoi bi mat
                var vnpayData = Request.Query;
                VnPayLibrary vnpay = new VnPayLibrary();

                foreach (var pair in vnpayData)
                {
                    string key = pair.Key;
                    string value = pair.Value;
                    //get all querystring data
                    if (!string.IsNullOrEmpty(key) && key.StartsWith("vnp_"))
                    {
                        vnpay.AddResponseData(key, value);
                    }
                }

                long orderId = Convert.ToInt64(vnpay.GetResponseData("vnp_TxnRef"));
                long vnpayTranId = Convert.ToInt64(vnpay.GetResponseData("vnp_TransactionNo"));
                string vnp_ResponseCode = vnpay.GetResponseData("vnp_ResponseCode");
                string vnp_TransactionStatus = vnpay.GetResponseData("vnp_TransactionStatus");
                String vnp_SecureHash = Request.Query["vnp_SecureHash"];
                String TerminalID = Request.Query["vnp_TmnCode"];
                long vnp_Amount = Convert.ToInt64(vnpay.GetResponseData("vnp_Amount")) / 100;
                String bankCode = Request.Query["vnp_BankCode"];

                bool checkSignature = vnpay.ValidateSignature(vnp_SecureHash, vnp_HashSecret);
                if (checkSignature)
                {
                    if (vnp_ResponseCode == "00" && vnp_TransactionStatus == "00")
                    {
                        //Thanh toan thanh cong
                        ViewBag.Result = "Giao dịch được thực hiện thành công";
                        _logger.LogInformation("Thanh toan thanh cong, OrderId={0}, VNPAY TranId={1}", orderId, vnpayTranId);
                        string urlapi = "";
                        var user = HttpContext.User;
                        var email = user.FindFirstValue(ClaimTypes.Email);
                        var hinhthucthanhtoan = _hinhthuc.DanhSachThanhToan().FirstOrDefault(c => c.ID == hd.IDHinhThucThanhToan).HinhThucThanhToan;
                        var id = _userRepository.lstnguoidung().FirstOrDefault(c => c.Email == email).Id;
                        string datetime = Convert.ToString(DateTime.Now);
                       
                        DateTime ngayChuyenDoi;
                        string ngayMoi = "";
                        if (DateTime.TryParseExact(datetime, "dd/MM/yyyy HH:mm:ss", null, System.Globalization.DateTimeStyles.None, out ngayChuyenDoi))
                        {
                            ngayMoi = ngayChuyenDoi.ToString("yyyy/MM/dd");
                        }
                        //Get payment input
                        if (hd.TrangThai == 2)
                        {
                            urlapi = $"https://localhost:7265/api/DatHang/ThanhToanHoaDonTaiQuay?id={hd.ID}&idNguoiDung={id}&idthanhtoan={hd.IDHinhThucThanhToan}";
                            var json = JsonConvert.SerializeObject(hd);
                            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                            HttpResponseMessage message = _client.PutAsync(urlapi, content).Result;
                        }
                        else
                        {
                            if (hd.IDVoucherChiTiet == null)
                            {
                                urlapi = $"https://localhost:7265/api/DatHang/DatHangOnline?IdNguoiDung={id}&hinhthucthanhtoan={hd.IDHinhThucThanhToan}&tenkhachang={hd.TenKhachHang}&sdtkh={hd.SDTKhachHang}&diachi={hd.DiaChi}&tongsoluong={hd.TongSoLuong}&thanhtien={Convert.ToInt32(hd.ThanhTien)}&tienship={Convert.ToInt32(hd.TienShip)}&tiengiamgia={Convert.ToInt32(hd.TienGiamGia)}&NgayThanhToan={ngayMoi}&ngaynhanhang={ngayMoi}&GhiChu={hd.GhiChu}";
                            }
                            else
                            {
                                urlapi = $"https://localhost:7265/api/DatHang/DatHangOnline?IdNguoiDung={id}&idvoucher={hd.IDVoucherChiTiet}&hinhthucthanhtoan={hd.IDHinhThucThanhToan}&tenkhachang={hd.TenKhachHang}&sdtkh={hd.SDTKhachHang}&diachi={hd.DiaChi}&tongsoluong={hd.TongSoLuong}&thanhtien={Convert.ToInt32(hd.ThanhTien)}&tienship={Convert.ToInt32(hd.TienShip)}&tiengiamgia={Convert.ToInt32(hd.TienGiamGia)}&NgayThanhToan={ngayMoi}&ngaynhanhang={ngayMoi}&GhiChu={hd.GhiChu}";
                            }
                            var json = JsonConvert.SerializeObject(hd);
                            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                            HttpResponseMessage message = _client.PostAsync(urlapi, content).Result;
                        }

                        //var json = JsonConvert.SerializeObject(hd);
                        //StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                        //HttpResponseMessage message = _client.PostAsync(urlapi, content).Result;
                    }
                    else
                    {
                        //Thanh toan khong thanh cong. Ma loi: vnp_ResponseCode
                        ViewBag.Result = "Có lỗi xảy ra trong quá trình xử lý.Mã lỗi: " + vnp_ResponseCode;
                        _logger.LogInformation("Thanh toan loi, OrderId={0}, VNPAY TranId={1},ResponseCode={2}", orderId, vnpayTranId, vnp_ResponseCode);
                    }
                }
                else
                {
                    _logger.LogInformation("Invalid signature, InputData={0}", HttpContext.Request.Path);
                    ViewBag.Result = "Có lỗi xảy ra trong quá trình xử lý";
                }
            }
            ViewBag.TrangThai = Convert.ToString(hd.TrangThai);
            ViewBag.idhd = hd.ID;
            return View();
        }
        [HttpGet]
        [HttpPost]
        public ActionResult DanhSachSanPhamCungMau([FromBody] SanPhamChiTietViewModel spctview)
        {
            try
            {
                string url = $"https://localhost:7265/api/SanPhamChiTiet/DanhSachSanPhamHoanThienTheoMau?Tensp={spctview.TenSP}&MauSac={spctview.MauSac}";
                var reponse = _client.GetAsync(url).Result;
                string apiData = reponse.Content.ReadAsStringAsync().Result;
                List<SanPhamChiTietViewModel> spct = JsonConvert.DeserializeObject<List<SanPhamChiTietViewModel>>(apiData);
                return Json(spct);
            } catch
            {
                return Json(null);
            }
        }

        [HttpGet]
        [HttpPost]
        public ActionResult ThayDoiSanPhamTrongGioHang([FromBody] SanPhamChiTietViewModel spctview)
        {
            try
            {
                string url = $"https://localhost:7265/api/SanPhamChiTiet/DanhSachSanPhamHoanThienTheoMau?Tensp={spctview.TenSP}&MauSac={spctview.MauSac}";
                var reponse = _client.GetAsync(url).Result;
                string apiData = reponse.Content.ReadAsStringAsync().Result;
                List<SanPhamChiTietViewModel> spct = JsonConvert.DeserializeObject<List<SanPhamChiTietViewModel>>(apiData);
                return Json(spct);
            } catch
            {
                return Json(null);
            }
        }

        [HttpGet]
        [HttpPost]
        public ActionResult UpdateSoluongtronggiohang([FromBody] GioHangChiTiet ghct)
        {
            ThongBao tb = new ThongBao();
            try
            {
                var user = HttpContext.User;
                var email = user.FindFirstValue(ClaimTypes.Email);
                if(email == null)
                {
                    tb.message = "Thất bại";
                    return Json(tb);
                } else
                {
                    var id = _userRepository.lstnguoidung().FirstOrDefault(c => c.Email == email).Id;
                    string url = $"https://localhost:7265/api/GioHangChiTiet/UpdateSoLuong?idnguoidung={id}&idghct={ghct.ID}&soluong={ghct.SoLuong}";
                    var json = JsonConvert.SerializeObject(ghct);
                    StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                    HttpResponseMessage message = _client.PutAsync(url, content).Result;
                    if (message.IsSuccessStatusCode)
                    {
                        var mess = message.Content.ReadAsStringAsync().Result;
                        tb.message = mess;
                        return Json(tb);
                    }
                    else
                    {
                        tb.message = "Thất bại";
                        return Json(tb);
                    }
                }
            } catch
            {
                tb.message = "Lỗi";
                return Json(tb);
            }
            
        }
        [HttpGet]
        [HttpPost]
        public ActionResult TimKiemVoucher([FromBody] string mavoucher)
        {
            try
            {
                var user = HttpContext.User;
                var email = user.FindFirstValue(ClaimTypes.Email);
                if (email == null)
                {
                    return Json(null);
                }
                else
                {
                    var id = _userRepository.lstnguoidung().FirstOrDefault(c => c.Email == email).Id;
                    string url = $"https://localhost:7265/api/Voucher/DanhSachVMVoucherTheoMa?idnguoidung={id}&ma={mavoucher}";
                    var response = _client.GetAsync(url).Result;
                    string apiData = response.Content.ReadAsStringAsync().Result;
                    VoucherDetailViewModel result = JsonConvert.DeserializeObject<VoucherDetailViewModel>(apiData);
                    return Json(result);
                }
            } catch
            {
                return Json(null);
            }
        }

        [HttpGet]
        public ActionResult DanhSachVoucherTheoKhachHang()
        {
            try
            {
                var user = HttpContext.User;
                var email = user.FindFirstValue(ClaimTypes.Email);
                if (email == null)
                {
                    return Json(null);
                }
                else
                {
                    var id = _userRepository.lstnguoidung().FirstOrDefault(c => c.Email == email).Id;
                    string url = $"https://localhost:7265/api/Voucher/DanhSachVoucherVMByIdNguoiDungChuaSuDung?idnguoidung={id}";
                    var response = _client.GetAsync(url).Result;
                    string apiData = response.Content.ReadAsStringAsync().Result;
                    List<VoucherDetailViewModel> result = JsonConvert.DeserializeObject<List<VoucherDetailViewModel>>(apiData);
                    return Json(result);
                }
            } catch
            {
                return Json(null);
            }
        }
    }
}

