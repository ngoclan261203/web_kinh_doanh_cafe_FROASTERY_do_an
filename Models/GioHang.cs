using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FROASTERY.Models
{
    [Table("GioHang")] // Tên bảng đúng với SQL Server
    public class GioHang
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("ma_san_pham")]
        public int MaSanPham { get; set; }

        [Column("so_luong", TypeName = "int")]
        public int? SoLuong { get; set; }
      
        [Column("chon")]
        public bool? Chon { get; set; } = false;

        [Required]
        [Column("ma_nguoi_dung")]
        public int MaNguoiDung { get; set; }

        // Navigation properties (liên kết khoá ngoại)
        [ForeignKey("MaSanPham")]
        public SanPham? SanPham { get; set; }

        [ForeignKey("MaNguoiDung")]
        public NguoiDung? NguoiDung { get; set; }
    }
}
