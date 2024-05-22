using AppData.Entities.Models;
using AppData.Entities.ViewModels;
using AppData.Entities.ViewModels.GioHangCTVM;
using AppData.Repositories.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos.Serialization.HybridRow;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Text;

namespace AppView.Areas.KhachHang.Controllers
{
    public class GioHangChiTietController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly HttpClient httpClient;
        public GioHangChiTietController()
        {
            _userRepository = new UserRepository();
            httpClient = new HttpClient();
        }
        [HttpGet]
        public async Task<IActionResult> ShowAllCartDetail()
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
                string apiUrl = $"https://localhost:7265/api/GioHangChiTiet/GioHangChitietTheoIDNguoiDung?{id}";
                var httpClient = new HttpClient();
                var response = await httpClient.GetAsync(apiUrl);
                string apiData = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<GioHangCTVM>>(apiData);
                return View(result);
            }
        }
        [HttpGet]
        [HttpPost]
        public async Task<IActionResult> CreateCartDetail([FromBody] GioHangChiTiet gioHangChiTiet)
        {
            ThongBao tb = new ThongBao();
            try
            {
                var user = HttpContext.User;
                var email = user.FindFirstValue(ClaimTypes.Email);
                if (email == null)
                {
                    tb.message = "Đăng nhập trước khi mua hàng.";
                    return Json(tb);
                }
                else
                {
                    if (gioHangChiTiet.SoLuong == null || gioHangChiTiet.SoLuong == 0 || gioHangChiTiet.SoLuong < 0)
                    {
                        tb.message = "Yêu cầu sản phẩm lớn hơn 1.";
                        return Json(tb);
                    };

                    var id = _userRepository.lstnguoidung().FirstOrDefault(c => c.Email == email).Id;
                    string apiUrl = $"https://localhost:7265/api/GioHangChiTiet/add?idnguoidung={id}&idsanphamct={gioHangChiTiet.IDSPCT}&soluong={gioHangChiTiet.SoLuong}";
                    var httpClient = new HttpClient();
                    var content = new StringContent(JsonConvert.SerializeObject(gioHangChiTiet), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await httpClient.PostAsync(apiUrl, content);

                    if (response.IsSuccessStatusCode)
                    {
                        var mess = await response.Content.ReadAsStringAsync();
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
        public async Task<IActionResult> UpdateCartDetail(Guid id)
        {
            string apiUrl = $"https://localhost:7265/api/GioHangChiTiet/detail?id={id}";
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(apiUrl);

            string apiData = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<GioHangCTRequestVM>(apiData);
            return View(result);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateCartDetail(GioHangCTRequestVM gioHangCTRequestVM)
        {
            string apiUrl = $"https://localhost:7265/api/GioHangChiTiet/update?ID={gioHangCTRequestVM.Id}&IdGioHang={gioHangCTRequestVM.IdGioHang}" +
                $"&IDSPCT={gioHangCTRequestVM.IDSPCT}&SoLuong={gioHangCTRequestVM.SoLuong}";
            var httpClient = new HttpClient();
            var content = new StringContent(JsonConvert.SerializeObject(gioHangCTRequestVM), Encoding.UTF8, "application/json");
            var response = await httpClient.PutAsync(apiUrl, content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("ShowAllCartDetail");
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        [HttpPost]
        public async Task<IActionResult> DeleteCartDetail([FromBody] Guid id)
        {
            ThongBao tb = new ThongBao();
            try
            {
                string apiUrl = $"https://localhost:7265/api/GioHangChiTiet/delete/{id}";
                var httpClient = new HttpClient();
                HttpResponseMessage response = await httpClient.DeleteAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var mess = await response.Content.ReadAsStringAsync();

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
        public ActionResult SanPhamChiTietTheoTenVaMauu([FromBody] SanPhamChiTietViewModel spct)
        {
            try
            {
                string apiUrl = $"https://localhost:7265/api/SanPhamChiTiet/DanhSachSanPhamHoanThienTheoMau?Tensp={spct.TenSP}&MauSac={spct.MauSac}";
                var response = httpClient.GetAsync(apiUrl).Result;
                string apiData = response.Content.ReadAsStringAsync().Result;
                List<SanPhamChiTietViewModel> result = JsonConvert.DeserializeObject<List<SanPhamChiTietViewModel>>(apiData);
                return Json(result);
            } catch
            {
                return Json(null);
            }
        }
    }
}
