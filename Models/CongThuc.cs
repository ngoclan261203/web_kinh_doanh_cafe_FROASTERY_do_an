using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FROASTERY.Models
{
    [Table("CongThuc")]
    [Index(nameof(MaSanPham), IsUnique = true)] // Đảm bảo mỗi sản phẩm chỉ có 1 công thức
    public class CongThuc
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ma_cong_thuc")]
        public int MaCongThuc { get; set; }

        [Required]
        [Column("ma_san_pham")]
        public int MaSanPham { get; set; }

        [Required(ErrorMessage = "Công thức không được để trống.")]
        [Column("Cong_thuc", TypeName = "nvarchar(max)")]
        public string CongThucMoTa { get; set; } = string.Empty;

        // Navigation property
        public SanPham? SanPham { get; set; }
        // Liên kết với CheBien
        public List<CheBien> CheBiens { get; set; } = new();


        [Column("da_xoa")]
        public bool DaXoa { get; set; } = false;

        // public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        // {
        //     if (MaSanPham == 0)
        //     {
        //         yield return new ValidationResult(
        //             "Vui lòng chọn sản phẩm.",
        //             new[] { nameof(MaSanPham) });
        //     }
        // }
    }
}
