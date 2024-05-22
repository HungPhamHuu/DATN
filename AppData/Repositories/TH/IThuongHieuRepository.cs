using AppData.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Repositories.TH;

public interface IThuongHieuRepository
{
    public Task<bool> Create(ThuongHieu thuongHieu);
    public Task<bool> Update(ThuongHieu thuongHieu, Guid id);
    public Task<bool> Delete(Guid id);
    public Task<List<ThuongHieu>> GetAll();
    public Task<ThuongHieu> GetById(Guid id);
    ThuongHieu GetByName(string name);
}
