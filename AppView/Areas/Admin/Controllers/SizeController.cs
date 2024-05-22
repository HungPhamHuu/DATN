using AppData.Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Security.Policy;
using System.Text;

namespace AppView.Areas.Admin.Controllers
{
    public class SizeController : Controller
    {
        private HttpClient _httpClient;
        public SizeController()
        {
            _httpClient = new HttpClient();
        }
        [HttpGet]
        public async Task<IActionResult> DanhSach()
        {
            string apiUrl = "https://localhost:7265/api/Size/DanhSach";
            var httpClient = new HttpClient();
            var reponse = await httpClient.GetAsync(apiUrl);
            string apiData = await reponse.Content.ReadAsStringAsync();
            var s = JsonConvert.DeserializeObject<List<Size>>(apiData);
            return View(s);
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Size size)
        {
            string apiURL = $"https://localhost:7265/api/Size/Create?size={size.SizeNumber}";
            var content = new StringContent(JsonConvert.SerializeObject(size), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(apiURL, content);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("DanhSach", "Size");
            }
            else
            {
                return RedirectToAction("Create", "Size");
                //return BadRequest();
            }
        }
        [HttpGet]
        public async Task<IActionResult> SizeDetail(Guid id)
        {
            string apiUrl = $"https://localhost:7265/api/Size/GetById/{id}";
            var httpClient = new HttpClient();
            var reponse = await httpClient.GetAsync(apiUrl);
            string apiData = await reponse.Content.ReadAsStringAsync();
            var size = JsonConvert.DeserializeObject<Size>(apiData);
            return View(size);
        }
        public async Task<IActionResult> Update(Guid id)
        {
            string apiURL = $"https://localhost:7265/api/Size/GetById/{id}";
            var response = await _httpClient.GetAsync(apiURL);
            var apiData = await response.Content.ReadAsStringAsync();
            var size = JsonConvert.DeserializeObject<Size>(apiData);
            return View(size);
        }
        [HttpPost]
        public async Task<IActionResult> Update(Guid id, Size size)
        {
            string apiURL = $"https://localhost:7265/api/Size/GetById/{id}?name={size.SizeNumber}";
            var content = new StringContent(JsonConvert.SerializeObject(size), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync(apiURL, content);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("DanhSach", "Size");
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            string apiURL = $"https://localhost:7265/api/Size/Delete/{id}";
            var response = await _httpClient.DeleteAsync(apiURL);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("DanhSach", "Size");
            }
            return BadRequest();
        }
    }

}
