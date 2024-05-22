using AppData.DB_Context;
using AppData.Entities.Models;
using AppData.Entities.ViewModels;
using AppData.Repositories.ProductDetail;
using AppData.Repositories.ViewModel;
using AppData.Repositories.VoucherDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Repositories.SaleDetail
{
    public class SaleDetailRepository : ISaleDetailRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly SPCTViewModel _spctviewmodel;
        private readonly IProductDetailRepository productDetailRepository;
        public SaleDetailRepository(ApplicationDbContext context)
        {
            _context = context;
            _spctviewmodel = new SPCTViewModel();
            productDetailRepository = new ProductDetailRepository(context);
        }

        public string BatDauSale(Guid id)
        {
            try
            {
                var detail = GetAllSaleDetail().FirstOrDefault(c => c.ID == id);
                if (detail != null)
                {
                    detail.TrangThai = 1;
                    _context.saleChiTiets.Update(detail);
                    _context.SaveChanges();
                    return "Cập nhật thành công.";
                }
                return "Cập nhật thất bại.";
            }
            catch (Exception)
            {
                return "Cập nhật thất bại.";
            }
        }

        public string CreateSaleDetail(SaleChiTiet saleDetail)
        {
            try
            {
                if (saleDetail != null)
                {
                    if (GetAllSaleDetail().Any(c => c.IDSPCT == saleDetail.IDSPCT && c.IDSale == saleDetail.IDSale) == true)
                    {
                        var spct = _spctviewmodel.DanhSachSanPhamHoanThien().FirstOrDefault(c => c.ID == saleDetail.IDSPCT);
                        if (saleDetail.SoLuong > spct.SoLuong)
                        {
                            return "Sản phẩm này chỉ còn" + spct.SoLuong;
                        }
                        else
                        {
                            var spctgoc = _context.sanPhamChiTiets.FirstOrDefault(c => c.ID == saleDetail.IDSPCT);
                            var saledetail = GetAllSaleDetail().FirstOrDefault(c => c.ID == saleDetail.ID);
                            // lấy lại số lượng
                            var soluongcu = saledetail.SoLuong + spctgoc.SoLuong;
                            // cập nhật số lượng mới của sản phẩm chi tiết
                            var soluongsanphammoi = soluongcu - saleDetail.SoLuong;

                            spctgoc.SoLuong = soluongsanphammoi;
                            productDetailRepository.CapNhatSanPham(spctgoc);

                            saledetail.SoLuong = saleDetail.SoLuong + saledetail.SoLuong;
                            saledetail.IDSale = saleDetail.IDSale;
                            saledetail.IDSPCT = saleDetail.IDSPCT;

                            UpdateSaleDetail(saledetail);
                            return "Thêm thành công";
                        }
                    }
                    else
                    {
                        var spct = _spctviewmodel.DanhSachSanPhamHoanThien().FirstOrDefault(c => c.ID == saleDetail.IDSPCT);
                        if (saleDetail.SoLuong > spct.SoLuong)
                        {
                            return "Sản phẩm này chỉ còn" + spct.SoLuong;
                        }
                        else
                        {
                            var soluong = saleDetail.SoLuong;
                            var saledetail = GetAllSaleDetail().FirstOrDefault(c => c.ID == saleDetail.ID);
                            var spctgoc = _context.sanPhamChiTiets.FirstOrDefault(c => c.ID == saleDetail.IDSPCT);
                            var soluongsanphammoi = spct.SoLuong - soluong;
                            spctgoc.SoLuong = soluongsanphammoi;
                            productDetailRepository.CapNhatSanPham(spctgoc);


                            _context.saleChiTiets.Add(saleDetail);
                            _context.SaveChanges();
                            return "Thêm thành công";
                        }
                    }
                }
                else
                {
                    return "Thêm thất bại";
                }
            }
            catch (Exception)
            {
                return "Thêm thất bại";
            }
        }

        public string DeleteSaleDetail(Guid id)
        {
            try
            {
                var detail = GetAllSaleDetail().FirstOrDefault(c => c.ID == id);
                if (detail != null)
                {
                    detail.TrangThai = 0;
                    _context.saleChiTiets.Update(detail);
                    _context.SaveChanges();
                    return "Cập nhật thành công.";
                }
                return "Cập nhật thất bại.";
            }
            catch (Exception)
            {
                return "Cập nhật thất bại.";
            }
        }

        public List<SaleChiTiet> GetAllSaleByIDSPCT(Guid IDSPCT)
        {
            try
            {
                return _context.saleChiTiets.Where(c => c.IDSPCT == IDSPCT).ToList();
            }
            catch
            {
                return null;
            }
        }

        public List<SaleChiTiet> GetAllSaleDetail()
        {
            return _context.saleChiTiets.ToList();
        }

        public SaleChiTiet GetSaleDetailById(Guid id)
        {
            return _context.saleChiTiets.FirstOrDefault(p => p.ID == id);
        }

        public string UpdateSaleDetail(SaleChiTiet saleDetail)
        {
            try
            {
                var detail = _context.saleChiTiets.FirstOrDefault(c => c.ID == saleDetail.ID);
                if (detail == null)
                {
                    return "Thất bại";
                }
                else
                {
                    var spct = _spctviewmodel.DanhSachSanPhamHoanThien().FirstOrDefault(c => c.ID == saleDetail.IDSPCT);
                    if (saleDetail.SoLuong > spct.SoLuong)
                    {
                        return "Sản phẩm này chỉ còn" + spct.SoLuong;
                    }
                    else
                    {
                        var saledetail = GetAllSaleDetail().FirstOrDefault(c => c.ID == saleDetail.ID);
                        saledetail.SoLuong = saleDetail.SoLuong;
                        saledetail.IDSale = saleDetail.IDSale;
                        saledetail.IDSPCT = saleDetail.IDSPCT;

                        _context.saleChiTiets.Update(saleDetail);
                        _context.SaveChanges();
                        return "Cập nhật thành công.";
                    }
                }
            }
            catch (Exception e)
            {
                return "Thất bại: " + e.Message;
            }

        }

        public string XoaCung(Guid id)
        {
            try
            {
                var detail = _context.saleChiTiets.FirstOrDefault(c => c.ID == id);
                if (detail != null)
                {
                    var spctgoc = _context.sanPhamChiTiets.FirstOrDefault(c => c.ID == detail.IDSPCT);
                    spctgoc.SoLuong = detail.SoLuong + spctgoc.SoLuong;
                    productDetailRepository.CapNhatSanPham(spctgoc);

                    _context.saleChiTiets.Remove(detail);
                    _context.SaveChanges();
                    return "Xóa thành công.";
                }
                return "Xóa thất bại.";
            }
            catch (Exception)
            {
                return "Xóa thất bại.";
            }
        }
    }
}
