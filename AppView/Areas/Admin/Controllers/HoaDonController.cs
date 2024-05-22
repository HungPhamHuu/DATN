using AppData.DB_Context;
using AppData.Entities.Models;
using AppData.Entities.ViewModels;
using AppData.Repositories.BillDetails;
using AppData.Repositories.Bills;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos.Serialization.HybridRow;
using Newtonsoft.Json;
using System.Text;

namespace AppView.Areas.Admin.Controllers
{
    public class HoaDonController : Controller
    {
        private readonly IBillRepository _billRepo;
        private readonly IBillDetailRepository _billDtRepo;
        private readonly XuatHoaDon xuathoadon;
        private readonly HttpClient client;
        public HoaDonController(ApplicationDbContext dbContext)
        {
            _billRepo = new BillRepository(dbContext);
            client = new HttpClient();
            xuathoadon = new XuatHoaDon();
            _billDtRepo = new BillDetailRepository(dbContext);
        }

        [HttpGet]
        public async Task<IActionResult> DanhSachHoaDon()
        {         
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> DanhSachHoaDonJson()
        {
            try
            {
                string apiUrl = "https://localhost:7265/api/HoaDon/Index";
                var response = await client.GetAsync(apiUrl);
                string apiData = await response.Content.ReadAsStringAsync();
                List<HoaDon> result = JsonConvert.DeserializeObject<List<HoaDon>>(apiData);
                return Json(result);
            } catch
            {
                return Json(null);
            }
        }
        [HttpGet]
        [HttpPost]
        public async Task<IActionResult> DanhSachHoaDonTheoTuKhoaJson([FromBody] string tukhoa)
        {
            try
            {
                string apiUrl = $"https://localhost:7265/api/HoaDon/DanhSachTheoTenvaSDT?tukhoa={tukhoa}";
                var response = await client.GetAsync(apiUrl);
                string apiData = await response.Content.ReadAsStringAsync();
                List<HoaDon> result = JsonConvert.DeserializeObject<List<HoaDon>>(apiData);
                return Json(result);
            } catch
            {
                return Json(null);
            }
            
        }
        [HttpGet]
        [HttpPost]
        public async Task<IActionResult> DanhSachHoaDonTheoTrangThaiJson([FromBody] int trangthai)
        {
            try
            {
                string apiUrl = $"https://localhost:7265/api/HoaDon/DanhSachTheoTrangThai?trangthai={trangthai}";
                var response = await client.GetAsync(apiUrl);
                string apiData = await response.Content.ReadAsStringAsync();
                List<HoaDon> result = JsonConvert.DeserializeObject<List<HoaDon>>(apiData);
                return Json(result);
            } catch
            {
                return Json(null);
            }
            
        }
        [HttpGet]
        public ActionResult HoaDonChiTiet(Guid idhd)
        {
            string urlapi = $"https://localhost:7265/api/HoaDonCT/DanhSachHDChiTietTheoIDhd?idhd={idhd}";
            var response =  client.GetAsync(urlapi).Result;
            string apiData = response.Content.ReadAsStringAsync().Result;
            List<HoaDonChiTietViewModel> result = JsonConvert.DeserializeObject<List<HoaDonChiTietViewModel>>(apiData);

            string hoadon = $"https://localhost:7265/api/HoaDon/id?id={idhd}";
            var response1 = client.GetAsync(hoadon).Result;
            string apiData1 = response1.Content.ReadAsStringAsync().Result;
            HoaDon result1 = JsonConvert.DeserializeObject<HoaDon>(apiData1);
            HttpContext.Session.Remove("idhd");
            HttpContext.Session.SetString("idhd", Convert.ToString(idhd));
            ViewBag.HoaDon = result1;
            return View(result);
        }
        [HttpPost]
        public async Task<IActionResult> Create(HoaDon hoaDon)
        {
            if (!ModelState.IsValid)
            {
                return View(hoaDon);
            }

            var client = new HttpClient();
            string apiUrl = $"https://localhost:7625/api/HoaDon";

            var json = JsonConvert.SerializeObject(hoaDon);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(apiUrl, content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("ShowHoaDon");
            }
            ModelState.AddModelError("", "Thêm Thất Bại");
            return View();
        }

        [HttpPut]
        public async Task<IActionResult> Update(Guid id)
        {
            var client = new HttpClient();
            string apiUrl = $"https://localhost:7625/api/HoaDon{id}";

            var response = await client.GetAsync($"{apiUrl}/{id}");
            string apiData = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<HoaDon>(apiData);
            return View(result);
        }
        public async Task<IActionResult> Update(HoaDon hoaDon)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri($"https://localhost:7625/api/HoaDon");
                var putTalk = client.PutAsJsonAsync(client.BaseAddress, hoaDon);
                putTalk.Wait();

                var result = putTalk.Result;
                if (result != null)
                {
                    return RedirectToAction("ShowHoaDon");
                }

                return View();
            }
        }
        [HttpGet]
        [HttpPost]
        public async Task<IActionResult> Delete([FromBody]Guid id)
        {
            ThongBao tb = new ThongBao();
            try
            {
                string url = $"https://localhost:7265/api/HoaDon/id?id={id}";
                var response = await client.DeleteAsync(url);
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
        public ActionResult InHoaDon([FromBody] Guid idHD)
        {
            ThongBao tb = new ThongBao();
            try
            {
                var mess = xuathoadon.InHoaDon(idHD);
                tb.message = mess;
                return Json(tb);
            } catch
            {
                tb.message = "Lỗi.";
                return Json(tb);
            }
        }
    }
}
