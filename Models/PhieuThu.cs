using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
namespace FROASTERY.Models
{
    [Table("PhieuThu")] // Đặt tên bảng đúng với SQL Server
    public class PhieuThu
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ma_phieu_thu")] // Cột khóa chính
        public int MaPhieuThu { get; set; }

        [Required]
        [Column("thoi_gian", TypeName = "datetime")]
        public DateTime ThoiGian { get; set; }

        [Required]
        [Column("ma_doi_tac")]
        public int MaDoiTac { get; set; }

        [Required(ErrorMessage = "Số tiền không được để trống.")]
        [Range(0, double.MaxValue, ErrorMessage = "Số tiền phải lớn hơn hoặc bằng 0.")]
        [Column("so_tien", TypeName = "float")]
        public float SoTien { get; set; }

        [Column("noi_dung", TypeName = "nvarchar(max)")]
        public string? NoiDung { get; set; }

        // Navigation property (nếu có entity DoiTac)
        [ForeignKey("MaDoiTac")]
        public DoiTac? DoiTac { get; set; }

        public PhieuThu()
        {
            ThoiGian = DateTime.Now;  // Set default value to today
        }
        
        [Column("da_xoa")]
        public bool DaXoa { get; set; } = false;
    }
}
