using AppData.Entities.Models;
using AppData.Entities.ViewModels;
using AppData.Entities.ViewModels.GioHangVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Repositories.Cart
{
    public interface ICartRepository
    {
        public List<GioHangVM> GetAllCart();
        public Task<bool> CreateCart(GioHangVM gioHang);
        public Task<GioHangVM> GetCartById(Guid id);
    }
}
