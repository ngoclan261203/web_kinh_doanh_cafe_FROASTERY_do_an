using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FROASTERY.Models
{
    [Table("DieuKien_SanPham")]
    public class DieuKien_SanPham
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

        [ForeignKey("MaSanPham")]
        public SanPham? SanPham { get; set; }
    }
}
