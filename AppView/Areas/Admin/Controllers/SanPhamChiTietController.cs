using AppData.DB_Context;
using AppData.Entities.Models;
using AppData.Entities.ViewModels;
using AppData.Repositories.Anhs;
using AppData.Repositories.Color;
using AppData.Repositories.Product;
using AppData.Repositories.ProductDetail;
using AppData.Repositories.Sizes;
using AppData.Repositories.TH;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using OfficeOpenXml;
using System.ComponentModel;
using System.Drawing;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using LicenseContext = OfficeOpenXml.LicenseContext;

namespace AppView.Areas.Admin.Controllers
{
    public class SanPhamChiTietController : Controller
    {
        private HttpClient _httpClient;
        private readonly ApplicationDbContext _context;
        private readonly IAnh anhservice;
        private readonly List<Anh> anhSanPhams = new List<Anh>();
        private readonly IAnhSanPhamRes _ianhsanpham;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IProductDetailRepository _sanphamchitietservice;
        private readonly IMauSacRepository _mausac;
        private readonly ISizeRepository _size;
        private readonly IProductRepository _sanpham;
        private readonly IThuongHieuRepository _thuonghieu;
        private readonly IAnh _anh;
        private readonly IAnhSanPhamRes _anhsanpham;
        private static List<SanPhamChiTiet> _tempProducts;
        private static List<string> _tempAnh;
        public static List<SanPhamChiTietViewModel> _temspctvm;
        public readonly List<Anh> anhsanpham = new List<Anh>();
        private readonly List<string> path = new List<string>();
        public SanPhamChiTietController(ApplicationDbContext context, IWebHostEnvironment hostingEnvironment)
        {
            _httpClient = new HttpClient();
            _mausac = new MauSacRepository(context);
            _size = new SizeRepository(context);
            _sanpham = new ProductRepository(context);
            _thuonghieu = new ThuongHieuRepository(context);
            _anh = new AnhReps(context);
            _anhsanpham = new AnhSanPhamRes();
            _context = context;
            _hostingEnvironment = hostingEnvironment;
            _sanphamchitietservice = new ProductDetailRepository(_context);
            _ianhsanpham = new AnhSanPhamRes();
            anhservice = new AnhReps(_context);
        }
        [HttpGet]
        public async Task<IActionResult> DanhSach()
        {
            string apiUrl = "https://localhost:7265/api/SanPhamChiTiet/DanhSach";
            var httpClient = new HttpClient();
            var reponse = await httpClient.GetAsync(apiUrl);
            string apiData = await reponse.Content.ReadAsStringAsync();
            var spct = JsonConvert.DeserializeObject<List<SanPhamChiTiet>>(apiData);
            return View(spct);
        }
        [HttpGet]
        [HttpPost]
        public IActionResult TimKiemTheoTenSanpham([FromBody] string tensp)
        {
            try
            {
                string apiUrl = $"https://localhost:7265/api/SanPhamChiTiet/DanhSachSanPhamHoanThienTheoTen?tensp={tensp}";
                var httpClient = new HttpClient();
                var reponse = httpClient.GetAsync(apiUrl).Result;
                string apiData = reponse.Content.ReadAsStringAsync().Result;
                List<SanPhamChiTietViewModel> spct = JsonConvert.DeserializeObject<List<SanPhamChiTietViewModel>>(apiData);
                return Json(spct);
            } catch
            {
                return Json(null);
            }

        }
        [HttpGet]
        public async Task<IActionResult> GetProductDetails()
        {
            string apiUrl = "https://localhost:7265/api/SanPhamChiTiet/DanhSachSanPhamHoanThien";
            var httpClient = new HttpClient();
            var reponse = await httpClient.GetAsync(apiUrl);
            string apiData = await reponse.Content.ReadAsStringAsync();
            var spct = JsonConvert.DeserializeObject<List<SanPhamChiTietViewModel>>(apiData);
            return View(spct); //
        }
        [HttpGet]
        public async Task<IActionResult> GetProductDetailsJson()
        {
            try
            {
                string apiUrl = "https://localhost:7265/api/SanPhamChiTiet/DanhSachSanPhamHoanThien";
                var httpClient = new HttpClient();
                var reponse = await httpClient.GetAsync(apiUrl);
                string apiData = await reponse.Content.ReadAsStringAsync();
                var spct = JsonConvert.DeserializeObject<List<SanPhamChiTietViewModel>>(apiData);
                return Json(spct);
            } catch
            {
                return Json(null);
            }
        }
        [HttpGet]
        [HttpPost]
        public async Task<IActionResult> TimKiemNangCao([FromBody] SanPhamChiTietViewModel viewmodel)
        {
            try
            {
                string apiUrl = $"https://localhost:7265/api/SanPhamChiTiet/LocSanPhamTheoMauSizeThuongHieuLoaiSanPham?mau={viewmodel.MauSac}&size={viewmodel.Size}&thuonghieu={viewmodel.TenThuongHieu}&loaisanpham={viewmodel.LoaiSanPham}";
                var httpClient = new HttpClient();
                var reponse = await httpClient.GetAsync(apiUrl);
                string apiData = await reponse.Content.ReadAsStringAsync();
                var spct = JsonConvert.DeserializeObject<List<SanPhamChiTietViewModel>>(apiData);
                return Json(spct);
            } catch
            {
                return Json(null);
            }

        }

        [HttpGet]
        [HttpPost]
        public string NhanMoTaSanPham([FromBody] string mota)
        {
            HttpContext.Session.Remove("MoTaSanPham");
            HttpContext.Session.SetString("MoTaSanPham", mota);
            return mota;
        }
        [HttpGet]
        [HttpPost]
        public async Task<IActionResult> Create(SanPhamChiTiet sanPhamChiTiet)
        {
            string mota = "";
            if (sanPhamChiTiet.MoTa == "" || sanPhamChiTiet.MoTa == null)
            {
                mota = HttpContext.Session.GetString("MoTaSanPham");
            }
            else
            {
                mota = sanPhamChiTiet.MoTa;
            }

            var url = $"https://localhost:7265/api/Anh/DanhSachAnh";
            var res = await _httpClient.GetAsync(url);
            var data = await res.Content.ReadAsStringAsync();
            List<Anh> album = JsonConvert.DeserializeObject<List<Anh>>(data);
            ViewBag.listanh = album;
            ViewBag.Size = new SelectList(_context.sizes.ToList().OrderBy(c => c.SizeNumber), "ID", "SizeNumber");
            ViewBag.ThuongHieu = new SelectList(_context.thuonghieu.ToList().OrderBy(c => c.TenThuongHieu), "ID", "TenThuongHieu");
            ViewBag.MauSac = new SelectList(_context.mauSacs.ToList().OrderBy(c => c.TenMauSac), "ID", "TenMauSac");
            ViewBag.SanPham = new SelectList(_context.sanPhams.ToList().Where(c => c.TrangThai == 1).OrderBy(c => c.TenSanPham), "ID", "TenSanPham");



            string apiUrl = $"https://localhost:7265/api/SanPhamChiTiet/ThemMoiSanPhamChiTiet?idsp={sanPhamChiTiet.IDSP}&idsize={sanPhamChiTiet.IDSize}&idmausac={sanPhamChiTiet.IDMauSac}&idthuonghieu={sanPhamChiTiet.IDThuongHieu}&masp={sanPhamChiTiet.MaSPCT}&loaisanpham={sanPhamChiTiet.LoaiSanPham}&soluong={sanPhamChiTiet.SoLuong}&giaban={sanPhamChiTiet.GiaBan}&mota={mota}";
            var content = new StringContent(JsonConvert.SerializeObject(sanPhamChiTiet), Encoding.UTF8, "application/json");
            var reponse = await _httpClient.PostAsync(apiUrl, content);
            if (reponse.IsSuccessStatusCode)
            {
                string anhSanPhamsJson = HttpContext.Session.GetString("anhSanPhams");
                var spctnew = _sanphamchitietservice.GetAll().FirstOrDefault(c => c.MaSPCT == sanPhamChiTiet.MaSPCT);
                if (anhSanPhamsJson == null)
                {
                    return RedirectToAction("GetProductDetails", "SanPhamChiTiet");
                }
                else
                {
                    List<Anh> anhSanPham1s = JsonConvert.DeserializeObject<List<Anh>>(anhSanPhamsJson);
                    AnhSanPham anhSanPham = new AnhSanPham();
                    foreach (var idanh in anhSanPham1s)
                    {
                        anhSanPham.IdSanPhamChiTiet = _sanphamchitietservice.GetAll().FirstOrDefault(c => c.MaSPCT == sanPhamChiTiet.MaSPCT).ID;
                        anhSanPham.Idanh = idanh.ID;
                        _ianhsanpham.AddAnhChoSanPham(anhSanPham);
                    }

                    return RedirectToAction("GetProductDetails", "SanPhamChiTiet");
                }
            }
            else
            {
                return View();
            }
        }
        [HttpPost]
        public List<Anh> LuuAnh([FromBody] List<string> ImagesPath)
        {
            var length = ImagesPath.Count;
            for (int i = 0; i < length; i++)
            {
                var item = anhservice.DanhSachAnh().FirstOrDefault(c => c.DuongDan == ImagesPath[i]);
                anhSanPhams.Add(item);
            }
            var anhSanPhamsJson = JsonConvert.SerializeObject(anhSanPhams);
            HttpContext.Session.Remove("anhSanPhams");
            HttpContext.Session.SetString("anhSanPhams", anhSanPhamsJson);
            return anhSanPhams;
        }
        [HttpGet]
        public async Task<IActionResult> SanPhamChiTietDetail(Guid id)
        {
            string apiUrl = $"https://localhost:7265/api/SanPhamChiTiet/GetById/{id}";
            var httpClient = new HttpClient();
            var reponse = await httpClient.GetAsync(apiUrl);
            string apiData = await reponse.Content.ReadAsStringAsync();
            var spct = JsonConvert.DeserializeObject<SanPhamChiTiet>(apiData);
            return View(spct);
        }
        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {

            var url = $"https://localhost:7265/api/Anh/DanhSachAnh";
            var res = await _httpClient.GetAsync(url);
            var data = await res.Content.ReadAsStringAsync();
            List<Anh> album = JsonConvert.DeserializeObject<List<Anh>>(data);
            ViewBag.listanh = album;
            ViewBag.Size = new SelectList(_context.sizes.ToList().OrderBy(c => c.SizeNumber), "ID", "SizeNumber");
            ViewBag.ThuongHieu = new SelectList(_context.thuonghieu.ToList().OrderBy(c => c.TenThuongHieu), "ID", "TenThuongHieu");
            ViewBag.MauSac = new SelectList(_context.mauSacs.ToList().OrderBy(c => c.TenMauSac), "ID", "TenMauSac");
            ViewBag.SanPham = new SelectList(_context.sanPhams.ToList().Where(c => c.TrangThai == 1).OrderBy(c => c.TenSanPham), "ID", "TenSanPham");


            string apiURL = $"https://localhost:7265/api/SanPhamChiTiet/id?id={id}";
            var response = await _httpClient.GetAsync(apiURL);
            var apiData = await response.Content.ReadAsStringAsync();
            SanPhamChiTiet spct = JsonConvert.DeserializeObject<SanPhamChiTiet>(apiData);

            string spctJson = JsonConvert.SerializeObject(spct);
            HttpContext.Session.Remove("sanphamchitiettheoid");
            HttpContext.Session.SetString("sanphamchitiettheoid", spctJson);
            return View(spct);
        }
        [HttpGet]
        public ActionResult SanPhamChiTietTheoID()
        {
            try
            {
                string spctJson = HttpContext.Session.GetString("sanphamchitiettheoid");
                return Json(spctJson);
            } catch
            {
                return Json(null);
            }

        }
        [HttpGet]
        [HttpPost]
        public int NhanTrangThai([FromBody] int trangthai)
        {
            HttpContext.Session.Remove("trangthai");
            HttpContext.Session.SetInt32("trangthai", trangthai);
            return trangthai;
        }

        [HttpPost]
        public ActionResult Update(SanPhamChiTiet sanPhamChiTiet)
        {
            string apiURL1 = $"https://localhost:7265/api/SanPhamChiTiet/id?id={sanPhamChiTiet.ID}";
            var response1 =  _httpClient.GetAsync(apiURL1).Result;
            var apiData =  response1.Content.ReadAsStringAsync().Result;
            SanPhamChiTiet spct = JsonConvert.DeserializeObject<SanPhamChiTiet>(apiData);

            var Nhantrangthai = HttpContext.Session.GetInt32("trangthai");
            var Nhanmota = HttpContext.Session.GetString("MoTaSanPham");
            if(NhanTrangThai == null)
            {
                sanPhamChiTiet.TrangThai = Convert.ToInt32(spct.TrangThai);
            } else
            {
                sanPhamChiTiet.TrangThai = Convert.ToInt32(Nhantrangthai);
            }

            if (Nhanmota == null)
            {
                sanPhamChiTiet.MoTa = spct.MoTa;
            }
            else
            {
                sanPhamChiTiet.MoTa = Nhanmota;
            }

            string apiURL = $"https://localhost:7265/api/SanPhamChiTiet/ChinhSuaSanPham?id={sanPhamChiTiet.ID}&idsp={sanPhamChiTiet.IDSP}&idsize={sanPhamChiTiet.IDSize}&idmausac={sanPhamChiTiet.IDMauSac}&idthuonghieu={sanPhamChiTiet.IDThuongHieu}&masp={sanPhamChiTiet.MaSPCT}&loaisanpham={sanPhamChiTiet.LoaiSanPham}&soluong={sanPhamChiTiet.SoLuong}&giaban={sanPhamChiTiet.GiaBan}&mota={sanPhamChiTiet.MoTa}&trangthai={sanPhamChiTiet.TrangThai}";
            var obj = JsonConvert.SerializeObject(sanPhamChiTiet);
            StringContent content = new StringContent(obj, Encoding.UTF8, "application/json");
            HttpResponseMessage response = _httpClient.PostAsync(apiURL, content).Result;
            if (response.IsSuccessStatusCode)
            {
                string anhSanPhamsJson = HttpContext.Session.GetString("anhSanPhams");
                List<Anh> anhSanPham1s = JsonConvert.DeserializeObject<List<Anh>>(anhSanPhamsJson);
                AnhSanPham anhSanPham = new AnhSanPham();
                foreach (var idanh in anhSanPham1s)
                {
                    anhSanPham.IdSanPhamChiTiet = sanPhamChiTiet.ID;
                    anhSanPham.Idanh = idanh.ID;
                    _ianhsanpham.AddAnhChoSanPham(anhSanPham);
                }
                return RedirectToAction("GetProductDetails", "SanPhamChiTiet");
            }
            else
            {
                return RedirectToAction("Update", "SanPhamChiTiet", new { id = sanPhamChiTiet.ID });
            }

        }
        [HttpGet]
        [HttpPost]
        public ActionResult Delete([FromBody] SanPhamChiTiet spct)
        {
            ThongBao tb = new ThongBao();
            try
            {
                string apiURL = $"https://localhost:7265/api/SanPhamChiTiet/Delete/{spct.ID}";
                var obj = JsonConvert.SerializeObject(spct);
                StringContent content = new StringContent(obj, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _httpClient.PutAsync(apiURL, content).Result;
                if (response.IsSuccessStatusCode)
                {
                    var mess = response.Content.ReadAsStringAsync().Result;
                    tb.message = mess;
                    return Json(tb);
                }
                else
                {
                    tb.message = "Thất bại";
                    return Json(tb);
                }
            } catch
            {
                tb.message = "Lỗi";
                return Json(tb);
            }
        }
        [HttpGet]
        [HttpPost]
        public ActionResult ConHang([FromBody] SanPhamChiTiet spct)
        {
            ThongBao tb = new ThongBao();
            try
            {
                string apiURL = $"https://localhost:7265/api/SanPhamChiTiet/ConHang?id={spct.ID}";
                var obj = JsonConvert.SerializeObject(spct);
                StringContent content = new StringContent(obj, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _httpClient.PutAsync(apiURL, content).Result;
                if (response.IsSuccessStatusCode)
                {
                    var mess = response.Content.ReadAsStringAsync().Result;
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
                tb.message = "Lỗi";
                return Json(tb);
            }
           
        }
        [HttpPost]
        public ActionResult AddDataToDatabase()
        {
            ThongBao tb = new ThongBao();
            try
            {
                List<string> list = new List<string>();
                if (_tempProducts.Count() > 0)
                {
                    for (int i = 0; i < _tempProducts.Count(); i++)
                    {
                        //string apiUrl = $"https://localhost:7265/api/SanPhamChiTiet/ThemMoiSanPhamChiTiet?idsp={_tempProducts[i].IDSP}&idsize={_tempProducts[i].IDSize}&idmausac={_tempProducts[i].IDMauSac}&idthuonghieu={_tempProducts[i].IDThuongHieu}&masp={_tempProducts[i].MaSPCT}&loaisanpham={_tempProducts[i].LoaiSanPham}&soluong={_tempProducts[i].SoLuong}&giaban={_tempProducts[i].GiaBan}&mota={_tempProducts[i].MoTa}";
                        //var content = new StringContent(JsonConvert.SerializeObject(_tempProducts[i]), Encoding.UTF8, "application/json");
                        //var reponse =  _httpClient.PostAsync(apiUrl, content).Result;
                        SanPhamChiTiet spct = new SanPhamChiTiet()
                        {
                            ID = Guid.NewGuid(),
                            MaSPCT = _tempProducts[i].MaSPCT,
                            IDSP = _tempProducts[i].IDSP,
                            LoaiSanPham = _tempProducts[i].LoaiSanPham,
                            IDMauSac = _tempProducts[i].IDMauSac,
                            IDSize = _tempProducts[i].IDSize,
                            IDThuongHieu = _tempProducts[i].IDThuongHieu,
                            SoLuong = _tempProducts[i].SoLuong,
                            GiaBan = _tempProducts[i].GiaBan,
                            MoTa = _tempProducts[i].MoTa,
                            TrangThai = 1,
                        };
                        var resul = _sanphamchitietservice.ThemSanPham(spct);
                        if (resul == "Thêm thành công." || resul == "Thêm số lượng thành công.")
                        {
                            var mess = "Thêm thành công.";
                            list.Add(mess);
                        }
                        //string anhSanPhamsJson = HttpContext.Session.GetString("anhSanPhams");
                        //List<Anh> anhSanPham1s = JsonConvert.DeserializeObject<List<Anh>>(anhSanPhamsJson);
                        //AnhSanPham anhSanPham = new AnhSanPham();
                        //foreach (var idanh in anhSanPham1s)
                        //{
                        //    anhSanPham.IdSanPhamChiTiet = _sanphamchitietservice.GetAll().OrderByDescending(c => c.MaSp).First().Id;
                        //    anhSanPham.Idanh = idanh.Id;
                        //    _ianhsanpham.AddAnhChoSanPham(anhSanPham);
                        //}
                    }
                    if (list.Count() > 0)
                    {
                        for (var i = 0; i < list.Count(); i++)
                        {
                            if (list[i] != "Thêm thành công.")
                            {
                                tb.message = "Thất bại.";
                                return Json(tb);
                            }
                            else
                            {
                                tb.message = "Thành công.";
                                return Json(tb);
                            }

                        }
                    }
                }
                else
                {
                    tb.message = "Không có dữ liệu thêm.";
                    return Json(tb);
                }
            }
            catch (Exception e) { }
            {
                tb.message = "Lỗi dữ  liệu.";
                return Json(tb);
            }
        }
        [HttpPost]
        public async Task<ActionResult> UploadExcelFile(IFormFile excelFile)
        {
            if (excelFile != null && excelFile.Length > 0)
            {
                using (var stream = excelFile.OpenReadStream())
                {
                    ExcelPackage.LicenseContext = LicenseContext.Commercial;

                    using (var package = new ExcelPackage(stream))
                    {
                        var worksheet = package.Workbook.Worksheets[0]; // Chọn worksheet đầu tiên (index 0)

                        // Lấy số dòng và cột
                        int rowCount = GetRowCountWithoutEmptyData(worksheet);
                        //int rowCount = worksheet.Dimension.End.Row;
                        int colCount = worksheet.Dimension.Columns;

                        // Tạo danh sách để lưu trữ dữ liệu
                        List<SanPhamChiTietViewModel> products = new List<SanPhamChiTietViewModel>();
                        List<SanPhamChiTiet> lstspct = new List<SanPhamChiTiet>();
                        List<string> lstanhsp = new List<string>();
                        string[] pathimages;
                        // Bắt đầu từ dòng thứ 2 (để bỏ qua tiêu đề)
                        for (int row = 2; row <= rowCount; row++)
                        {
                            // view hiển thị
                            SanPhamChiTietViewModel product = new SanPhamChiTietViewModel
                            {
                                MaSPCT = worksheet.Cells[row, 1].Value.ToString(),
                                TenSP = worksheet.Cells[row, 2].Value.ToString(),
                                LoaiSanPham = worksheet.Cells[row, 3].Value.ToString(),
                                MauSac = worksheet.Cells[row, 4].Value.ToString(),
                                Size = worksheet.Cells[row, 5].Value.ToString(),

                                TenThuongHieu = worksheet.Cells[row, 6].Value.ToString(),
                                SoLuong = Convert.ToInt32(worksheet.Cells[row, 7].Value),
                                GiaBan = Convert.ToInt32(worksheet.Cells[row, 8].Value.ToString()),
                                MoTa = worksheet.Cells[row, 9].Value.ToString() == "" ? "" : worksheet.Cells[row, 9].Value.ToString(), // Mô tả bắt buộc 
                            };
                            products.Add(product);


                            Guid idsanpham, idmausac, idsize, idthuonghieu;
                            idsanpham = IDSP(worksheet.Cells[row, 2].Value.ToString().ToLower());
                            idmausac = IDMauSac(worksheet.Cells[row, 4].Value.ToString().ToLower());
                            idsize = IDsize(worksheet.Cells[row, 5].Value.ToString().ToLower());
                            idthuonghieu = IDThuongHieu(worksheet.Cells[row, 6].Value.ToString().ToLower());
                            SanPhamChiTiet spct = new SanPhamChiTiet()
                            {
                                MaSPCT = worksheet.Cells[row, 1].Value.ToString(),
                                IDSP = idsanpham,
                                LoaiSanPham = worksheet.Cells[row, 3].Value.ToString(),
                                IDMauSac = idmausac,
                                IDSize = idsize,
                                IDThuongHieu = idthuonghieu,
                                SoLuong = Convert.ToInt32(worksheet.Cells[row, 7].Value),
                                GiaBan = Convert.ToDecimal(worksheet.Cells[row, 8].Value),
                                MoTa = worksheet.Cells[row, 9].Value.ToString(),
                                TrangThai = 1,
                            };
                            lstspct.Add(spct);
                        }

                        string productListJson = JsonConvert.SerializeObject(products);
                        _tempProducts = lstspct;
                        _temspctvm = products;
                        TempData["Products"] = productListJson;
                        //TempData["Anhs"] = anhlistJson;


                        // Ở đây, bạn có danh sách "products" chứa dữ liệu từ tệp Excel
                        // Bạn có thể lưu dữ liệu này vào cơ sở dữ liệu hoặc hiển thị trên một trang khác.

                        return RedirectToAction("DisplayData", products);
                    }
                }
            }
            else
            {
                return RedirectToAction("Create", "SanPhamChiTiet");
            }
        }
        private int GetRowCountWithoutEmptyData(ExcelWorksheet worksheet)
        {
            int rowCount = worksheet.Dimension.End.Row;
            int columnCount = worksheet.Dimension.End.Column;
            int rowWithNonEmptyData = 0;

            for (int row = 1; row <= rowCount; row++)
            {
                bool rowIsEmpty = true;
                for (int col = 1; col <= columnCount; col++)
                {
                    if (worksheet.Cells[row, col].Value != null)
                    {
                        rowIsEmpty = false;
                        break;
                    }
                }

                if (!rowIsEmpty)
                {
                    rowWithNonEmptyData++;
                }
            }

            return rowWithNonEmptyData;
        }
        [HttpGet]
        public IActionResult DisplayData()
        {
            // Lấy danh sách sản phẩm từ TempData
            string productListJson = TempData["Products"] as string;
            //string anhlistjson = TempData["Anhs"] as string;
            //List<string[]> manglon = new List<string[]>();
            //List<string[]> mangnho = new List<string[]>();

            //List<string> mangChuoi = JsonConvert.DeserializeObject<List<string>>(anhlistjson);


            var products = JsonConvert.DeserializeObject<List<SanPhamChiTietViewModel>>(productListJson);
            //for (int i = 0; i < mangChuoi.Count; i++)
            //{
            //    if (mangChuoi[i].Contains(","))
            //    {
            //        string[] Chuoitach = mangChuoi[i].Split(',');
            //        mangnho.Add(Chuoitach);
            //        //manglon.Add(mangnho[]);
            //    }
            //    else
            //    {
            //        //manglon.Add(mangChuoi[i]);
            //    }
            //}
            //var anhs = JsonConvert.DeserializeObject<List<Anh>>(anhlistjson);
            //ViewBag.hienanh = anhs;
            return View(products);
        }

        public Guid IDSP(string tensap)
        {
            Guid idsanpham;
            string url = $"https://localhost:7265/api/SanPham/SanPhamTheoTen?name={tensap}";
            var reponse = _httpClient.GetAsync(url).Result;
            string apiData = reponse.Content.ReadAsStringAsync().Result;
            SanPham spham = JsonConvert.DeserializeObject<SanPham>(apiData);
            if (spham != null)
            {
                idsanpham = spham.ID;
            }
            else
            {
                SanPham sp = new SanPham()
                {
                    ID = Guid.NewGuid(),
                    TenSanPham = tensap.ToLower(),
                    KhoiLuong = 300,
                    TrangThai = 1,
                };
                _sanpham.Create(sp);
                idsanpham = sp.ID;
            }
            return idsanpham;
        }
        public Guid IDsize(string size)
        {
            Guid idsize;
            string url = $"https://localhost:7265/api/Size/SizetheoSize?size={size}";
            var reponse = _httpClient.GetAsync(url).Result;
            string apiData = reponse.Content.ReadAsStringAsync().Result;
            AppData.Entities.Models.Size si = JsonConvert.DeserializeObject<AppData.Entities.Models.Size>(apiData);
            if (si != null)
            {
                idsize = si.ID;
            }
            else
            {
                AppData.Entities.Models.Size s = new AppData.Entities.Models.Size()
                {
                    ID = Guid.NewGuid(),
                    SizeNumber = size.ToLower(),
                };
                _size.Create(s);
                idsize = s.ID;
            }
            return idsize;
        }
        public Guid IDThuongHieu(string thuonghieu)
        {
            Guid idthuonghieu;
            string url = $"https://localhost:7265/api/ThuongHieu/ThuongHieuTheoTen?th={thuonghieu}";
            var reponse = _httpClient.GetAsync(url).Result;
            string apiData = reponse.Content.ReadAsStringAsync().Result;
            ThuongHieu th = JsonConvert.DeserializeObject<ThuongHieu>(apiData);
            if (th != null)
            {
                idthuonghieu = th.ID;
            }
            else
            {
                ThuongHieu th1 = new ThuongHieu()
                {
                    ID = Guid.NewGuid(),
                    TenThuongHieu = thuonghieu.ToString().ToLower(),
                };
                _thuonghieu.Create(th1);
                idthuonghieu = th1.ID;
            }
            return idthuonghieu;
        }
        public Guid IDMauSac(string mausac)
        {
            Guid idmausac;
            string url = $"https://localhost:7265/api/MauSac/MauSacTheoTen?mau={mausac}";
            var reponse = _httpClient.GetAsync(url).Result;
            string apiData = reponse.Content.ReadAsStringAsync().Result;
            MauSac ms = JsonConvert.DeserializeObject<MauSac>(apiData);
            if (ms != null)
            {
                idmausac = ms.ID;
            }
            else
            {
                MauSac ms1 = new MauSac()
                {
                    ID = Guid.NewGuid(),
                    TenMauSac = mausac.ToLower(),
                };
                _mausac.Create(ms1);
                idmausac = ms1.ID;
            }
            return idmausac;
        }

        [HttpGet]
        [HttpPost]
        public ActionResult ThemMau([FromBody] MauSac mauSac)
        {
            string apiURL = $"https://localhost:7265/api/MauSac/Create?mau={mauSac.TenMauSac}";
            StringContent content = new StringContent(JsonConvert.SerializeObject(mauSac), Encoding.UTF8, "application/json");
            HttpResponseMessage response = _httpClient.PostAsync(apiURL, content).Result;
            if (response.IsSuccessStatusCode)
            {
                var mess = response.Content.ReadAsStringAsync().Result;
                ThongBao tb = new ThongBao()
                {
                    message = mess,
                };
                return Json(tb);
            }
            else
            {
                return Json(null);
            }
        }
        [HttpGet]
        [HttpPost]
        public ActionResult ThemSanPham([FromBody] SanPham sanPham)
        {
            ThongBao tb = new ThongBao();
            try
            {
                string apiUrl = $"https://localhost:7265/api/SanPham/Create";
                StringContent content = new StringContent(JsonConvert.SerializeObject(sanPham), Encoding.UTF8, "application/json");
                HttpResponseMessage reponse = _httpClient.PostAsync(apiUrl, content).Result;
                if (reponse.IsSuccessStatusCode)
                {
                    var mess = reponse.Content.ReadAsStringAsync().Result;
                    tb.message = mess;
                    return Json(tb);
                }
                else
                {
                    tb.message = "Thất bại";
                    return Json(tb);
                }
            } catch
            {
                tb.message = "Lỗi";
                return Json(tb);
            }
           
        }
        [HttpGet]
        [HttpPost]
        public ActionResult ThemSize([FromBody] AppData.Entities.Models.Size size)
        {
            ThongBao tb = new ThongBao();
            try
            {
                string apiURL = $"https://localhost:7265/api/Size/Create?size={size.SizeNumber}";
                StringContent content = new StringContent(JsonConvert.SerializeObject(size), Encoding.UTF8, "application/json");
                HttpResponseMessage response = _httpClient.PostAsync(apiURL, content).Result;
                if (response.IsSuccessStatusCode)
                {
                    var mess = response.Content.ReadAsStringAsync().Result;
                    tb.message = mess;
                    return Json(tb);
                }
                else
                {
                    tb.message = "Thất bại";
                    return Json(tb);
                }
            } catch
            {
                tb.message = "Lỗi";
                return Json(tb);
            }
            
        }
        [HttpGet]
        [HttpPost]
        public ActionResult ThemThuongHieu([FromBody] ThuongHieu th)
        {
            ThongBao tb = new ThongBao();
            try
            {
                string apiURL = $"https://localhost:7265/api/ThuongHieu/Create?thuongHieu={th.TenThuongHieu}";
                StringContent content = new StringContent(JsonConvert.SerializeObject(th), Encoding.UTF8, "application/json");
                HttpResponseMessage response = _httpClient.PostAsync(apiURL, content).Result;
                if (response.IsSuccessStatusCode)
                {
                    var mess = response.Content.ReadAsStringAsync().Result;
                    tb.message = mess;
                    return Json(tb);
                }
                else
                {
                    tb.message = "Thất bại";
                    return Json(tb);
                }
            } catch
            {
                tb.message = "Lỗi";
                return Json(tb);
            }
            
        }
        [HttpGet]
        [HttpPost]
        public ActionResult DanhsachAnhCuaSP([FromBody] string id)
        {
            try
            {
                string url = $"https://localhost:7265/api/Anh/DanhSachAnhChoSanPhamBySP?idsanpham={id}";
                var reponse = _httpClient.GetAsync(url).Result;
                string apiData = reponse.Content.ReadAsStringAsync().Result;
                List<Anh> spct = JsonConvert.DeserializeObject<List<Anh>>(apiData);
                return Json(spct);
            } catch
            {
                return Json(null);
            }

        }
        [HttpPost]
        public ActionResult XoaAnhSP([FromBody] AnhSanPham anhsanpham)
        {
            try
            {
                string url = $"https://localhost:7265/api/Anh/RemoveAnhSp?idanh={anhsanpham.Idanh}&idsp={anhsanpham.IdSanPhamChiTiet}";
                HttpResponseMessage response = _httpClient.DeleteAsync(url).Result;
                ThongBao tb = new ThongBao();
                tb.message = response.Content.ReadAsStringAsync().Result;
                return Json(tb);
            } catch
            {
                return Json(null);
            }
        }
    }
}
