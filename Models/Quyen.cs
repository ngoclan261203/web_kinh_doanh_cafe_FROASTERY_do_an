using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FROASTERY.Models
{
    [Table("Quyen")] // Tên bảng đúng trong SQL Server
    [Index(nameof(TenQuyen), IsUnique = true)] // Ràng buộc UNIQUE cho Tên quyền
    public class Quyen
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ma_quyen")] // Tên cột chính xác trong SQL
        public int MaQuyen { get; set; }

        [Required(ErrorMessage = "Tên quyền không được để trống.")]
        [StringLength(255, ErrorMessage = "Tên quyền không được vượt quá 255 ký tự.")]
        [Column("ten_quyen")]
        public string TenQuyen { get; set; } = string.Empty;

        [Required]
        [Column("trang_thai")]
        public bool TrangThai { get; set; } = true;
        public List<PhanQuyen>? PhanQuyens { get; set; }


        [Column("da_xoa")]
        public bool DaXoa { get; set; } = false;
    }
}
