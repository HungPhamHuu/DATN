using AppData.DB_Context;
using AppData.Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos.Serialization.HybridRow;
using Microsoft.EntityFrameworkCore;
using Microsoft.JSInterop.Implementation;
using Newtonsoft.Json;
using System.Text;

namespace AppView.Controllers
{
    public class VoucherController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly ApplicationDbContext _context;
        public VoucherController() { 
            _httpClient = new HttpClient();
            _context = new ApplicationDbContext();
        }

        [HttpGet]
        public async Task<IActionResult> DanhSach()
        {
            string apiURL = "https://localhost:7265/api/Voucher/DanhSach";
            var response = await _httpClient.GetAsync(apiURL); // Lấy kết quả
                                                              // Đọc ra string Json
            string apiData = await response.Content.ReadAsStringAsync();
            // Lấy kết quả thu được bằng cách bóc dữ liệu Json
            var result = JsonConvert.DeserializeObject<List<Voucher>>(apiData);
            return View(result);
        }

        [HttpGet]
        public async Task<IActionResult> DanhSachJson()
        {
            try
            {
                string apiURL = "https://localhost:7265/api/Voucher/DanhSach";
                var response = await _httpClient.GetAsync(apiURL); // Lấy kết quả
                                                                   // Đọc ra string Json
                string apiData = await response.Content.ReadAsStringAsync();
                // Lấy kết quả thu được bằng cách bóc dữ liệu Json
                var result = JsonConvert.DeserializeObject<List<Voucher>>(apiData);
                return Json(result);
            } catch
            {
                return Json(null);
            }
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Voucher vc)
        {
            var ngaybatdau = vc.NgayBatDau.Value.ToString("yyyy/MM/dd hh:mm:ss");
            var ngaykt = vc.NgayKetThuc.Value.ToString("yyyy/MM/dd hh:mm:ss");
            string apiURL = $"https://localhost:7265/api/Voucher/CreateVC?ma={vc.MaVoucher}&NgayTao={DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss")}&NgayBatDau={ngaybatdau}&NgayKetThuc={ngaykt}&GiaTriVoucher={vc.GiaTriVoucher}&DkMin={vc.DieuKienMin}&DkMax={vc.DieuKienMax}&SoLuong={vc.SoLuong}&moTa={vc.MoTa}&TrangThai=1";
            var json = JsonConvert.SerializeObject(vc);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(apiURL, content);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("DanhSach");
            }
            ModelState.AddModelError("", "Sai rồi");

            return View(vc);
        }
        [HttpGet]
        public ActionResult UpdateVoucher(Guid id)
        {
            string apiURL = $"https://localhost:7265/api/Voucher/getByid-voucher/{id}";
            var response =  _httpClient.GetAsync(apiURL).Result;
            var apiData =  response.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<Voucher>(apiData);
            return View(result);
        }
        //[HttpPost]
        //public ActionResult UpdateVoucher(Voucher vc)
        //{
        //    string ngaybd = Convert.ToString(vc.NgayBatDau);
        //    string ngaykt = Convert.ToString(vc.NgayKetThuc);
        //    DateTime ngayChuyenDoi;
        //    string ngaybdMoi = "";
        //    string ngayktMoi = "";
        //    if (DateTime.TryParseExact(ngaybd, "dd/MM/yyyy HH:mm:ss", null, System.Globalization.DateTimeStyles.None, out ngayChuyenDoi))
        //    {
        //        ngaybdMoi = ngayChuyenDoi.ToString("yyyy/MM/dd HH:mm:ss");
        //    }
        //    if (DateTime.TryParseExact(ngaykt, "dd/MM/yyyy HH:mm:ss", null, System.Globalization.DateTimeStyles.None, out ngayChuyenDoi))
        //    {
        //        ngayktMoi = ngayChuyenDoi.ToString("yyyy/MM/dd HH:mm:ss");
        //    }
        //    string apiURL = $"https://localhost:7265/api/Voucher/UpdateVC?id={vc.ID}&NgayBatDau={ngaybdMoi}&NgayKetThuc={ngayktMoi}&GiaTriVoucher={vc.GiaTriVoucher}&DieuKienMin={vc.DieuKienMin}&DieuKienMax={vc.DieuKienMax}&SoLuong={vc.SoLuong}&MoTa={vc.MoTa}&TrangThai={vc.TrangThai}";
        //    var json = JsonConvert.SerializeObject(vc);
        //    StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
        //    HttpResponseMessage response = _httpClient.PutAsync(apiURL, content).Result;
        //    if (response.IsSuccessStatusCode)
        //    {
        //        return RedirectToAction("DanhSach");
        //    } else
        //    {
        //        return RedirectToAction("UpdateVoucher", "Voucher" , new {id = vc.ID});
        //    }
        //}
        [HttpPost]
        public String UpdateVoucher([FromBody] Voucher vc)
        {
            var trangthai = HttpContext.Session.GetInt32("trangthaivoucher");
            var vch = _context.voucher.Find(vc.ID);
            if (trangthai != null)
            {
                vch.TrangThai = Convert.ToInt32(trangthai);
            }
            vch.NgayBatDau = vc.NgayBatDau;
            vch.NgayKetThuc = vc.NgayKetThuc;
            vch.DieuKienMin = vc.DieuKienMin;
            vch.DieuKienMax = vc.DieuKienMax;
            vch.GiaTriVoucher = vc.GiaTriVoucher;
            vch.SoLuong = vc.SoLuong;
            vch.MoTa = vc.MoTa;
            _context.voucher.Update(vch);
            _context.SaveChanges();
            return "";
        }

        public IActionResult Detail(Guid id)
        {
            var httpClient = new HttpClient();
            var url = $"https://localhost:7265/api/Voucher/getByid-voucher/{id}";
            var response = httpClient.GetAsync(url).Result;
            var result = response.Content.ReadAsStringAsync().Result;
            var vc = JsonConvert.DeserializeObject<Voucher>(result);
            return View(vc);
        }
        public async Task<IActionResult> Delete(Guid id)
        {

            string apiURL = $"https://localhost:7265/api/Voucher/DeleteVC?id={id}";
            var httpClient = new HttpClient();
            var response = await httpClient.DeleteAsync(apiURL);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("DanhSach");
            }
            ModelState.AddModelError("", "Không chính xác");
            return BadRequest();
        }

        [HttpGet]
        [HttpPost]
        public int NhanTrangThai([FromBody] int trangthai)
        {
            HttpContext.Session.Remove("trangthaivoucher");
            HttpContext.Session.SetInt32("trangthaivoucher", trangthai);
            return trangthai;
        }
    }
}
