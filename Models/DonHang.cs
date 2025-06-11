using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FROASTERY.Models
{
    [Table("DonHang")] // Tên bảng đúng như SQL Server
    public class DonHang
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ma_don_hang")]
        public int MaDonHang { get; set; }

        // [Required(ErrorMessage = "Vui lòng nhập thời gian.")]
        [Column("thoi_gian")]
        public DateTime? ThoiGian { get; set; }

        public DonHang()
        {
            ThoiGian = DateTime.Now;  // Set default value to today
        }

        [Column("loai_don")]
        public bool? LoaiDon { get; set; } = false; // true = sỉ, false = lẻ

        [Required(ErrorMessage = "Tên không được để trống")]
        [StringLength(50, ErrorMessage = "Tên không được vượt quá 50 ký tự.")]
        [Column("ten")]
        public string Ten { get; set; } = string.Empty;

        [StringLength(100, ErrorMessage = "Thông tin liên lạc không được vượt quá 100 ký tự.")]
        [Column("thong_tin_lien_lac")]
        public string? ThongTinLienLac { get; set; }

        [StringLength(100, ErrorMessage = "Địa chỉ không được vượt quá 100 ký tự.")]
        [Column("dia_chi")]
        public string? DiaChi { get; set; }


        [Column("ma_doi_tac")]
        public int? MaDoiTac { get; set; } // Có thể NULL nếu đơn lẻ

        [Column("ma_khuyen_mai")]
        public int? MaKhuyenMai { get; set; } // Có thể NULL

        [Column("tong_tien", TypeName = "float")]
        public float? TongTien { get; set; }

        [StringLength(100)]
        [Column("phuong_thuc_tt")]
        public string? PhuongThucThanhToan { get; set; }

        [StringLength(50)]
        [Column("trang_thai")]
        public string? TrangThai { get; set; } = "Chưa duyệt";

        // Navigation Properties
        [ForeignKey("MaKhuyenMai")]
        public SuKien? KhuyenMai { get; set; }

        [ForeignKey("MaDoiTac")]
        public DoiTac? DoiTac { get; set; }


        [Column("ma_nguoi_dung")]
        public int? MaNguoiDung { get; set; }

        [ForeignKey("MaNguoiDung")]
        public NguoiDung? NguoiDung { get; set; }


        // Danh sách chi tiết đơn hàng (nếu bạn có bảng ChiTietDonHang)
        public List<ChiTietDonHang> ChiTietDonHangs { get; set; } = new();
        public List<ChiTietQuaTangDonHang> ChiTietQuaTangDonHangs { get; set; } = new();
         
         [Column("da_xoa")]
        public bool DaXoa { get; set; } = false;
    }
}
