using AppData.DB_Context;
using AppData.Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Repositories.Vouchers
{
    public class VoucherRepository<T> : IVoucherRepository<T> where T : class
    {
        ApplicationDbContext dbContext;
        DbSet<T> dbSet;
        public VoucherRepository()
        {
        }
        public VoucherRepository(ApplicationDbContext dbContext, DbSet<T> dbSet)
        {
            this.dbContext = dbContext;
            this.dbSet = dbSet;
        }

        public bool CreateVoucher(T voucher)
        {
            try
            {
                dbSet.Add(voucher);
                dbContext.SaveChanges(); return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteVoucher(T voucher)
        {
            try
            {
                dbSet.Remove(voucher);
                dbContext.SaveChanges(); return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<T> GetAllVoucher()
        {
            return dbSet.ToList();
        }

        public T GetById(Guid id)
        {
            return dbSet.Find(id);
        }

        public bool UpdateVoucher(T voucher)
        {
            try
            {
                dbSet.Update(voucher);
                dbContext.SaveChanges(); return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
