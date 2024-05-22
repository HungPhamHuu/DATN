using AppData.DB_Context;
using AppData.Entities.Models;
using AppData.Entities.ViewModels;
using AppData.Repositories.BillDetails;
using AppData.Repositories.Bills;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Repositories.ViewModel
{
    public class HoaDonChiTietVm
    {
        private readonly IBillRepository _billRepository;
        private readonly IBillDetailRepository _billDetailRepository;
        private readonly SPCTViewModel _viewModel;
        private readonly SaleChiTietVm salectmd;
        private readonly ApplicationDbContext _context;
        public HoaDonChiTietVm(ApplicationDbContext context)
        {
            _billRepository = new BillRepository(context);
            _viewModel = new SPCTViewModel();
            _context = context;
            _billDetailRepository = new BillDetailRepository(context);
            salectmd = new SaleChiTietVm();

        }

        public List<HoaDonChiTietViewModel> DanhSachHoaDonTheoIDHD(Guid IDHoaDon)
        {
          
            try
            {
                List<HoaDonChiTiet> lsthoadonct = _context.hoaDonChiTiets.Where(c => c.IDHoaDon == IDHoaDon).ToList();
                List<HoaDonChiTietViewModel> hdctvm = new List<HoaDonChiTietViewModel>();

                foreach(var item in  lsthoadonct)
                {
                    if(item.IDSPCT == null)
                    {
                        var sanphamsale = salectmd.DanhSachSanPhamSale().FirstOrDefault(c => c.IDSalechitiet == item.IDSaleCT);
                        var obj = from a in _billDetailRepository.GetAll()
                                  join b in _billRepository.GetAll() on a.IDHoaDon equals b.ID
                                  join d in salectmd.DanhSachSanPhamSale() on a.IDSaleCT equals d.IDSalechitiet
                                  select new HoaDonChiTietViewModel
                                  {
                                      ID = item.ID,
                                      IDHoaDon = item.IDHoaDon,
                                      IDSPCT = null ,
                                      IDSaleCT = item.IDSaleCT,
                                      TenSP = sanphamsale.TenSP,
                                      SoLuong = item.SoLuong,
                                      Gia = item.Gia,
                                      //image = c.lstAnhSanPham == null ? null : c.lstAnhSanPham[0].DuongDan,
                                      Size = sanphamsale.Size,
                                      MauSac = sanphamsale.Mau,
                                      ThanhTien = item.SoLuong * item.Gia
                                  };
                        var ds = obj;
                        var lst = obj.FirstOrDefault(c => c.IDSaleCT == item.IDSaleCT);
                        hdctvm.Add(lst);
                    } else
                    {
                        var sanpham = _viewModel.DanhSachSanPhamHoanThien().FirstOrDefault(c => c.ID == item.IDSPCT);
                        var obj = from a in _billDetailRepository.GetAll()
                                  join b in _billRepository.GetAll() on a.IDHoaDon equals b.ID
                                  join c in _viewModel.DanhSachSanPhamHoanThien() on a.IDSPCT equals c.ID
                                  select new HoaDonChiTietViewModel
                                  {
                                      ID = item.ID,
                                      IDHoaDon = item.IDHoaDon,
                                      IDSPCT = item.IDSPCT,
                                      IDSaleCT = null,
                                      TenSP = sanpham.TenSP,
                                      SoLuong = item.SoLuong,
                                      Gia = item.Gia,
                                      //image = c.lstAnhSanPham == null ? null : c.lstAnhSanPham[0].DuongDan,
                                      Size = sanpham.Size,
                                      MauSac = sanpham.MauSac,
                                      ThanhTien = item.SoLuong * item.Gia
                                  }; 
                        var lst = obj.FirstOrDefault(c => c.IDSPCT == item.IDSPCT); 
                        hdctvm.Add(lst); 
                    }
                }
                return hdctvm;
            }
            catch (Exception ex)
            {
                return null;
            }

        }
    }
}
