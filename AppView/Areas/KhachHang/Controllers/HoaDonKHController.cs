using AppData.Entities.Models;
using AppData.Entities.ViewModels;
using AppData.Repositories.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos.Serialization.HybridRow;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Text;

namespace AppView.Areas.KhachHang.Controllers
{
    public class HoaDonKHController : Controller
    {
        // GET: HoaDonController
        private readonly HttpClient _httpClient;
        private readonly IUserRepository userRepository;
        public HoaDonKHController()
        {
            _httpClient = new HttpClient();
            userRepository = new UserRepository();
        }
        public string IDNguoiDung()
        {
            var user = HttpContext.User;
            var email = user.FindFirstValue(ClaimTypes.Email);
            if(email == null)
            {
                return null;
            } else
            {
                var id = userRepository.lstnguoidung().FirstOrDefault(c => c.Email == email).Id;
                return id.ToString();
            }
           
        }
        [HttpGet]
        public ActionResult DanhSachHoaDonTheoNguoiDungKH()
        {
            return View();
        }
        [HttpGet]
        public ActionResult DanhSachHoaDonTheoNguoiDungKHJson()
        {
            try
            {
                var id = IDNguoiDung();
                if (id == null)
                {
                    return Json(null);
                }
                else
                {
                    string urlapi = $"https://localhost:7265/api/HoaDon/GetAllHoaDonTheoIDNguoiDung?idnguoidung={id}";
                    var response = _httpClient.GetAsync(urlapi).Result;
                    string apiData = response.Content.ReadAsStringAsync().Result;
                    List<HoaDon> result = JsonConvert.DeserializeObject<List<HoaDon>>(apiData);
                    return Json(result);
                }
            } catch
            {
                return Json(null);
            }
        }

        [HttpGet]
        [HttpPost]
        public ActionResult DanhSachTrangThaiHoaDonTheoNguoiDungKHJson([FromBody]int trangthai)
        {
            try
            {
                var id = IDNguoiDung();
                if (id == null)
                {
                    return Json(null);
                }
                else
                {
                    string urlapi = $"https://localhost:7265/api/HoaDon/GetAllHoaDonTrangThaiTheoIDNguoiDung?idnguoidung={id}&trangthai={trangthai}";
                    var response = _httpClient.GetAsync(urlapi).Result;
                    string apiData = response.Content.ReadAsStringAsync().Result;
                    List<HoaDon> result = JsonConvert.DeserializeObject<List<HoaDon>>(apiData);
                    return Json(result);
                }
            }
            catch
            {
                return Json(null);
            }
        }

        [HttpGet]
        public ActionResult HoaDonChiTietKH(Guid idhd)
        {
            string urlapi = $"https://localhost:7265/api/HoaDonCT/DanhSachHDChiTietTheoIDhd?idhd={idhd}";
            var response = _httpClient.GetAsync(urlapi).Result;
            string apiData = response.Content.ReadAsStringAsync().Result;
            List<HoaDonChiTietViewModel> result = JsonConvert.DeserializeObject<List<HoaDonChiTietViewModel>>(apiData);

            string hoadon = $"https://localhost:7265/api/HoaDon/id?id={idhd}";
            var response1 = _httpClient.GetAsync(hoadon).Result;
            string apiData1 = response1.Content.ReadAsStringAsync().Result;
            HoaDon result1 = JsonConvert.DeserializeObject<HoaDon>(apiData1);
            HttpContext.Session.Remove("idhd");
            HttpContext.Session.SetString("idhd", Convert.ToString(idhd));
            ViewBag.HoaDon = result1;
            return View(result);
        }
        [HttpGet]
        [HttpPost]
        public ActionResult HuyDonHang([FromBody] HoaDon hoaDon)
        {
            ThongBao tb = new ThongBao();
            try
            {
                string url = $"https://localhost:7265/api/HoaDon/HuyDonHang?id={hoaDon.ID}";
                var obj = JsonConvert.SerializeObject(hoaDon);
                StringContent content = new StringContent(obj, Encoding.UTF8, "application/json");
                HttpResponseMessage message = _httpClient.PutAsync(url, content).Result;
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
        [HttpGet]
        [HttpPost]
        public ActionResult DaNhan([FromBody] HoaDon hoaDon)
        {
            ThongBao tb = new ThongBao();
            try
            {
                string url = $"https://localhost:7265/api/HoaDon/DaNhan?id={hoaDon.ID}";
                var obj = JsonConvert.SerializeObject(hoaDon);
                StringContent content = new StringContent(obj, Encoding.UTF8, "application/json");
                HttpResponseMessage message = _httpClient.PutAsync(url, content).Result;
                if (message.IsSuccessStatusCode)
                {
                    var mess = message.Content.ReadAsStringAsync().Result;
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
        public ActionResult DangGiao([FromBody] HoaDon hoaDon)
        {
            ThongBao tb = new ThongBao();
            try
            {
                string url = $"https://localhost:7265/api/HoaDon/DangGiao?id={hoaDon.ID}";
                var obj = JsonConvert.SerializeObject(hoaDon);
                StringContent content = new StringContent(obj, Encoding.UTF8, "application/json");
                HttpResponseMessage message = _httpClient.PutAsync(url, content).Result;
                if (message.IsSuccessStatusCode)
                {
                    var mess = message.Content.ReadAsStringAsync().Result;
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
        public ActionResult XacNhanDonHang([FromBody] HoaDon hoaDon)
        {
            ThongBao tb = new ThongBao();
            try
            {
                string url = $"https://localhost:7265/api/HoaDon/XacNhan?id={hoaDon.ID}";
                var obj = JsonConvert.SerializeObject(hoaDon);
                StringContent content = new StringContent(obj, Encoding.UTF8, "application/json");
                HttpResponseMessage message = _httpClient.PutAsync(url, content).Result;
                if (message.IsSuccessStatusCode)
                {
                    var mess = message.Content.ReadAsStringAsync().Result;
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
        public ActionResult UpdateHoaDon([FromBody] HoaDon hoaDon)
        {
            ThongBao tb = new ThongBao();
            try
            {
                string url = $"https://localhost:7265/api/HoaDon/Updatehoadon?idhd={hoaDon.ID}";
                var obj = JsonConvert.SerializeObject(hoaDon);
                StringContent content = new StringContent(obj, Encoding.UTF8, "application/json");
                HttpResponseMessage message = _httpClient.PutAsync(url, content).Result;
                if (message.IsSuccessStatusCode)
                {
                    var mess = message.Content.ReadAsStringAsync().Result;
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
        public ActionResult HoaDonDetail([FromBody] Guid id)
        {
            try
            {
                string urlapi = $"https://localhost:7265/api/HoaDon/id?id={id}";
                var response = _httpClient.GetAsync(urlapi).Result;
                string apiData = response.Content.ReadAsStringAsync().Result;
                HoaDon result = JsonConvert.DeserializeObject<HoaDon>(apiData);
                return Json(result);
            } catch
            {
                return Json(null);
            }
           
        }
        [HttpGet]
        public ActionResult DanhSachSanPhamThem()
        {
            return View();
        }
        [HttpGet]
        public ActionResult DanhSachSanPhamThemJson()
        {
            try
            {
                string apiUrl = "https://localhost:7265/api/SanPhamChiTiet/DanhSachSanPhamHoanThien";
                var reponse = _httpClient.GetAsync(apiUrl).Result;
                string apiData = reponse.Content.ReadAsStringAsync().Result;
                var spct = JsonConvert.DeserializeObject<List<SanPhamChiTietViewModel>>(apiData);
                return Json(spct);
            } catch
            {
                return Json(null);
            }
         
        }
        [HttpGet]
        public ActionResult SanPhamChiTietThem(Guid idspct)
        {
            string urlapi = $"https://localhost:7265/api/SanPhamChiTiet/SanPhamHoanThienByID?id={idspct}";
            var reponse = _httpClient.GetAsync(urlapi).Result;
            string apiData = reponse.Content.ReadAsStringAsync().Result;
            SanPhamChiTietViewModel DanhsachKetQua = JsonConvert.DeserializeObject<SanPhamChiTietViewModel>(apiData);
            return View(DanhsachKetQua);
        }
        [HttpGet]
        [HttpPost]
        public ActionResult ThemVaoHDCT([FromBody] HoaDonChiTiet hdct)
        {
            ThongBao tb = new ThongBao();
            try
            {
                string url = $"https://localhost:7265/api/HoaDonCT/Create?idhd={hdct.IDHoaDon}&idspct={hdct.IDSPCT}&soluong={hdct.SoLuong}";
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
        public ActionResult UpdateThongTinKHHD([FromBody] HoaDon hoaDon)
        {
            ThongBao tb = new ThongBao();

            try
            {
                string url = $"https://localhost:7265/api/HoaDon/UpdateThongTinKhachHangTrongHD?idhd={hoaDon.ID}&tenkhach={hoaDon.TenKhachHang}&diachi={hoaDon.DiaChi}&sdt={hoaDon.SDTKhachHang}";
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
                tb.message = "Lỗi";
                return Json(tb);
            }
            
        }
        //public string XuatHoaDon()
        //{
        //    using (FileStream fs = new FileStream("New.pdf", FileMode.Create)) ;
        //}
        [HttpGet]
        [HttpPost]
        public ActionResult HoanHang([FromBody] HoaDon hoaDon)
        {
            ThongBao tb = new ThongBao();
            try
            {
                string url = $"https://localhost:7265/api/HoaDon/HoanHang?id={hoaDon.ID}";
                var obj = JsonConvert.SerializeObject(hoaDon);
                StringContent content = new StringContent(obj, Encoding.UTF8, "application/json");
                HttpResponseMessage message = _httpClient.PutAsync(url, content).Result;
                if (message.IsSuccessStatusCode)
                {
                    var mess = message.Content.ReadAsStringAsync().Result;
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
        public ActionResult XacNhanHoanHang([FromBody] HoaDon hoaDon)
        {
            ThongBao tb = new ThongBao();
            try
            {
                string url = $"https://localhost:7265/api/HoaDon/XacNhanHoanHang?id={hoaDon.ID}";
                var obj = JsonConvert.SerializeObject(hoaDon);
                StringContent content = new StringContent(obj, Encoding.UTF8, "application/json");
                HttpResponseMessage message = _httpClient.PutAsync(url, content).Result;
                if (message.IsSuccessStatusCode)
                {
                    var mess = message.Content.ReadAsStringAsync().Result;
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

    }
}
