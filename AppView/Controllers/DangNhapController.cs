using AppData.Entities.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Text;
using AppData.Repositories.Anhs;
using AppData.Repositories.Roles;
using AppData.Repositories.Users;
using AppData.Repositories.Cart;
using AppData.DB_Context;
using AppData.Entities.ViewModels;
using System.Net.WebSockets;
using AppData.Entities.ViewModels.GioHangCTVM;
using AppData.Entities.ViewModels.GioHangVM;

namespace AppView.Controllers
{
    public class DangNhapController : Controller
    {
        // GET: DangNhapController
        HttpClient client;
        private readonly IRoleRepository roleRepository;
        private readonly IUserRepository userRepository;
        private readonly ICartRepository cartRepository;
        private readonly ApplicationDbContext _context;
        public DangNhapController(ApplicationDbContext context)
        {
            client = new HttpClient();
            cartRepository = new CartRepository(context);
            roleRepository = new RoleRepository();
            userRepository = new UserRepository();
        }
        [HttpGet]
        public ActionResult DanhNhap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DanhNhap(NguoiDung nguoiDung)
        {
            if (nguoiDung.Email == null && nguoiDung.MatKhau == null)
            {
                return RedirectToAction("DanhNhap", "DangNhap");
            }
            else
            {
                string url = $"https://localhost:7265/api/NguoiDung/DangNhap?emai={nguoiDung.Email}&matkhau={nguoiDung.MatKhau}";
                var obj = JsonConvert.SerializeObject(nguoiDung);
                StringContent content = new StringContent(obj, Encoding.UTF8, "application/json");
                HttpResponseMessage message = client.PostAsJsonAsync(url, content).Result;
                List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email , nguoiDung.Email),
                new Claim("OtherProperties", "Example Role")
            };
                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims,
                    CookieAuthenticationDefaults.AuthenticationScheme);
                AuthenticationProperties properties = new AuthenticationProperties()
                {
                    AllowRefresh = true,
                    IsPersistent = false

                };
                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), properties);

                if (message.IsSuccessStatusCode)
                {
                    var mess = message.Content.ReadAsStringAsync().Result;
                    if (mess == "Khách hàng đăng nhập thành công.")
                    {
                        var role = "Khách hàng";
                        var ten = userRepository.lstnguoidung().FirstOrDefault(c => c.Email == nguoiDung.Email).TenNguoiDung;
                        HttpContext.Session.Remove("chucvu");
                        HttpContext.Session.SetString("chucvu", role);

                        HttpContext.Session.Remove("Tennguoidung");
                        HttpContext.Session.SetString("Tennguoidung", ten);


                        return RedirectToAction("Index", "TrangChu", new { area = "KhachHang" });
                    }
                    else if (mess == "Quản lý đăng nhập thành công." || mess == "Nhân viên đăng nhập thành công.")
                    {
                        var role = "";
                        if (mess == "Quản lý đăng nhập thành công.")
                        {
                            role = "Quản lý";
                        } else
                        {
                            role = "Nhân viên";
                        }
                        var ten = userRepository.lstnguoidung().FirstOrDefault(c => c.Email == nguoiDung.Email).TenNguoiDung;

                        HttpContext.Session.Remove("chucvu");
                        HttpContext.Session.SetString("chucvu", role);

                        HttpContext.Session.Remove("Tennguoidung");
                        HttpContext.Session.SetString("Tennguoidung", ten);

                        return RedirectToAction("GetProductDetails", "SanPhamChiTiet", new { area = "Admin" });
                    }
                    else
                    {
                        return RedirectToAction("DanhNhap", "DangNhap");
                    }
                }
                else
                {
                    return RedirectToAction("DanhNhap", "DangNhap");
                }

            }

        }

        // GET: DangNhapController/Details/5
        [HttpGet]
        public ActionResult DangKyKhachHang()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangKyKhachHang(NguoiDung nguoidung)
        {
            if (DateTime.Now.Year - nguoidung.NgaySinh.Value.Year < 18)
            {
                return RedirectToAction("DangKyKhachHang", "DangNhap");
            }
            else
            {
                NguoiDung nguoiDung = new NguoiDung()
                {
                    Id = Guid.NewGuid(),
                    IdChucVu = roleRepository.DanhSachChucVu().FirstOrDefault(c => c.TenChucVu == "Khách hàng").Id,
                    TenNguoiDung = nguoidung.TenNguoiDung,
                    NgaySinh = nguoidung.NgaySinh,
                    Anh = nguoidung.Anh,
                    SDT = nguoidung.SDT,
                    Email = nguoidung.Email,
                    MatKhau = nguoidung.MatKhau,
                    PhuongXa = nguoidung.PhuongXa,
                    TinhThanh = nguoidung.TinhThanh,
                    DiaChi = nguoidung.DiaChi,
                    QuanHuyen = nguoidung.QuanHuyen,
                    TrangThai = 1
                };

                string message = userRepository.CreateKhachHang(nguoiDung);
                if (message == "Email đã đăng ký. Mời bạn đăng ký lại.")
                {

                    return RedirectToAction("DangKyKhachHang", "DangNhap");
                }
                else if (message == "SDT đã đăng ký. Mời bạn đăng ký lại.")
                {

                    return RedirectToAction("DangKyKhachHang", "DangNhap");
                }
                else if (message == "Đăng ký khách hàng thất bại.")
                {

                    return RedirectToAction("DangKyKhachHang", "DangNhap");
                }
                else
                {
                    // Tạo giỏ hàng 
                    GioHangVM gh = new GioHangVM()
                    {
                        ID = Guid.NewGuid(),
                        IDNguoiDung = nguoiDung.Id,
                    };
                    cartRepository.CreateCart(gh);
                    return RedirectToAction("DanhNhap", "DangNhap");
                }
            }

        }
        public async Task<IActionResult> Logout()
        {
            // Xóa thông tin xác thực khỏi cookie
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Remove("chucvu");
            // Redirect đến trang chính hoặc trang đăng nhập
            return RedirectToAction("Index", "Home");
        }
        // GET: DangNhapController/Create
        [HttpGet]
        [HttpPost]
        public ActionResult QuenMatKhau(string email)
        {
            return View();
        }

        [HttpGet]
        [HttpPost]
        public ActionResult KiemTraEmail([FromBody] string email)
        {
            var url = $"https://localhost:7265/api/NguoiDung/lstNguoiDungTheoEmai?email={email}";
            var respos = client.GetAsync(url).Result;
            var data = respos.Content.ReadAsStringAsync().Result;
            List<NguoiDung> lstnguoidung = JsonConvert.DeserializeObject<List<NguoiDung>>(data);
            return Json(lstnguoidung);
        }
        [HttpGet]
        [HttpPost]
        public ActionResult DoiMatKhau([FromBody] NguoiDung nguoidung)
        {
            var url = $"https://localhost:7265/api/NguoiDung/DoiMatKhau?idnguoidung={nguoidung.Id}&matkhau={nguoidung.MatKhau}";
            var obj = JsonConvert.SerializeObject(nguoidung);
            StringContent content = new StringContent(obj, Encoding.UTF8, "application/json");
            HttpResponseMessage message = client.PutAsJsonAsync(url, content).Result;
            ThongBao tb = new ThongBao();
            if (message.IsSuccessStatusCode)
            {
                var mess = message.Content.ReadAsStringAsync().Result;
                tb.message = mess;
                return Json(tb);
            }
            else
            {
                tb.message = "Lỗi.";
                return Json(tb);
            }

        }
    }
}
