using AppData.Entities.Models;
using AppData.Entities.ViewModels;
using AppData.Entities.ViewModels.GioHangVM;
using AppData.Repositories.Cart;
using Microsoft.AspNetCore.Mvc;

namespace AppAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class GioHangController : ControllerBase
    {
        private readonly ICartRepository _cartRepository;
        public GioHangController(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        [HttpGet("get")]
        public IActionResult Index()

        {
            var gh =  _cartRepository.GetAllCart();
            return Ok(gh);
        }
      

        [HttpGet("getById/{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var gh = await _cartRepository.GetCartById(id);
            return Ok(gh);
        }

        [HttpPost("add")]
        public async Task<IActionResult> Create([FromBody]GioHangVM gioHang)
        {
            gioHang.ID = Guid.NewGuid();
            var gh = await _cartRepository.CreateCart(gioHang);
            return Ok(gh);
        }
    }
}
