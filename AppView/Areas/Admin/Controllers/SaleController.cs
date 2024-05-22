using AppData.DB_Context;
using AppData.Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Text;

namespace AppView.Areas.Admin.Controllers
{
    public class SaleController : Controller
    {
        private readonly ApplicationDbContext _context;
        public SaleController()
        {
            _context = new ApplicationDbContext();
        }
        [HttpGet]
        public async Task<IActionResult> DanhSach()
        {
            string apiURL = "https://localhost:7265/api/Sale/DanhSach";
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(apiURL); // Lấy kết quả
                                                              // Đọc ra string Json
            string apiData = await response.Content.ReadAsStringAsync();
            // Lấy kết quả thu được bằng cách bóc dữ liệu Json
            var result = JsonConvert.DeserializeObject<List<Sale>>(apiData);
            return View(result);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Sale sale)
        {
            var ngaybatdau = sale.NgayBatDau.Value.ToString("yyyy/MM/dd hh:mm:ss");
            var ngaykt = sale.NgayKetThuc.Value.ToString("yyyy/MM/dd hh:mm:ss");
            string apiURL = $"https://localhost:7265/api/Sale/CreateSale?ma={sale.MaSale}&NgayBatDau={ngaybatdau}&NgayKetThuc={ngaykt}&phanTramGiam={sale.PhanTramGiam}";
            var json = JsonConvert.SerializeObject(sale);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var httpClient = new HttpClient();
            var response = await httpClient.PostAsync(apiURL, content);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("DanhSach");
            }
            ModelState.AddModelError("", "Sai rồi");

            return View(sale);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var httpClient = new HttpClient();
            string apiURL = $"https://localhost:7265/api/Sale/getByid-sale/{id}";
            var response = await httpClient.GetAsync(apiURL);
            var apiData = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Sale>(apiData);
            return View(result);
        }
        //[HttpPut]
        //public async Task<IActionResult> Edit(Sale sale)
        //{
        //    HttpClient httpClient = new HttpClient();
        //    string apiURL = $"https://localhost:7265/api/Sale/UpdateSale/ma={sale.MaSale}&NgayBatDau={sale.NgayBatDau}&NgayKetThuc={sale.NgayKetThuc}&PhanTramGiam={sale.PhanTramGiam}&TrangThai={sale.TrangThai}";
        //    var json = JsonConvert.SerializeObject(sale);
        //    StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
        //    HttpResponseMessage response = await httpClient.PutAsync(apiURL, content);
        //    if (response.IsSuccessStatusCode)
        //    {
        //        return RedirectToAction("DanhSach");
        //    }
        //    ModelState.AddModelError("", "Sai rồi");

        //    return View(sale);
        //}
        [HttpPost]
        public String Edit([FromBody] Sale sale)
        {
            var trangthai = HttpContext.Session.GetInt32("trangthaisale") ;
            if(trangthai == null || trangthai == -1)
            {
                var httpClient = new HttpClient();
                string apiURL = $"https://localhost:7265/api/Sale/getByid-sale/{sale.ID}";
                var response =  httpClient.GetAsync(apiURL).Result;
                var apiData =  response.Content.ReadAsStringAsync().Result;
                var result = JsonConvert.DeserializeObject<Sale>(apiData);
                trangthai = result.TrangThai;
            }
            var sl = _context.sales.Find(sale.ID);
            sl.NgayBatDau = sale.NgayBatDau;
            sl.NgayKetThuc = sale.NgayKetThuc;
            sl.PhanTramGiam = sale.PhanTramGiam;
            sl.TrangThai = Convert.ToInt32(trangthai);
            _context.sales.Update(sl);
            _context.SaveChanges();
            return "";
        }

        public async Task<IActionResult> Detail(Guid id)
        {
            var httpClient = new HttpClient();
            var url = $"https://localhost:7265/api/Sale/getByid-sale/{id}";
            var response = httpClient.GetAsync(url).Result;
            var result = response.Content.ReadAsStringAsync().Result;
            var sale = JsonConvert.DeserializeObject<Sale>(result);

            //spct
            String sql1 = "SELECT SanPhamChiTiet.* FROM SanPhamChiTiet LEFT JOIN SaleChiTiet ON SaleChiTiet.IDSPCT = SanPhamChiTiet.ID\r\nWHERE SaleChiTiet.IDSPCT IS NULL";
            List<SanPhamChiTiet> spct1 = _context.sanPhamChiTiets.FromSqlRaw(
                sql1)
                                .Include(spct => spct.MauSac)
                .Include(spct => spct.SanPham)
                .Include(spct => spct.Size)
                .Include(spct => spct.ThuongHieu)
                .ToList();
            ViewBag.spct = spct1;
            String sql2 = "SELECT SanPhamChiTiet.* FROM SanPhamChiTiet LEFT JOIN SaleChiTiet ON SaleChiTiet.IDSPCT = SanPhamChiTiet.ID\r\nWHERE SaleChiTiet.IDSALE = '" + id + "'";
            List<SanPhamChiTiet> spct2 = _context.sanPhamChiTiets.FromSqlRaw(
                sql2)
                                .Include(spct => spct.MauSac)
                .Include(spct => spct.SanPham)
                .Include(spct => spct.Size)
                .Include(spct => spct.ThuongHieu)
                .ToList();
            ViewBag.spct2 = spct2;
            ViewBag.idsl = id;
            return View(sale);
        }
        [HttpGet]
        [HttpPost]
        public async Task<IActionResult> DetailJson([FromBody]string id)
        {
            var idsale = Guid.Parse(id);
            var httpClient = new HttpClient();
            var url = $"https://localhost:7265/api/Sale/getByid-sale/{idsale}";
            var response = httpClient.GetAsync(url).Result;
            var result = response.Content.ReadAsStringAsync().Result;
            var sale = JsonConvert.DeserializeObject<Sale>(result);
            return  Json(sale);
        }
            public async Task<IActionResult> Delete(Guid id)
        {

            string apiURL = $"https://localhost:7265/api/Sale/XoaCung?id={id}";
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
        public JsonResult DeleteSlct(Guid idSale, Guid idSpct)
        {
            try
            {
                SaleChiTiet spct = _context.saleChiTiets.Where(spct => spct.IDSale == idSale && spct.IDSPCT == idSpct).First();
                if (spct == null)
                {
                    return Json(new { success = false, message = "Không tìm thấy sản phẩm chi tiết" });
                }

                _context.saleChiTiets.Remove(spct);
                _context.SaveChangesAsync();

                return Json(new { success = true, message = "Xóa sản phẩm chi tiết thành công" });
            }
            catch
            {
                return Json(new { success = false, message = "Lỗi." });
            }
        }


        [HttpGet]
        public JsonResult AddSlct(Guid idSale, Guid idSpct)
        {
            try
            {
                SaleChiTiet slct = new SaleChiTiet();
                slct.IDSale = idSale;
                slct.IDSPCT = idSpct;
                _context.saleChiTiets.Add(slct);
                _context.SaveChanges();
                return Json(new { success = true, message = "Thêm spct" });
            } catch
            {
                return Json(new { success = false, message = "Lỗi." });
            }
        }
        [HttpGet]
        [HttpPost]
        public int NhanTrangThai([FromBody] int trangthai)
        {
            HttpContext.Session.Remove("trangthaisale");
            HttpContext.Session.SetInt32("trangthaisale" , trangthai);
            return trangthai;
        }
    }
}
