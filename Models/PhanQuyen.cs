using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FROASTERY.Models
{
    [Table("PhanQuyen")] // Tên bảng đúng với SQL Server
    [Index(nameof(MaNguoiDung), nameof(MaQuyen), IsUnique = true)] // Đảm bảo không trùng người dùng + quyền
    public class PhanQuyen
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ma_phan_quyen")] // Tên cột trong SQL Server
        public int MaPhanQuyen { get; set; }

        [Required]
        [Column("ma_nguoi_dung")]
        public int MaNguoiDung { get; set; }

        [Required]
        [Column("ma_quyen")]
        public int MaQuyen { get; set; }

        [Required]
        [Column("trang_thai")]
        public bool TrangThai { get; set; } = true;

        /// Navigation properties (quan hệ với NguoiDung và Quyen)
        [ForeignKey("MaNguoiDung")]
        public NguoiDung? NguoiDung { get; set; }

        [ForeignKey("MaQuyen")]
        public Quyen? Quyen { get; set; }

        [Column("da_xoa")]
        public bool DaXoa { get; set; } = false;
    }
}
