using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FROASTERY.Models
{
    [Table("ChiTietQuaTangDonHang")] // Tên bảng trong SQL Server
    public class ChiTietQuaTangDonHang
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ma_chi_tiet_qua_tang")]
        public int MaChiTietQuaTang { get; set; }

        [Required]
        [Column("ma_don_hang")]
        public int MaDonHang { get; set; }

        [Required]
        [Column("ma_san_pham_qua_tang")]
        public int MaSanPhamQuaTang { get; set; }

        [Column("so_luong", TypeName = "int")]
        public int? SoLuong { get; set; } = 1;

        // Liên kết với bảng DonHang
        [ForeignKey("MaDonHang")]
        public DonHang? DonHang { get; set; }

        // Liên kết với bảng SanPham (quà tặng)
        [ForeignKey("MaSanPhamQuaTang")]
        public SanPham? SanPhamQuaTang { get; set; }
    }
}
