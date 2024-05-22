using AppData.DB_Context;
using AppData.Entities.Models;
using AppData.Repositories.TH;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Repositories.TH;

public class ThuongHieuRepository : IThuongHieuRepository
{
    private readonly ApplicationDbContext _dbContext;
    public ThuongHieuRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> Create(ThuongHieu thuongHieu)
    {
        try
        {
            if (thuongHieu != null)
            {
                if (GetAll().Result.Any(c => c.TenThuongHieu.ToLower() == thuongHieu.TenThuongHieu.ToLower()))
                {
                    return false;
                }
                else
                {
                    await _dbContext.thuongHieus.AddAsync(thuongHieu);
                    await _dbContext.SaveChangesAsync();
                    return true;
                }

            }
            else
            {
                return false;
            }

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
            var ms = await _dbContext.mauSacs.FindAsync(id);
            if (ms != null)
            {
                _dbContext.mauSacs.Remove(ms);
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

    public async Task<bool> Update(ThuongHieu thuongHieu, Guid id)
    {
        if (thuongHieu == null) return false;
        try
        {
            var th = await _dbContext.thuongHieus.FindAsync(thuongHieu.ID);

            if (th == null) return false;
            _dbContext.Entry(th).CurrentValues.SetValues(thuongHieu);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task<List<ThuongHieu>> GetAll()
    {
        return await _dbContext.thuongHieus.OrderByDescending(c => c.TenThuongHieu).ToListAsync();
    }

    public async Task<ThuongHieu> GetById(Guid id)
    {
        return await _dbContext.thuongHieus.FindAsync(id);
    }

    public ThuongHieu GetByName(string name)
    {
        try
        {
            return _dbContext.thuongHieus.FirstOrDefault(c => c.TenThuongHieu.ToLower() == name.ToLower());
        }
        catch
        {
            return null;
        }
    }
}
