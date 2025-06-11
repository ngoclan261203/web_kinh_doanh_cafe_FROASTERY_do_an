using System.ComponentModel.DataAnnotations;

namespace FROASTERY.Models
{
    public class ForgotPasswordModel
    {
        [Required(ErrorMessage = "Email không được để trống.")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập mã xác minh.")]
        public string VerificationCode { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập mật khẩu mới.")]
        public string NewPassword { get; set; }

        [Compare("NewPassword", ErrorMessage = "Mật khẩu không khớp.")]
        public string ConfirmPassword { get; set; }

        }
}    