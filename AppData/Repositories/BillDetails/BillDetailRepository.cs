using AppData.DB_Context;
using AppData.Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Repositories.BillDetails
{
    public class BillDetailRepository : IBillDetailRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public BillDetailRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public bool Create(HoaDonChiTiet hdCT)
        {
            try
            {
                if(hdCT != null)
                {
                    if(GetByIdBill(hdCT.IDHoaDon).Any(c => c.IDSPCT == hdCT.IDSPCT) == true)
                    {
                        var hdct = GetByIdBill(hdCT.IDHoaDon).FirstOrDefault(c => c.IDSPCT == hdCT.IDSPCT);
                        hdct.SoLuong = hdCT.SoLuong;
                        Update(hdct, hdct.ID);
                        return true;
                    } else
                    {
                        _dbContext.hoaDonChiTiets.Add(hdCT);
                        _dbContext.SaveChanges();
                        return true;
                    }       
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> Delete(Guid id)
        {
            try
            {
                var hdCT = await _dbContext.hoaDonChiTiets.FindAsync(id);

                if(hdCT != null)
                {
                    _dbContext.hoaDonChiTiets.Remove(hdCT);
                    await _dbContext.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<HoaDonChiTiet> GetAll()
        {
            return  _dbContext.hoaDonChiTiets.ToList();
        }

        public List<HoaDonChiTiet> GetByIdBill(Guid idHD)
        {
            return  _dbContext.hoaDonChiTiets.Where(c => c.IDHoaDon == idHD).ToList();
        }

        public async Task<HoaDonChiTiet> GetId(Guid id)
        {
            return await _dbContext.hoaDonChiTiets.FindAsync(id);
        }

        public async Task<bool> Update(HoaDonChiTiet hdCT, Guid id)
        {
            if (hdCT == null) return false;
            try
            {
                var _hoadonCT = await _dbContext.hoaDonChiTiets.FindAsync(hdCT.ID);

                if (hdCT == null) return false;
                _dbContext.Entry(_hoadonCT).CurrentValues.SetValues(hdCT);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
