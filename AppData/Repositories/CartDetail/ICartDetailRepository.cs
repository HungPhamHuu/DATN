using AppData.Entities.Models;
using AppData.Entities.ViewModels;
using AppData.Entities.ViewModels.GioHangCTVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Repositories.CartDetail
{
    public interface ICartDetailRepository
    {
        public Task<List<GioHangCTVM>> GetAllCartDetail();
        public List<GioHangChiTiet> DanhSachGioHang();
        public string CreateCartDetail(Guid idnguoidung, GioHangChiTiet obj);
        public Task<bool> UpdateCartDetail(GioHangCTRequestVM obj);
        public Task<bool> UpdateGiohangChiTiet(GioHangChiTiet obj);
        public Task<GioHangCTVM> DetailCartDetail(Guid id);
        public bool DeleteCartDetail(Guid id);
        public Task<GioHangCTVM> GetCartDetailById(Guid idgiohang, Guid idspct);
        public List<GioHangCTVM> GetCartDetailByIdGioHang(Guid idnguoidung);
        public List<GioHangChiTiet> GetCartDetailByIdNguoiDung(Guid idnguoidung);

    }
}
