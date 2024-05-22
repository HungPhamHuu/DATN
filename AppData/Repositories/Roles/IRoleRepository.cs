using AppCommon.RepositoryAsync;
using AppData.Entities.Models;

namespace AppData.Repositories.Roles;

public interface IRoleRepository 
{
    public bool ThemMoiChucVu(ChucVu chucVu);
    public bool ChinhSuaChucVu(ChucVu chucVu);
    public bool XoaChucVu(Guid id);
    public List<ChucVu> DanhSachChucVu();
}