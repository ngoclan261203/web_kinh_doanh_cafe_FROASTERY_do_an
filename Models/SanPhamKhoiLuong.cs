// using System.ComponentModel.DataAnnotations;
// using System.ComponentModel.DataAnnotations.Schema;

// namespace FROASTERY.Models
// {
//     [Table("SanPhamKhoiLuong")] // Chỉ định tên bảng trong SQL Server
//     public class SanPhamKhoiLuong
//     {
//         [Key]
//         [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
//         [Column("id")] // Ánh xạ cột "id"
//         public int Id { get; set; } // Khóa chính

//         [Required]
//         [Column("ma_san_pham")] // Ánh xạ cột "ma_san_pham"
//         public int MaSanPham { get; set; } // Liên kết với bảng SanPham

//         [Column("khoi_luong", TypeName = "nvarchar(50)")] // Ánh xạ cột "khoi_luong" với kiểu nvarchar(50)
//         public string? KhoiLuong { get; set; } // VD: "100g", "200g", có thể NULL

//         [Column("gia", TypeName = "float")] // Ánh xạ cột "gia" với kiểu float
//        public float? Gia { get; set; }

//          [ForeignKey("MaSanPham")]
//         public  SanPham? SanPham { get; set; }
//     }
// }
