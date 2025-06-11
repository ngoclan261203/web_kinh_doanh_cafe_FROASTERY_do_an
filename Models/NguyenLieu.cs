using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FROASTERY.Models
{
    [Table("NguyenLieu")] // Đặt tên bảng đúng với SQL Server
    [Index(nameof(TenNguyenLieu), IsUnique = true)] // Đảm bảo tên nguyên liệu là duy nhất
    public class NguyenLieu
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ma_nguyen_lieu")] // Tên cột trong SQL Server
        public int MaNguyenLieu { get; set; }


        [Required(ErrorMessage = "Tên nguyên liệu không được để trống.")]
        [StringLength(255, ErrorMessage = "Tên nguyên liệu không được vượt quá 255 ký tự.")]
        [Column("ten_nguyen_lieu")] // Đặt tên cột đúng theo SQL
        public string TenNguyenLieu { get; set; } = string.Empty;

        [StringLength(50, ErrorMessage = "Đơn vị tính không được vượt quá 50 ký tự.")]
        [Column("don_vi_tinh")] // Giữ nguyên kiểu dữ liệu
        public string? DonViTinh { get; set; }

        [Column("so_luong", TypeName = "float")]
        [Range(0, double.MaxValue, ErrorMessage = "Vui lòng nhập số lượng lớn hơn hoặc bằng 0.")]
        public float? SoLuong { get; set; }




        [Required] // Không cho phép null để đồng nhất với cách viết của `LoaiSanPham`
        [Column("trang_thai")]
        public bool TrangThai { get; set; } = true;
        public List<ChiTietPhieuNhapNguyenLieu> ChiTietPhieuNhapNguyenLieus { get; set; } = new();
        // Liên kết với bảng CTCBNuyenLieu
        public List<CTCBNguyenLieu> CTCBNguyenLieus { get; set; } = new();
        
        [Column("da_xoa")]
        public bool DaXoa { get; set; } = false;

    }
}
