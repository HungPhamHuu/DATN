using AppData.Entities.Models;
using AppData.Entities.ViewModels;
using AppData.Repositories.Users;
using AppData.Repositories.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace AppView.Areas.KhachHang.Controllers
{
    public class VoucherKHController : Controller
    {
        HttpClient _httpClient;
        private readonly IUserRepository userRepository;
        public VoucherKHController()
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
                return "Yêu cầu đăng nhập";
            } else
            {
                var id = userRepository.lstnguoidung().FirstOrDefault(c => c.Email == email).Id;
                return Convert.ToString(id);
            }
        }
        // GET: VoucherKHController
        [HttpGet]
        public ActionResult DanhSachVoucherTheoKhachHang()
        {
            var id = IDNguoiDung();
            string url = $"https://localhost:7265/api/Voucher/DanhSachVoucherVMByIdNguoiDung?idnguoidung={id}";
            var response = _httpClient.GetAsync(url).Result;
            string apiData = response.Content.ReadAsStringAsync().Result;
            List<VoucherDetailViewModel> result = JsonConvert.DeserializeObject<List<VoucherDetailViewModel>>(apiData);
            return View(result);
        }
        [HttpGet]
        public ActionResult VoucherTheoIdVoucher(Guid idvoucherdetail)
        {
            var id = IDNguoiDung();
            string url = $"https://localhost:7265/api/Voucher/VoucherVMTheoID?IDnguoidung={id}&idvoucherdetail={idvoucherdetail}";
            var response = _httpClient.GetAsync(url).Result;
            string apiData = response.Content.ReadAsStringAsync().Result;
            VoucherDetailViewModel result = JsonConvert.DeserializeObject<VoucherDetailViewModel>(apiData);
            return View(result);
        }

        [HttpGet]
        [HttpPost]
        public ActionResult LayVoucherChoNguoiDung([FromBody]VoucherChiTiet voucherchitiet)
        {
            ThongBao tb = new ThongBao();
            try
            {
                var nhanidnguoidung = IDNguoiDung();
                if(nhanidnguoidung == "Yêu cầu đăng nhập")
                {
                    tb.message = "Yêu cầu đăng nhập";
                    return Json(tb);
                }
                else
                {
                    string url = $"https://localhost:7265/api/Voucher/CreateVoucherChiTiet?IDVoucher={voucherchitiet.IDVoucher}&IDnguoiDung={nhanidnguoidung}&soluong=1";
                    var content = new StringContent(JsonConvert.SerializeObject(voucherchitiet), Encoding.UTF8, "application/json");
                    var reponse = _httpClient.PostAsync(url, content).Result;
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
                }
               
            } catch (Exception ex)
            {
                tb.message = "Lỗi";
                return Json(tb);
            }
           
        }
    }
}
