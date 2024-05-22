using AppData.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Repositories.BillDetails;

public interface IBillDetailRepository
{
    bool Create(HoaDonChiTiet hdCT);
    Task<bool> Update(HoaDonChiTiet hdCT, Guid id);
    Task<bool> Delete(Guid id);
    public List<HoaDonChiTiet> GetAll();
    Task<HoaDonChiTiet> GetId(Guid id);
    List<HoaDonChiTiet> GetByIdBill(Guid idHD);
}
