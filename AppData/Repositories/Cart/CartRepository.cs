using AppData.DB_Context;
using AppData.Entities.Models;
using AppData.Entities.ViewModels;
using AppData.Entities.ViewModels.GioHangVM;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Repositories.Cart
{
    public class CartRepository : ICartRepository
    {
        private ApplicationDbContext _context;
        public CartRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateCart(GioHangVM gioHang)
        {
            try
            {
                var gh = new GioHang
                {
                    ID = gioHang.ID,
                    IDNguoiDung = gioHang.IDNguoiDung
                };

                await _context.AddAsync(gh);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                await Console.Out.WriteLineAsync(e.Message);
                return false;
            }
        }

        public List<GioHangVM> GetAllCart()
        {
            var query = from c in _context.gioHangs
                        select new GioHangVM
                        {
                            ID = c.ID,
                            IDNguoiDung = c.IDNguoiDung
                        };
            var result =  query.ToList();
            return result;
        }

        public async Task<GioHangVM> GetCartById(Guid id)
        {
            var gh = await _context.gioHangs.AsQueryable().FirstOrDefaultAsync(p => p.IDNguoiDung == id);
            if (gh == null)
            {
                return null;
            }
            var a = new GioHangVM
            {
                ID = gh.ID,
                IDNguoiDung = gh.IDNguoiDung
            };

            return a;
        }
    }
}
