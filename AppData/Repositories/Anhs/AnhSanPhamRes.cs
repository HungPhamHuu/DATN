using AppData.DB_Context;
using AppData.Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Repositories.Anhs
{
    public class AnhSanPhamRes : IAnhSanPhamRes
    {
        private readonly ApplicationDbContext _context;
        private readonly IAnh _anh; 
        public AnhSanPhamRes()
        {
            _context = new ApplicationDbContext();
            _anh = new AnhReps(_context);
        }
        public bool AddAnhChoSanPham(AnhSanPham anhSanPham)
        {
            try
            {
                _context.anhSanPhams.Add(anhSanPham);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<AnhSanPham> GetAllAnhChoSanPham()
        {
            return _context.anhSanPhams.ToList();
        }

        public List<AnhSanPham> GetAllAnhChoSanPhamBySP(Guid idsanpham)
        {
            return _context.anhSanPhams.Where(c => c.IdSanPhamChiTiet == idsanpham).ToList();
        }
        public List<Anh> DanhSachAnhChoSanPhamBySP(Guid idsanpham)
        {
            var lstanh = new List<Anh>();
            try
            {
                var lstanhsp = _context.anhSanPhams.Where(c => c.IdSanPhamChiTiet == idsanpham).ToList();
               
                foreach (var item in lstanhsp)
                {
                    var anh = _anh.DanhSachAnh().FirstOrDefault(c => c.ID == item.Idanh);
                    lstanh.Add(anh);
                }
                return lstanh;
            } catch
            {
                return lstanh;
            }

        }

        public bool RemoveAnhSp(Guid idanh, Guid idsp)
        {
            try
            {
                AnhSanPham anhsanpham = _context.anhSanPhams.FirstOrDefault(c => c.Idanh == idanh && c.IdSanPhamChiTiet == idsp);
                _context.anhSanPhams.Remove(anhsanpham);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
