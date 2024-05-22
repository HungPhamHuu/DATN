using AppData.Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace AppView.Areas.Admin.Controllers
{
    public class SanPhamController : Controller
    {
        private HttpClient _httpClient;
        public SanPhamController()
        {
            _httpClient = new HttpClient();
        }
        [HttpGet]
        public async Task<IActionResult> DanhSach()
        {
            string apiUrl = "https://localhost:7265/api/SanPham/DanhSach";
            var httpClient = new HttpClient();
            var reponse = await httpClient.GetAsync(apiUrl);
            string apiData = await reponse.Content.ReadAsStringAsync();
            var sp = JsonConvert.DeserializeObject<List<SanPham>>(apiData);
            return View(sp);
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(SanPham sanPham)
        {
            string apiUrl = $"https://localhost:7265/api/SanPham/Create?tensp={sanPham.TenSanPham}&khoiluong={sanPham.KhoiLuong}";
            var content = new StringContent(JsonConvert.SerializeObject(sanPham), Encoding.UTF8, "application/json");
            var reponse = await _httpClient.PostAsync(apiUrl, content);
            if (reponse.IsSuccessStatusCode)
            {
                return RedirectToAction("DanhSach", "SanPham");
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpGet]
        public async Task<ActionResult> SanPhamDetail(Guid id)
        {
            string apiUrl = $"https://localhost:7265/api/SanPham/id?id={id}";
            var httpClient = new HttpClient();
            var reponse = await httpClient.GetAsync(apiUrl);
            string apiData = await reponse.Content.ReadAsStringAsync();
            var sp = JsonConvert.DeserializeObject<SanPham>(apiData);
            return View(sp);
        }

        [HttpGet]
        [HttpPost]
        public ActionResult SanPhamDetail1([FromBody] Guid id)
        {
            string apiUrl = $"https://localhost:7265/api/SanPham/id?id={id}";
            var httpClient = new HttpClient();
            var reponse = httpClient.GetAsync(apiUrl).Result;
            string apiData = reponse.Content.ReadAsStringAsync().Result;
            var sp = JsonConvert.DeserializeObject<SanPham>(apiData);
            return Json(sp);
        }

        [HttpGet]
        [HttpPost]
        public ActionResult NhanTrangThai([FromBody] int trangthai)
        {
            HttpContext.Session.Remove("trangthai");

            HttpContext.Session.SetInt32("trangthai", trangthai); ;
            return Json(trangthai);
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            string apiURL = $"https://localhost:7265/api/SanPham/id?id={id}";
            var response = await _httpClient.GetAsync(apiURL);
            var apiData = await response.Content.ReadAsStringAsync();
            var sp = JsonConvert.DeserializeObject<SanPham>(apiData);
            return View(sp);
        }

        [HttpPost]
        public async Task<IActionResult> Update(SanPham sanPham)
        {

            var trangthai = HttpContext.Session.GetInt32("trangthai");
            if(trangthai == null)
            {
                string apiURL1 = $"https://localhost:7265/api/SanPham/id?id={sanPham.ID}";
                var response1 = await _httpClient.GetAsync(apiURL1);
                var apiData1 = await response1.Content.ReadAsStringAsync();
                var sp1 = JsonConvert.DeserializeObject<SanPham>(apiData1);
                trangthai = sp1.TrangThai;
            }

            string apiURL = $"https://localhost:7265/api/SanPham/Update/id?id={sanPham.ID}&tensp={sanPham.TenSanPham}&khoiluong={sanPham.KhoiLuong}&trangthai={trangthai}";
            var content = new StringContent(JsonConvert.SerializeObject(sanPham), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync(apiURL, content);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("DanhSach", "SanPham");
            }
            else
            {
                return RedirectToAction("Update", "SanPham", new { id = sanPham.ID });
            }
        }
        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            string apiURL = $"https://localhost:7265/api/SanPham/Delete/{id}";
            var response = await _httpClient.DeleteAsync(apiURL);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("DanhSach", "SanPham");
            }
            return BadRequest();
        }
    }
}
