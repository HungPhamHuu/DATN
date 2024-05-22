using AppData.DB_Context;
using AppData.Entities.Models;
using AppData.Entities.ViewModels;
using AppData.Repositories.Color;
using AppData.Repositories.Product;
using AppData.Repositories.ProductDetail;
using AppData.Repositories.Sizes;
using AppData.Repositories.TH;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Repositories.ViewModel
{
    public class SPCTViewModel
    {

        private readonly ApplicationDbContext _dbContext;
        private readonly IMauSacRepository _MauSacrepository;
        private readonly ISizeRepository _SizeRepository;
        private readonly IProductRepository _productRepository;
        private readonly IThuongHieuRepository _thuongHieuRepository;
        private readonly IProductDetailRepository _productDetailRepository;
        public SPCTViewModel()
        {
            _dbContext = new ApplicationDbContext();
            _MauSacrepository = new MauSacRepository(_dbContext);
            _thuongHieuRepository = new ThuongHieuRepository(_dbContext);
            _productDetailRepository = new ProductDetailRepository(_dbContext);
            _productRepository = new ProductRepository(_dbContext);
            _SizeRepository = new SizeRepository(_dbContext);
        }
        public List<SanPhamChiTietViewModel> DanhSachSanPhamHoanThien()
        {
            try
            {
                var lstspct = from a in _dbContext.sanPhamChiTiets.ToList()
                              join b in _dbContext.sizes.ToList() on a.IDSize equals b.ID
                              join c in _dbContext.sanPhams.ToList() on a.IDSP equals c.ID
                              join d in _dbContext.mauSacs.ToList() on a.IDMauSac equals d.ID
                              join e in _dbContext.thuonghieu.ToList() on a.IDThuongHieu equals e.ID
                              select new SanPhamChiTietViewModel
                              {
                                  ID = a.ID,
                                  TenSP = c.TenSanPham,
                                  Size = b.SizeNumber,
                                  TenThuongHieu = e.TenThuongHieu,
                                  MauSac = d.TenMauSac,
                                  MaSPCT = a.MaSPCT,
                                  LoaiSanPham = a.LoaiSanPham,
                                  SoLuong = a.SoLuong,
                                  GiaBan = a.GiaBan,
                                  MoTa = a.MoTa,
                                  QrImage = a.QrImage,
                                  TrangThai = a.TrangThai,
                              };
                var lst = lstspct.OrderByDescending(c => c.MaSPCT).ToList();
                foreach (var spct in lst)
                {
                    var anhsp = _dbContext.anhSanPhams.Where(c => c.IdSanPhamChiTiet == spct.ID).Select(c => new Anh { ID = c.Idanh, DuongDan = c.Anh.DuongDan }).ToList();
                    spct.lstAnhSanPham = anhsp;
                }
                return lst.ToList();


            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<SanPhamChiTietViewModel> DanhSachSanPhamHoanThienTheoTenTheLoaiHang()
        {
            try
            {
                var lstspct = from a in _dbContext.sanPhamChiTiets.ToList()
                              join b in _dbContext.sizes.ToList() on a.IDSize equals b.ID
                              join c in _dbContext.sanPhams.ToList() on a.IDSP equals c.ID
                              join d in _dbContext.mauSacs.ToList() on a.IDMauSac equals d.ID
                              join e in _dbContext.thuonghieu.ToList() on a.IDThuongHieu equals e.ID
                              select new SanPhamChiTietViewModel
                              {
                                  ID = a.ID,
                                  TenSP = c.TenSanPham,
                                  Size = b.SizeNumber,
                                  TenThuongHieu = e.TenThuongHieu,
                                  MauSac = d.TenMauSac,
                                  MaSPCT = a.MaSPCT,
                                  LoaiSanPham = a.LoaiSanPham,
                                  SoLuong = a.SoLuong,
                                  GiaBan = a.GiaBan,
                                  MoTa = a.MoTa,
                                  QrImage = a.QrImage,
                                  TrangThai = a.TrangThai,
                              };
                var lst = lstspct.Where(c => c.TrangThai == 1).OrderByDescending(c => c.MaSPCT).ToList();
                foreach (var spct in lst)
                {
                    var anhsp = _dbContext.anhSanPhams.Where(c => c.IdSanPhamChiTiet == spct.ID).Select(c => new Anh { ID = c.Idanh, DuongDan = c.Anh.DuongDan }).ToList();
                    spct.lstAnhSanPham = anhsp;
                }
                var lstsp = lst.GroupBy(c => new { c.TenSP, c.TenThuongHieu, c.LoaiSanPham }).Select(c => c.First()).ToList();
                return lstsp.ToList();


            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<SanPhamChiTietViewModel> DanhSachSanPhamHoanThienTheoTen(string tensp)
        {
            try
            {
                var lst = from a in _dbContext.sanPhamChiTiets.ToList()
                          join b in _dbContext.sizes.ToList() on a.IDSize equals b.ID
                          join c in _dbContext.sanPhams.ToList() on a.IDSP equals c.ID
                          join d in _dbContext.mauSacs.ToList() on a.IDMauSac equals d.ID
                          join e in _dbContext.thuonghieu.ToList() on a.IDThuongHieu equals e.ID
                          select new SanPhamChiTietViewModel
                          {
                              ID = a.ID,
                              TenSP = c.TenSanPham,
                              Size = b.SizeNumber,
                              TenThuongHieu = e.TenThuongHieu,
                              MauSac = d.TenMauSac,
                              MaSPCT = a.MaSPCT,
                              LoaiSanPham = a.LoaiSanPham,
                              SoLuong = a.SoLuong,
                              GiaBan = a.GiaBan,
                              MoTa = a.MoTa,
                              QrImage = a.QrImage,
                              TrangThai = a.TrangThai,
                          };

                var lstspct = lst.Where(c => c.TenSP.ToLower().Contains(tensp.ToLower()) || c.MaSPCT.ToLower().Contains(tensp.ToLower())).OrderByDescending(c => c.MaSPCT).ToList();
                foreach (var spct in lstspct)
                {
                    var anhsp = _dbContext.anhSanPhams.Where(c => c.IdSanPhamChiTiet == spct.ID).Select(c => new Anh { ID = c.Idanh, DuongDan = c.Anh.DuongDan }).ToList();
                    spct.lstAnhSanPham = anhsp;
                }
                //var lstsp = lstspct.GroupBy(c => new {c.MauSac , c.Size}).Select(c => c.First()).ToList();
                return lstspct.ToList();


            }
            catch (Exception ex)
            {
                return null;
            }
        }
        
        public List<SanPhamChiTietViewModel> LayDanhSachSanPhamHoanThienTheoTen(string tensp, string thuonghieu , string loaisp)
        {
            try
            {
                var lst = from a in _dbContext.sanPhamChiTiets.ToList()
                          join b in _dbContext.sizes.ToList() on a.IDSize equals b.ID
                          join c in _dbContext.sanPhams.ToList() on a.IDSP equals c.ID
                          join d in _dbContext.mauSacs.ToList() on a.IDMauSac equals d.ID
                          join e in _dbContext.thuonghieu.ToList() on a.IDThuongHieu equals e.ID
                          select new SanPhamChiTietViewModel
                          {
                              ID = a.ID,
                              TenSP = c.TenSanPham,
                              Size = b.SizeNumber,
                              TenThuongHieu = e.TenThuongHieu,
                              MauSac = d.TenMauSac,
                              MaSPCT = a.MaSPCT,
                              LoaiSanPham = a.LoaiSanPham,
                              SoLuong = a.SoLuong,
                              GiaBan = a.GiaBan,
                              MoTa = a.MoTa,
                              QrImage = a.QrImage,
                              TrangThai = a.TrangThai,
                          };

                var lstspct = lst.Where(c => c.TenSP.ToLower() == tensp.ToLower() && c.TenThuongHieu.ToLower() == thuonghieu.ToLower() && c.LoaiSanPham.ToLower() == loaisp.ToLower() && c.TrangThai == 1).ToList();
                foreach (var spct in lstspct)
                {
                    var anhsp = _dbContext.anhSanPhams.Where(c => c.IdSanPhamChiTiet == spct.ID).Select(c => new Anh { ID = c.Idanh, DuongDan = c.Anh.DuongDan }).ToList();
                    spct.lstAnhSanPham = anhsp;
                }
                var lstsp = lstspct.GroupBy(c => new {c.MauSac , c.Size}).Select(c => c.First()).ToList();
                return lstsp.ToList();


            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public SanPhamChiTietViewModel SanPhamHoanThienTheoId(Guid ID)
        {
            try
            {
                var lst = from a in _dbContext.sanPhamChiTiets.ToList()
                          join b in _dbContext.sizes.ToList() on a.IDSize equals b.ID
                          join c in _dbContext.sanPhams.ToList() on a.IDSP equals c.ID
                          join d in _dbContext.mauSacs.ToList() on a.IDMauSac equals d.ID
                          join e in _dbContext.thuonghieu.ToList() on a.IDThuongHieu equals e.ID
                          select new SanPhamChiTietViewModel
                          {
                              ID = a.ID,
                              TenSP = c.TenSanPham,
                              Size = b.SizeNumber,
                              TenThuongHieu = e.TenThuongHieu,
                              MauSac = d.TenMauSac,
                              MaSPCT = a.MaSPCT,
                              LoaiSanPham = a.LoaiSanPham,
                              SoLuong = a.SoLuong,
                              GiaBan = a.GiaBan,
                              MoTa = a.MoTa,
                              QrImage = a.QrImage,
                              TrangThai = a.TrangThai,
                          };
                var lstspct = lst.FirstOrDefault(c => c.ID == ID);

                var anhsp = _dbContext.anhSanPhams.Where(c => c.IdSanPhamChiTiet == ID).Select(c => new Anh { ID = c.Idanh, DuongDan = c.Anh.DuongDan }).ToList();
                lstspct.lstAnhSanPham = anhsp;

                return lstspct;


            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public SanPhamChiTietViewModel SanPhamHoanThienTheotenmausize(string ten, string mau, string size , string loaisp, string tenthuonghieu)
        {
            try
            {
                var lst = from a in _dbContext.sanPhamChiTiets.ToList()
                          join b in _dbContext.sizes.ToList() on a.IDSize equals b.ID
                          join c in _dbContext.sanPhams.ToList() on a.IDSP equals c.ID
                          join d in _dbContext.mauSacs.ToList() on a.IDMauSac equals d.ID
                          join e in _dbContext.thuonghieu.ToList() on a.IDThuongHieu equals e.ID
                          select new SanPhamChiTietViewModel
                          {
                              ID = a.ID,
                              TenSP = c.TenSanPham,
                              Size = b.SizeNumber,
                              TenThuongHieu = e.TenThuongHieu,
                              MauSac = d.TenMauSac,
                              MaSPCT = a.MaSPCT,
                              LoaiSanPham = a.LoaiSanPham,
                              SoLuong = a.SoLuong,
                              GiaBan = a.GiaBan,
                              MoTa = a.MoTa,
                              QrImage = a.QrImage,
                              TrangThai = a.TrangThai,
                          };
                var lstspct = lst.FirstOrDefault(c => c.TenSP.ToLower() == ten.ToLower() && c.MauSac.ToLower() == mau.ToLower() && c.Size.ToLower() == size.ToLower() && c.TenThuongHieu.ToLower() == tenthuonghieu.ToLower() && c.LoaiSanPham.ToLower() == loaisp.ToLower() && c.TrangThai == 1);

                var anhsp = _dbContext.anhSanPhams.Where(c => c.IdSanPhamChiTiet == lstspct.ID).Select(c => new Anh { ID = c.Idanh, DuongDan = c.Anh.DuongDan }).ToList();
                lstspct.lstAnhSanPham = anhsp;

                return lstspct;


            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<SanPhamChiTietViewModel> DanhSachSanPhamHoanThienTheoSize(string tensp, string size, string loaisp, string tenthuonghieu)
        {
            try
            {
                var lst = from a in _dbContext.sanPhamChiTiets.ToList()
                          join b in _dbContext.sizes.ToList() on a.IDSize equals b.ID
                          join c in _dbContext.sanPhams.ToList() on a.IDSP equals c.ID
                          join d in _dbContext.mauSacs.ToList() on a.IDMauSac equals d.ID
                          join e in _dbContext.thuonghieu.ToList() on a.IDThuongHieu equals e.ID
                          select new SanPhamChiTietViewModel
                          {
                              ID = a.ID,
                              TenSP = c.TenSanPham,
                              Size = b.SizeNumber,
                              TenThuongHieu = e.TenThuongHieu,
                              MauSac = d.TenMauSac,
                              MaSPCT = a.MaSPCT,
                              LoaiSanPham = a.LoaiSanPham,
                              SoLuong = a.SoLuong,
                              GiaBan = a.GiaBan,
                              MoTa = a.MoTa,
                              QrImage = a.QrImage,
                              TrangThai = a.TrangThai,
                          };

                var lstspct = lst.Where(c => c.TenSP.ToLower() == tensp.ToLower() && c.Size.ToLower() == size.ToLower() && c.TenThuongHieu.ToLower() == tenthuonghieu.ToLower() && c.LoaiSanPham.ToLower() == loaisp.ToLower() && c.TrangThai == 1).ToList();
                foreach (var spct in lstspct)
                {
                    var anhsp = _dbContext.anhSanPhams.Where(c => c.IdSanPhamChiTiet == spct.ID).Select(c => new Anh { ID = c.Idanh, DuongDan = c.Anh.DuongDan }).ToList();
                    spct.lstAnhSanPham = anhsp;
                }
                return lstspct.ToList();


            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<SanPhamChiTietViewModel> DanhSachSanPhamHoanThienTheoMau(string Tensp, string MauSac, string loaisp, string tenthuonghieu)
        {
            try
            {
                var lst = from a in _dbContext.sanPhamChiTiets.ToList()
                          join b in _dbContext.sizes.ToList() on a.IDSize equals b.ID
                          join c in _dbContext.sanPhams.ToList() on a.IDSP equals c.ID
                          join d in _dbContext.mauSacs.ToList() on a.IDMauSac equals d.ID
                          join e in _dbContext.thuonghieu.ToList() on a.IDThuongHieu equals e.ID
                          select new SanPhamChiTietViewModel
                          {
                              ID = a.ID,
                              TenSP = c.TenSanPham,
                              Size = b.SizeNumber,
                              TenThuongHieu = e.TenThuongHieu,
                              MauSac = d.TenMauSac,
                              MaSPCT = a.MaSPCT,
                              LoaiSanPham = a.LoaiSanPham,
                              SoLuong = a.SoLuong,
                              GiaBan = a.GiaBan,
                              MoTa = a.MoTa,
                              QrImage = a.QrImage,
                              TrangThai = a.TrangThai,
                          };

                var lstspct = lst.Where(c => c.TenSP.ToLower() == Tensp.ToLower() && c.MauSac.ToLower() == MauSac.ToLower()  && c.TenThuongHieu.ToLower() == tenthuonghieu.ToLower() && c.LoaiSanPham.ToLower() == loaisp.ToLower() && c.TrangThai == 1).ToList();
                foreach (var spct in lstspct)
                {
                    var anhsp = _dbContext.anhSanPhams.Where(c => c.IdSanPhamChiTiet == spct.ID).Select(c => new Anh { ID = c.Idanh, DuongDan = c.Anh.DuongDan }).ToList();
                    spct.lstAnhSanPham = anhsp;
                }
                return lstspct.ToList();


            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<SanPhamChiTietViewModel> LocSanPhamTheoMauSizeThuongHieuLoaiSanPham(string? mau, string? size, string? thuonghieu, string? loaisanpham)
        {
            try
            {
                if (mau == null && size == null && thuonghieu == null && loaisanpham == null)
                {
                    return DanhSachSanPhamHoanThien();
                }
                else
                {
                    if (mau != null && size == null && thuonghieu == null && loaisanpham == null)
                    {
                        return DanhSachSanPhamHoanThien().Where(c => c.MauSac.ToLower() == mau.ToLower() && c.TrangThai == 1).ToList();
                    }
                    else if (mau == null && size != null && thuonghieu == null && loaisanpham == null)
                    {
                        return DanhSachSanPhamHoanThien().Where(c => c.Size.ToLower() == size.ToLower() && c.TrangThai == 1).ToList();
                    }
                    else if (mau == null && size == null && thuonghieu != null && loaisanpham == null)
                    {
                        return DanhSachSanPhamHoanThien().Where(c => c.TenThuongHieu.ToLower() == thuonghieu.ToLower() && c.TrangThai == 1).ToList();
                    }
                    else if (mau == null && size == null && thuonghieu == null && loaisanpham != null)
                    {
                        return DanhSachSanPhamHoanThien().Where(c => c.LoaiSanPham.ToLower() == loaisanpham.ToLower() && c.TrangThai == 1).ToList();
                    }
                    else if (mau != null && size != null && thuonghieu == null && loaisanpham == null)
                    {
                        return DanhSachSanPhamHoanThien().Where(c => c.MauSac.ToLower() == mau.ToLower() && c.Size.ToLower() == size.ToLower() && c.TrangThai == 1).ToList();
                    }
                    else if (mau != null && size == null && thuonghieu != null && loaisanpham == null)
                    {
                        return DanhSachSanPhamHoanThien().Where(c => c.MauSac.ToLower() == mau.ToLower() && c.TenThuongHieu.ToLower() == thuonghieu.ToLower() && c.TrangThai == 1).ToList();
                    }
                    else if (mau != null && size == null && thuonghieu == null && loaisanpham != null)
                    {
                        return DanhSachSanPhamHoanThien().Where(c => c.MauSac.ToLower() == mau.ToLower() && c.LoaiSanPham.ToLower() == loaisanpham.ToLower() && c.TrangThai == 1).ToList();
                    }
                    else if (mau == null && size != null && thuonghieu != null && loaisanpham == null)
                    {
                        return DanhSachSanPhamHoanThien().Where(c => c.Size.ToLower() == size.ToLower() && c.TenThuongHieu.ToLower() == thuonghieu.ToLower() && c.TrangThai == 1).ToList();
                    }
                    else if (mau == null && size != null && thuonghieu == null && loaisanpham != null)
                    {
                        return DanhSachSanPhamHoanThien().Where(c => c.Size.ToLower() == size.ToLower() && c.LoaiSanPham.ToLower() == loaisanpham.ToLower() && c.TrangThai == 1).ToList();
                    }
                    else if (mau == null && size == null && thuonghieu != null && loaisanpham != null)
                    {
                        return DanhSachSanPhamHoanThien().Where(c => c.TenThuongHieu.ToLower() == thuonghieu.ToLower() && c.LoaiSanPham.ToLower() == loaisanpham.ToLower() && c.TrangThai == 1).ToList();
                    }
                    else if (mau != null && size != null && thuonghieu != null && loaisanpham == null)
                    {
                        return DanhSachSanPhamHoanThien().Where(c => c.MauSac.ToLower() == mau.ToLower() && c.Size.ToLower() == size.ToLower() && c.TenThuongHieu.ToLower() == thuonghieu.ToLower() && c.TrangThai == 1).ToList();
                    }
                    else if (mau != null && size == null && thuonghieu != null && loaisanpham != null)
                    {
                        return DanhSachSanPhamHoanThien().Where(c => c.MauSac.ToLower() == mau.ToLower() && c.TenThuongHieu.ToLower() == thuonghieu.ToLower() && c.LoaiSanPham.ToLower() == loaisanpham.ToLower() && c.TrangThai == 1).ToList();
                    }
                    else if (mau == null && size != null && thuonghieu != null && loaisanpham != null)
                    {
                        return DanhSachSanPhamHoanThien().Where(c => c.LoaiSanPham.ToLower() == loaisanpham.ToLower() && c.Size.ToLower() == size.ToLower() && c.TenThuongHieu.ToLower() == thuonghieu.ToLower() && c.TrangThai == 1).ToList();
                    }
                    else
                    {
                        return DanhSachSanPhamHoanThien().Where(c => c.LoaiSanPham == loaisanpham && c.Size == size && c.TenThuongHieu == thuonghieu && c.MauSac == mau && c.TrangThai == 1).ToList();
                    }
                }
            }
            catch
            {
                return null;
            }

        }
        public List<SanPhamChiTietViewModel> DanhSachTheLoaiSanPham()
        {
            try
            {
                var danhsachtheloai = DanhSachSanPhamHoanThien().GroupBy(c => c.LoaiSanPham).Select(c => c.First()).ToList();
                return danhsachtheloai;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<SanPhamChiTietViewModel> LocTheoLoaiSanPham(string loaisanpham)
        {
            try
            {
                var danhsachtheloai = DanhSachSanPhamHoanThien().Where(c => c.LoaiSanPham.ToLower().Trim() == loaisanpham.ToLower().Trim() && c.TrangThai == 1).ToList();
                return danhsachtheloai;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<SanPhamChiTietViewModel> DanhSachThuongHieu()
        {
            try
            {
                var danhsachtheloai = DanhSachSanPhamHoanThien().GroupBy(c => c.TenThuongHieu).Select(c => c.First()).ToList();
                return danhsachtheloai;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<SanPhamChiTietViewModel> LocTheoThuongHieu(string thuonghieu)
        {
            try
            {
                var danhsachtheloai = DanhSachSanPhamHoanThien().Where(c => c.TenThuongHieu.ToLower().Trim() == thuonghieu.ToLower().Trim() && c.TrangThai == 1).ToList();
                return danhsachtheloai;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<SanPhamChiTietViewModel> DanhSachGiaCuaSanPham()
        {
            try
            {
                var danhsachtheloai = DanhSachSanPhamHoanThien().GroupBy(c => c.GiaBan).Select(c => c.First()).ToList();
                return danhsachtheloai;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<SanPhamChiTietViewModel> LocTheoGia(decimal giamin, decimal giamax)
        {
            try
            {
                var danhsachtheloai = DanhSachSanPhamHoanThien().Where(c => c.GiaBan >= giamin && c.GiaBan <= giamax && c.TrangThai == 1).ToList();
                return danhsachtheloai;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<SanPhamChiTietViewModel> DanhSachSize()
        {
            try
            {
                var danhsachsize = DanhSachSanPhamHoanThien().GroupBy(c => c.Size).Select(c => c.First()).ToList();
                return danhsachsize;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<SanPhamChiTietViewModel> LoctheoSize(string size)
        {
            try
            {
                var danhsachtheosize = DanhSachSanPhamHoanThien().Where(c => c.TenThuongHieu == size && c.TrangThai == 1).ToList();
                return danhsachtheosize;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }
}