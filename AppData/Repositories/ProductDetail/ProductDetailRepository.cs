using AppData.DB_Context;
using AppData.Entities.Models;
using AppData.Entities.ViewModels;
using AppData.Repositories.Color;
using AppData.Repositories.Product;
using AppData.Repositories.Sizes;
using AppData.Repositories.TH;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Repositories.ProductDetail;

public class ProductDetailRepository : IProductDetailRepository
{
    private readonly ApplicationDbContext _dbContext;
    public ProductDetailRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public bool Create(SanPhamChiTiet sanPhamChiTiet)
    {
        try
        {
            if (sanPhamChiTiet != null)
            {
                 _dbContext.sanPhamChiTiets.Add(sanPhamChiTiet);
                 _dbContext.SaveChanges();
                return true;
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
            var sp = await _dbContext.sanPhamChiTiets.FindAsync(id);
            if (sp != null)
            {
                sp.TrangThai = 0;
                _dbContext.sanPhamChiTiets.Update(sp);
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

    public List<SanPhamChiTiet> GetAll()
    {
        return _dbContext.sanPhamChiTiets.ToList();
    }

    public async Task<SanPhamChiTiet> GetId(Guid id)
    {
        return await _dbContext.sanPhamChiTiets.FindAsync(id);
    }


    public async Task<bool> Update(SanPhamChiTiet sanPham)
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
    public IEnumerable<object> GetProductDetails()
    {
        var query = _dbContext.sanPhamChiTiets
               .Include(s => s.SanPham)
               .Include(s => s.Size)
               .Include(s => s.MauSac)
               .Include(s => s.ThuongHieu)
               .Select(s => new
               {
                   ID = s.ID,
                   TenSanPham = s.SanPham.TenSanPham,
                   SizeNumber = s.Size.SizeNumber,
                   TenMauSac = s.MauSac.TenMauSac,
                   TenThuongHieu = s.ThuongHieu.TenThuongHieu,
                   MaSPCT = s.MaSPCT,
                   SoLuong = s.SoLuong,
                   GiaBan = s.GiaBan,
                   LoaiSanPham = s.LoaiSanPham,
                   MoTa = s.MoTa,
                   QrImage = s.QrImage,
                   TrangThai = s.TrangThai,
               });

        return query.ToList();
    }
    public string ThemSanPham(SanPhamChiTiet sp)
    {
        try
        {
            if (sp != null)
            {
                if (GetAll().Any(c => c.IDMauSac == sp.IDMauSac && c.IDSize == sp.IDSize && c.IDSP == sp.IDSP && c.IDThuongHieu == sp.IDThuongHieu && c.MaSPCT == sp.MaSPCT))
                {
                    SanPhamChiTiet sp1 = GetAll().FirstOrDefault(c => c.IDMauSac == sp.IDMauSac && c.IDSize == sp.IDSize && c.IDSP == sp.IDSP && c.IDThuongHieu == sp.IDThuongHieu && c.MaSPCT == sp.MaSPCT);
                    sp1.SoLuong = sp1.SoLuong + sp.SoLuong;
                    _dbContext.sanPhamChiTiets.Update(sp1);
                    _dbContext.SaveChanges();
                    return "Thêm số lượng thành công.";
                }
                else if (GetAll().Any(c => c.MaSPCT == sp.MaSPCT))
                {
                    int soluongtrung = GetAll().Where(c => c.MaSPCT == sp.MaSPCT).ToList().Count();
                    sp.MaSPCT = sp.MaSPCT + "(" + Convert.ToString(soluongtrung + 1)+ ")";
                    _dbContext.sanPhamChiTiets.Add(sp);
                    _dbContext.SaveChanges();
                    return "Thêm thành công.";
                } else
                {
                    _dbContext.sanPhamChiTiets.Add(sp);
                    _dbContext.SaveChanges();
                    return "Thêm thành công.";
                }
            }
            return "Thêm thất bại";
        }
        catch (Exception ex)
        {
            return "Lỗi: " + ex.Message;
        }

    }
    public string CapNhatSanPham(SanPhamChiTiet sp)
    {
        try
        {
            _dbContext.Update(sp);
            _dbContext.SaveChanges();
            return "Cập nhật thành công.";
        }
        catch (Exception ex)
        {
            return "Lỗi: " + ex.Message;
        }

    }

    public bool ConHang(Guid id)
    {
        try
        {
            var sp =  _dbContext.sanPhamChiTiets.FirstOrDefault(c => c.ID == id);
            if (sp != null)
            {
                sp.TrangThai = 1;
                _dbContext.sanPhamChiTiets.Update(sp);
                 _dbContext.SaveChanges();
                return true;
            }
            return false;
        }
        catch (Exception)
        {
            return false;
        }
    }
    public bool XoaCung(Guid id)
    {
        try
        {
            var sp = _dbContext.sanPhamChiTiets.FirstOrDefault(c => c.ID == id);
            if (sp != null)
            {
                _dbContext.sanPhamChiTiets.Remove(sp);
                _dbContext.SaveChanges();
                return true;
            }
            return false;
        }
        catch (Exception)
        {
            return false;
        }
    }
}