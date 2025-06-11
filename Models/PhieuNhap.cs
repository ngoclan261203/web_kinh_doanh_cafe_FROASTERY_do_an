
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace FROASTERY.Models
{
    [Table("PhieuNhap")] // Đặt tên bảng trùng với SQL Server
    public class PhieuNhap
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ma_phieu_nhap")] // Đặt tên cột đúng theo SQL Server
        public int MaPhieuNhap { get; set; }

        [Column("thoi_gian")]
        public DateTime? ThoiGian { get; set; }

        [Required]
        [Column("ma_doi_tac")]
        public int MaDoiTac { get; set; }

        [Column("tong_tien", TypeName = "float")]
        public float? TongTien { get; set; }

        [StringLength(255, ErrorMessage = "Ghi chú không được vượt quá 255 ký tự.")]
        [Column("ghi_chu")]
        public string? GhiChu { get; set; }

        [ForeignKey("MaDoiTac")]
        public DoiTac? DoiTac { get; set; } // Liên kết với bảng DoiTac
        public List<ChiTietPhieuNhapNguyenLieu> ChiTietPhieuNhapNguyenLieus { get; set; } = new();
        
        [Column("da_xoa")]
        public bool DaXoa { get; set; } = false;

    }
}
