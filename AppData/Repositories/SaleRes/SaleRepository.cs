using AppData.DB_Context;
using AppData.Entities.Models;
using AppData.Repositories.Vouchers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Repositories.SaleRes
{
    public class SaleRepository : ISaleRepository
    {
        ApplicationDbContext dbContext;
        public SaleRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;

        }
        public bool CreateSale(AppData.Entities.Models.Sale sale)
        {
            try
            {
                dbContext.sales.Add(sale);
                dbContext.SaveChanges(); return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool XoaCung(Guid id)
        {
            try
            {
                var sale = dbContext.sales.FirstOrDefault(c => c.ID == id);
                dbContext.sales.Remove(sale);
                dbContext.SaveChanges(); return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool DeleteSale(AppData.Entities.Models.Sale sale)
        {
            try
            {
                var sale1 = dbContext.sales.FirstOrDefault(c => c.ID == sale.ID);
                sale1.TrangThai = 0;
                dbContext.sales.Update(sale1);
                dbContext.SaveChanges(); 
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<AppData.Entities.Models.Sale> GetAllSale()
        {
            return dbContext.sales.ToList();
        }

        public AppData.Entities.Models.Sale GetById(Guid id)
        {
            return dbContext.sales.FirstOrDefault(c => c.ID == id);
        }

        public bool UpdateSale(AppData.Entities.Models.Sale sale)
        {
            try
            {
                dbContext.sales.Update(sale);
                dbContext.SaveChanges(); return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
