using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FROASTERY.Models
{
    [Table("LoaiTin")] // Tên bảng trong SQL Server
    [Index(nameof(TenLoaiTin), IsUnique = true)] // Ràng buộc UNIQUE cho Tên loại tin
    public class LoaiTin
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ma_loai_tin")] // Tên cột trong SQL
        public int MaLoaiTin { get; set; }

        [Required(ErrorMessage = "Tên loại tin không được để trống.")]
        [StringLength(100, ErrorMessage = "Tên loại tin không được vượt quá 100 ký tự.")]
        [Column("ten_loai_tin")]
        public string TenLoaiTin { get; set; } = string.Empty;

        [Required]
        [Column("trang_thai")]
        public bool TrangThai { get; set; } = true;
        public List<SuKien>? SuKiens { get; set; }
        public List<TinTuc>? TinTucs { get; set; }


        [Column("da_xoa")]
        public bool DaXoa { get; set; } = false;
    }
}
