using AppData.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Repositories.Sizes;

public interface ISizeRepository
{
    Task<bool> Create(Size size);
    public Task<bool> Update(Guid id, Size size);
    public Task<bool> Delete(Guid id);
    public Task<List<Size>> GetAll();
    public Task<Size> GetById(Guid id);
    Size GetByTen(string size);
}
