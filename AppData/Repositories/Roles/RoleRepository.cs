using System;
using AppCommon.RepositoryAsync;
using AppData.DB_Context;
using AppData.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace AppData.Repositories.Roles;

public class RoleRepository : IRoleRepository
{
    private readonly ApplicationDbContext _context;
    public RoleRepository()
    {
        _context = new ApplicationDbContext();
    }

    public bool ChinhSuaChucVu(ChucVu chucVu)
    {
        try
        {
            var chucvu1 = DanhSachChucVu().FirstOrDefault(c => c.Id == chucVu.Id);
            if (chucvu1 != null)
            {
                chucvu1.TenChucVu = chucVu.TenChucVu;
                chucvu1.TrangThai = chucVu.TrangThai;
                _context.chucvu.Update(chucvu1);
                _context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
        catch
        {
            return false;
        }
    }

    public List<ChucVu> DanhSachChucVu()
    {
        try
        {
            return _context.chucvu.ToList();
        } catch
        {
            return null;
        }
    }
    public bool ThemMoiChucVu(ChucVu chucVu)
    {
        try
        {
            if (DanhSachChucVu().Any(c => c.TenChucVu == chucVu.TenChucVu))
            {
                return false;
            }
            else
            {
                ChucVu cv = new ChucVu();
                cv.Id = Guid.NewGuid();
                cv.TenChucVu = chucVu.TenChucVu.ToString();
                cv.TrangThai = 1;
                _context.chucvu.Add(cv);
                _context.SaveChanges();
                return true;
            }
        }
        catch
        {
            return false;
        }
    }

    public bool XoaChucVu(Guid id)
    {
        try
        {
            var chucvu1 = DanhSachChucVu().FirstOrDefault(c => c.Id == id);
            if (chucvu1 != null)
            {
                chucvu1.TrangThai = 0;
                _context.chucvu.Update(chucvu1);
                _context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }

        }
        catch
        {
            return false;
        }
    }
}