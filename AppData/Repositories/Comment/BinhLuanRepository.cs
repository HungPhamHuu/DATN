using AppData.DB_Context;
using AppData.Entities.Models;
using AppData.Repositories.Product;
using AppData.Repositories.ProductDetail;
using AppData.Repositories.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Repositories.Comment
{
    public class BinhLuanRepository : IBinhLuanRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IProductDetailRepository _ispct;
        private readonly SaleChiTietVm saleChiTietVm;
        public BinhLuanRepository(ApplicationDbContext context)
        {
            _context = context;
            _ispct = new ProductDetailRepository(_context);
            saleChiTietVm = new SaleChiTietVm();
        }
        public string ChinhSuaBinhLuan(BinhLuan binhLuan)
        {
            try
            {
                if (binhLuan.IDSpCt == null)
                {
                    return "Lỗi chỉnh sửa";
                }
                else
                {

                    _context.binhLuans.Update(binhLuan);
                    _context.SaveChanges();
                    return "Chỉnh sửa thành công";
                }

            }
            catch (Exception ex)
            {
                return "Lỗi: " + ex.Message;
            }
        }

        public List<BinhLuan> DanhSachBinhLuan()
        {
            try
            {
                return _context.binhLuans.OrderByDescending(c => c.NgayTao).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<BinhLuan> DanhSachBinhLuanTheoIdSPCT(Guid idspct)
        {
            try
            {
                return _context.binhLuans.Where(c => c.IDSpCt == idspct).OrderByDescending(c => c.NgayTao).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<BinhLuan> DanhSachBinhLuanTheoIdSPCTSale(Guid idspsale)
        {
            try
            {
                var idsp = saleChiTietVm.DanhSachSanPhamSale().FirstOrDefault(c => c.IDSalechitiet == idspsale).IdSanPhamCT;
                if(idsp == null)
                {
                    return null;
                } else
                {
                    return _context.binhLuans.Where(c => c.IDSpCt == idsp).OrderByDescending(c => c.NgayTao).ToList();
                }
            }
            catch
            {
                return null;
            }
        }
        public string ThemMoiBinhLuan(BinhLuan binhLuan)
        {
            try
            {
                if (binhLuan == null)
                {
                    return "Thêm thất bại.";
                }
                else
                { 
                    _context.binhLuans.Add(binhLuan);
                    _context.SaveChanges();
                    return "Thêm thành công.";
                }
            }
            catch (Exception ex)
            {
                return "Lỗi: " + ex.Message;
            }
        }

        public string XoaBinhLuan(Guid IDBinhLuan)
        {
            try
            {
                if (DanhSachBinhLuan().Any(c => c.ID == IDBinhLuan))
                {
                    var cmt = DanhSachBinhLuan().FirstOrDefault(c => c.ID == IDBinhLuan);
                    _context.binhLuans.Remove(cmt);
                    _context.SaveChanges();
                    return "Xóa thành công.";
                }
                else
                {
                    return "Xóa thât bại";
                }

            }
            catch (Exception ex)
            {
                return "Lỗi: " + ex.Message;
            }
        }
    }
}
