using AppData.DB_Context;
using AppData.Entities.ViewModels;
using AppData.Repositories.BillDetails;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThongkeController : ControllerBase
    {
        private readonly ThongkeRepository _thongke;
        private readonly ApplicationDbContext _context;
        public ThongkeController(ApplicationDbContext context)
        {
            _thongke = new ThongkeRepository(context);
        }

        [HttpGet("[action]")]
        public int TongSoLuongHoaDonThanhCong()
        {
            return _thongke.TongSoLuongHoaDonThanhCong();
        }

        [HttpGet("[action]")]
        public decimal TongDoanhThu()
        {
            return _thongke.TongDoanhThu();
        }
        [HttpGet("[action]")]
        public int TongHoaDonBiHuy()
        {
            return _thongke.TongHoaDonBiHuy();
        }
        [HttpGet("[action]")]
        public List<Thongke> Top10SanPhamBanChayNhat()
        {
            return _thongke.Top10SanPhamBanChayNhat();
        }

        [HttpGet("[action]")]
        public List<Thongke> Top10SanPhamSaleBanChayNhat()
        {
            return _thongke.Top10SanPhamSaleBanChayNhat();
        }
    }
}
