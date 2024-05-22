using AppData.Entities.Models;
using AppData.Entities.ViewModels;
using AppView.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http;
using System.Security.Claims;

namespace AppView.Controllers
{
    public class HomeController : Controller
    {
        HttpClient _httpClient;

        public HomeController()
        {
            _httpClient = new HttpClient();
        }
        [HttpGet]
        public IActionResult Index()
        {
            string apiUrl = "https://localhost:7265/api/SanPhamChiTiet/DanhSachSanPhamHoanThien";
            var reponse = _httpClient.GetAsync(apiUrl).Result;
            string apiData = reponse.Content.ReadAsStringAsync().Result;
            List<SanPhamChiTietViewModel> spct = JsonConvert.DeserializeObject<List<SanPhamChiTietViewModel>>(apiData);
            ViewBag.spct = spct;
            var url = $"https://localhost:7265/api/SaleChiTiet/DanhSachKh";
            var res = _httpClient.GetAsync(url).Result;
            var data = res.Content.ReadAsStringAsync().Result;
            List<SaleChiTietViewModel> lstsale = JsonConvert.DeserializeObject<List<SaleChiTietViewModel>>(data);
            ViewBag.sale = lstsale;
            if (spct == null)
            {
                return View("Error");
            }
            else
            {
                return View(spct);
            }
        }
        [HttpGet]
        public ActionResult TimKiem(string tensp)
        {
            string apiUrl = $"https://localhost:7265/api/SanPhamChiTiet/DanhSachSanPhamHoanThienTheoTen?tensp={tensp}";
            var reponse = _httpClient.GetAsync(apiUrl).Result;
            string apiData = reponse.Content.ReadAsStringAsync().Result;

            List<SanPhamChiTietViewModel> DanhsachKetQua = JsonConvert.DeserializeObject<List<SanPhamChiTietViewModel>>(apiData);
            ViewBag.spct = DanhsachKetQua;


            string apiUrlsale = $"https://localhost:7265/api/SaleChiTiet/SanPhamSaleTheoTenSPKH?tensp={tensp}";
            var reponsesale = _httpClient.GetAsync(apiUrlsale).Result;
            string apiDataSale = reponsesale.Content.ReadAsStringAsync().Result;
            List<SaleChiTietViewModel> DanhsachKetQuaSale = JsonConvert.DeserializeObject<List<SaleChiTietViewModel>>(apiDataSale);
            ViewBag.spctSale = DanhsachKetQuaSale;

            var user = HttpContext.User;
            var email = user.FindFirstValue(ClaimTypes.Email);
            if (email == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("TimKiem", "TrangChu", new { area = "KhachHang", tensp = tensp });

            }
        }

        [HttpGet]
        public ActionResult LocTheoThuongHieu(string TenThuongHieu)
        {
            string apiUrl = $"https://localhost:7265/api/SanPhamChiTiet/LocTheoThuongHieu?thuonghieu={TenThuongHieu}";
            var reponse = _httpClient.GetAsync(apiUrl).Result;
            string apiData = reponse.Content.ReadAsStringAsync().Result;
            List<SanPhamChiTietViewModel> DanhsachKetQua = JsonConvert.DeserializeObject<List<SanPhamChiTietViewModel>>(apiData);
            ViewBag.spct = DanhsachKetQua;

            string apiUrlsale = $"https://localhost:7265/api/SaleChiTiet/SanPhamSaleTheoThuonghieu?thuonghieu={TenThuongHieu}";
            var reponsesale = _httpClient.GetAsync(apiUrlsale).Result;
            string apiDataSale = reponsesale.Content.ReadAsStringAsync().Result;
            List<SaleChiTietViewModel> DanhsachKetQuaSale = JsonConvert.DeserializeObject<List<SaleChiTietViewModel>>(apiDataSale);
            ViewBag.spctSale = DanhsachKetQuaSale;

            return RedirectToAction("LocTheoThuongHieu", "TrangChu", new { area = "KhachHang", TenThuongHieu = TenThuongHieu });
        }

        [HttpGet]
        public ActionResult LocTheoLoaisanpham(string loaisanpham)
        {
            string apiUrl = $"https://localhost:7265/api/SanPhamChiTiet/LocTheoLoaiSanPham?loaisanpham={loaisanpham}";
            var reponse = _httpClient.GetAsync(apiUrl).Result;
            string apiData = reponse.Content.ReadAsStringAsync().Result;
            List<SanPhamChiTietViewModel> DanhsachKetQua = JsonConvert.DeserializeObject<List<SanPhamChiTietViewModel>>(apiData);
            ViewBag.spct = DanhsachKetQua;

            string apiUrlsale = $"https://localhost:7265/api/SaleChiTiet/SanPhamSaleTheoLoaisanpham?loaisp={loaisanpham}";
            var reponsesale = _httpClient.GetAsync(apiUrlsale).Result;
            string apiDataSale = reponsesale.Content.ReadAsStringAsync().Result;
            List<SaleChiTietViewModel> DanhsachKetQuaSale = JsonConvert.DeserializeObject<List<SaleChiTietViewModel>>(apiDataSale);
            ViewBag.spctSale = DanhsachKetQuaSale;

            return RedirectToAction("LocTheoLoaisanpham", "TrangChu", new { area = "KhachHang", loaisanpham = loaisanpham });

        }

    }
}