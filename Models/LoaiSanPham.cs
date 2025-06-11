using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FROASTERY.Models
{
    [Table("LoaiSanPham")] // Đảm bảo Entity map đúng bảng SQL
    [Index(nameof(TenLoaiSP), IsUnique = true)] // UNIQUE Constraint
    public class LoaiSanPham
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ma_loai_sp")] // Đặt tên cột chính xác theo SQL
        public int MaLoaiSP { get; set; }

        [Required(ErrorMessage = "Tên loại sản phẩm không được để trống.")]
        [StringLength(100, ErrorMessage = "Tên loại sản phẩm không được vượt quá 100 ký tự.")]
        [Column("ten_loai_sp")] // Đặt tên cột chính xác theo SQL
        public string TenLoaiSP { get; set; } = string.Empty;

        [Required]
        [Column("trang_thai")] // Đặt tên cột chính xác theo SQL
        public bool TrangThai { get; set; } = true;

        [StringLength(255)]
        [Column("anh")]
        public string? Anh { get; set; }

        // Thêm danh sách sản phẩm liên kết (nếu không có thì dẫn đến lỗi dữ liệu)
        public List<SanPham>? SanPhams { get; set; }

        [Column("da_xoa")]
        public bool DaXoa { get; set; } = false;

    }
}
