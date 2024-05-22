using AppData.DB_Context;
using AppData.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Repositories.Anhs
{
    public class AnhReps : IAnh
    {
        private readonly ApplicationDbContext _context;
        public AnhReps(ApplicationDbContext context)
        {
            _context = context;
        }
        public List<Anh> DanhSachAnh()
        {
            try
            {
                return _context.anhs.ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public string ThemAnh(Anh anh)
        {
            try
            {
                if (anh != null)
                {
                    _context.anhs.Add(anh);
                    _context.SaveChanges();
                    return "Thêm thành công.";
                }
                else
                {
                    return "Thêm thất bại.";
                }
            }
            catch (Exception ex)
            {
                return "Lỗi: " + ex.Message;
            }
        }

        public string XoaAnh(Guid idanh)
        {
            try
            {
                return "";
            }
            catch (Exception ex)
            {
                return "Lỗi: " + ex.Message;
            }
        }
    }
}
