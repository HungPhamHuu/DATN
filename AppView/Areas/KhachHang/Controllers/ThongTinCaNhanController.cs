using AppData.DB_Context;
using AppData.Entities.Models;
using AppData.Entities.ViewModels;
using AppData.Repositories.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos.Serialization.HybridRow;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Text;

namespace AppView.Areas.KhachHang.Controllers
{
    public class ThongTinCaNhanController : Controller
    {
        // GET: ThongTinCaNhan
        HttpClient _httpClient;
        private readonly IUserRepository userRepository;
        ApplicationDbContext _context;
        public ThongTinCaNhanController()
        {
            _httpClient = new HttpClient();
            userRepository = new UserRepository();
        }
        public string IDNguoiDung()
        {
            var user = HttpContext.User;
            var email = user.FindFirstValue(ClaimTypes.Email);
            if(email == null)
            {
                return null;
            }else
            {
                var id = userRepository.lstnguoidung().FirstOrDefault(c => c.Email == email).Id;
                return id.ToString();
            }
        }
        [HttpGet]
        public ActionResult ThongTinCaNhan()
        {
            return View();
        }
        [HttpGet]
        public ActionResult ThongTinCaNhanJson()
        {
            try
            {
                var id = IDNguoiDung();
                if(id == null)
                {
                    return Json(null);
                }
                else
                {
                    string url = $"https://localhost:7265/api/NguoiDung/NguoiDungTheoId?idnguoidung={id}";
                    var response = _httpClient.GetAsync(url).Result;
                    string apiData = response.Content.ReadAsStringAsync().Result;
                    NguoiDung result = JsonConvert.DeserializeObject<NguoiDung>(apiData);
                    return Json(result);
                }
            } catch
            {
                return Json(null);
            }

        }
        [HttpGet]
        [HttpPost]
        public ActionResult ThayThoiThongTinCaNhan([FromBody]NguoiDung nguoidung)
        {
            ThongBao tb = new ThongBao();
            try
            {
                //string url = $"https://localhost:7265/api/NguoiDung/ChinhSuaNguoiDung?idnguoidung={nguoidung.Id}&name={nguoidung.TenNguoiDung}&Ngaysinh={nguoidung.NgaySinh}&anh={nguoidung.Anh}&sdt={nguoidung.SDT}&email={nguoidung.Email}&quanhuyen={nguoidung.QuanHuyen}&tinhthanh={nguoidung.TinhThanh}&phuongxa={nguoidung.PhuongXa}&diachi={nguoidung.DiaChi}";
                //var obj = JsonConvert.SerializeObject(nguoidung);
                //StringContent content = new StringContent(obj, Encoding.UTF8, "application/json");
                //HttpResponseMessage message = _httpClient.PutAsJsonAsync(url, content).Result;
                //if (message.IsSuccessStatusCode)
                //{
                //    var mess = message.Content.ReadAsStringAsync().Result;
                //    tb.message = mess;
                //    return Json(tb);
                //}
                //else
                //{
                //    tb.message = "Thất bại.";
                //    return Json(tb);
                //}
                NguoiDung user = userRepository.lstnguoidung().FirstOrDefault(c => c.Id == nguoidung.Id);
                user.TenNguoiDung = nguoidung.TenNguoiDung;
                user.NgaySinh = nguoidung.NgaySinh;
                user.Email = nguoidung.Email;
                user.SDT = nguoidung.SDT;
                user.TinhThanh = nguoidung.TinhThanh;
                user.QuanHuyen = nguoidung.QuanHuyen;
                user.PhuongXa = nguoidung.PhuongXa;
                user.DiaChi = nguoidung.DiaChi;
                tb.message = userRepository.ChinhSuaThongTin(user);
                return Json(tb);

            } catch
            {
                tb.message = "Lỗi";
                return Json(tb);
            }
        }
        [HttpGet]
        [HttpPost]
        public ActionResult DoiMatKhau([FromBody] NguoiDung nguoidung)
        {
            try
            {
                var user = _context.nguoidung.FirstOrDefault(u => u.Id == nguoidung.Id);
                user.MatKhau = nguoidung.MatKhau;
                _context.nguoidung.Update(user);
                _context.SaveChanges();
                return Json(new { message = "Thành công" });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Json(new { message = "Lỗi" });
            }

        }
    }
}
