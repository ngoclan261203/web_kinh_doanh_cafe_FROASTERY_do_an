using System.ComponentModel.DataAnnotations;

namespace FROASTERY.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Vui lòng nhập Email.")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vui lòng nhập họ và tên.")]
        public string HoTen { get; set; } = string.Empty;

        public string? SoDienThoai { get; set; }

        [MinLength(6)]
        [Required(ErrorMessage = "Vui lòng nhập mật khẩu.")]
        public string MatKhau { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vui lòng nhập lại mật khẩu.")]
        [Compare("MatKhau", ErrorMessage = "Mật khẩu nhập lại không khớp.")]
        public string MatKhauLai { get; set; } = string.Empty;
    }
}
