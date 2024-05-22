using AppData.DB_Context;
using AppData.Entities.Models;
using AppData.Repositories.Sizes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SizeController : ControllerBase
    {
        private readonly ISizeRepository _sizeRepository;
        private readonly ApplicationDbContext _context;
        public SizeController(ISizeRepository sizeRepository)
        {
            _sizeRepository = sizeRepository;
        }
        [HttpGet("[action]")]
        public async Task<ActionResult<Size>> DanhSach()
        {
            var listSize = await _sizeRepository.GetAll();
            return Ok(listSize);
        }
        [HttpGet("[action]")]
        public Size SizetheoSize(string size)
        {
            return _sizeRepository.GetByTen(size);
        }
        [HttpGet("getById/{id}")]
        public async Task<Size> GetById(Guid id)
        {
            var s = await _sizeRepository.GetById(id);
            return (s);
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> Create(string size)
        {
            Size s = new Size()
            {
                ID = Guid.NewGuid(),
                SizeNumber = size,
            };
            return Ok(await _sizeRepository.Create(s));


        }
        
        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            
            if (await _sizeRepository.Delete(id))
            {
                return Ok("Xóa thành công");
            }
            else return BadRequest("Xóa thất bại");
        }
        
        [HttpPut("[action]/{id}")]
        public async Task<ActionResult<Size>> Update(Guid id, Size size)
        {
            await _sizeRepository.Update(id, size);
            return Ok(size);
        }

    }
}
