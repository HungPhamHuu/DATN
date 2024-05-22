using AppData.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Repositories.Product;

public interface IProductRepository
{
    public Task<bool> Create(SanPham sanPham);
    public Task<bool> Update(Guid id, SanPham sanPham);
    Task<bool> Delete(Guid id);
    public Task<IEnumerable<SanPham>> GetAll();
    Task<SanPham> GetById(Guid id);
    Task<List<SanPham>> GetByName(string name);
    SanPham GetByten(string name);
}
