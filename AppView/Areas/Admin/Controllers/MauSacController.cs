using AppData.Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace AppView.Areas.Admin.Controllers
{
    public class MauSacController : Controller
    {
        private HttpClient _httpClient;
        public MauSacController()
        {
            _httpClient = new HttpClient();
        }
        [HttpGet]
        public async Task<IActionResult> DanhSach()
        {
            string apiUrl = $"https://localhost:7265/api/MauSac/DanhSach";
            var reponse = _httpClient.GetAsync(apiUrl).Result;
            var apiData = reponse.Content.ReadAsStringAsync().Result;
            List<MauSac> lstMauSac = JsonConvert.DeserializeObject<List<MauSac>>(apiData);
            return View(lstMauSac);
        }
        [HttpGet]
        public async Task<ActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Create(MauSac mauSac)
        {
            string apiURL = $"https://localhost:7265/api/MauSac/Create?mau={mauSac.TenMauSac}";
            var content = new StringContent(JsonConvert.SerializeObject(mauSac), Encoding.UTF8, "application/json");
            var response =  _httpClient.PostAsync(apiURL, content).Result;
            if (response.IsSuccessStatusCode)
            {

                return RedirectToAction("DanhSach", "MauSac");
            }
            else
            {
                return RedirectToAction("Create", "MauSac");
                //return BadRequest();
            }
        }
        [HttpGet]
        public async Task<ActionResult> MauSacDetail(Guid id)
        {
            string apiUrl = $"https://localhost:7265/api/MauSac/GetById/{id}";
            var httpClient = new HttpClient();
            var reponse = await httpClient.GetAsync(apiUrl);
            string apiData = await reponse.Content.ReadAsStringAsync();
            var mauSac = JsonConvert.DeserializeObject<MauSac>(apiData);
            return View(mauSac);
        }
        public async Task<IActionResult> Update(Guid id)
        {
            string apiURL = $"https://localhost:7265/api/MauSac/GetById/{id}";
            var response = await _httpClient.GetAsync(apiURL);
            var apiData = await response.Content.ReadAsStringAsync();
            var mauSac = JsonConvert.DeserializeObject<MauSac>(apiData);
            return View(mauSac);
        }
        [HttpPost]
        public async Task<IActionResult> Update(Guid id, MauSac mauSac)
        {
            string apiURL = $"https://localhost:7265/api/MauSac/GetById/{id}?name={mauSac.TenMauSac}";
            var content = new StringContent(JsonConvert.SerializeObject(mauSac), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync(apiURL, content);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("DanhSach", "MauSac");
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            string apiURL = $"https://localhost:7265/api/MauSac/Delete/{id}";
            var response = await _httpClient.DeleteAsync(apiURL);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("DanhSach", "MauSac");
            }
            return BadRequest();
        }
    }
}
