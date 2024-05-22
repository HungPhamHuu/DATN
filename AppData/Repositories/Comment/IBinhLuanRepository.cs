using AppData.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Repositories.Comment
{
    public interface IBinhLuanRepository
    {
        public List<BinhLuan> DanhSachBinhLuan();
        public List<BinhLuan> DanhSachBinhLuanTheoIdSPCT(Guid idspct);
        public List<BinhLuan> DanhSachBinhLuanTheoIdSPCTSale(Guid idspsale);
        public string ThemMoiBinhLuan(BinhLuan binhLuan);
        public string ChinhSuaBinhLuan(BinhLuan binhLuan);
        public string XoaBinhLuan(Guid IDBinhLuan);
    }
}
