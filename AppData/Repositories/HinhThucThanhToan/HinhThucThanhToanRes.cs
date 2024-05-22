using AppData.DB_Context;
using AppData.Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace AppData.Repositories.HinhThucThanhToan
{
    public class HinhThucThanhToanRes : IHinhThucThanhToanRes
    {
        private readonly ApplicationDbContext _context;
        public HinhThucThanhToanRes()
        {
            _context = new ApplicationDbContext();
        }
        public List<ThanhToan> DanhSachThanhToan()
        {
            try
            {
                return _context.thanhtoans.ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public string SuaHinhThuc(ThanhToan thanhToan)
        {
            try
            {
                if (thanhToan == null)
                {
                    return "Lỗi";
                }
                else
                {
                    if (DanhSachThanhToan().Any(c => c.ID == thanhToan.ID))
                    {
                        _context.thanhtoans.Update(thanhToan);
                        _context.SaveChanges();
                        return "Thành công.";
                    }
                    else
                    {
                        return "Không có";
                    }
                }
            }
            catch (Exception ex)
            {
                return "Lỗi";
            }
        }

        public string ThemHinhThuc(ThanhToan thanhToan)
        {
            try
            {
                if (thanhToan == null)
                {
                    return "Lỗi";
                }
                else
                {
                    if (DanhSachThanhToan().Any(c => c.HinhThucThanhToan == thanhToan.HinhThucThanhToan))
                    {
                        return "Đã có.";
                    }
                    else
                    {
                        thanhToan.TrangThai = 1;
                        _context.thanhtoans.Add(thanhToan);
                        _context.SaveChanges();
                        return "Thành công.";
                    }
                }
            }
            catch (Exception ex)
            {
                return "Lỗi";
            }
        }
        public string XoaHinhThuc(Guid idhinhthuc)
        {
            try
            {
                if (idhinhthuc == null)
                {
                    return "Lỗi";
                }
                else
                {
                    if (DanhSachThanhToan().Any(c => c.ID == idhinhthuc))
                    {
                        var httt = DanhSachThanhToan().FirstOrDefault(c => c.ID == idhinhthuc);
                        httt.TrangThai = 0;
                        _context.thanhtoans.Update(httt);
                        _context.SaveChanges();
                        return "Thành công.";
                    }
                    else
                    {
                        return "Không có";
                    }
                }
            }
            catch (Exception ex)
            {
                return "Lỗi";
            }
        }
        public string KichHoat(Guid idhinhthuc)
        {
            try
            {
                if (idhinhthuc == null)
                {
                    return "Lỗi";
                }
                else
                {
                    if (DanhSachThanhToan().Any(c => c.ID == idhinhthuc))
                    {
                        var httt = DanhSachThanhToan().FirstOrDefault(c => c.ID == idhinhthuc);
                        httt.TrangThai = 1;
                        _context.thanhtoans.Update(httt);
                        _context.SaveChanges();
                        return "Thành công.";
                    }
                    else
                    {
                        return "Không có";
                    }
                }
            }
            catch (Exception ex)
            {
                return "Lỗi";
            }
        }
    }
}

