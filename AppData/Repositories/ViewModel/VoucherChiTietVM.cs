using AppData.DB_Context;
using AppData.Entities.Models;
using AppData.Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Repositories.ViewModel
{
    public class VoucherChiTietVM
    {
        private readonly ApplicationDbContext _context; 
        public VoucherChiTietVM()
        {
            _context = new ApplicationDbContext();
        }
        public List<VoucherDetailViewModel> DanhSachVoucherChiTiet()
        {
            try
            {
                var lst = from a in _context.voucherDetail.ToList()
                          join b in _context.voucher.ToList() on a.IDVoucher equals b.ID
                          select new VoucherDetailViewModel
                          {
                              ID = a.ID,
                              IDVoucher = a.IDVoucher,
                              IDNguoiDung = a.IDNguoiDung,
                              MaVoucher = b.MaVoucher,
                              NgayBatDau = b.NgayBatDau,
                              NgayKetThuc = b.NgayKetThuc,
                              GiaTriVoucher = b.GiaTriVoucher,
                              DieuKienMin = b.DieuKienMin,
                              DieuKienMax = b.DieuKienMax,
                              SoLuong = a.SoLuong,
                              MoTa = b.MoTa,
                              TrangThai = a.TrangThai,
                          };
                return lst.ToList();
            } catch
            {
                return null;
            }
           
        }
        public List<VoucherDetailViewModel> DanhSachVoucherChiTietTheoIDNguoiDung(Guid idnguoidung)
        {
            try
            {
                var lst = from a in _context.voucherDetail.ToList()
                          join b in _context.voucher.ToList() on a.IDVoucher equals b.ID
                          select new VoucherDetailViewModel
                          {
                              ID = a.ID,
                              IDVoucher = a.IDVoucher,
                              IDNguoiDung = a.IDNguoiDung,
                              MaVoucher = b.MaVoucher,
                              NgayBatDau = b.NgayBatDau,
                              NgayKetThuc = b.NgayKetThuc,
                              GiaTriVoucher = b.GiaTriVoucher,
                              DieuKienMin = b.DieuKienMin,
                              DieuKienMax = b.DieuKienMax,
                              SoLuong = a.SoLuong,
                              MoTa = b.MoTa,
                              TrangThai = a.TrangThai,
                          };
                return lst.Where(c => c.IDNguoiDung == idnguoidung).ToList();
            }
            catch
            {
                return null;
            }

        }
        public VoucherDetailViewModel DanhSachVoucherChiTietByID(Guid idnguoidung, Guid IDVoucherDetail)
        {
            try
            {
                var lst = from a in _context.voucherDetail.ToList()
                          join b in _context.voucher.ToList() on a.IDVoucher equals b.ID
                          select new VoucherDetailViewModel
                          {
                              ID = a.ID,
                              IDVoucher = a.IDVoucher,
                              IDNguoiDung = a.IDNguoiDung,
                              MaVoucher = b.MaVoucher,
                              NgayBatDau = b.NgayBatDau,
                              NgayKetThuc = b.NgayKetThuc,
                              GiaTriVoucher = b.GiaTriVoucher,
                              DieuKienMin = b.DieuKienMin,
                              DieuKienMax = b.DieuKienMax,
                              SoLuong = a.SoLuong,
                              MoTa = b.MoTa,
                              TrangThai = a.TrangThai,
                          };
                return lst.FirstOrDefault(c => c.ID == IDVoucherDetail && c.IDNguoiDung == idnguoidung);
            }
            catch
            {
                return null;
            }
        }
        public VoucherDetailViewModel DanhSachVoucherChiTietTheoMaVoucher(Guid id, string ma)
        {
            try
            {
                var lst = from a in _context.voucherDetail.ToList()
                          join b in _context.voucher.ToList() on a.IDVoucher equals b.ID
                          select new VoucherDetailViewModel
                          {
                              ID = a.ID,
                              IDVoucher = a.IDVoucher,
                              IDNguoiDung = a.IDNguoiDung,
                              MaVoucher = b.MaVoucher,
                              NgayBatDau = b.NgayBatDau,
                              NgayKetThuc = b.NgayKetThuc,
                              GiaTriVoucher = b.GiaTriVoucher,
                              DieuKienMin = b.DieuKienMin,
                              DieuKienMax = b.DieuKienMax,
                              SoLuong = a.SoLuong,
                              MoTa = b.MoTa,
                              TrangThai = a.TrangThai,
                          };
                return lst.FirstOrDefault(c => c.MaVoucher.ToLower() == ma.ToLower() && c.IDNguoiDung == id);
            }
            catch
            {
                return null;
            }
        }
    }
}
