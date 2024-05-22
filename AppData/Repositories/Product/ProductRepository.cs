using AppData.DB_Context;
using AppData.Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Repositories.Product;

public class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _dbContext;
    public ProductRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<bool> Create(SanPham sanPham)
    {
        try
        {
            if (sanPham != null)
            {
                if (GetAll().Result.Any(c => c.TenSanPham.Trim().ToLower() == sanPham.TenSanPham.Trim().ToLower() && c.KhoiLuong == sanPham.KhoiLuong))
                {
                    return false;
                }
                else
                {
                    await _dbContext.sanPhams.AddAsync(sanPham);
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
            var hd = await _dbContext.sanPhams.FindAsync(id);
            if (hd != null)
            {
                _dbContext.sanPhams.Remove(hd);
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

    public async Task<IEnumerable<SanPham>> GetAll()
    {
        return await _dbContext.sanPhams.ToListAsync();
    }
    public async Task<List<SanPham>> GetByName(string name)
    {
        return await _dbContext.sanPhams.Where(x => x.TenSanPham.Contains(name)).ToListAsync();
    }
    public async Task<SanPham> GetById(Guid id)
    {
        return await _dbContext.sanPhams.FindAsync(id);
    }
    public async Task<bool> Update(Guid id, SanPham sanPham)
    {
        try
        {
            _dbContext.Update(sanPham);
            _dbContext.SaveChanges(); return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
    public SanPham GetByten(string name)
    {
        try
        {
            return _dbContext.sanPhams.FirstOrDefault(c => c.TenSanPham.ToLower() == name.ToLower());
        }
        catch (Exception)
        {
            return null;
        }
    }
}
