using AppData.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Repositories.VoucherDetails
{
    public interface IVoucherDetailRepository
    {
        List<VoucherChiTiet> GetAllVoucherDetail();
        bool CreateVoucherDetail(VoucherChiTiet voucherDetail);
        bool UpdateVoucherDetail(VoucherChiTiet voucherDetail);
        bool DeleteVoucherDetail(Guid id);
        VoucherChiTiet GetVoucherDetailById(Guid id);
        List<VoucherChiTiet> GetAllVoucherByIDNguoiDung(Guid IDnguoiDung);
    }
}
