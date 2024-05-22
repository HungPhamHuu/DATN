using AppData.Entities.Models;
using AppData.Repositories.Color;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;

namespace AppAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class MauSacController : ControllerBase
    {
        private readonly IMauSacRepository _mauSacRepository;
        public MauSacController(IMauSacRepository mauSacRepository)
        { 
            _mauSacRepository = mauSacRepository;
        }

        [HttpGet("[action]")]
        public async Task<ActionResult> DanhSach()

        {
            var ms = await _mauSacRepository.GetAll();
            return Ok(ms);
        }
        [HttpGet("[action]")]
        public MauSac MauSacTheoTen(string mau)
        {
           return _mauSacRepository.GetByName(mau);
        }

        [HttpGet("getById/{id}")]
        public async Task<MauSac> GetById(Guid id)
        {
            var ms = await _mauSacRepository.GetById(id);
            return (ms);
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> Create(string mau)
        {
            MauSac ms = new MauSac()
            {
                ID = Guid.NewGuid(),
                TenMauSac = mau,
            };
            return Ok(await _mauSacRepository.Create(ms));
        }

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            await _mauSacRepository.Delete(id);
            return Ok();
        }
        [HttpPut("id")]
        public async Task<ActionResult> Update(MauSac mauSac) 
        {
            await _mauSacRepository.Update(mauSac, mauSac.ID);
            return Ok();
        }
        
    }
}
