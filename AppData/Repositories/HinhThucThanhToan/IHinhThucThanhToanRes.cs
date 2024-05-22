using AppData.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Repositories.HinhThucThanhToan
{
    public interface IHinhThucThanhToanRes
    {
        public List<ThanhToan> DanhSachThanhToan();
        public string ThemHinhThuc(ThanhToan thanhToan);
        public string SuaHinhThuc(ThanhToan thanhToan);
        public string XoaHinhThuc(Guid idhinhthuc);
        public string KichHoat(Guid idhinhthuc);

    }
}
