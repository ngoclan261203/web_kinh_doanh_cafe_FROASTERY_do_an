using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore; // Cần thiết để sử dụng [Index]

namespace FROASTERY.Models
{
    public class ResetPasswordDTO
    {
        [Required]
        [StringLength(255, MinimumLength = 6)]
        public string NewPassword { get; set; }

        [Required]
        [StringLength(255, MinimumLength = 6)]
        public string ConfirmPassword { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}