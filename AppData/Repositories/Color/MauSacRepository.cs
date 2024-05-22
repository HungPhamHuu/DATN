using AppData.DB_Context;
using AppData.Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Repositories.Color;

public class MauSacRepository : IMauSacRepository
{
    private readonly ApplicationDbContext _dbContext;
    public MauSacRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> Create(MauSac mauSac)
    {
        try
        {
            if (mauSac != null)
            {
                if (GetAll().Result.Any(c => c.TenMauSac.ToLower() == mauSac.TenMauSac.ToLower()))
                {
                    return false;
                }
                else
                {
                    await _dbContext.mauSacs.AddAsync(mauSac);
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

    public async Task<List<MauSac>> GetAll()
    {
        var mauSacs = await _dbContext.mauSacs.OrderByDescending(c => c.TenMauSac).ToListAsync();
        return mauSacs;
    }

    public async Task<MauSac> GetById(Guid id)
    {
        return await _dbContext.mauSacs.FindAsync(id);
    }

    public MauSac GetByName(string name)
    {
        try
        {
            return _dbContext.mauSacs.FirstOrDefault(c => c.TenMauSac.ToLower() == name.ToLower());
        }
        catch
        {
            return null;
        }
    }

    public async Task<bool> Update(MauSac mauSac, Guid id)
    {
        if (mauSac == null) return false;
        try
        {
            var ms = await _dbContext.mauSacs.FindAsync(mauSac.ID);

            if (ms == null) return false;
            _dbContext.Entry(ms).CurrentValues.SetValues(mauSac);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}
