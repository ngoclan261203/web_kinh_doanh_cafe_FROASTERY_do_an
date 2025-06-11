using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FROASTERY.Models
{
    [Table("DieuKienKhuyenMai")]
    public class DieuKienKhuyenMai
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ma_chi_tiet")]
        public int MaChiTiet { get; set; }

        [Required]
        [Column("ma_su_kien")]
        public int MaSuKien { get; set; }

        [Column("kieu_ap_dung")]
        [StringLength(50)]
        public string? KieuApDung { get; set; } // "DonHang" hoặc "SanPham"

        [Column("gia_tri_don_hang", TypeName = "float")]
        // [Range(0, double.MaxValue, ErrorMessage = "Giá trị đơn hàng phải lớn hơn hoặc bằng 0.")]
        // [CustomValidation(typeof(DieuKienKhuyenMai), "ValidateGiaTriDonHangRequired")]
        public float? GiaTriDonHang { get; set; }

        // ... giữ nguyên các using và phần trên

        [Column("gia_tri_don_hang_tu", TypeName = "float")]
        [Range(0, double.MaxValue, ErrorMessage = "Giá trị đơn hàng từ phải >= 0.")]
        public float? GiaTriDonHangTu { get; set; }

        [Column("gia_tri_don_hang_den", TypeName = "float")]
        [Range(0, double.MaxValue, ErrorMessage = "Giá trị đơn hàng đến phải >= 0.")]
        public float? GiaTriDonHangDen { get; set; }


        [Column("kieu_giam")]
        [StringLength(50)]
        public string? KieuGiam { get; set; } // "%" hoặc "VND"

        [Column("gia_tri_giam", TypeName = "float")]
        [Range(1, double.MaxValue, ErrorMessage = "Giá trị giảm phải lớn hơn hoặc bằng 1.")]
        [CustomValidation(typeof(DieuKienKhuyenMai), "ValidateKieuGiamRequired")]
        public float? GiaTriGiam { get; set; }

        [Column("so_luong_yeu_cau", TypeName = "int")]
        public int? SoLuongYeuCau { get; set; }




        [ForeignKey("MaSuKien")]
        public SuKien? SuKien { get; set; }

        public List<DieuKien_SanPham> DKSanPhams { get; set; } = new();
        public List<DieuKien_QuaTang> QuaTangs { get; set; } = new();

        // Phương thức kiểm tra nếu GiaTriGiam có giá trị mà KieuGiam không có giá trị
        public static ValidationResult ValidateKieuGiamRequired(float? giaTriGiam, ValidationContext context)
        {
            var instance = context.ObjectInstance as DieuKienKhuyenMai;

            // Kiểm tra nếu GiaTriGiam có giá trị nhưng KieuGiam không được chọn
            if (giaTriGiam.HasValue && string.IsNullOrEmpty(instance?.KieuGiam))
            {
                // Trả về lỗi nếu KieuGiam không có giá trị
                return new ValidationResult("Khi giá trị giảm có giá trị, bạn phải chọn kiểu giảm.", new[] { "KieuGiam" });
            }
            // Kiểm tra nếu KieuGiam là "%" và GiaTriGiam vượt quá 100
            if (instance?.KieuGiam == "%" && giaTriGiam.HasValue && giaTriGiam > 100)
            {
                return new ValidationResult("Giá trị giảm không được vượt quá 100% khi kiểu giảm là '%'.", new[] { "GiaTriGiam" });
            }

            // Trả về ValidationResult.Success nếu không có lỗi
            return ValidationResult.Success!;
        }

        // public static ValidationResult ValidateGiaTriDonHangRequired(float? giaTriDonHang, ValidationContext context)
        // {
        //     var instance = context.ObjectInstance as DieuKienKhuyenMai;

        //     if (instance?.KieuApDung == "Đơn hàng" && !giaTriDonHang.HasValue)
        //     {
        //         return new ValidationResult("Giá trị đơn hàng là bắt buộc khi áp dụng theo đơn hàng.", new[] { "GiaTriDonHang" });
        //     }

        //     return ValidationResult.Success!;
        // }

        [NotMapped]
        [CustomValidation(typeof(DieuKienKhuyenMai), "ValidateGiaTriDonHangTuDen")]
        public string? GiaTriTuDenValidationDummy { get; set; }

        public static ValidationResult ValidateGiaTriDonHangTuDen(object? dummy, ValidationContext context)
        {
            var instance = context.ObjectInstance as DieuKienKhuyenMai;

            if (instance?.KieuApDung?.ToLower() == "đơn hàng" && instance.GiaTriDonHangTu.HasValue && instance.GiaTriDonHangDen.HasValue)
            {
                if (instance.GiaTriDonHangTu >= instance.GiaTriDonHangDen)
                {
                    return new ValidationResult("Giá trị 'Từ' phải nhỏ hơn giá trị 'Đến'.", new[] { "GiaTriDonHangTu", "GiaTriDonHangDen" });
                }
            }

            return ValidationResult.Success!;
        }



        // [NotMapped]
        // [CustomValidation(typeof(DieuKienKhuyenMai), "ValidateSanPhamListRequired")]
        // public string? SanPhamListValidationDummy { get; set; }

        // public static ValidationResult ValidateSanPhamListRequired(object? dummy, ValidationContext context)
        // {
        //     var instance = context.ObjectInstance as DieuKienKhuyenMai;

        //     if (instance?.KieuApDung == "Sản phẩm" && (instance.DKSanPhams == null || instance.DKSanPhams.Count == 0))
        //     {
        //         return new ValidationResult("Vui lòng thêm ít nhất một sản phẩm khi áp dụng theo sản phẩm.", new[] { "DKSanPhams" });
        //     }

        //     return ValidationResult.Success!;
        // }



    }
}
