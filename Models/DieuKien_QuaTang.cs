using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FROASTERY.Models
{
    [Table("DieuKien_QuaTang")]
    public class DieuKien_QuaTang
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("ma_chi_tiet")]
        public int MaChiTiet { get; set; }

        [Required]
        [Column("ma_san_pham")]
        public int MaSanPham { get; set; }

        [ForeignKey("MaChiTiet")]
        public DieuKienKhuyenMai? DieuKienKhuyenMai { get; set; }

        [Column("so_luong", TypeName = "int")]
        [Range(0, int.MaxValue)]
        public int? SoLuong { get; set; }


        [ForeignKey("MaSanPham")]
        public SanPham? SanPham { get; set; }
    }
}
