using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FROASTERY.Models
{
    [Table("DoiTac")] // Đặt tên bảng đúng với SQL Server
    public class DoiTac
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ma_doi_tac")] // Tên cột trong SQL Server
        public int MaDoiTac { get; set; }

        [Required(ErrorMessage = "Tên sản phẩm không được để trống.")]
        [StringLength(50, ErrorMessage = "Tên đối tác không được vượt quá 50 ký tự.")]
        [Column("ten")] // Đặt tên cột đúng theo SQL
        public string Ten { get; set; } = string.Empty;

        [StringLength(100, ErrorMessage = "Thông tin liên lạc không được vượt quá 100 ký tự.")]
        [Column("thong_tin_lien_lac")]
        public string? ThongTinLienLac { get; set; }

        [StringLength(100, ErrorMessage = "Địa chỉ không được vượt quá 100 ký tự.")]
        [Column("dia_chi")]
        public string? DiaChi { get; set; }


        [Column("khach_hang")]
        public bool KhachHang { get; set; } = false;


        [Column("nha_cung_cap")]
        public bool NhaCungCap { get; set; } = false;

        [Column("du_no", TypeName = "float")]
        public float? DuNo { get; set; }  // Đảm bảo giá trị mặc định là 0
        public List<PhieuNhap>? PhieuNhaps { get; set; }
        public List<DonHang>? DonHangs { get; set; }
        public List<PhieuThu>? PhieuThus { get; set; }
        public List<PhieuChi>? PhieuChis { get; set; }

        [Column("da_xoa")]
        public bool DaXoa { get; set; } = false;
    }
}
