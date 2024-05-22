using AppData.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Repositories.Color;

public interface IMauSacRepository
{
    Task<bool> Create(MauSac mauSac);
    Task<bool> Update(MauSac mauSac, Guid id);
    Task<bool> Delete(Guid id);
    Task<List<MauSac>> GetAll();
    Task<MauSac> GetById(Guid id);
    MauSac GetByName(string name);
}
