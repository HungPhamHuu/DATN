using AppData.DB_Context;
using AppData.Entities.Models;
using AppData.Repositories.Anhs;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnhController : ControllerBase
    {
        private readonly IAnh _ianh;
        private readonly IAnhSanPhamRes _anhSanPhamRes;
        private readonly ApplicationDbContext _context;
        public AnhController(ApplicationDbContext context)
        {
            _ianh = new AnhReps(context);
            _anhSanPhamRes = new AnhSanPhamRes();
        }

        // GET: api/<AnhController>
        [HttpGet("[action]")]
        public IEnumerable<Anh> DanhSachAnh()
        {
            return _ianh.DanhSachAnh();
        }
        [HttpGet("[action]")]
        public List<AnhSanPham> GetAllAnhChoSanPhamBySP(Guid idsanpham)
        {
            return _anhSanPhamRes.GetAllAnhChoSanPhamBySP(idsanpham);
        }
        [HttpGet("[action]")]
        public List<Anh> DanhSachAnhChoSanPhamBySP(Guid idsanpham)
        {
            return _anhSanPhamRes.DanhSachAnhChoSanPhamBySP(idsanpham);
        }

        [HttpDelete("[action]")]
        public bool RemoveAnhSp(Guid idanh, Guid idsp)
        {
            return _anhSanPhamRes.RemoveAnhSp(idanh, idsp);
        }
    }
}
