using System.ComponentModel.DataAnnotations;

namespace FROASTERY.Models
{
    public class ForgotPasswordRequest
    {
        [Required(ErrorMessage = "Email không được để trống.")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ.")]
        public string Email { get; set; }
    }
}