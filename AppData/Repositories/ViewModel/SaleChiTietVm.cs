using AppData.DB_Context;
using AppData.Entities.ViewModels;
using AppData.Repositories.SaleRes;
using AppData.Repositories.SaleDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Repositories.ViewModel
{
    public class SaleChiTietVm
    {
        private readonly ApplicationDbContext context;
        private readonly ISaleRepository _sale;
        private readonly ISaleDetailRepository _saleDetail;
        private readonly SPCTViewModel _spct;

        public SaleChiTietVm()
        {
            context = new ApplicationDbContext();
            _sale = new SaleRepository(context);
            _saleDetail = new SaleDetailRepository(context);
            _spct = new SPCTViewModel();
        }

        public List<SaleChiTietViewModel> DanhSachSanPhamSale()
        {
            try
            {
                var lst = from a in _saleDetail.GetAllSaleDetail()
                          join b in _sale.GetAllSale() on a.IDSale equals b.ID
                          join c in _spct.DanhSachSanPhamHoanThien() on a.IDSPCT equals c.ID
                          select new SaleChiTietViewModel
                          {
                              IDSalechitiet = a.ID,
                              IDSale = a.IDSale,
                              IdSanPhamCT = a.IDSPCT,
                              MaSale = b.MaSale,
                              TenSP = c.TenSP,
                              Size = c.Size,
                              Mau = c.MauSac,
                              ThuongHieu = c.TenThuongHieu,
                              TheLoai = c.LoaiSanPham,
                              SoLuong = a.SoLuong,
                              GiaGoc = c.GiaBan,
                              PhanTramGiam = b.PhanTramGiam,
                              lstAnhSanPham = c.lstAnhSanPham,
                              GiaGiam = c.GiaBan - (c.GiaBan * b.PhanTramGiam) / 100,
                              NgayBatDau = b.NgayBatDau,
                              NgayKetThuc = b.NgayKetThuc,
                              MoTa = c.MoTa,
                              TrangThai = a.TrangThai,
                          };
                return lst.ToList();
            } catch
            {
                return null;
            }
           
        }
        public List<SaleChiTietViewModel> DanhSachSanPhamSaleKH()
        {
            try
            {
                var lst = from a in _saleDetail.GetAllSaleDetail()
                          join b in _sale.GetAllSale() on a.IDSale equals b.ID
                          join c in _spct.DanhSachSanPhamHoanThien() on a.IDSPCT equals c.ID
                          select new SaleChiTietViewModel
                          {
                              IDSalechitiet = a.ID,
                              IDSale = a.IDSale,
                              IdSanPhamCT = a.IDSPCT,
                              MaSale = b.MaSale,
                              TenSP = c.TenSP,
                              Size = c.Size,
                              Mau = c.MauSac,
                              ThuongHieu = c.TenThuongHieu,
                              TheLoai = c.LoaiSanPham,
                              SoLuong = a.SoLuong,
                              GiaGoc = c.GiaBan,
                              PhanTramGiam = b.PhanTramGiam,
                              lstAnhSanPham = c.lstAnhSanPham,
                              GiaGiam = c.GiaBan - (c.GiaBan * b.PhanTramGiam) / 100,
                              NgayBatDau = b.NgayBatDau,
                              NgayKetThuc = b.NgayKetThuc,
                              MoTa = c.MoTa,
                              TrangThai = a.TrangThai,
                          };
                return lst.Where(c => c.TrangThai == 1).ToList();
            }
            catch
            {
                return null;
            }

        }
        public SaleChiTietViewModel SanPhamSaleTheoID(Guid id)
        {
            try
            {
                var lst = from a in _saleDetail.GetAllSaleDetail()
                          join b in _sale.GetAllSale() on a.IDSale equals b.ID
                          join c in _spct.DanhSachSanPhamHoanThien() on a.IDSPCT equals c.ID
                          select new SaleChiTietViewModel
                          {
                              IDSalechitiet = a.ID,
                              IDSale = a.IDSale,
                              IdSanPhamCT = a.IDSPCT,
                              MaSale = b.MaSale,
                              TenSP = c.TenSP,
                              Size = c.Size,
                              Mau = c.MauSac,
                              ThuongHieu = c.TenThuongHieu,
                              TheLoai = c.LoaiSanPham,
                              SoLuong = a.SoLuong,
                              GiaGoc = c.GiaBan,
                              PhanTramGiam = b.PhanTramGiam,
                              lstAnhSanPham = c.lstAnhSanPham,
                              GiaGiam = c.GiaBan - (c.GiaBan * b.PhanTramGiam) / 100,
                              NgayBatDau = b.NgayBatDau,
                              NgayKetThuc = b.NgayKetThuc,
                              MoTa = c.MoTa,
                              TrangThai = a.TrangThai,
                          };
                var spsale = lst.FirstOrDefault(c => c.IDSalechitiet == id);
                if (spsale != null)
                {
                    return spsale;
                }
                else
                {
                    return null;
                }
            } catch
            {
                return null;
            }
         
        }
        public List<SaleChiTietViewModel> SanPhamSaleTheoTenSP(string tensp)
        {
            var lst = from a in _saleDetail.GetAllSaleDetail()
                      join b in _sale.GetAllSale() on a.IDSale equals b.ID
                      join c in _spct.DanhSachSanPhamHoanThien() on a.IDSPCT equals c.ID
                      select new SaleChiTietViewModel
                      {
                          IDSalechitiet = a.ID,
                          IDSale = a.IDSale,
                          IdSanPhamCT = a.IDSPCT,
                          MaSale = b.MaSale,
                          TenSP = c.TenSP,
                          Size = c.Size,
                          Mau = c.MauSac,
                          ThuongHieu = c.TenThuongHieu,
                          TheLoai = c.LoaiSanPham,
                          SoLuong = a.SoLuong,
                          GiaGoc = c.GiaBan,
                          PhanTramGiam = b.PhanTramGiam,
                          lstAnhSanPham = c.lstAnhSanPham,
                          GiaGiam = c.GiaBan - (c.GiaBan * b.PhanTramGiam) / 100,
                          NgayBatDau = b.NgayBatDau,
                          NgayKetThuc = b.NgayKetThuc,
                          MoTa = c.MoTa,
                          TrangThai = a.TrangThai,
                      };
            var lstspsale =  lst.Where(c => c.TenSP.ToLower().Contains(tensp.ToLower()) || c.MaSale.ToLower().Contains(tensp.ToLower())).ToList();
            if(lstspsale.Count() > 0)
            {
                return lstspsale;
            } else
            {
                return null;
            }
        }
        public SaleChiTietViewModel SanPhamSaleTheoTenSPMauSizeThuongHieuTheLoai(string tensp , string mau , string size , string thuonghieu , string theloai)
        {
            try
            {
                var lst = from a in _saleDetail.GetAllSaleDetail()
                          join b in _sale.GetAllSale() on a.IDSale equals b.ID
                          join c in _spct.DanhSachSanPhamHoanThien() on a.IDSPCT equals c.ID
                          select new SaleChiTietViewModel
                          {
                              IDSalechitiet = a.ID,
                              IDSale = a.IDSale,
                              IdSanPhamCT = a.IDSPCT,
                              MaSale = b.MaSale,
                              TenSP = c.TenSP,
                              Size = c.Size,
                              Mau = c.MauSac,
                              ThuongHieu = c.TenThuongHieu,
                              TheLoai = c.LoaiSanPham,
                              SoLuong = a.SoLuong,
                              GiaGoc = c.GiaBan,
                              PhanTramGiam = b.PhanTramGiam,
                              lstAnhSanPham = c.lstAnhSanPham,
                              GiaGiam = c.GiaBan - (c.GiaBan * b.PhanTramGiam) / 100,
                              NgayBatDau = b.NgayBatDau,
                              NgayKetThuc = b.NgayKetThuc,
                              MoTa = c.MoTa,
                              TrangThai = a.TrangThai,
                          };
                var spsale = lst.FirstOrDefault(c => c.TenSP.ToLower() == tensp.ToLower() && c.Mau.ToLower() == mau.ToLower() && c.Size.ToLower() == size.ToLower() && c.ThuongHieu.ToLower() == thuonghieu.ToLower() && c.TheLoai.ToLower() == theloai.ToLower());
                if (spsale == null)
                {
                    return null;
                }
                else
                {
                    return spsale;
                }
            } catch
            {
                return null;
            }
          
        }
        public List<SaleChiTietViewModel> SanPhamSaleTheoTenSpThuongHieuTheLoai(string tensp, string thuonghieu, string theloai)
        {
            try
            {
                var lst = from a in _saleDetail.GetAllSaleDetail()
                          join b in _sale.GetAllSale() on a.IDSale equals b.ID
                          join c in _spct.DanhSachSanPhamHoanThien() on a.IDSPCT equals c.ID
                          select new SaleChiTietViewModel
                          {
                              IDSalechitiet = a.ID,
                              IDSale = a.IDSale,
                              IdSanPhamCT = a.IDSPCT,
                              MaSale = b.MaSale,
                              TenSP = c.TenSP,
                              Size = c.Size,
                              Mau = c.MauSac,
                              ThuongHieu = c.TenThuongHieu,
                              TheLoai = c.LoaiSanPham,
                              SoLuong = a.SoLuong,
                              GiaGoc = c.GiaBan,
                              PhanTramGiam = b.PhanTramGiam,
                              lstAnhSanPham = c.lstAnhSanPham,
                              GiaGiam = c.GiaBan - (c.GiaBan * b.PhanTramGiam) / 100,
                              NgayBatDau = b.NgayBatDau,
                              NgayKetThuc = b.NgayKetThuc,
                              MoTa = c.MoTa,
                              TrangThai = a.TrangThai,
                          };
                var spsale = lst.Where(c => c.TenSP.ToLower() == tensp.ToLower() && c.ThuongHieu.ToLower() == thuonghieu.ToLower() && c.TheLoai.ToLower() == theloai.ToLower()).ToList();
                if (spsale.Count == 0)
                {
                    return null;
                }
                else
                {
                    return spsale;
                }
            }
            catch
            {
                return null;
            }
        }
        public List<SaleChiTietViewModel> SanPhamSaleTruyVanMau(string tensp, string size, string thuonghieu, string theloai)
        {
            try
            {
                var lst = from a in _saleDetail.GetAllSaleDetail()
                          join b in _sale.GetAllSale() on a.IDSale equals b.ID
                          join c in _spct.DanhSachSanPhamHoanThien() on a.IDSPCT equals c.ID
                          select new SaleChiTietViewModel
                          {
                              IDSalechitiet = a.ID,
                              IDSale = a.IDSale,
                              IdSanPhamCT = a.IDSPCT,
                              MaSale = b.MaSale,
                              TenSP = c.TenSP,
                              Size = c.Size,
                              Mau = c.MauSac,
                              ThuongHieu = c.TenThuongHieu,
                              TheLoai = c.LoaiSanPham,
                              SoLuong = a.SoLuong,
                              GiaGoc = c.GiaBan,
                              PhanTramGiam = b.PhanTramGiam,
                              lstAnhSanPham = c.lstAnhSanPham,
                              GiaGiam = c.GiaBan - (c.GiaBan * b.PhanTramGiam) / 100,
                              NgayBatDau = b.NgayBatDau,
                              NgayKetThuc = b.NgayKetThuc,
                              TrangThai = a.TrangThai,
                              MoTa = c.MoTa,
                          };
                var spsale = lst.Where(c => c.TenSP.ToLower() == tensp.ToLower() && c.Size.ToLower() == size.ToLower() && c.ThuongHieu.ToLower() == thuonghieu.ToLower() && c.TheLoai.ToLower() == theloai.ToLower()).ToList();
                if (spsale == null)
                {
                    return null;
                }
                else
                {
                    return spsale;
                }
            }
            catch
            {
                return null;
            }
        }
        public List<SaleChiTietViewModel> SanPhamSaleTruyVanSize(string tensp, string mau, string thuonghieu, string theloai)
        {
            try
            {
                var lst = from a in _saleDetail.GetAllSaleDetail()
                          join b in _sale.GetAllSale() on a.IDSale equals b.ID
                          join c in _spct.DanhSachSanPhamHoanThien() on a.IDSPCT equals c.ID
                          select new SaleChiTietViewModel
                          {
                              IDSalechitiet = a.ID,
                              IDSale = a.IDSale,
                              IdSanPhamCT = a.IDSPCT,
                              MaSale = b.MaSale,
                              TenSP = c.TenSP,
                              Size = c.Size,
                              Mau = c.MauSac,
                              ThuongHieu = c.TenThuongHieu,
                              TheLoai = c.LoaiSanPham,
                              SoLuong = a.SoLuong,
                              GiaGoc = c.GiaBan,
                              PhanTramGiam = b.PhanTramGiam,
                              lstAnhSanPham = c.lstAnhSanPham,
                              GiaGiam = c.GiaBan - (c.GiaBan * b.PhanTramGiam) / 100,
                              NgayBatDau = b.NgayBatDau,
                              NgayKetThuc = b.NgayKetThuc,
                              TrangThai = a.TrangThai,
                              MoTa = c.MoTa,
                          };
                var spsale = lst.Where(c => c.TenSP.ToLower() == tensp.ToLower() && c.Mau.ToLower() == mau.ToLower() &&  c.ThuongHieu.ToLower() == thuonghieu.ToLower() && c.TheLoai.ToLower() == theloai.ToLower()).ToList();
                if (spsale == null)
                {
                    return null;
                }
                else
                {
                    return spsale;
                }
            }
            catch
            {
                return null;
            }
        }
        public List<SaleChiTietViewModel> LocSanPhamSaleTheoMauSizeThuongHieuLoaiSanPham(string? mau, string? size, string? thuonghieu, string? loaisanpham)
        {
            try
            {
                if (mau == null && size == null && thuonghieu == null && loaisanpham == null)
                {
                    return DanhSachSanPhamSale();
                }
                else
                {
                    if (mau != null && size == null && thuonghieu == null && loaisanpham == null)
                    {
                        return DanhSachSanPhamSale().Where(c => c.Mau.ToLower() == mau.ToLower()).ToList();
                    }
                    else if (mau == null && size != null && thuonghieu == null && loaisanpham == null)
                    {
                        return DanhSachSanPhamSale().Where(c => c.Size.ToLower() == size.ToLower()).ToList();
                    }
                    else if (mau == null && size == null && thuonghieu != null && loaisanpham == null)
                    {
                        return DanhSachSanPhamSale().Where(c => c.ThuongHieu.ToLower() == thuonghieu.ToLower()).ToList();
                    }
                    else if (mau == null && size == null && thuonghieu == null && loaisanpham != null)
                    {
                        return DanhSachSanPhamSale().Where(c => c.TheLoai.ToLower() == loaisanpham.ToLower()).ToList();
                    }
                    else if (mau != null && size != null && thuonghieu == null && loaisanpham == null)
                    {
                        return DanhSachSanPhamSale().Where(c => c.Mau.ToLower() == mau.ToLower() && c.Size.ToLower() == size.ToLower()).ToList();
                    }
                    else if (mau != null && size == null && thuonghieu != null && loaisanpham == null)
                    {
                        return DanhSachSanPhamSale().Where(c => c.Mau.ToLower() == mau.ToLower() && c.ThuongHieu.ToLower() == thuonghieu.ToLower()).ToList();
                    }
                    else if (mau != null && size == null && thuonghieu == null && loaisanpham != null)
                    {
                        return DanhSachSanPhamSale().Where(c => c.Mau.ToLower() == mau.ToLower() && c.TheLoai.ToLower() == loaisanpham.ToLower()).ToList();
                    }
                    else if (mau == null && size != null && thuonghieu != null && loaisanpham == null)
                    {
                        return DanhSachSanPhamSale().Where(c => c.Size.ToLower() == size.ToLower() && c.ThuongHieu.ToLower() == thuonghieu ).ToList();
                    }
                    else if (mau == null && size != null && thuonghieu == null && loaisanpham != null)
                    {
                        return DanhSachSanPhamSale().Where(c => c.Size.ToLower() == size.ToLower() && c.TheLoai.ToLower() == loaisanpham.ToLower()).ToList();
                    }
                    else if (mau == null && size == null && thuonghieu != null && loaisanpham != null)
                    {
                        return DanhSachSanPhamSale().Where(c => c.ThuongHieu.ToLower() == thuonghieu.ToLower() && c.TheLoai.ToLower() == loaisanpham.ToLower()).ToList();
                    }
                    else if (mau != null && size != null && thuonghieu != null && loaisanpham == null)
                    {
                        return DanhSachSanPhamSale().Where(c => c.Mau.ToLower() == mau.ToLower() && c.Size.ToLower() == size.ToLower() && c.ThuongHieu.ToLower() == thuonghieu.ToLower()).ToList();
                    }
                    else if (mau != null && size == null && thuonghieu != null && loaisanpham != null)
                    {
                        return DanhSachSanPhamSale().Where(c => c.Mau.ToLower() == mau.ToLower() && c.ThuongHieu.ToLower() == thuonghieu.ToLower() && c.TheLoai.ToLower() == loaisanpham.ToLower()).ToList();
                    }
                    else if (mau == null && size != null && thuonghieu != null && loaisanpham != null)
                    {
                        return DanhSachSanPhamSale().Where(c => c.TheLoai.ToLower() == loaisanpham.ToLower() && c.Size.ToLower() == size.ToLower() && c.ThuongHieu.ToLower() == thuonghieu.ToLower()).ToList();
                    }
                    else
                    {
                        return DanhSachSanPhamSale().Where(c => c.TheLoai.ToLower() == loaisanpham.ToLower() && c.Size.ToLower() == size.ToLower() && c.ThuongHieu.ToLower() == thuonghieu.ToLower() && c.Mau.ToLower() == mau.ToLower()).ToList();
                    }
                }
            }
            catch
            {
                return null;
            }

        }
        public List<SaleChiTietViewModel> LocTheoNgay(DateTime ngaybatdau , DateTime ngayketthuc)
        {
            return DanhSachSanPhamSale().Where(c => c.NgayBatDau.Value.Date <= ngaybatdau.Date && c.NgayKetThuc.Value.Date <= ngayketthuc.Date).ToList();
        }
        public List<SaleChiTietViewModel> LocTheoGia(decimal giamax)
        {
            try
            {
                var lstspsale = DanhSachSanPhamSale().Where(c => c.GiaGiam <= giamax).ToList();
                return lstspsale;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<SaleChiTietViewModel> SanPhamSaleTheoLoaisanpham(string loaisp)
        {
            var lst = from a in _saleDetail.GetAllSaleDetail()
                      join b in _sale.GetAllSale() on a.IDSale equals b.ID
                      join c in _spct.DanhSachSanPhamHoanThien() on a.IDSPCT equals c.ID
                      select new SaleChiTietViewModel
                      {
                          IDSalechitiet = a.ID,
                          IDSale = a.IDSale,
                          IdSanPhamCT = a.IDSPCT,
                          MaSale = b.MaSale,
                          TenSP = c.TenSP,
                          Size = c.Size,
                          Mau = c.MauSac,
                          ThuongHieu = c.TenThuongHieu,
                          TheLoai = c.LoaiSanPham,
                          SoLuong = a.SoLuong,
                          GiaGoc = c.GiaBan,
                          PhanTramGiam = b.PhanTramGiam,
                          lstAnhSanPham = c.lstAnhSanPham,
                          GiaGiam = c.GiaBan - (c.GiaBan * b.PhanTramGiam) / 100,
                          NgayBatDau = b.NgayBatDau,
                          NgayKetThuc = b.NgayKetThuc,
                          MoTa = c.MoTa,
                          TrangThai = a.TrangThai,
                      };
            var lstspsale = lst.Where(c => c.TheLoai.ToLower().Trim() == loaisp.ToLower().Trim()).ToList();
            if (lstspsale.Count() > 0)
            {
                return lstspsale;
            }
            else
            {
                return null;
            }
        }
        public List<SaleChiTietViewModel> SanPhamSaleTheoThuonghieu(string Thuonghieu)
        {
            var lst = from a in _saleDetail.GetAllSaleDetail()
                      join b in _sale.GetAllSale() on a.IDSale equals b.ID
                      join c in _spct.DanhSachSanPhamHoanThien() on a.IDSPCT equals c.ID
                      select new SaleChiTietViewModel
                      {
                          IDSalechitiet = a.ID,
                          IDSale = a.IDSale,
                          IdSanPhamCT = a.IDSPCT,
                          MaSale = b.MaSale,
                          TenSP = c.TenSP,
                          Size = c.Size,
                          Mau = c.MauSac,
                          ThuongHieu = c.TenThuongHieu,
                          TheLoai = c.LoaiSanPham,
                          SoLuong = a.SoLuong,
                          GiaGoc = c.GiaBan,
                          PhanTramGiam = b.PhanTramGiam,
                          lstAnhSanPham = c.lstAnhSanPham,
                          GiaGiam = c.GiaBan - (c.GiaBan * b.PhanTramGiam) / 100,
                          NgayBatDau = b.NgayBatDau,
                          NgayKetThuc = b.NgayKetThuc,
                          MoTa = c.MoTa,
                          TrangThai = a.TrangThai,
                      };
            var lstspsale = lst.Where(c => c.ThuongHieu.ToLower().Trim() == Thuonghieu.ToLower().Trim()).ToList();
            if (lstspsale.Count() > 0)
            {
                return lstspsale;
            }
            else
            {
                return null;
            }
        }

    }
}
