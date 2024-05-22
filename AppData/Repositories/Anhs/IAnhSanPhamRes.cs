using AppData.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Repositories.Anhs
{
    public interface IAnhSanPhamRes
    {
        public bool AddAnhChoSanPham(AnhSanPham anhSanPham);
        public List<AnhSanPham> GetAllAnhChoSanPham();
        public List<AnhSanPham> GetAllAnhChoSanPhamBySP(Guid idsanpham);
        public bool RemoveAnhSp(Guid idanh, Guid idsp);
        public List<Anh> DanhSachAnhChoSanPhamBySP(Guid idsanpham);
    }
}
