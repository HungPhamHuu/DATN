using AppData.Entities.Models;
using AppData.Entities.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos.Core;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Text;

namespace AppView.Areas.KhachHang.Controllers
{
    public class TrangChuController : Controller
    {
        // GET: TrangChuController
        public readonly HttpClient _httpClient;
        public TrangChuController()
        {
            _httpClient = new HttpClient();
        }
        public ActionResult Index()
        {
            string apiUrl = "https://localhost:7265/api/SanPhamChiTiet/DanhSachSanPhamHoanThienTheoTenTheLoaiHang";
            var reponse = _httpClient.GetAsync(apiUrl).Result;
            string apiData = reponse.Content.ReadAsStringAsync().Result;
            List<SanPhamChiTietViewModel> spct = JsonConvert.DeserializeObject<List<SanPhamChiTietViewModel>>(apiData);
            ViewBag.spct = spct;

            return View(spct);
        }
        public ActionResult DanhSachSanPhamBan()
        {
            string apiUrl = "https://localhost:7265/api/SanPhamChiTiet/DanhSachSanPhamHoanThien";
            var reponse = _httpClient.GetAsync(apiUrl).Result;
            string apiData = reponse.Content.ReadAsStringAsync().Result;
            var spct = JsonConvert.DeserializeObject<List<SanPhamChiTietViewModel>>(apiData);
            return View(spct);
        }

        [HttpGet]
        public ActionResult DanhSachTheLoai()
        {
            try
            {
                string apiUrl = "https://localhost:7265/api/SanPhamChiTiet/DanhSachTheLoaiSanPham";
                var reponse = _httpClient.GetAsync(apiUrl).Result;
                string apiData = reponse.Content.ReadAsStringAsync().Result;
                List<SanPhamChiTietViewModel> danhsachtheloai = JsonConvert.DeserializeObject<List<SanPhamChiTietViewModel>>(apiData);
                return Json(danhsachtheloai);
            }
            catch
            {
                return Json(null);
            }
        }

        [HttpGet]
        public ActionResult DanhSachGiaCuaSanPham()
        {
            try
            {
                string apiUrl = "https://localhost:7265/api/SanPhamChiTiet/DanhSachGiaCuaSanPham";
                var reponse = _httpClient.GetAsync(apiUrl).Result;
                string apiData = reponse.Content.ReadAsStringAsync().Result;
                List<SanPhamChiTietViewModel> danhsachgia = JsonConvert.DeserializeObject<List<SanPhamChiTietViewModel>>(apiData);
                return Json(danhsachgia);
            }
            catch
            {
                return Json(null);
            }
        }
        [HttpGet]
        public ActionResult DanhSachThuongHieu()
        {
            try
            {
                string apiUrl = "https://localhost:7265/api/ThuongHieu/DanhSach";
                var reponse = _httpClient.GetAsync(apiUrl).Result;
                string apiData = reponse.Content.ReadAsStringAsync().Result;
                List<ThuongHieu> danhsachthuonghieu = JsonConvert.DeserializeObject<List<ThuongHieu>>(apiData);
                return Json(danhsachthuonghieu);
            }
            catch
            {
                return Json(null);
            }
        }
        [HttpGet]
        public ActionResult DanhsachSize()
        {
            try
            {
                string apiUrl = "https://localhost:7265/api/Size/DanhSach";
                var reponse = _httpClient.GetAsync(apiUrl).Result;
                string apiData = reponse.Content.ReadAsStringAsync().Result;
                List<Size> danhsachsize = JsonConvert.DeserializeObject<List<Size>>(apiData);
                return Json(danhsachsize);
            }
            catch
            {
                return Json(null);
            }
        }
        [HttpGet]
        public ActionResult DanhSachMauSac()
        {
            try
            {
                string apiUrl = "https://localhost:7265/api/MauSac/DanhSach";
                var reponse = _httpClient.GetAsync(apiUrl).Result;
                string apiData = reponse.Content.ReadAsStringAsync().Result;
                List<MauSac> danhsachmausac = JsonConvert.DeserializeObject<List<MauSac>>(apiData);
                return Json(danhsachmausac);
            }
            catch
            {
                return Json(null);
            }
        }

        [HttpGet]
        [HttpPost]
        public ActionResult LocSanPhamTheoMaiSizeThuongHieuLoaiSanPham([FromBody] SanPhamChiTietViewModel spctvm)
        {
            try
            {
                string apiUrl = $"https://localhost:7265/api/SanPhamChiTiet/LocSanPhamTheoMauSizeThuongHieuLoaiSanPham?mau={spctvm.MauSac}&size={spctvm.Size}&thuonghieu={spctvm.TenThuongHieu}&loaisanpham={spctvm.LoaiSanPham}";
                var reponse = _httpClient.GetAsync(apiUrl).Result;
                string apiData = reponse.Content.ReadAsStringAsync().Result;
                List<SanPhamChiTietViewModel> DanhsachKetQua = JsonConvert.DeserializeObject<List<SanPhamChiTietViewModel>>(apiData);
                return Json(DanhsachKetQua);
            }
            catch
            {
                return Json(null);
            }
        }

        [HttpGet]
        [HttpPost]
        public ActionResult LocTheoTheLoaiSanPham([FromBody] string theloai)
        {
            try
            {
                string apiUrl = $"https://localhost:7265/api/SanPhamChiTiet/LocTheoLoaiSanPham?loaisanpham={theloai}";
                var reponse = _httpClient.GetAsync(apiUrl).Result;
                string apiData = reponse.Content.ReadAsStringAsync().Result;
                List<SanPhamChiTietViewModel> DanhsachKetQua = JsonConvert.DeserializeObject<List<SanPhamChiTietViewModel>>(apiData);
                return Json(DanhsachKetQua);
            }
            catch
            {
                return Json(null);
            }
        }

        [HttpGet]
        [HttpPost]
        public ActionResult LocTheoThuongHieuSanPham([FromBody] string tenthuonghieu)
        {
            try
            {
                string apiUrl = $"https://localhost:7265/api/SanPhamChiTiet/LocTheoThuongHieu?thuonghieu={tenthuonghieu}";
                var reponse = _httpClient.GetAsync(apiUrl).Result;
                string apiData = reponse.Content.ReadAsStringAsync().Result;
                List<SanPhamChiTietViewModel> DanhsachKetQua = JsonConvert.DeserializeObject<List<SanPhamChiTietViewModel>>(apiData);
                return Json(DanhsachKetQua);
            }
            catch
            {
                return Json(null);
            }
        }

        [HttpPost]
        public decimal NhanGiaMax([FromBody] decimal GiaMax)
        {
            string giaMaxString = GiaMax.ToString();
            HttpContext.Session.Remove("Giamax");
            HttpContext.Session.SetString("Giamax", giaMaxString);
            return GiaMax;
        }

        [HttpPost]
        public decimal NhanGiaMin([FromBody] decimal GiaMin)
        {
            string giaMinString = GiaMin.ToString();
            HttpContext.Session.Remove("Giamin");
            HttpContext.Session.SetString("Giamin", giaMinString);
            return GiaMin;
        }

        [HttpGet]
        [HttpPost]
        public ActionResult LocTheoGiaCuaSanPham()
        {
            try
            {
                string giaMaxString = HttpContext.Session.GetString("Giamax");
                var giamax = double.Parse(giaMaxString);
                string giaMinString = HttpContext.Session.GetString("Giamin");
                var giamin = double.Parse(giaMinString);
                string apiUrl = $"https://localhost:7265/api/SanPhamChiTiet/LocTheoGia?giamin={giamin}&giamax={giamax}";
                var reponse = _httpClient.GetAsync(apiUrl).Result;
                string apiData = reponse.Content.ReadAsStringAsync().Result;
                List<SanPhamChiTietViewModel> DanhsachKetQua = JsonConvert.DeserializeObject<List<SanPhamChiTietViewModel>>(apiData);
                return Json(DanhsachKetQua);
            }
            catch
            {
                return Json(null);
            }
        }
        [HttpGet]
        [HttpPost]
        public ActionResult LocTheoGiaCuaSanPhamKeo()
        {
            try
            {
                string giaMaxString = HttpContext.Session.GetString("Giamax");
                var giamax = double.Parse(giaMaxString);
                string apiUrl = $"https://localhost:7265/api/SanPhamChiTiet/LocTheoGia?giamin=0&giamax={giamax}";
                var reponse = _httpClient.GetAsync(apiUrl).Result;
                string apiData = reponse.Content.ReadAsStringAsync().Result;
                List<SanPhamChiTietViewModel> DanhsachKetQua = JsonConvert.DeserializeObject<List<SanPhamChiTietViewModel>>(apiData);
                return Json(DanhsachKetQua);
            }
            catch
            {
                return Json(null);
            }
        }
        [HttpGet]
        [HttpPost]
        public ActionResult LocTheoSize([FromBody] string size)
        {
            try
            {
                string apiUrl = $"https://localhost:7265/api/SanPhamChiTiet/LocTheoSize?size={size}";
                var reponse = _httpClient.GetAsync(apiUrl).Result;
                string apiData = reponse.Content.ReadAsStringAsync().Result;
                List<SanPhamChiTietViewModel> DanhsachKetQua = JsonConvert.DeserializeObject<List<SanPhamChiTietViewModel>>(apiData);
                return Json(DanhsachKetQua);
            }
            catch
            {
                return Json(null);
            }

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
        [HttpPost]
        public ActionResult LayDanhSachSanPhamHoanThienTheoTen([FromBody] SanPhamChiTietViewModel spctvm)
        {
            try
            {
                string urlapi = $"https://localhost:7265/api/SanPhamChiTiet/LayDanhSachSanPhamHoanThienTheoTen?tensp={spctvm.TenSP}&thuonghieu={spctvm.TenThuongHieu}&loaisp={spctvm.LoaiSanPham}";
                var reponse = _httpClient.GetAsync(urlapi).Result;
                string apiData = reponse.Content.ReadAsStringAsync().Result;
                List<SanPhamChiTietViewModel> DanhsachKetQua = JsonConvert.DeserializeObject<List<SanPhamChiTietViewModel>>(apiData);
                return Json(DanhsachKetQua);
            }
            catch
            {
                return Json(null);
            }
        }

        [HttpGet]
        [HttpPost]
        public ActionResult TruyVanSize([FromBody] SanPhamChiTietViewModel spctvm)
        {
            try
            {
                string urlapi = $"https://localhost:7265/api/SanPhamChiTiet/DanhSachSanPhamHoanThienTheoMau?Tensp={spctvm.TenSP}&MauSac={spctvm.MauSac}&loaisp={spctvm.LoaiSanPham}&tenth={spctvm.TenThuongHieu}";
                var reponse = _httpClient.GetAsync(urlapi).Result;
                string apiData = reponse.Content.ReadAsStringAsync().Result;
                List<SanPhamChiTietViewModel> DanhsachKetQua = JsonConvert.DeserializeObject<List<SanPhamChiTietViewModel>>(apiData);
                return Json(DanhsachKetQua);
            }
            catch
            {
                return Json(null);
            }
        }

        [HttpGet]
        [HttpPost]
        public ActionResult TruyVanMau([FromBody] SanPhamChiTietViewModel spctvm)
        {
            try
            {
                string urlapi = $"https://localhost:7265/api/SanPhamChiTiet/DanhSachSanPhamHoanThienTheoSize?tensp={spctvm.TenSP}&size={spctvm.Size}&loaisp={spctvm.LoaiSanPham}&tenth={spctvm.TenThuongHieu}";
                var reponse = _httpClient.GetAsync(urlapi).Result;
                string apiData = reponse.Content.ReadAsStringAsync().Result;
                List<SanPhamChiTietViewModel> DanhsachKetQua = JsonConvert.DeserializeObject<List<SanPhamChiTietViewModel>>(apiData);
                return Json(DanhsachKetQua);
            }
            catch
            {
                return Json(null);
            }
        }
        [HttpGet]
        [HttpPost]
        public ActionResult TruyVanSanPham([FromBody] SanPhamChiTietViewModel spctvm)
        {
            try
            {
                string urlapi = $"https://localhost:7265/api/SanPhamChiTiet/SanPhamHoanThienTheotenmausize?ten={spctvm.TenSP}&mau={spctvm.MauSac}&size={spctvm.Size}&loaisp={spctvm.LoaiSanPham}&tenth={spctvm.TenThuongHieu}";
                var reponse = _httpClient.GetAsync(urlapi).Result;
                string apiData = reponse.Content.ReadAsStringAsync().Result;
                SanPhamChiTietViewModel DanhsachKetQua = JsonConvert.DeserializeObject<SanPhamChiTietViewModel>(apiData);
                return Json(DanhsachKetQua);
            }
            catch
            {
                return Json(null);
            }
        }
        [HttpGet]
        public ActionResult TimKiem(string tensp)
        {
            string apiUrl = $"https://localhost:7265/api/SanPhamChiTiet/DanhSachSanPhamHoanThienTheoTen?tensp={tensp}";
            var reponse = _httpClient.GetAsync(apiUrl).Result;
            string apiData = reponse.Content.ReadAsStringAsync().Result;

            string apiUrl1 = "https://localhost:7265/api/SanPhamChiTiet/DanhSachSanPhamHoanThienTheoTenTheLoaiHang";
            var reponse1 = _httpClient.GetAsync(apiUrl1).Result;
            string apiData1 = reponse.Content.ReadAsStringAsync().Result;
            List<SanPhamChiTietViewModel> spct1 = JsonConvert.DeserializeObject<List<SanPhamChiTietViewModel>>(apiData);
            if (!reponse.IsSuccessStatusCode)
            {
                ViewBag.spct = spct1;
            }
            else
            {
                List<SanPhamChiTietViewModel> DanhsachKetQua = JsonConvert.DeserializeObject<List<SanPhamChiTietViewModel>>(apiData);
                ViewBag.spct = DanhsachKetQua;
            }

            string apiUrlsale = $"https://localhost:7265/api/SaleChiTiet/SanPhamSaleTheoTenSPKH?tensp={tensp}";
            var reponsesale = _httpClient.GetAsync(apiUrlsale).Result;
            string apiDataSale = reponsesale.Content.ReadAsStringAsync().Result;

            var apiUrlsale1 = $"https://localhost:7265/api/SaleChiTiet/DanhSachKh";
            var reponsesale1 = _httpClient.GetAsync(apiUrlsale1).Result;
            var apiDataSale1 = reponsesale1.Content.ReadAsStringAsync().Result;
            List<SaleChiTietViewModel> lst = JsonConvert.DeserializeObject<List<SaleChiTietViewModel>>(apiDataSale1);


            if (!reponsesale.IsSuccessStatusCode)
            {
                ViewBag.spctSale = lst;
            }
            else
            {
                List<SaleChiTietViewModel> DanhsachKetQuaSale = JsonConvert.DeserializeObject<List<SaleChiTietViewModel>>(apiDataSale);
                ViewBag.spctSale = DanhsachKetQuaSale;
            }
            var user = HttpContext.User;
            var email = user.FindFirstValue(ClaimTypes.Email);
            if (email == null)
            {
                return RedirectToAction("TimKiem", "Home", new { tensp = tensp });
            }
            else
            {
                return View();
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

            return View();
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

            return View();
        }
    }
}
