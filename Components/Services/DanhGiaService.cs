using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FROASTERY.Data;
using FROASTERY.Models;
using Microsoft.EntityFrameworkCore;

namespace FROASTERY.Services
{
    public class DanhGiaService
    {
        private readonly AppDbContext _dbContext;

        public DanhGiaService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Chức năng cho trang người dùng (User)

        public async Task LuuDanhGia(DanhGia danhGia)
        {
            danhGia.ThoiGianGui = DateTime.Now;
            danhGia.TrangThai = 0; // Mặc định là mới, cần admin duyệt
            _dbContext.DanhGias.Add(danhGia);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<DanhGia>> LayDanhSachDanhGiaDaDuyet(int count = 0)
        {
            IQueryable<DanhGia> query = _dbContext.DanhGias
                .Where(d => d.TrangThai == 1)
                .OrderByDescending(d => d.ThoiGianGui);

            if (count > 0)
            {
                query = query.Take(count);
            }

            return await query.ToListAsync();
        }

        public async Task<double> TinhDiemTrungBinhDanhGiaDaDuyet()
        {
            return await _dbContext.DanhGias
                .Where(d => d.TrangThai == 1)
                .AverageAsync(d => (double?)d.SoSao) ?? 0;
        }

        public async Task<int> DemSoLuongDanhGiaDaDuyet()
        {
            return await _dbContext.DanhGias
                .CountAsync(d => d.TrangThai == 1);
        }

        public async Task<Dictionary<int, int>> ThongKeDanhGiaTheoSaoDaDuyet()
        {
            return await _dbContext.DanhGias
                .Where(d => d.TrangThai == 1)
                .GroupBy(d => d.SoSao)
                .ToDictionaryAsync(g => g.Key, g => g.Count());
        }

        // Chức năng cho trang quản trị (Admin)

    public async Task<List<DanhGia>> LayTatCaDanhGia(int pageNumber = 1, int pageSize = 10)
    {
        pageNumber = pageNumber <= 0 ? 1 : pageNumber;
        pageSize = pageSize <= 0 ? 10 : pageSize;

        return await _dbContext.DanhGias
            .OrderByDescending(d => d.ThoiGianGui)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

        public async Task<int> DemTatCaDanhGia()
        {
            return await _dbContext.DanhGias.CountAsync();
        }

    public async Task<DanhGia?> LayDanhGiaTheoId(int id)
    {
        return await _dbContext.DanhGias.FindAsync(id);
    }
        public async Task CapNhatDanhGia(DanhGia danhGia)
        {
            var existingDanhGia = await _dbContext.DanhGias.FindAsync(danhGia.MaDanhGia);
            if (existingDanhGia != null)
            {
                existingDanhGia.NoiDung = danhGia.NoiDung;
                existingDanhGia.HoTen = danhGia.HoTen;
                existingDanhGia.ThongTinLienLac = danhGia.ThongTinLienLac;
                existingDanhGia.GhiChu = danhGia.GhiChu;
                existingDanhGia.SoSao = danhGia.SoSao;
                existingDanhGia.TrangThai = danhGia.TrangThai; // Cho phép admin cập nhật trạng thái
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task XoaDanhGia(int id)
        {
            var danhGia = await _dbContext.DanhGias.FindAsync(id);
            if (danhGia != null)
            {
                _dbContext.DanhGias.Remove(danhGia);
                await _dbContext.SaveChangesAsync();
            }
        }

    public async Task<List<DanhGia>> TimKiemDanhGia(string? keyword)
    {
        keyword = (keyword ?? string.Empty)
                    .Trim()
                    .ToLowerInvariant()
                    .Replace(" ", "");

        // Nếu từ khóa là số
        if (int.TryParse(keyword, out int id))
        {
            return await _dbContext.DanhGias
                .Where(d =>
                    d.MaDanhGia == id ||
                    (!string.IsNullOrEmpty(d.ThongTinLienLac) &&
                    d.ThongTinLienLac.Replace(" ", "").Contains(keyword))
                )
                .OrderByDescending(d => d.ThoiGianGui)
                .ToListAsync();
        }

        // Nếu là chuỗi: tìm trong các trường chữ
        return await _dbContext.DanhGias
            .Where(d =>
                (!string.IsNullOrEmpty(d.NoiDung) && d.NoiDung.ToLower().Replace(" ", "").Contains(keyword)) ||
                (!string.IsNullOrEmpty(d.HoTen) && d.HoTen.ToLower().Replace(" ", "").Contains(keyword)) ||
                (!string.IsNullOrEmpty(d.ThongTinLienLac) && d.ThongTinLienLac.ToLower().Replace(" ", "").Contains(keyword)) ||
                (!string.IsNullOrEmpty(d.GhiChu) && d.GhiChu.ToLower().Replace(" ", "").Contains(keyword))
            )
            .OrderByDescending(d => d.ThoiGianGui)
            .ToListAsync();
    }


        public async Task<List<DanhGia>> LocDanhGiaTheoTrangThai(int trangThai)
        {
            return await _dbContext.DanhGias
                .Where(d => d.TrangThai == trangThai)
                .OrderByDescending(d => d.ThoiGianGui)
                .ToListAsync();
        }
    }
}