using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore; 

namespace FROASTERY.Models
{
    [Table("TinTuc")]
    public class TinTuc
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ma_tin_tuc")]
        public int MaTinTuc { get; set; }

        [Required(ErrorMessage = "Thời gian là bắt buộc.")]
        [Column("thoi_gian")]
        public DateTime ThoiGian { get; set; }
        public TinTuc()
        {
            ThoiGian = DateTime.Now;  // Set default value to today
        }

        [Required(ErrorMessage = "Mã loại tin là bắt buộc.")]
        [Column("ma_loai_tin")]
        public int MaLoaiTin { get; set; }

        [Required(ErrorMessage = "Tiêu đề là bắt buộc.")]
        [Column("tieu_de")]
        [MaxLength(255, ErrorMessage = "Tiêu đề không được vượt quá 255 ký tự.")]
        public string TieuDe { get; set; } = string.Empty;
        [Column("hinh_anh")]
        // [MaxLength(255, ErrorMessage = "Đường dẫn ảnh không được vượt quá 255 ký tự.")]
        public string? HinhAnh { get; set; }

        [Column("file_dinh_kem")]
        public string? FileDinhKem { get; set; }

        [ForeignKey("MaLoaiTin")]
        public LoaiTin? LoaiTin { get; set; } 
        
        [Column("da_xoa")]
        public bool DaXoa { get; set; } = false;
    }
}