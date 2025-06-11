using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FROASTERY.Models
{
    [Table("ChiTietDonHang")] // Tên bảng trong SQL Server
    public class ChiTietDonHang
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ma_chi_tiet_don_hang")]
        public int MaChiTietDonHang { get; set; }

        [Required]
        [Column("ma_don_hang")]
        public int MaDonHang { get; set; }

        [Required]
        [Column("ma_san_pham")]
        public int MaSanPham { get; set; }

        [Column("so_luong", TypeName = "int")]
        public int? SoLuong { get; set; }

         
        [Column("thanh_tien", TypeName = "float")]
        public float? ThanhTien { get; set; }

       

        // Liên kết với bảng DonHang
        [ForeignKey("MaDonHang")]
        public DonHang? DonHang { get; set; }

        // Liên kết với bảng SanPham
        [ForeignKey("MaSanPham")]
        public SanPham? SanPham { get; set; }
    }
}
