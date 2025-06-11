using Microsoft.EntityFrameworkCore;
using FROASTERY.Data;
using FROASTERY.Models;
namespace FROASTERY.Services
{
    public class DashboardService
    {
        private readonly AppDbContext _dbContext; 

        public DashboardService(AppDbContext dbContext) 
        {
            _dbContext = dbContext;
        }


         public async Task<decimal> GetTodayRevenueAsync()
        {
            var today = DateTime.Now.Date;
            return await _dbContext.DonHangs.Where(p => !p.DaXoa)
                .Where(dh => dh.ThoiGian.HasValue && dh.ThoiGian.Value.Date == today)
                .SumAsync(dh => (decimal?)dh.TongTien) ?? 0;
        }

        public async Task<int> GetTodayNewOrdersAsync()
        {
            var today = DateTime.Now.Date;
            return await _dbContext.DonHangs.Where(p => !p.DaXoa)
                .CountAsync(dh => dh.ThoiGian.HasValue && dh.ThoiGian.Value.Date == today);
        }
        public async Task<List<SuKien>> GetActiveAndUpcomingEventsByStatusAsync()
        {
            var allEvents = await _dbContext.SuKiens.Where(p => !p.DaXoa)
                .OrderBy(sk => sk.ThoiGianBatDau)
                .ToListAsync();

            return allEvents
                .Where(sk => sk.TrangThai == "Đang diễn ra" || sk.TrangThai == "Sắp diễn ra")
                .Take(5)
                .ToList();
        }
            public async Task<string> GetBestSellingProductAsync()
            {
                var today = DateTime.Now.Date;
                return await _dbContext.ChiTietDonHangs
                    .Where(ct => ct.DonHang != null && !ct.DonHang.DaXoa && ct.DonHang.ThoiGian.HasValue && ct.DonHang.ThoiGian.Value.Date == today)
                    .GroupBy(ct => ct.MaSanPham)
                    .OrderByDescending(g => g.Sum(ct => ct.SoLuong) ?? 0)
                    .Select(g => g.FirstOrDefault() == null ? null : (g.FirstOrDefault().SanPham == null ? null : g.FirstOrDefault().SanPham.TenSanPham))
                    .FirstOrDefaultAsync() ?? "Đang cập nhật...";
            }

        public async Task<List<string>> GetRecentNotificationsAsync()
        {
            return await _dbContext.TinTucs.Where(p => !p.DaXoa)
                .OrderByDescending(t => t.ThoiGian)
                .Take(5)
                .Select(t => t.TieuDe)
                .ToListAsync();
        }
    }
}