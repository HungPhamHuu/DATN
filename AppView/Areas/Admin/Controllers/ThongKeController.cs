using AppData.Entities.Models;
using AppData.Entities.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos.Serialization.HybridRow;
using Newtonsoft.Json;

namespace AppView.Areas.Admin.Controllers
{
    public class ThongKeController : Controller
    {
        private readonly HttpClient _httpClient;
        public ThongKeController()
        {
            _httpClient = new HttpClient();
        }
        public ActionResult ThongkeIndex()
        {
            string apiUrl = "https://localhost:7265/api/Thongke/TongSoLuongHoaDonThanhCong";
            HttpResponseMessage response = _httpClient.GetAsync(apiUrl).Result;
            string responseBody = response.Content.ReadAsStringAsync().Result;
            ViewBag.TongSoLuongHoaDonThanhCong = Convert.ToInt32(responseBody);

            string apiUrl1 = "https://localhost:7265/api/Thongke/TongDoanhThu";
            HttpResponseMessage response1 = _httpClient.GetAsync(apiUrl1).Result;
            string responseBody1 = response1.Content.ReadAsStringAsync().Result;
            ViewBag.TongDoanhThu = Convert.ToInt32(responseBody1);

            string apiUrl2 = "https://localhost:7265/api/Thongke/TongHoaDonBiHuy";
            HttpResponseMessage response2 = _httpClient.GetAsync(apiUrl2).Result;
            string responseBody2 = response2.Content.ReadAsStringAsync().Result;
            ViewBag.TongHoaDonBiHuy = responseBody2;

            return View();
        }

        [HttpGet]
        public ActionResult Top10SanPhamBanChayNhat()
        {
            try
            {
                string apiUrl = "https://localhost:7265/api/Thongke/Top10SanPhamBanChayNhat";
                HttpResponseMessage response = _httpClient.GetAsync(apiUrl).Result;
                string responseBody = response.Content.ReadAsStringAsync().Result;
                List<Thongke> result = JsonConvert.DeserializeObject<List<Thongke>>(responseBody);
                return Json(result);
            } catch
            {
                return Json(null);
            }

        }
        [HttpGet]
        public ActionResult Top10SanPhamSaleBanChayNhat()
        {
            try
            {
                string apiUrl = "https://localhost:7265/api/Thongke/Top10SanPhamSaleBanChayNhat";
                HttpResponseMessage response = _httpClient.GetAsync(apiUrl).Result;
                string responseBody = response.Content.ReadAsStringAsync().Result;
                List<Thongke> result = JsonConvert.DeserializeObject<List<Thongke>>(responseBody);
                return Json(result);
            }
            catch
            {
                return Json(null);
            }
        }
    }
}
