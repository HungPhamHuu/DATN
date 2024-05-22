using AppData.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Repositories.SaleRes
{
    public interface ISaleRepository
    {
        public IEnumerable<AppData.Entities.Models.Sale> GetAllSale ();
        public AppData.Entities.Models.Sale GetById(Guid id);
        public bool CreateSale(AppData.Entities.Models.Sale sale);
        public bool DeleteSale(AppData.Entities.Models.Sale sale);
        public bool XoaCung(Guid id);
        public bool UpdateSale(AppData.Entities.Models.Sale sale);
    }
}
