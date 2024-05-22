using AppData.Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace AppView.Areas.Admin.Controllers
{
    public class ThuongHieuController : Controller
    {
        private HttpClient _httpClient;
        public ThuongHieuController()
        {
            _httpClient = new HttpClient();
        }
        [HttpGet]
        public async Task<IActionResult> DanhSach()
        {
            string apiUrl = $"https://localhost:7265/api/ThuongHieu/DanhSach";
            var reponse = _httpClient.GetAsync(apiUrl).Result;
            var apiData = reponse.Content.ReadAsStringAsync().Result;
            List<ThuongHieu> lstTh = JsonConvert.DeserializeObject<List<ThuongHieu>>(apiData);
            return View(lstTh);
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ThuongHieu th)
        {
            string apiURL = $"https://localhost:7265/api/ThuongHieu/Create?thuongHieu={th.TenThuongHieu}";
            var content = new StringContent(JsonConvert.SerializeObject(th), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(apiURL, content);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("DanhSach", "ThuongHieu");
            }
            else
            {
                return RedirectToAction("Create", "ThuongHieu");
                //return BadRequest();
            }
        }
        [HttpGet]
        public async Task<IActionResult> ThuongHieuDetail(Guid id)
        {
            string apiUrl = $"https://localhost:7265/api/ThuongHieu/GetById/{id}";
            var httpClient = new HttpClient();
            var reponse = await httpClient.GetAsync(apiUrl);
            string apiData = await reponse.Content.ReadAsStringAsync();
            var thuongHieu = JsonConvert.DeserializeObject<ThuongHieu>(apiData);
            return View(thuongHieu);
        }
        public async Task<IActionResult> Update(Guid id)
        {
            string apiURL = $"https://localhost:7265/api/ThuongHieu/GetById/{id}";
            var response = await _httpClient.GetAsync(apiURL);
            var apiData = await response.Content.ReadAsStringAsync();
            var thuongHieu = JsonConvert.DeserializeObject<ThuongHieu>(apiData);
            return View(thuongHieu);
        }
        [HttpPost]
        public async Task<IActionResult> Update(Guid id, ThuongHieu thuongHieu)
        {
            string apiURL = $"https://localhost:7265/api/ThuongHieu/Update/{id}?name={thuongHieu.TenThuongHieu}";
            var content = new StringContent(JsonConvert.SerializeObject(thuongHieu), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync(apiURL, content);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("DanhSach", "ThuongHieu");
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            string apiURL = $"https://localhost:7265/api/ThuongHieu/Delete/{id}";
            var response = await _httpClient.DeleteAsync(apiURL);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("DanhSach", "ThuongHieu");
            }
            return BadRequest();
        }
    }
}
