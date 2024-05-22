using AppData.DB_Context;
using AppData.Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Repositories.VoucherDetails
{
    public class VoucherDetailRepository : IVoucherDetailRepository
    {
        private readonly ApplicationDbContext _context;
        public VoucherDetailRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool CreateVoucherDetail(VoucherChiTiet voucherDetail)
        {
            try
            {
                if (voucherDetail != null)
                {
                     _context.voucherDetail.Add(voucherDetail);
                     _context.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteVoucherDetail(Guid id)
        {
            try
            {
                var detail =  _context.gioHangChiTiets.FirstOrDefault(c => c.ID == id);
                if (detail != null)
                {
                    _context.gioHangChiTiets.Remove(detail);
                    _context.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<VoucherChiTiet> GetAllVoucherByIDNguoiDung(Guid IDnguoiDung)
        {
            try
            {
                return _context.voucherDetail.Where(c => c.IDNguoiDung == IDnguoiDung).ToList();
            } catch
            {
                return null;
            }
        }

        public  List<VoucherChiTiet> GetAllVoucherDetail()
        {
            return  _context.voucherDetail.ToList();
        }

        public  VoucherChiTiet GetVoucherDetailById(Guid id)
        {
            return  _context.voucherDetail.FirstOrDefault(p => p.IDVoucher == id);
        }

        public bool UpdateVoucherDetail(VoucherChiTiet voucherDetail)
        {
            var detail =  _context.voucherDetail.FirstOrDefault(c => c.ID == voucherDetail.ID);
            detail.IDVoucher = voucherDetail.IDVoucher;
            detail.IDNguoiDung = voucherDetail.IDNguoiDung;
            detail.SoLuong = voucherDetail.SoLuong;
            detail.TrangThai = voucherDetail.TrangThai;
            _context.voucherDetail.Update(detail);
            _context.SaveChanges();
            return true;
        }
    }
}
