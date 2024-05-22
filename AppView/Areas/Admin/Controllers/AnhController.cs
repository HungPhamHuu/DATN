using AppData.DB_Context;
using AppData.Entities.Models;
using AppData.Repositories.Anhs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace AppView.Areas.Admin.Controllers
{
    public class AnhController : Controller
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IAnh _anh;
        private readonly ApplicationDbContext _context;
        HttpClient client ;
        public AnhController(IWebHostEnvironment hostingEnvironment , ApplicationDbContext context)
        {
            _hostingEnvironment = hostingEnvironment;
            _anh = new AnhReps(context);
            client = new HttpClient();
        }

        [HttpGet]
        public async Task<IActionResult> DanhSachAnh()
        {
            var url = $"https://localhost:7265/api/Anh/DanhSachAnh";
            var res = await client.GetAsync(url);
            var data = await res.Content.ReadAsStringAsync();
            List<Anh> anh = JsonConvert.DeserializeObject<List<Anh>>(data);
            return View(anh);
        }

        [HttpGet]
        [HttpPost]
        public async Task<IActionResult> UploadImages(List<IFormFile> imageFiles)
        {
            if (imageFiles != null && imageFiles.Count > 0)
            {
                var uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");

                foreach (var imageFile in imageFiles)
                {
                    if (imageFile.Length > 0)
                    {
                        // Kiểm tra định dạng tệp
                        if (imageFile.ContentType != "image/jpeg" && imageFile.ContentType != "image/png")
                        {
                            ModelState.AddModelError("ImageFiles", "Chỉ cho phép tải lên tệp .jpg và .png");
                            return View();
                        }

                        var uniqueFileName = imageFile.FileName;
                        string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await imageFile.CopyToAsync(fileStream);
                        }

                        // Lưu đường dẫn vào thuộc tính Path của đối tượng Anh
                        Anh anh = new Anh
                        {
                            ID = Guid.NewGuid(),
                            DuongDan = filePath,                           
                        };

                        _anh.ThemAnh(anh);
                    }
                }
                return RedirectToAction("DanhSachAnh");
            }
            return View();
        }
        [HttpGet]
        public IActionResult DisplayImage(string DuongDan)
        {
            // Kiểm tra xem đường dẫn có tồn tại không
            if (System.IO.File.Exists(DuongDan))
            {
                byte[] imageBytes = System.IO.File.ReadAllBytes(DuongDan);
                string fileName = Path.GetFileName(DuongDan);
                return File(imageBytes, "image/jpeg", fileName); // Thay đổi kiểu hình ảnh tùy theo định dạng thực tế
            }
            else
            {
                // Trả về một hình ảnh mặc định hoặc thông báo lỗi
                return NotFound();
            }
        }
    }
}
