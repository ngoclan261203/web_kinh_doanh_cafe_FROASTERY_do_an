using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FROASTERY.Models
{
    [Table("ChiTietPhieuNhapNguyenLieu")] // Đặt tên bảng đúng với SQL Server
    public class ChiTietPhieuNhapNguyenLieu
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ma_chi_tiet_phieu_nhap")]
        public int MaChiTietPhieuNhap { get; set; }

        [Required]
        [Column("ma_phieu_nhap")]
        public int MaPhieuNhap { get; set; }

        [Required]
        [Column("ma_nguyen_lieu")]
        public int MaNguyenLieu { get; set; }

        [Column("so_luong", TypeName = "float")]
        // [Range(0, int.MaxValue, ErrorMessage = "Số lượng phải lớn hơn hoặc bằng 0.")]
        public float? SoLuong { get; set; }

        [Column("gia_nhap", TypeName = "float")]
        [Range(0, double.MaxValue, ErrorMessage = "Giá tiền phải lớn hơn hoặc bằng 0.")]
        public float? GiaNhap { get; set; }

        [Column("thanh_tien", TypeName = "float")]
        public float? ThanhTien { get; set; }

        // Liên kết với bảng PhieuNhap
        [ForeignKey("MaPhieuNhap")]
        public PhieuNhap? PhieuNhap { get; set; }

        // Liên kết với bảng NguyenLieu
        [ForeignKey("MaNguyenLieu")]
        public NguyenLieu? NguyenLieu { get; set; }
    }
}
