using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FROASTERY.Models
{
    [Table("CTCBNguyenLieu")]
    public class CTCBNguyenLieu
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ma_ct_cn_nguyen_lieu")]
        public int MaCTCNNguyenLieu { get; set; }

        [Required]
        [Column("ma_che_bien")]
        public int MaCheBien { get; set; }

        [Required]
        [Column("ma_nguyen_lieu")]
        public int MaNguyenLieu { get; set; }

        [Column("so_luong_nl", TypeName = "float")]
        public float? SoLuong { get; set; }

        // Liên kết với bảng CheBien
        [ForeignKey("MaCheBien")]
        public CheBien? CheBien { get; set; }

        // Liên kết với bảng NguyenLieu
        [ForeignKey("MaNguyenLieu")]
        public NguyenLieu? NguyenLieu { get; set; }
    }
}
