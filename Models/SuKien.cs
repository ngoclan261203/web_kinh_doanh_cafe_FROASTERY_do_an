using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FROASTERY.Models
{
    [Table("SuKien")] // Map đúng tên bảng
    public class SuKien
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ma_su_kien")] // Tên cột trong SQL
        public int MaSuKien { get; set; }

        [Required]
        [Column("ma_loai_tin")]
        public int MaLoaiTin { get; set; }

        // Liên kết với bảng LoaiTin (nếu đã có model LoaiTin)
        [ForeignKey("MaLoaiTin")]
        public LoaiTin? LoaiTin { get; set; }

        [Required(ErrorMessage = "Tiêu đề không được để trống.")]
        [StringLength(255, ErrorMessage = "Tiêu đề không được vượt quá 255 ký tự.")]
        [Column("tieu_de")]
        public string TieuDe { get; set; } = string.Empty;

        [StringLength(255)]
        [Column("hinh_anh")]
        public string? Anh { get; set; }

        [Column("noi_dung")]
        public string? NoiDung { get; set; }

        [Column("tg_bat_dau")]
        public DateTime? ThoiGianBatDau { get; set; }

        [Column("tg_ket_thuc")]
        public DateTime? ThoiGianKetThuc { get; set; }

        [StringLength(255)]
        [Column("lap_lai_theo_tuan")]
        public string? LapLaiTheoTuan { get; set; }

        [NotMapped]
        public List<string> NgayLapLai
        {
            get => string.IsNullOrEmpty(LapLaiTheoTuan) ? new List<string>() : LapLaiTheoTuan.Split(", ").ToList();
            set => LapLaiTheoTuan = string.Join(", ", value);
        }


        // [StringLength(50)]
        // [Column("trang_thai")]

        [NotMapped]
        public string TrangThai
        {
            get
            {
                var now = DateTime.Now;

                // Không thiết lập gì cả
                if (!ThoiGianBatDau.HasValue && !ThoiGianKetThuc.HasValue)
                    return "Chưa thiết lập";

                // Có cả bắt đầu và kết thúc
                if (ThoiGianBatDau.HasValue && ThoiGianKetThuc.HasValue)
                {
                    if (now < ThoiGianBatDau.Value)
                        return "Sắp diễn ra";
                    else if (now >= ThoiGianBatDau.Value && now <= ThoiGianKetThuc.Value)
                        return "Đang diễn ra";
                    else
                        return "Kết thúc";
                }

                // Chỉ có bắt đầu → không có giới hạn kết thúc
                if (ThoiGianBatDau.HasValue && !ThoiGianKetThuc.HasValue)
                {
                    if (now >= ThoiGianBatDau.Value)
                        return "Đang diễn ra";
                    else
                        return "Sắp diễn ra";
                }

                // Chỉ có kết thúc → không có bắt đầu
                if (!ThoiGianBatDau.HasValue && ThoiGianKetThuc.HasValue)
                {
                    if (now <= ThoiGianKetThuc.Value)
                        return "Đang diễn ra";
                    else
                        return "Kết thúc";
                }

                // Trường hợp fallback
                return "Không xác định";
            }
        }



        // Liên kết ngược lại: 1 Sự kiện có nhiều Điều kiện khuyến mãi
        public List<DieuKienKhuyenMai> DieuKienKhuyenMais { get; set; } = new();

        public List<DonHang>? DonHangs { get; set; }
        

        [Column("da_xoa")]
        public bool DaXoa { get; set; } = false;
    }
}
