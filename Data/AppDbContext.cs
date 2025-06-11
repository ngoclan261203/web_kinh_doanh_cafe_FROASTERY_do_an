using Microsoft.EntityFrameworkCore;
using FROASTERY.Models;

namespace FROASTERY.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<LoaiSanPham> LoaiSanPhams { get; set; }
        public DbSet<SanPham> SanPhams { get; set; }
        // public DbSet<SanPhamKhoiLuong> SanPhamKhoiLuongs { get; set; } // Bảng mới
        public DbSet<NguyenLieu> NguyenLieus { get; set; }
        public DbSet<DoiTac> DoiTacs { get; set; }
        public DbSet<PhieuNhap> PhieuNhaps { get; set; }
        public DbSet<ChiTietPhieuNhapNguyenLieu> ChiTietPhieuNhapNguyenLieus { get; set; }
        public DbSet<Quyen> Quyens { get; set; }
        public DbSet<LoaiTin> LoaiTins { get; set; }
        public DbSet<NguoiDung> NguoiDungs { get; set; }
        public DbSet<PhanQuyen> PhanQuyens { get; set; }
        public DbSet<SuKien> SuKiens { get; set; }
        public DbSet<TinTuc> TinTucs { get; set; }

        public DbSet<DanhGia> DanhGias { get; set; }
        public DbSet<DieuKien_QuaTang> DieuKien_QuaTangs { get; set; }
        public DbSet<DieuKien_SanPham> DieuKien_SanPhams { get; set; }

        public DbSet<DieuKienKhuyenMai> DieuKienKhuyenMais { get; set; }
        public DbSet<DonHang> DonHangs { get; set; }
        public DbSet<ChiTietDonHang> ChiTietDonHangs { get; set; }
        public DbSet<ChiTietQuaTangDonHang> ChiTietQuaTangDonHangs { get; set; }
        public DbSet<CongThuc> CongThucs { get; set; }
        public DbSet<CheBien> CheBiens { get; set; }  // Thêm DbSet cho CheBien
        public DbSet<CTCBNguyenLieu> CTCBNguyenLieus { get; set; }
        public DbSet<PhieuThu> PhieuThus { get; set; }
        public DbSet<PhieuChi> PhieuChis { get; set; }
        public DbSet<GioHang> GioHangs { get; set; }
        //hàm dưới giúp EF Core hiểu rằng bảng này đã tồn tại, không cần tạo lại.



        //hàm dưới giúp EF Core hiểu rằng bảng này đã tồn tại, không cần tạo lại.

        // protected override void OnModelCreating(ModelBuilder modelBuilder)
        // {
        //     modelBuilder.Entity<LoaiSanPham>().ToTable("LoaiSanPham");
        //      modelBuilder.Entity<SanPham>().ToTable("SanPham");
        // }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DanhGia>()
                    .Property(d => d.MaDanhGia)
                    .ValueGeneratedOnAdd(); // Đảm bảo có dòng này (hoặc không có cấu hình nào cho ValueGenerated)
            // Cấu hình 1-1 rõ ràng
            modelBuilder.Entity<SanPham>()
            .HasOne(s => s.CongThuc)
            .WithOne(c => c.SanPham)
            .HasForeignKey<CongThuc>(c => c.MaSanPham);
        }
    }



}
