using AppData.DB_Context;
using AppData.Entities.Models;
using AppData.Repositories.Color;
using AppData.Repositories.TH;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;


namespace AppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThuongHieuController : ControllerBase
    {
        private readonly IThuongHieuRepository _thuongHieuRepository;

        private readonly ApplicationDbContext _context;
        public ThuongHieuController(ApplicationDbContext context)
        {
            _thuongHieuRepository = new ThuongHieuRepository(context);
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> DanhSach()

        {
            var th = await _thuongHieuRepository.GetAll();
            return Ok(th);
        }
        [HttpGet("[action]")]
        public ThuongHieu ThuongHieuTheoTen(string th)

        {
            return _thuongHieuRepository.GetByName(th);
        }

        [HttpGet("getById/{id}")]
        public async Task<ThuongHieu> GetById(Guid id)
        {
            var th = await _thuongHieuRepository.GetById(id);
            return (th);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Create(string thuongHieu)
        {
            ThuongHieu th = new ThuongHieu()
            {
                ID = Guid.NewGuid(),
                TenThuongHieu = thuongHieu,
            };
            return Ok(await _thuongHieuRepository.Create(th));
        }

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            await _thuongHieuRepository.Delete(id);
            return Ok();
        }
        [HttpPut("id")]
        public async Task<ActionResult> Update(ThuongHieu thuongHieu)
        {
            await _thuongHieuRepository.Update(thuongHieu, thuongHieu.ID);
            return Ok();
        }
    }
}
