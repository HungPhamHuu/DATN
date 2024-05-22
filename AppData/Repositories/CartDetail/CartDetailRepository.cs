using AppData.DB_Context;
using AppData.Entities.Models;
using AppData.Entities.ViewModels;
using AppData.Entities.ViewModels.GioHangCTVM;
using AppData.Repositories.Cart;
using AppData.Repositories.ViewModel;
using Microsoft.Azure.Cosmos.Serialization.HybridRow;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Repositories.CartDetail
{
    public class CartDetailRepository : ICartDetailRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ICartRepository cartRepository;
        private readonly SPCTViewModel viewModel;
        private readonly SaleChiTietVm salechitietvm;
        public CartDetailRepository(ApplicationDbContext context)
        {
            _context = context;
            cartRepository = new CartRepository(context);
            viewModel = new SPCTViewModel();
            salechitietvm = new SaleChiTietVm();
        }
        public string CreateCartDetail(Guid idnguoidung, GioHangChiTiet obj)
        {

            try
            {

                var gh = _context.gioHangs.FirstOrDefault(c => c.IDNguoiDung == idnguoidung);
                var spct = _context.sanPhamChiTiets.ToList().FirstOrDefault(c => c.ID == obj.IDSPCT);
                var sale = salechitietvm.DanhSachSanPhamSale().FirstOrDefault(c => c.IDSalechitiet == obj.IDSaleCT);
                if (gh == null)
                {
                    GioHang ghthem = new GioHang()
                    {
                        ID = Guid.NewGuid(),
                        IDNguoiDung = idnguoidung,
                    };
                    _context.gioHangs.Add(ghthem);
                    _context.SaveChanges();
                    GioHangChiTiet ghct = new GioHangChiTiet();
                    if (obj.IDSPCT == null)
                    {
                        ghct.ID = Guid.NewGuid();
                        ghct.IDGioHang = ghthem.ID;
                        ghct.IDSaleCT = obj.IDSaleCT;
                        ghct.IDSPCT = null;
                        ghct.SoLuong = obj.SoLuong == null ? 1 : obj.SoLuong;
                        ghct.Gia = sale.GiaGiam;
                    }
                    else
                    {
                        ghct.ID = Guid.NewGuid();
                        ghct.IDGioHang = ghthem.ID;
                        ghct.IDSaleCT = null;
                        ghct.IDSPCT = obj.IDSPCT;
                        ghct.SoLuong = obj.SoLuong == null ? 1 : obj.SoLuong;
                        ghct.Gia = spct.GiaBan;
                    }
                    _context.gioHangChiTiets.Add(ghct);
                    _context.SaveChanges();
                    return "Thêm thành công.";
                }
                else
                {
                    if (spct == null)
                    {
                        if (GetCartDetailByIdNguoiDung(idnguoidung).Any(c => c.IDSPCT == obj.IDSaleCT) == true)
                        {
                            var sale1 = GetCartDetailByIdNguoiDung(idnguoidung).FirstOrDefault(c => c.IDSaleCT == obj.IDSaleCT);
                            sale1.SoLuong = obj.SoLuong;
                            _context.gioHangChiTiets.Update(sale1);
                            _context.SaveChanges();
                            return "Thêm số lượng thành công.";
                        }
                        else
                        {
                            GioHangChiTiet ghct = new GioHangChiTiet();
                            ghct.ID = Guid.NewGuid();
                            ghct.IDGioHang = gh.ID;
                            ghct.IDSaleCT = obj.IDSaleCT;
                            ghct.IDSPCT = null;
                            ghct.SoLuong = obj.SoLuong;
                            ghct.Gia = sale.GiaGiam;
                            _context.gioHangChiTiets.Add(ghct);
                            _context.SaveChanges();
                            return "Thêm thành công.";
                        }
                    }
                    else
                    {
                        if (GetCartDetailByIdNguoiDung(idnguoidung).Any(c => c.IDSPCT == obj.IDSPCT) == true)
                        {
                            var ghct1 = GetCartDetailByIdNguoiDung(idnguoidung).FirstOrDefault(c => c.IDSPCT == obj.IDSPCT);
                            ghct1.SoLuong = obj.SoLuong;
                            _context.gioHangChiTiets.Update(ghct1);
                            _context.SaveChanges();
                            return "Thêm số lượng thành công.";
                        }
                        else
                        {
                            GioHangChiTiet ghct = new GioHangChiTiet();
                            ghct.ID = Guid.NewGuid();
                            ghct.IDGioHang = gh.ID;
                            ghct.IDSaleCT = null;
                            ghct.IDSPCT = obj.IDSPCT;
                            ghct.SoLuong = obj.SoLuong;
                            ghct.Gia = spct.GiaBan;
                            _context.gioHangChiTiets.Add(ghct);
                            _context.SaveChanges();
                            return "Thêm thành công.";
                        }
                    }
                }

            }
            catch (Exception e)
            {
                return "Lỗi: " + e.Message;
            }
        }

        public bool DeleteCartDetail(Guid id)
        {
            try
            {
                var ghct = _context.gioHangChiTiets.FirstOrDefault(p => p.ID == id);
                if (ghct != null)
                {
                    _context.Remove(ghct);
                    _context.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<GioHangCTVM> DetailCartDetail(Guid id)
        {
            foreach (var item in DanhSachGioHang())
            {
                if (item.IDSPCT == null)
                {
                    var query = from a in DanhSachGioHang()
                                join c in salechitietvm.DanhSachSanPhamSaleKH() on a.IDSaleCT equals c.IDSalechitiet
                                select new GioHangCTVM
                                {
                                    ID = a.ID,
                                    IDGioHang = a.IDGioHang,
                                    IDSPCT = null,
                                    IDSaleCT = a.IDSaleCT,
                                    TenSP = c.TenSP,
                                    SoLuong = a.SoLuong,
                                    ThuongHieu = c.ThuongHieu,
                                    LoaiSanPham = c.TheLoai,
                                    Gia = c.GiaGiam,
                                    Size = c.Size,
                                    MauSac = c.Mau,
                                    ThanhTien = a.SoLuong * c.GiaGiam
                                };
                    var lstgiohang = query.FirstOrDefault(c => c.ID == id);
                    var anhsale = _context.anhSanPhams.Where(c => c.IdSanPhamChiTiet == lstgiohang.IDSPCT).Select(c => new Anh { ID = c.Idanh, DuongDan = c.Anh.DuongDan }).ToList();

                    if (anhsale == null && anhsale.Count == 0)
                    {
                        lstgiohang.DuongDanAnh = "https://congtygiaphat104.com/template/Default/img/no.png";
                    }
                    else
                    {
                        lstgiohang.DuongDanAnh = anhsale[0].DuongDan;
                    }

                    return lstgiohang;
                }
                else
                {
                    var query = from a in DanhSachGioHang()
                                join b in viewModel.DanhSachSanPhamHoanThien() on a.IDSPCT equals b.ID
                                select new GioHangCTVM
                                {
                                    ID = a.ID,
                                    IDGioHang = a.IDGioHang,
                                    IDSPCT = a.IDSPCT,
                                    IDSaleCT = null,
                                    TenSP = b.TenSP,
                                    SoLuong = a.SoLuong,
                                    ThuongHieu = b.TenThuongHieu,
                                    LoaiSanPham = b.LoaiSanPham,
                                    Gia = b.GiaBan,
                                    Size = b.Size,
                                    MauSac = b.MauSac,
                                    ThanhTien = a.SoLuong * b.GiaBan
                                };
                    var lstgiohangct = query.FirstOrDefault(c => c.ID == id);

                    //var sanphamsale = salechitietvm.DanhSachSanPhamSale().FirstOrDefault(c => c.IdSanPhamCT == lstgiohang.IDSPCT);
                    var anhspct = _context.anhSanPhams.Where(c => c.IdSanPhamChiTiet == lstgiohangct.IDSPCT).Select(c => new Anh { ID = c.Idanh, DuongDan = c.Anh.DuongDan }).ToList();
                    //var anhsale = _context.anhSanPhams.Where(c => c.IdSanPhamChiTiet == sanphamsale.IdSanPhamCT).Select(c => new Anh { ID = c.Idanh, DuongDan = c.Anh.DuongDan }).ToList();

                    if (anhspct == null && anhspct.Count == 0)
                    {
                        lstgiohangct.DuongDanAnh = "https://congtygiaphat104.com/template/Default/img/no.png";
                    }
                    else
                    {
                        lstgiohangct.DuongDanAnh = anhspct[0].DuongDan;
                    }

                    return lstgiohangct;
                }
            }
            return null;
        }
        public List<GioHangChiTiet> DanhSachGioHang()
        {
            return _context.gioHangChiTiets.ToList();
        }

        public async Task<List<GioHangCTVM>> GetAllCartDetail()
        {
            List<GioHangCTVM> lstgiohangctvm = new List<GioHangCTVM>();

            foreach (var item in DanhSachGioHang())
            {
                if (item.IDSPCT == null)
                {
                    var query = from a in DanhSachGioHang()
                                join c in salechitietvm.DanhSachSanPhamSaleKH() on a.IDSaleCT equals c.IDSalechitiet
                                select new GioHangCTVM
                                {
                                    ID = a.ID,
                                    IDGioHang = a.IDGioHang,
                                    IDSPCT = null,
                                    IDSaleCT = a.IDSaleCT,
                                    TenSP = c.TenSP,
                                    SoLuong = a.SoLuong,
                                    ThuongHieu = c.ThuongHieu,
                                    LoaiSanPham = c.TheLoai,
                                    Gia = c.GiaGiam,
                                    Size = c.Size,
                                    MauSac = c.Mau,
                                    ThanhTien = a.SoLuong * c.GiaGiam
                                };
                    var lstgiohang = query.FirstOrDefault(c => c.IDSaleCT == item.IDSaleCT);
                    var anhsale = _context.anhSanPhams.Where(c => c.IdSanPhamChiTiet == lstgiohang.IDSPCT).Select(c => new Anh { ID = c.Idanh, DuongDan = c.Anh.DuongDan }).ToList();

                    if (anhsale == null && anhsale.Count == 0)
                    {
                        lstgiohang.DuongDanAnh = "https://congtygiaphat104.com/template/Default/img/no.png";
                    }
                    else
                    {
                        lstgiohang.DuongDanAnh = anhsale[0].DuongDan;
                    }

                    lstgiohangctvm.Add(lstgiohang);
                }
                else
                {
                    var query = from a in DanhSachGioHang()
                                join b in viewModel.DanhSachSanPhamHoanThien() on a.IDSPCT equals b.ID
                                select new GioHangCTVM
                                {
                                    ID = a.ID,
                                    IDGioHang = a.IDGioHang,
                                    IDSPCT = a.IDSPCT,
                                    IDSaleCT = null,
                                    TenSP = b.TenSP,
                                    SoLuong = a.SoLuong,
                                    ThuongHieu = b.TenThuongHieu,
                                    LoaiSanPham = b.LoaiSanPham,
                                    Gia = b.GiaBan,
                                    Size = b.Size,
                                    MauSac = b.MauSac,
                                    ThanhTien = a.SoLuong * b.GiaBan
                                };
                    var lstgiohangct = query.FirstOrDefault(c => c.IDSPCT == item.IDSPCT);

                    //var sanphamsale = salechitietvm.DanhSachSanPhamSale().FirstOrDefault(c => c.IdSanPhamCT == lstgiohang.IDSPCT);
                    var anhspct = _context.anhSanPhams.Where(c => c.IdSanPhamChiTiet == lstgiohangct.IDSPCT).Select(c => new Anh { ID = c.Idanh, DuongDan = c.Anh.DuongDan }).ToList();
                    //var anhsale = _context.anhSanPhams.Where(c => c.IdSanPhamChiTiet == sanphamsale.IdSanPhamCT).Select(c => new Anh { ID = c.Idanh, DuongDan = c.Anh.DuongDan }).ToList();

                    if (anhspct == null && anhspct.Count == 0)
                    {
                        lstgiohangct.DuongDanAnh = "https://congtygiaphat104.com/template/Default/img/no.png";
                    }
                    else
                    {
                        lstgiohangct.DuongDanAnh = anhspct[0].DuongDan;
                    }

                    lstgiohangctvm.Add(lstgiohangct);
                }
            }
            return lstgiohangctvm;
        }

        public async Task<GioHangCTVM> GetCartDetailById(Guid idgiohang, Guid idspct)
        {
            var query = from a in _context.gioHangChiTiets
                        join b in _context.sanPhamChiTiets on a.IDSPCT equals b.ID
                        join c in _context.sanPhams on b.IDSP equals c.ID
                        join d in _context.sizes on b.IDSize equals d.ID
                        join e in _context.mauSacs on b.IDMauSac equals e.ID
                        join f in salechitietvm.DanhSachSanPhamSaleKH() on a.IDSaleCT equals f.IDSalechitiet
                        select new { a, b, c, d, e, f };
            var result = await query.FirstOrDefaultAsync();
            if (result == null || result.a.IDSPCT == null)
            {
                return null;
            }
            var data = new GioHangCTVM
            {
                ID = result.a.ID,
                IDGioHang = result.a.IDGioHang,
                IDSPCT = result.a.IDSPCT,
                IDSaleCT = result.a.IDSaleCT,
                TenSP = result.a.IDSaleCT == null ? result.c.TenSanPham : result.f.TenSP,
                ThuongHieu = result.a.IDSaleCT == null ? result.b.ThuongHieu.TenThuongHieu : result.f.ThuongHieu,
                LoaiSanPham = result.a.IDSaleCT == null ? result.b.LoaiSanPham : result.f.TheLoai,
                SoLuong = result.a.SoLuong,
                Gia = result.a.IDSaleCT == null ? result.a.Gia : result.f.GiaGiam,
                Size = result.a.IDSaleCT == null ? result.d.SizeNumber : result.f.Size,
                MauSac = result.a.IDSaleCT == null ? result.e.TenMauSac : result.f.Mau
            };
            var anhsp = _context.anhSanPhams.Where(c => c.IdSanPhamChiTiet == data.IDSPCT).Select(c => new Anh { ID = c.Idanh, DuongDan = c.Anh.DuongDan }).ToList();
            if (anhsp == null || anhsp.Count == 0)
            {
                data.DuongDanAnh = "https://congtygiaphat104.com/template/Default/img/no.png";
            }
            else
            {
                data.DuongDanAnh = anhsp[0].DuongDan;
            }
            return data;
        }

        public List<GioHangCTVM> GetCartDetailByIdGioHang(Guid idnguoidung)
        {
            var idgiohang = cartRepository.GetAllCart().FirstOrDefault(c => c.IDNguoiDung == idnguoidung).ID;
            var lstgiohang1 = _context.gioHangChiTiets.Where(c => c.IDGioHang == idgiohang).ToList();

            List<GioHangCTVM> lstgiohangctvm = new List<GioHangCTVM>();

            foreach (var item in lstgiohang1)
            {
                if (item.IDSPCT == null)
                {
                    var query = from a in DanhSachGioHang()
                                join c in salechitietvm.DanhSachSanPhamSaleKH() on a.IDSaleCT equals c.IDSalechitiet
                                select new GioHangCTVM
                                {
                                    ID = a.ID,
                                    IDGioHang = a.IDGioHang,
                                    IDSPCT = null,
                                    IDSaleCT = a.IDSaleCT,
                                    TenSP = c.TenSP,
                                    SoLuong = a.SoLuong,
                                    ThuongHieu = c.ThuongHieu,
                                    LoaiSanPham = c.TheLoai,
                                    Gia = c.GiaGiam,
                                    GiaGoc = c.GiaGoc,
                                    Size = c.Size,
                                    MauSac = c.Mau,
                                    ThanhTien = a.SoLuong * c.GiaGiam
                                };
                    var lstgiohang = query.FirstOrDefault(c => c.IDSaleCT == item.IDSaleCT);
                    var anhsale = salechitietvm.DanhSachSanPhamSale().FirstOrDefault(c => c.IDSalechitiet == lstgiohang.IDSaleCT).lstAnhSanPham;

                    if (anhsale.Count == 0)
                    {
                        lstgiohang.DuongDanAnh = "https://congtygiaphat104.com/template/Default/img/no.png";
                    } else
                    {
                        lstgiohang.DuongDanAnh = anhsale[0].DuongDan;
                    }


                    lstgiohangctvm.Add(lstgiohang);
                }
                else
                {
                    var query = from a in DanhSachGioHang()
                                join b in viewModel.DanhSachSanPhamHoanThien() on a.IDSPCT equals b.ID
                                select new GioHangCTVM
                                {
                                    ID = a.ID,
                                    IDGioHang = a.IDGioHang,
                                    IDSPCT = a.IDSPCT,
                                    IDSaleCT = null,
                                    TenSP = b.TenSP,
                                    SoLuong = a.SoLuong,
                                    ThuongHieu = b.TenThuongHieu,
                                    LoaiSanPham = b.LoaiSanPham,
                                    Gia = b.GiaBan,
                                    GiaGoc = b.GiaBan,
                                    Size = b.Size,
                                    MauSac = b.MauSac,
                                    ThanhTien = a.SoLuong * b.GiaBan
                                };
                    var lstgiohangct = query.FirstOrDefault(c => c.IDSPCT == item.IDSPCT);

                    //var sanphamsale = salechitietvm.DanhSachSanPhamSale().FirstOrDefault(c => c.IdSanPhamCT == lstgiohang.IDSPCT);
                    var anhspct = _context.anhSanPhams.Where(c => c.IdSanPhamChiTiet == item.IDSPCT).Select(c => new Anh { ID = c.Idanh, DuongDan = c.Anh.DuongDan }).ToList();
                    //var anhsale = _context.anhSanPhams.Where(c => c.IdSanPhamChiTiet == sanphamsale.IdSanPhamCT).Select(c => new Anh { ID = c.Idanh, DuongDan = c.Anh.DuongDan }).ToList();

                    if (anhspct.Count == 0)
                    {
                        lstgiohangct.DuongDanAnh = "https://congtygiaphat104.com/template/Default/img/no.png";
                    }
                    else
                    {
                        lstgiohangct.DuongDanAnh = anhspct[0].DuongDan;
                    }

                    lstgiohangctvm.Add(lstgiohangct);
                }
            }
            return lstgiohangctvm;
        }


        public List<GioHangChiTiet> GetCartDetailByIdNguoiDung(Guid idnguoidung)
        {
            var idgiohang = cartRepository.GetAllCart().FirstOrDefault(c => c.IDNguoiDung == idnguoidung).ID;
            return _context.gioHangChiTiets.Where(c => c.IDGioHang == idgiohang).ToList();
        }

        public async Task<bool> UpdateCartDetail(GioHangCTRequestVM obj)
        {
            try
            {
                var ghct = await _context.gioHangChiTiets.FindAsync(obj.Id);
                if (ghct != null)
                {
                    ghct.IDSPCT = obj.IDSPCT;
                    ghct.SoLuong = obj.SoLuong;
                    _context.Update(ghct);
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public async Task<bool> UpdateGiohangChiTiet(GioHangChiTiet obj)
        {
            try
            {
                _context.gioHangChiTiets.Update(obj);
                _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}
