using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FROASTERY.Models
{
    [Table("DanhGia")]
    public class DanhGia
    {
        [Key]
        [Column("ma_danh_gia")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int MaDanhGia { get; set; }

        [Column("noi_dung")]
        [Required(ErrorMessage = "Vui lòng nhập nội dung.")]
        public string NoiDung { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập họ tên.")]
        [Column("ho_ten_khach")]
        [MaxLength(50)]
        public string HoTen { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập thông tin liên lạc.")]
        [Column("thong_tin_lien_lac")]
        [MaxLength(255)]
        public string ThongTinLienLac { get; set; }

        [Column("ghi_chu")]
        [MaxLength(255)]
        public string? GhiChu { get; set; }

        [Required]
        [Column("ThoiGianGui")]
        public DateTime ThoiGianGui { get; set; } = DateTime.Now;

        [Column("SoSao")]
        [Range(1, 5, ErrorMessage = "Vui lòng chọn số sao từ 1 đến 5.")]
        [Required(ErrorMessage = "Vui lòng chọn số sao.")]
        public int SoSao { get; set; }

        [Required]
        [Column("TrangThai")]
        public int TrangThai { get; set; } = 0;

        [Column("da_xoa")]
        public bool DaXoa { get; set; } = false;
    }
}