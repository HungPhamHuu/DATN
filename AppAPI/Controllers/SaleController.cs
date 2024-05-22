using AppData.DB_Context;
using AppData.Entities.Models;
using AppData.Repositories.SaleRes;
using AppData.Repositories.SaleDetail;
using AppData.Repositories.ViewModel;
using AppData.Repositories.VoucherDetails;
using AppData.Repositories.Vouchers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleController : ControllerBase
    {
        private readonly ISaleRepository _vcSale;
        ApplicationDbContext _context ;
        private readonly ISaleDetailRepository saleDetailRepository;
        public SaleController(ApplicationDbContext context)
        {
            _context = context;
            _vcSale = new SaleRepository(context);
        }
        [HttpGet("[action]")]
        public IEnumerable<Sale> DanhSach()
        {
            return _vcSale.GetAllSale();
        }
        [HttpPost("[action]")]
        public bool CreateSale(string ma, DateTime NgayBatDau, DateTime NgayKetThuc,int phanTramGiam)
        {
            Sale sale = new Sale();
            sale.ID = Guid.NewGuid();
            sale.MaSale = ma;
            sale.NgayBatDau = NgayBatDau;
            sale.NgayKetThuc = NgayKetThuc;
            sale.PhanTramGiam = phanTramGiam;
            sale.TrangThai = 1;
            return _vcSale.CreateSale(sale);
        }
        [HttpPut("[action]")]
        public bool UpdateSale(Guid id, string ma, DateTime NgayBatDau, DateTime NgayKetThuc, int phanTramGiam, int TrangThai)
        {
            var sale = _vcSale.GetAllSale().FirstOrDefault(c => c.ID == id);
            sale.MaSale = ma;
            sale.NgayBatDau = NgayBatDau;
            sale.NgayKetThuc = NgayKetThuc;
            sale.PhanTramGiam= phanTramGiam;
            sale.TrangThai = TrangThai;
            return _vcSale.UpdateSale(sale);
        }

        [HttpPut("[action]")]
        public bool DeleteSale(Guid id)
        {
            var sale = _context.sales.First(c => c.ID == id);
            return _vcSale.DeleteSale(sale);
        }
        [HttpDelete("[action]")]
        public bool XoaCung(Guid id)
        {
            return _vcSale.XoaCung(id);
        }

        [HttpGet("getByid-sale/{id}")]
        public IActionResult GetById(Guid id)
        {
            var sale = _vcSale.GetById(id);
            if (sale == null)
            {
                return NotFound();
            }
            return Ok(sale);
        }
    }
}
