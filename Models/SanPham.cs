using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore; // Cần thiết để sử dụng [Index]

namespace FROASTERY.Models
{
    [Table("SanPham")]
    [Index(nameof(TenSanPham), IsUnique = true)] // Đảm bảo UNIQUE cho ten_san_pham
    public class SanPham
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ma_san_pham")]
        public int MaSanPham { get; set; }

        [Required]
        [Column("ma_loai_sp")]
        public int MaLoaiSP { get; set; }

        [Required(ErrorMessage = "Tên sản phẩm không được để trống.")]
        [StringLength(100, ErrorMessage = "Tên sản phẩm không được vượt quá 100 ký tự.")]
        [Column("ten_san_pham")]
        public string TenSanPham { get; set; } = string.Empty;

        [StringLength(255, ErrorMessage = "Mô tả không được vượt quá 255 ký tự.")]
        [Column("mo_ta")]
        public string? MoTa { get; set; }

        [Column("chi_tiet", TypeName = "nvarchar(max)")]
        public string? ChiTiet { get; set; }

        [StringLength(255)]
        [Column("anh")]
        public string? Anh { get; set; }

        [Column("so_luong", TypeName = "int")]
        [Range(0, int.MaxValue, ErrorMessage = "Số lượng phải lớn hơn hoặc bằng 0.")]
        public int? SoLuong { get; set; }

        [Column("gia", TypeName = "float")] // Ánh xạ cột "gia" với kiểu float
        public float? Gia { get; set; }

        [ForeignKey("MaLoaiSP")]
        public LoaiSanPham? LoaiSanPham { get; set; }

        // public List<SanPhamKhoiLuong>? KhoiLuongs { get; set; }//ể hỗ trợ mối quan hệ 1-N.
        public List<DieuKien_SanPham> DKSanPhams { get; set; } = new();
        public List<DieuKien_QuaTang> QuaTangs { get; set; } = new();
        public List<ChiTietDonHang> ChiTietDonHangs { get; set; } = new();
        public List<ChiTietQuaTangDonHang> ChiTietQuaTangDonHangs { get; set; } = new();
        public CongThuc? CongThuc { get; set; }
        // Liên kết với CheBien
        public List<CheBien> CheBiens { get; set; } = new();
        public List<GioHang> GioHangs { get; set; } = new();

        [Column("da_xoa")]
        public bool DaXoa { get; set; } = false;
    }
}
