using System.ComponentModel.DataAnnotations;

namespace FROASTERY.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Email không được bỏ trống")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Mật khẩu không được bỏ trống")]
        public string MatKhau { get; set; } = string.Empty;
    }
}
