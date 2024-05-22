using AppData.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Repositories.SaleDetail
{
    public interface ISaleDetailRepository
    {
        List<SaleChiTiet> GetAllSaleDetail();
        string CreateSaleDetail(SaleChiTiet saleDetail);
        string UpdateSaleDetail(SaleChiTiet saleDetail);
        string DeleteSaleDetail(Guid id);
        string BatDauSale(Guid id);
        string XoaCung(Guid id);
        SaleChiTiet GetSaleDetailById(Guid id);
        List<SaleChiTiet> GetAllSaleByIDSPCT(Guid IDSPCT);
    }
}
