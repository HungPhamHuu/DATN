using AppData.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Repositories.Anhs
{
    public interface IAnh
    {
        public string ThemAnh(Anh anh);
        public List<Anh> DanhSachAnh();
        public string XoaAnh(Guid idanh);
    }
}
