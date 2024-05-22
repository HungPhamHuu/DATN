using AppData.DB_Context;
using AppData.Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Repositories.Sizes;

public class SizeRepository : ISizeRepository
{
    private readonly ApplicationDbContext _dbContext;
    public SizeRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public async Task<bool> Create(Size size)
    {
        try
        {
            if (size != null)
            {
                if (GetAll().Result.Any(c => c.SizeNumber.ToLower() == size.SizeNumber.ToLower()))
                {
                    return false;
                }
                else
                {
                    await _dbContext.sizes.AddAsync(size);
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
            var s = await _dbContext.sizes.FindAsync(id);
            if (s != null)
            {
                _dbContext.sizes.Remove(s);
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

    public async Task<List<Size>> GetAll()
    {
        return await _dbContext.sizes.OrderByDescending(c => c.SizeNumber).ToListAsync();
    }

    public async Task<Size> GetById(Guid id)
    {
        return await _dbContext.sizes.FindAsync(id);
    }

    public Size GetByTen(string size)
    {
        try
        {
            return _dbContext.sizes.FirstOrDefault(c => c.SizeNumber.ToLower() == size.ToLower());
        }
        catch (Exception)
        {
            return null;
        }
    }

    //public async Task<bool> Update(Size size)
    //{
    //    try
    //    {
    //        _dbContext.Update(size);
    //        _dbContext.SaveChanges(); return true;
    //    }
    //    catch (Exception)
    //    {
    //        return false;
    //    }
    //    var s = _dbContext.sizes.FirstOrDefault(p => p.ID == id);
    //    s.SizeNumber = size.SizeNumber;

    //    _dbContext.sizes.Update(s);
    //    _dbContext.SaveChanges();
    //    return s;
    //}
    public async Task<bool> Update(Guid id, Size size)
    {
        if (size == null) return false;
        try
        {
            var si = await _dbContext.sizes.FindAsync(size.ID);

            if (si == null) return false;
            _dbContext.Entry(si).CurrentValues.SetValues(size);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

}




