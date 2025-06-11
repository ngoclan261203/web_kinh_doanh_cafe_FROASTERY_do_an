using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FROASTERY.Models
{
    [Table("CheBien")]
    public class CheBien
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ma_che_bien")]
        public int MaCheBien { get; set; }

        [Required(ErrorMessage = "Thời gian không được bỏ trống.")]
        [Column("thoi_gian")]
        public DateTime ThoiGian { get; set; }

        [Column("ma_cong_thuc")]
        public int? MaCongThuc { get; set; }  // Nullable now ✅

        [Required]
        [Column("ma_san_pham")]
        public int MaSanPham { get; set; }

        [Column("so_luong_sp", TypeName = "int")]
        [Range(0, int.MaxValue, ErrorMessage = "Số lượng phải lớn hơn hoặc bằng 0.")]
        public int? SoLuongSP { get; set; }

        // Liên kết với Công Thức
        [ForeignKey("MaCongThuc")]
        public CongThuc? CongThuc { get; set; }

        // Liên kết với Chi Tiết Chế Biến Nguyên Liệu
        public List<CTCBNguyenLieu> CTCBNguyenLieus { get; set; } = new();

        // Liên kết với Chi Tiết Chế Biến Sản Phẩm
        // Liên kết với Sản Phẩm
        [ForeignKey("MaSanPham")]
        public SanPham? SanPham { get; set; }

        public CheBien()
        {
            ThoiGian = DateTime.Now;  // Set default value to today
        }

        [Column("da_xoa")]
        public bool DaXoa { get; set; } = false;
    }
}
