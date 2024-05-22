using AppData.DB_Context;
using AppData.Entities.Models;
using AppData.Repositories.Bills;
using AppData.Repositories.Product;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using System.Net.Http.Headers;

namespace AppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SanPhamController : ControllerBase
    {
        private readonly IProductRepository _productRepo;
        private readonly ApplicationDbContext _context;
        public SanPhamController(ApplicationDbContext context)
        {
            _productRepo = new ProductRepository(context);
        }
        [HttpGet("[action]")]
        public async Task<ActionResult<SanPham>> DanhSach()
        {
            var listProduct= await _productRepo.GetAll();
            return Ok(listProduct);
        }
        [HttpGet("[action]")]
        public SanPham SanPhamTheoTen(string name)
        {
            return _productRepo.GetByten(name);
        }

        [HttpPut("[action]/id")]
        public async Task<ActionResult<SanPham>> Update(Guid id, string tensp , int khoiluong , int trangthai)
        {
            var sanPham = await _productRepo.GetById(id);
            sanPham.TenSanPham = tensp;
            sanPham.KhoiLuong = khoiluong;
            sanPham.TrangThai = trangthai;
            await _productRepo.Update(id, sanPham);
            return Ok(sanPham);
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> Create(string tensp, int khoiluong)
        {
            
            SanPham sp = new SanPham()
            {
                ID = Guid.NewGuid(),
                TenSanPham = tensp,
                TrangThai = 1,
                KhoiLuong = khoiluong,
            };
            return Ok(await _productRepo.Create(sp));
        }
        [HttpDelete("id")]
        public async Task<ActionResult> Delete(Guid id)
        {
            if (await _productRepo.Delete(id))
            {
                return Ok("Xóa thành công");
            }
            else return BadRequest("Xóa thất bại");
        }

        [HttpGet("id")]
        public async Task<ActionResult> GetById(Guid id)
        {
            var sp = await _productRepo.GetById(id);
            return Ok(sp);
        }

        [HttpGet("[action]")]
        public async Task<ActionResult> GetByName(string name)
        {
            var listProduct = await _productRepo.GetByName(name);
            return Ok(listProduct);
        }
    }
}
