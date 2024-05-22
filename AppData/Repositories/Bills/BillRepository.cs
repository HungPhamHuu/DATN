using AppData.DB_Context;
using AppData.Entities.Models;
using AppData.Repositories.VoucherDetails;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Repositories.Bills;

public class BillRepository : IBillRepository
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IVoucherDetailRepository _voucher;
    public BillRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
        _voucher = new VoucherDetailRepository(dbContext);
    }
    public bool Ceate(HoaDon hoaDon)
    {
        try
        {
            if (hoaDon != null)
            {
                if (hoaDon.IDVoucherChiTiet != null)
                {
                    var voucher = _voucher.GetAllVoucherByIDNguoiDung(hoaDon.IDNguoiDung).FirstOrDefault(c => c.ID == hoaDon.IDVoucherChiTiet);
                    voucher.SoLuong = voucher.SoLuong - 1;
                    if(voucher.SoLuong == 0)
                    {
                        voucher.TrangThai = 0;
                    }
                    _voucher.UpdateVoucherDetail(voucher);
                }

               _dbContext.hoaDons.Add(hoaDon);
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

    public string DangGiao(Guid id)
    {
        try
        {
            var HD = _dbContext.hoaDons.FirstOrDefault(c => c.ID == id);
            if (HD == null)
            {
                return "Thất bại.";
            }
            else
            {
                HD.TrangThai = 5;
                _dbContext.hoaDons.Update(HD);
                _dbContext.SaveChanges();
                return "Đơn hàng đã được đưa cho bên vận chuyển.";
            }
        }
        catch (Exception ex1)
        {
            return "Lỗi." + ex1.Message;
        }
    }

    public string DaNhan(Guid id)
    {
        try
        {
            var HD = _dbContext.hoaDons.FirstOrDefault(c => c.ID == id);
            if (HD == null)
            {
                return "Thất bại.";
            }
            else
            {
                HD.TrangThai = 4;
                _dbContext.hoaDons.Update(HD);
                _dbContext.SaveChanges();
                return "Đã nhận.";
            }
        }
        catch (Exception ex1)
        {
            return "Lỗi." + ex1.Message;
        }
    }

    public List<HoaDon> DanhSachHoaDonChoTheoTenvaSDT(string tukhoa)
    {
        try
        {
            var hoadoncho = _dbContext.hoaDons.Where(c => c.TrangThai == 2).ToList();
            return hoadoncho.Where(c => c.TenKhachHang.Contains(tukhoa) || c.SDTKhachHang.Contains(tukhoa) || Convert.ToString(c.MaHoaDon).Substring(0, 8).ToUpper() == tukhoa.ToUpper()).OrderByDescending(c => c.NgayTao).ToList();
        }
        catch (Exception)
        {
            return null;
        }
    }

    public List<HoaDon> DanhSachTheoTenvaSDT(string tukhoa)
    {
        try
        {
            return _dbContext.hoaDons.Where(c => c.TenKhachHang.Contains(tukhoa) || c.SDTKhachHang.Contains(tukhoa) || Convert.ToString(c.MaHoaDon).Substring(0, 8).ToUpper() == tukhoa.ToUpper()).OrderByDescending(c => c.NgayTao).ToList();
        }catch (Exception)
        {
            return null;
        }
    }

    public List<HoaDon> DanhSachTheoTrangThai(int trangthai)
    {
        try
        {
            return _dbContext.hoaDons.Where(c => c.TrangThai == trangthai).OrderByDescending(c => c.NgayTao).ToList();
        }
        catch (Exception)
        {
            return null;
        }
    }

    public async Task<bool> Delete(Guid id)
    {
        try
        {
            var hd = await _dbContext.hoaDons.FindAsync(id);
            if (hd != null)
            {
                _dbContext.hoaDons.Remove(hd);
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

    public List<HoaDon> GetAll()
    {
        return  _dbContext.hoaDons.OrderByDescending(c => c.NgayTao).ToList();
    }

    public List<HoaDon> GetAllByIdNguoiDung(Guid idnguoidung)
    {
        return _dbContext.hoaDons.Where(c => c.IDNguoiDung == idnguoidung).OrderByDescending(c => c.NgayTao).ToList();
    }

    public async Task<HoaDon> GetId(Guid id)
    {
        return await _dbContext.hoaDons.FindAsync(id);
    }

    public async Task<bool> GetMa(string ma)
    {
        return await _dbContext.hoaDons.AnyAsync(x => x.MaHoaDon == ma);
    }

    public string HuyHoaDon(Guid id)
    {
        try
        {
            var HD = _dbContext.hoaDons.FirstOrDefault(c => c.ID == id);
            if(HD == null)
            {
                return "Thất bại.";
            } else
            {
                HD.TrangThai = 6;
                _dbContext.hoaDons.Update(HD);
                _dbContext.SaveChanges();
                return "Hủy thành công.";
            }
        } catch (Exception ex1) 
        {
            return "Lỗi." + ex1.Message;
        }
    }

    public HoaDon LayHoaDonChoMoiNhat()
    {
        return _dbContext.hoaDons.OrderByDescending(c => c.NgayTao).First(c => c.TrangThai == 2);
    }

    public List<HoaDon> ListTrangThaiHoaDon()
    {
        try
        {
            var lsthoadon = _dbContext.hoaDons.ToList();
            var uniqueTrangThai = lsthoadon.GroupBy(c => c.TrangThai).Select(c => c.First()).ToList();
            return uniqueTrangThai;
        } catch (Exception)
        {
            return null;
        }
    }

    public bool Update(HoaDon hoaDon, Guid id)
    {
        if (hoaDon == null) return false;
        try
        {
            var hd =  _dbContext.hoaDons.FindAsync(hoaDon.ID);

           if(hd == null) return false;
            _dbContext.hoaDons.Update(hoaDon);
            _dbContext.SaveChangesAsync();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public string XacNhanHoaDon(Guid id)
    {
        try
        {
            var HD = _dbContext.hoaDons.FirstOrDefault(c => c.ID == id);
            if (HD == null)
            {
                return "Thất bại.";
            }
            else
            {
                HD.TrangThai = 7;
                _dbContext.hoaDons.Update(HD);
                _dbContext.SaveChanges();
                return "Xác nhận đơn hàng thành công.";
            }
        }
        catch (Exception ex1)
        {
            return "Lỗi." + ex1.Message;
        }
    }
    public HoaDon HoaDonTaoMoiNhat(string sdtkhachhang)
    {
        return _dbContext.hoaDons.Where(c => c.SDTKhachHang == sdtkhachhang).OrderByDescending(c => c.NgayTao).FirstOrDefault();
    }

    public string HoanHang(Guid id)
    {
        try
        {
            var HD = _dbContext.hoaDons.FirstOrDefault(c => c.ID == id);
            if (HD == null)
            {
                return "Thất bại.";
            }
            else
            {
                HD.TrangThai = 8;
                _dbContext.hoaDons.Update(HD);
                _dbContext.SaveChanges();
                return "Chờ xác nhận hoàn hàng bên shop.";
            }
        }
        catch (Exception ex1)
        {
            return "Lỗi." + ex1.Message;
        }
    }

    public string XacNhanHoanHang(Guid id)
    {
        try
        {
            var HD = _dbContext.hoaDons.FirstOrDefault(c => c.ID == id);
            if (HD == null)
            {
                return "Thất bại.";
            }
            else
            {
                HD.TrangThai = 9;
                _dbContext.hoaDons.Update(HD);
                _dbContext.SaveChanges();
                return "Xác nhận hoàn hàng.";
            }
        }
        catch (Exception ex1)
        {
            return "Lỗi." + ex1.Message;
        }
    }
}
