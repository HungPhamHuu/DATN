using AppData.Entities.Models;
using AppData.Entities.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;

namespace AppView.Areas.KhachHang.Controllers
{
    public class SaleKHController : Controller
    {
        // GET: SaleKH
        private readonly HttpClient _client;
        public SaleKHController()
        {
            _client = new HttpClient();
        }
        [HttpGet]
        public ActionResult DanhSachSaleBan()
        {
            //var url = $"https://localhost:7265/api/SaleChiTiet/DanhSachKh";
            //var res = _client.GetAsync(url).Result;
            //var data = res.Content.ReadAsStringAsync().Result;
            //List<SaleChiTietViewModel> lst = JsonConvert.DeserializeObject<List<SaleChiTietViewModel>>(data);
            return View();
        }
        [HttpGet]
        public ActionResult DanhSachSaleJson()
        {
            try
            {
                var url = $"https://localhost:7265/api/SaleChiTiet/DanhSachKh";
                var res = _client.GetAsync(url).Result;
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
        [HttpPost]
        public ActionResult TimKiemSPSale([FromBody] string tukhoa)
        {
            try
            {
                var url = $"https://localhost:7265/api/SaleChiTiet/SanPhamSaleTheoTenSPKH?tensp={tukhoa}";
                var res = _client.GetAsync(url).Result;
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
        public ActionResult SaleChiTiet(Guid id)
        {
            var url = $"https://localhost:7265/api/SaleChiTiet/SanphamChiTiettheoID?id={id}";
            var res = _client.GetAsync(url).Result;
            var data = res.Content.ReadAsStringAsync().Result;
            SaleChiTietViewModel sale = JsonConvert.DeserializeObject<SaleChiTietViewModel>(data);
            return View(sale);
        }

        [HttpGet]
        [HttpPost]
        public ActionResult TruyVanSize([FromBody] SanPhamChiTietViewModel salect)
        {
            try
            {
                var url = $"https://localhost:7265/api/SaleChiTiet/SanPhamSaleTruyVanSize?tensp={salect.TenSP}&mau={salect.MauSac}&thuonghieu={salect.TenThuongHieu}&theloai={salect.LoaiSanPham}";
                var res = _client.GetAsync(url).Result;
                var data = res.Content.ReadAsStringAsync().Result;
                List<SaleChiTietViewModel> sale = JsonConvert.DeserializeObject<List<SaleChiTietViewModel>>(data);
                return Json(sale);
            }
            catch
            {
                return Json(null);
            }
        }
        [HttpGet]
        [HttpPost]
        public ActionResult TruyVanMau([FromBody] SanPhamChiTietViewModel salect)
        {
            try
            {
                var url = $"https://localhost:7265/api/SaleChiTiet/SanPhamSaleTruyVanMau?tensp={salect.TenSP}&size={salect.Size}&thuonghieu={salect.TenThuongHieu}&theloai={salect.LoaiSanPham}";
                var res = _client.GetAsync(url).Result;
                var data = res.Content.ReadAsStringAsync().Result;
                List<SaleChiTietViewModel> sale = JsonConvert.DeserializeObject<List<SaleChiTietViewModel>>(data);
                return Json(sale);
            }
            catch
            {
                return Json(null);
            }
        }

        [HttpGet]
        [HttpPost]
        public ActionResult TruyVanIDSale([FromBody] SanPhamChiTietViewModel salect)
        {
            try
            {
                var url = $"https://localhost:7265/api/SaleChiTiet/SanPhamSaleTheoTenSPMauSizeThuongHieuTheLoai?tensp={salect.TenSP}&mau={salect.MauSac}&size={salect.Size}&thuonghieu={salect.TenThuongHieu}&theloai={salect.LoaiSanPham}";
                var res = _client.GetAsync(url).Result;
                var data = res.Content.ReadAsStringAsync().Result;
                SaleChiTietViewModel sale = JsonConvert.DeserializeObject<SaleChiTietViewModel>(data);
                return Json(sale);
            }
            catch
            {
                return Json(null);
            }
        }
        [HttpGet]
        [HttpPost]
        public ActionResult TruyVanSanPhamTheoTenThuongHieuTheLoai([FromBody] SanPhamChiTietViewModel salect)
        {
            try
            {
                var url = $"https://localhost:7265/api/SaleChiTiet/SanPhamSaleTheoTenSpThuongHieuTheLoai?tensp={salect.TenSP}&thuonghieu={salect.TenThuongHieu}&theloai={salect.LoaiSanPham}";
                var res = _client.GetAsync(url).Result;
                var data = res.Content.ReadAsStringAsync().Result;
                List<SaleChiTietViewModel> sale = JsonConvert.DeserializeObject<List<SaleChiTietViewModel>>(data);
                return Json(sale);
            }
            catch
            {
                return Json(null);
            }

        }
        [HttpGet]
        [HttpPost]
        public ActionResult LocSanPhamSaleTheoMauSizeThuongHieuLoaiSanPhamKH([FromBody] SanPhamChiTietViewModel salect)
        {
            try
            {
                string url = $"https://localhost:7265/api/SaleChiTiet/LocSanPhamSaleTheoMauSizeThuongHieuLoaiSanPhamKH?mau={salect.MauSac}&size={salect.Size}&thuonghieu={salect.TenThuongHieu}&loaisanpham={salect.LoaiSanPham}";
                var res = _client.GetAsync(url).Result;
                var data = res.Content.ReadAsStringAsync().Result;
                List<SaleChiTietViewModel> sale = JsonConvert.DeserializeObject<List<SaleChiTietViewModel>>(data);
                return Json(sale);
            } catch
            {
                return Json(null);
            }

        }
        [HttpGet]
        [HttpPost]
        public decimal NhanGiaMaxSale([FromBody] decimal GiaMax)
        {
            string giaMaxString = GiaMax.ToString();
            HttpContext.Session.Remove("Giamaxsale");
            HttpContext.Session.SetString("Giamaxsale", giaMaxString);
            return GiaMax;
        }

        [HttpGet]
        public ActionResult LocTheoGiaCuaSanPhamSale()
        {
            try
            {
                string giaMaxString = HttpContext.Session.GetString("Giamaxsale");
                var giamax = double.Parse(giaMaxString);
                string apiUrl = $"https://localhost:7265/api/SaleChiTiet/LocTheoGia?giamax={giamax}";
                var reponse = _client.GetAsync(apiUrl).Result;
                string apiData = reponse.Content.ReadAsStringAsync().Result;
                List<SaleChiTietViewModel> DanhsachKetQua = JsonConvert.DeserializeObject<List<SaleChiTietViewModel>>(apiData);
                return Json(DanhsachKetQua);
            }
            catch
            {
                return Json(null);
            }
        }
    }
}
