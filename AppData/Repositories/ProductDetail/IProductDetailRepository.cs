using AppData.Entities.Models;
using AppData.Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Repositories.ProductDetail;

public interface IProductDetailRepository
{
    public bool Create(SanPhamChiTiet sanPham);
    public Task<bool> Update(SanPhamChiTiet sanPham/*, Guid id*/);
    Task<bool> Delete(Guid id);
    public bool XoaCung(Guid id);
    bool ConHang(Guid id);
    List<SanPhamChiTiet> GetAll();
    Task<SanPhamChiTiet> GetId(Guid id);
    IEnumerable<object> GetProductDetails();
    public string ThemSanPham(SanPhamChiTiet sp);
    public string CapNhatSanPham(SanPhamChiTiet sp);

}
