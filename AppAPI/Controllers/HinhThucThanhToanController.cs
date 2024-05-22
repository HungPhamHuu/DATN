using AppData.Entities.Models;
using AppData.Repositories.HinhThucThanhToan;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HinhThucThanhToanController : ControllerBase
    {
        // GET: api/<HinhThucThanhToanController>
        private readonly IHinhThucThanhToanRes _hinhThucThanhToan;
        public HinhThucThanhToanController()
        {
            _hinhThucThanhToan = new HinhThucThanhToanRes();
        }
        [HttpGet("[action]")]
        public IEnumerable<ThanhToan> DanhSachThanhToan()
        {
            return _hinhThucThanhToan.DanhSachThanhToan();
        }

        // POST api/<HinhThucThanhToanController>
        [HttpPost("[action]")]
        public string ThemHinhThuc(string HinhThucThanhToan)
        {
            ThanhToan thanhtoan = new ThanhToan()
            {
                ID = Guid.NewGuid(),
                HinhThucThanhToan = HinhThucThanhToan,
                TrangThai = 1,
            };
            return _hinhThucThanhToan.ThemHinhThuc(thanhtoan);
        }

        // PUT api/<HinhThucThanhToanController>/5
        [HttpPut("[action]")]
        public string ChinhSua(Guid id , string hinhthuc)
        {

            ThanhToan thanhtoan = _hinhThucThanhToan.DanhSachThanhToan().FirstOrDefault(c => c.ID == id);
            thanhtoan.HinhThucThanhToan = hinhthuc;
            return _hinhThucThanhToan.SuaHinhThuc(thanhtoan);
        }

        // DELETE api/<HinhThucThanhToanController>/5
        [HttpPut("[action]")]
        public string XoaHinhThuc(Guid id)
        {
            return _hinhThucThanhToan.XoaHinhThuc(id);
        }
        [HttpPut("[action]")]
        public string KichHoatHinhThuc(Guid id)
        {
            return _hinhThucThanhToan.KichHoat(id);
        }
    }
}
