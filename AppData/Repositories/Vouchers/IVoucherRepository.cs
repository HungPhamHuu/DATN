using AppData.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Repositories.Vouchers
{
   public interface IVoucherRepository<T>
    {
        public IEnumerable<T> GetAllVoucher();
        public T GetById(Guid id);
        public bool CreateVoucher(T voucher);
        public bool DeleteVoucher(T voucher);
        public bool UpdateVoucher(T voucher);
    }
}
