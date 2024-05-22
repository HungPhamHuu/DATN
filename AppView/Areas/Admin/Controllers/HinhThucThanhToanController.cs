using AppData.Entities.Models;
using AppData.Entities.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Security.Policy;
using System.Text;

namespace AppView.Areas.Admin.Controllers
{
    public class HinhThucThanhToanController : Controller
    {
        private readonly HttpClient _httpClient;
        public HinhThucThanhToanController()
        {
            _httpClient = new HttpClient();
        }
        // GET: HinhThucThanhToanController
        [HttpGet]
        public ActionResult DanhSachHinhThuc()
        {
            string urlapi = $"https://localhost:7265/api/HinhThucThanhToan/DanhSachThanhToan";
            var reponse = _httpClient.GetAsync(urlapi).Result;
            var apiData = reponse.Content.ReadAsStringAsync().Result;
            List<ThanhToan> lstThanhToan = JsonConvert.DeserializeObject<List<ThanhToan>>(apiData);
            return View(lstThanhToan);
        }

        [HttpGet]
        public ActionResult DanhSachHinhThucJson()
        {
            string urlapi = $"https://localhost:7265/api/HinhThucThanhToan/DanhSachThanhToan";
            var reponse = _httpClient.GetAsync(urlapi).Result;
            var apiData = reponse.Content.ReadAsStringAsync().Result;
            List<ThanhToan> lstThanhToan = JsonConvert.DeserializeObject<List<ThanhToan>>(apiData);
            return Json(lstThanhToan);
        }

        // GET: HinhThucThanhToanController/Details/5
        [HttpGet]
        [HttpPost]
        public ActionResult ThemHinhThuc(ThanhToan thanhtoan)
        {
            string urlapi = $"https://localhost:7265/api/HinhThucThanhToan/ThemHinhThuc?HinhThucThanhToan={thanhtoan.HinhThucThanhToan}";
            var content = new StringContent(JsonConvert.SerializeObject(thanhtoan), Encoding.UTF8, "application/json");
            var response = _httpClient.PostAsync(urlapi, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("DanhSachHinhThuc", "HinhThucThanhToan");
            }
            else
            {
                return View();
            }
        }
        [HttpGet]
        [HttpPost]
        // GET: HinhThucThanhToanController/Create
        public ActionResult XoaHinhThuc(ThanhToan thanhtoan)
        {
            string url = $"https://localhost:7265/api/HinhThucThanhToan/XoaHinhThuc?id={thanhtoan.ID}";
            var content = new StringContent(JsonConvert.SerializeObject(thanhtoan), Encoding.UTF8, "application/json");
            var response = _httpClient.PutAsync(url, content).Result;
            return RedirectToAction("DanhSachHinhThuc", "HinhThucThanhToan");
        }
        [HttpGet]
        [HttpPost]
        public ActionResult KichHoat(ThanhToan thanhtoan)
        {
            string urlapi = $"https://localhost:7265/api/HinhThucThanhToan/KichHoatHinhThuc?id={thanhtoan.ID}";
            var content = new StringContent(JsonConvert.SerializeObject(thanhtoan), Encoding.UTF8, "application/json");
            var response = _httpClient.PutAsync(urlapi, content).Result;
            return RedirectToAction("DanhSachHinhThuc", "HinhThucThanhToan");
        }
    }
}
