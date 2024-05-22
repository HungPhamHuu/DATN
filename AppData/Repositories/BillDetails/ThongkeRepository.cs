using AppData.DB_Context;
using AppData.Entities.Models;
using AppData.Entities.ViewModels;
using AppData.Repositories.Bills;
using AppData.Repositories.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace AppData.Repositories.BillDetails
{
    public class ThongkeRepository
    {
        private readonly ApplicationDbContext _context;

        private readonly IBillRepository _billRepository;
        private readonly IBillDetailRepository _billDetailRepository;
        private readonly SPCTViewModel _viewModel;
        private readonly SaleChiTietVm _saleChiTietVm;

        public ThongkeRepository(ApplicationDbContext context)
        {
            _context = context;
            _billDetailRepository = new BillDetailRepository(context);
            _billRepository = new BillRepository(context);
            _viewModel = new SPCTViewModel();
            _saleChiTietVm = new SaleChiTietVm();
        }

        public int TongSoLuongHoaDonThanhCong()
        {
            var soluong = _billRepository.GetAll().Where(c => c.TrangThai == 4).ToList();
            return soluong.Count;
        }
        public decimal TongDoanhThu()
        {
            decimal tongdoanhthu = 0;
            var hoadonthanhcong = _billRepository.GetAll().Where(c => c.TrangThai == 4).ToList();
            foreach (var hoadon in hoadonthanhcong)
            {
                tongdoanhthu += hoadon.ThanhTien;
            }
            return tongdoanhthu;
        }
        public int TongHoaDonBiHuy()
        {
            var soluong = _billRepository.GetAll().Where(c => c.TrangThai == 6).ToList();
            return soluong.Count;
        }

        public List<Thongke> Top10SanPhamBanChayNhat()
        {
            List<Thongke> lstThongKe1 = new List<Thongke>();
            lstThongKe1.Clear();
            var hoadonthanhcong = _billRepository.GetAll().Where(c => c.TrangThai == 4).ToList();
            foreach (var hoadon in hoadonthanhcong)
            {
                var hoadonchitiet = _billDetailRepository.GetByIdBill(hoadon.ID);
                foreach (var sp in hoadonchitiet)
                {
                    if (sp.IDSPCT == null)
                    {

                    }
                    else
                    {
                        var sanpham = _viewModel.DanhSachSanPhamHoanThien().FirstOrDefault(c => c.ID == sp.IDSPCT).TenSP;
                        Thongke tk = new Thongke();
                        if (lstThongKe1.Any(c => c.tensanpham.ToLower().Trim() == sanpham.ToLower().Trim()))
                        {
                            var sp1 = lstThongKe1.FirstOrDefault(c => c.tensanpham.ToLower().Trim() == sanpham.ToLower().Trim());
                            sp1.soluongban += sp.SoLuong;
                            lstThongKe1.Remove(sp1);

                            tk.tensanpham = sanpham;
                            tk.soluongban = sp1.soluongban;
                            lstThongKe1.Add(tk);
                        }
                        else
                        {
                            tk.tensanpham = sanpham;
                            tk.soluongban = sp.SoLuong;
                            lstThongKe1.Add(tk);
                        }
                    }
                }
            }
            return lstThongKe1.OrderByDescending(c => c.soluongban).Take(6).ToList();
        }
        public List<Thongke> Top10SanPhamSaleBanChayNhat()
        {
            List<Thongke> lstThongKe = new List<Thongke>();
            lstThongKe.Clear();
            var hoadonthanhcong = _billRepository.GetAll().Where(c => c.TrangThai == 4).ToList();
            foreach (var hoadon in hoadonthanhcong)
            {
                var hoadonchitiet = _billDetailRepository.GetByIdBill(hoadon.ID);
                foreach (var sp in hoadonchitiet)
                {
                    if (sp.IDSaleCT == null)
                    {

                    }
                    else
                    {
                        var sanpham = _saleChiTietVm.DanhSachSanPhamSale().FirstOrDefault(c => c.IDSalechitiet == sp.IDSaleCT).TenSP;
                        Thongke tk = new Thongke();
                        if (lstThongKe.Any(c => c.tensanpham.ToLower().Trim() == sanpham.ToLower().Trim()))
                        {
                            var sp1 = lstThongKe.FirstOrDefault(c => c.tensanpham.ToLower().Trim() == sanpham.ToLower().Trim());
                            sp1.soluongban += sp.SoLuong;
                            lstThongKe.Remove(sp1);

                            tk.tensanpham = sanpham;
                            tk.soluongban = sp1.soluongban;
                            lstThongKe.Add(tk);
                        }
                        else
                        {
                            tk.tensanpham = sanpham;
                            tk.soluongban = sp.SoLuong;
                            lstThongKe.Add(tk);

                        }
                    }
                }
            }
            return lstThongKe.OrderByDescending(c => c.soluongban).Take(6).ToList();
        }
    }
}
