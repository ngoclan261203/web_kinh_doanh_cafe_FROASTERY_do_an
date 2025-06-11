using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FROASTERY.Models
{
    [Table("NguoiDung")] // Map đúng tên bảng SQL
    [Index(nameof(Email), IsUnique = true)] // Unique constraint cho Email
    public class NguoiDung
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ma_nguoi_dung")]
        public int MaNguoiDung { get; set; }

        [Required(ErrorMessage = "Email không được để trống.")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ.")]
        [StringLength(50, ErrorMessage = "Email không được vượt quá 50 ký tự.")]
        [Column("email")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Mật khẩu không được để trống.")]
        [MinLength(6, ErrorMessage = "Mật khẩu tối thiểu 6 ký tự.")]
        [StringLength(255, ErrorMessage = "Mật khẩu không được vượt quá 255 ký tự.")]
        [Column("mat_khau")]
        public string MatKhau { get; set; } = string.Empty;

        [Required(ErrorMessage = "Họ tên không được để trống.")]
        [StringLength(50, ErrorMessage = "Họ tên không được vượt quá 50 ký tự.")]
        [Column("ho_ten")]
        public string HoTen { get; set; } = string.Empty;

        [RegularExpression(@"^\d{10}$", ErrorMessage = "Số điện thoại phải gồm đúng 10 chữ số.")]
        [StringLength(10, ErrorMessage = "Số điện thoại không được vượt quá 10 ký tự.")]
        [Column("so_dien_thoai")]
        public string? SoDienThoai { get; set; }
        // Quan hệ 1 người dùng - nhiều phân quyền
        // public List<PhanQuyen>? PhanQuyens { get; set; }
        public List<PhanQuyen> PhanQuyens { get; set; } = new();
        public List<DonHang> DonHangs { get; set; } = new();
        public List<GioHang> GioHangs { get; set; } = new();

        // [NotMapped]
        // public string? VerificationCode { get; set; }
        [Column("reset_token")]
        public string? ResetToken { get; set; }

        [Column("reset_token_expiry")]
        public DateTime? ResetTokenExpiry { get; set; }


        [Column("da_xoa")]
        public bool DaXoa { get; set; } = false;
    }
}
