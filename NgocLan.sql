use F_ROASTERY
go

---bảng loại sản phẩm
CREATE TABLE LoaiSanPham (
    ma_loai_sp INT PRIMARY KEY IDENTITY(1,1),
    ten_loai_sp NVARCHAR(100) UNIQUE NOT NULL,
    trang_thai BIT NOT NULL DEFAULT 1
);
ALTER TABLE LoaiSanPham 
ADD anh NVARCHAR(255) ;
select * from LoaiSanPham

--bảng sản phẩm
CREATE TABLE SanPham (
    ma_san_pham INT PRIMARY KEY IDENTITY(1,1),
    ma_loai_sp INT NOT NULL,
    ten_san_pham NVARCHAR(100) UNIQUE NOT NULL,
    mo_ta NVARCHAR(255),
    chi_tiet NVARCHAR(MAX),
    anh VARCHAR(255),
    so_luong INT ,
	gia FLOAT
    CONSTRAINT FK_SanPham_LoaiSanPham FOREIGN KEY (ma_loai_sp) REFERENCES LoaiSanPham(ma_loai_sp)
);


----bảng phụ khối lượng (hijen tại đang bỏ)
CREATE TABLE SanPhamKhoiLuong (
	id INT IDENTITY(1,1) PRIMARY KEY,
    ma_san_pham INT NOT NULL,  
    khoi_luong NVARCHAR(50),      
    gia FLOAT,          
    FOREIGN KEY (ma_san_pham) REFERENCES SanPham(ma_san_pham) ON DELETE CASCADE
);
select * from SanPhamKhoiLuong

select * from SanPham


CREATE TABLE NguyenLieu (
    ma_nguyen_lieu INT PRIMARY KEY IDENTITY(1,1),  -- Mã số nguyên liệu, tự động tăng
    ten_nguyen_lieu NVARCHAR(255) UNIQUE NOT NULL, -- Tên nguyên liệu, không trùng lặp
    don_vi_tinh NVARCHAR(50),                 -- Đơn vị tính có thể để trống
    so_luong FLOAT ,                             -- Số lượng có thể để trống
    trang_thai BIT NOT NULL DEFAULT 1                            -- Trạng thái có thể để trống
);


select * from NguyenLieu

CREATE TABLE DoiTac (
    ma_doi_tac INT IDENTITY(1,1) PRIMARY KEY, 
    ten NVARCHAR(50) NOT NULL,
    thong_tin_lien_lac NVARCHAR(100),
    dia_chi NVARCHAR(100),
    khach_hang BIT  DEFAULT 0,
    nha_cung_cap BIT DEFAULT 0,
    du_no FLOAT DEFAULT 0
);



CREATE TABLE ChiTietPhieuNhapNguyenLieu (
    ma_chi_tiet_phieu_nhap INT IDENTITY(1,1) PRIMARY KEY,
    ma_phieu_nhap INT NOT NULL,
    ma_nguyen_lieu INT NOT NULL,
    so_luong FLOAT ,
    gia_nhap FLOAT DEFAULT 0,
    ---thanh_tien AS (ISNULL(so_luong, 0) * ISNULL(gia_nhap, 0)) PERSISTED,
	thanh_tien FLOAT DEFAULT 0,
    FOREIGN KEY (ma_phieu_nhap) REFERENCES PhieuNhap(ma_phieu_nhap) ON DELETE CASCADE,
    FOREIGN KEY (ma_nguyen_lieu) REFERENCES NguyenLieu(ma_nguyen_lieu)
);

select * from ChiTietPhieuNhapNguyenLieu
CREATE TABLE PhieuNhap (
    ma_phieu_nhap INT IDENTITY(1,1) PRIMARY KEY, 
    thoi_gian DATETIME ,                   
    ma_doi_tac INT NOT NULL,                   
    tong_tien FLOAT DEFAULT 0,   
	ghi_chu NVARCHAR(255)
    FOREIGN KEY (ma_doi_tac) REFERENCES DoiTac(ma_doi_tac) 
	--chú ý khả nawgn phải thêm on delete để khi đối tác bị xóa thì phiếu nhập cg bị xóa theo
);
//lệnh để sửa thêm xóa on delete cho khóa ngoại
SELECT name
FROM sys.foreign_keys
WHERE parent_object_id = OBJECT_ID('PhieuNhap');

ALTER TABLE PhieuNhap
DROP CONSTRAINT FK__PhieuNhap__ghi_c__6477ECF3;

ALTER TABLE PhieuNhap
ADD CONSTRAINT FK__PhieuNhap__ghi_c__6477ECF3
FOREIGN KEY (ma_doi_tac) REFERENCES DoiTac(ma_doi_tac)
ON DELETE CASCADE;


select * from PhieuNhap



CREATE TABLE Quyen (
    ma_quyen INT IDENTITY(1,1) PRIMARY KEY,  -- Tự động tăng
    ten_quyen NVARCHAR(255) UNIQUE NOT NULL, -- Không được để trống
    trang_thai BIT NOT NULL DEFAULT 1        -- Mặc định là hoạt động
);




CREATE TABLE NguoiDung (
    ma_nguoi_dung INT IDENTITY(1,1) PRIMARY KEY,  -- Tự động tăng, khóa chính
    email VARCHAR(50) NOT NULL UNIQUE,            -- Duy nhất, không null
    mat_khau NVARCHAR(255) NOT NULL,              -- Bắt buộc nhập
    ho_ten NVARCHAR(50) NOT NULL,                 -- Bắt buộc nhập
    so_dien_thoai VARCHAR(10)                     -- Cho phép null nếu cần
);
ALTER TABLE NguoiDung
ADD reset_token NVARCHAR(100) NULL,
    reset_token_expiry DATETIME NULL;
select * from NguoiDung
CREATE TABLE PhanQuyen (
    ma_phan_quyen INT IDENTITY(1,1) PRIMARY KEY,
    ma_nguoi_dung INT NOT NULL,
    ma_quyen INT NOT NULL,
	trang_thai  BIT NOT NULL DEFAULT 1,  
 
      
	CONSTRAINT UQ_PhanQuyen UNIQUE (ma_nguoi_dung, ma_quyen),
    -- Khóa ngoại đến NguoiDung với xóa cascade
    CONSTRAINT FK_PhanQuyen_NguoiDung FOREIGN KEY (ma_nguoi_dung)
        REFERENCES NguoiDung(ma_nguoi_dung)
        ON DELETE CASCADE,

    -- Khóa ngoại đến Quyen với xóa cascade
    CONSTRAINT FK_PhanQuyen_Quyen FOREIGN KEY (ma_quyen)
        REFERENCES Quyen(ma_quyen)
        ON DELETE CASCADE
);

CREATE TABLE LoaiTin (
    ma_loai_tin INT IDENTITY(1,1) PRIMARY KEY,
    ten_loai_tin NVARCHAR(100) UNIQUE NOT NULL,
    trang_thai  BIT NOT NULL DEFAULT 1    
);

CREATE TABLE SuKien (
    ma_su_kien INT IDENTITY(1,1) PRIMARY KEY,                       
    ma_loai_tin INT NOT NULL,                                  
    tieu_de NVARCHAR(255) NOT NULL,                   
    hinh_anh NVARCHAR(255),                           
    noi_dung NVARCHAR(MAX),                           
    tg_bat_dau DATETIME ,                   
    tg_ket_thuc DATETIME ,                  
    lap_lai_theo_tuan NVARCHAR(255),                   
    trang_thai NVARCHAR(50),
    
    CONSTRAINT FK_SuKien_LoaiTin FOREIGN KEY (ma_loai_tin)
        REFERENCES LoaiTin(ma_loai_tin)
        ON DELETE CASCADE
)

ALTER TABLE DieuKien_QuaTang
ADD so_luong INT  DEFAULT 1;

CREATE TABLE DieuKienKhuyenMai (
    ma_chi_tiet INT IDENTITY(1,1) PRIMARY KEY,
    ma_su_kien INT NOT NULL,
    kieu_ap_dung NVARCHAR(50),
    gia_tri_don_hang FLOAT,
    kieu_giam NVARCHAR(50),
    gia_tri_giam FLOAT,
    FOREIGN KEY (ma_su_kien) REFERENCES SuKien(ma_su_kien)
);
ALTER TABLE DieuKienKhuyenMai
ADD gia_tri_don_hang_tu FLOAT ,
    gia_tri_don_hang_den FLOAT ;
ALTER TABLE DieuKienKhuyenMai
ADD so_luong_yeu_cau INT ;


--có thể xóa cột trạng thái ở sql
select * from SuKien
select * from DieuKien_QuaTang
select * from DieuKien_SanPham
select * from DieuKienKhuyenMai


CREATE TABLE DonHang (
    ma_don_hang INT IDENTITY(1,1) PRIMARY KEY, 
    thoi_gian DATETIME,
	loai_don BIT,
    ten NVARCHAR(50) NOT NULL,
    thong_tin_lien_lac NVARCHAR(100),
    dia_chi NVARCHAR(100),
	ma_doi_tac INT,   
    ma_khuyen_mai INT,
	tong_tien FLOAT DEFAULT 0,
    phuong_thuc_tt NVARCHAR(100),
    trang_thai NVARCHAR(50),
  
    -- Khóa ngoại
    CONSTRAINT FK_DonHang_KhuyenMai FOREIGN KEY (ma_khuyen_mai) REFERENCES SuKien(ma_su_kien),
    CONSTRAINT FK_DonHang_DoiTac FOREIGN KEY (ma_doi_tac) REFERENCES DoiTac(ma_doi_tac) ON DELETE CASCADE
);


CREATE TABLE ChiTietDonHang(
    ma_chi_tiet_don_hang INT IDENTITY(1,1) PRIMARY KEY,
    ma_don_hang INT NOT NULL,
    ma_san_pham INT NOT NULL,
    so_luong INT ,
    ---thanh_tien AS (ISNULL(so_luong, 0) * ISNULL(gia_nhap, 0)) PERSISTED,
	thanh_tien FLOAT DEFAULT 0,
    FOREIGN KEY (ma_don_hang) REFERENCES DonHang(ma_don_hang) ON DELETE CASCADE,
    CONSTRAINT FK_ChiTietDonHang_SanPham FOREIGN KEY (ma_san_pham) REFERENCES SanPham(ma_san_pham)
);

CREATE TABLE ChiTietQuaTangDonHang (
    ma_chi_tiet_qua_tang INT IDENTITY(1,1) PRIMARY KEY,
    ma_don_hang INT NOT NULL,
    ma_san_pham_qua_tang INT NOT NULL,
    so_luong INT DEFAULT 1,
    FOREIGN KEY (ma_don_hang) REFERENCES DonHang(ma_don_hang) ON DELETE CASCADE,
    FOREIGN KEY (ma_san_pham_qua_tang) REFERENCES SanPham(ma_san_pham)
);


select * from ChiTietQuaTangDonHang


select * from ChiTietDonHang
select * from DonHang

CREATE TABLE CongThuc (
    ma_cong_thuc INT IDENTITY(1,1) PRIMARY KEY,
    ma_san_pham INT UNIQUE NOT NULL,
    Cong_thuc NVARCHAR(MAX) NOT NULL,
    CONSTRAINT FK_CongThuc_SanPham FOREIGN KEY (ma_san_pham) REFERENCES SanPham(ma_san_pham)
);

CREATE TABLE CTCBNguyenLieu (
    ma_ct_cn_nguyen_lieu INT IDENTITY(1,1) PRIMARY KEY,
    ma_che_bien INT NOT NULL,
    ma_nguyen_lieu INT NOT NULL,
    so_luong_nl FLOAT,
     FOREIGN KEY (ma_che_bien) REFERENCES CheBien(ma_che_bien) ON DELETE CASCADE,
    FOREIGN KEY (ma_nguyen_lieu) REFERENCES NguyenLieu(ma_nguyen_lieu)
);
select * from CTCBNguyenLieu

CREATE TABLE CheBien (
    ma_che_bien INT IDENTITY(1,1) PRIMARY KEY,
    thoi_gian DATETIME NOT NULL,
    ma_cong_thuc INT,
    ma_san_pham INT NOT NULL,
    so_luong_sp INT,
    FOREIGN KEY (ma_cong_thuc) REFERENCES CongThuc(ma_cong_thuc),
    FOREIGN KEY (ma_san_pham) REFERENCES SanPham(ma_san_pham)
)

CREATE TABLE PhieuThu (
    ma_phieu_thu INT IDENTITY(1,1) PRIMARY KEY,
    thoi_gian DATETIME NOT NULL,
    ma_doi_tac INT NOT NULL,
	so_tien FLOAT NOT NULL,
    noi_dung NVARCHAR(MAX),

    CONSTRAINT FK_PhieuThu_DoiTac FOREIGN KEY (ma_doi_tac) REFERENCES DoiTac(ma_doi_tac)
);
CREATE TABLE PhieuChi (
    ma_phieu_chi INT IDENTITY(1,1) PRIMARY KEY,
    thoi_gian DATETIME NOT NULL,
    ma_doi_tac INT NOT NULL,
	so_tien FLOAT NOT NULL,
    noi_dung NVARCHAR(MAX),

    CONSTRAINT FK_PhieuChi_DoiTac FOREIGN KEY (ma_doi_tac) REFERENCES DoiTac(ma_doi_tac)
);



CREATE TABLE TinTuc (
    ma_tin_tuc INT PRIMARY KEY IDENTITY(1,1), 
    thoi_gian DATETIME NOT NULL,                       
    ma_loai_tin INT NOT NULL,                       
    tieu_de NVARCHAR(255) NOT NULL,                
    noi_dung NVARCHAR(MAX),                  

    CONSTRAINT FK_TinTuc_LoaiTin FOREIGN KEY (ma_loai_tin)
    REFERENCES LoaiTin(ma_loai_tin)
);
ALTER TABLE TinTuc
ADD hinh_anh NVARCHAR(255) NULL; 

ALTER TABLE TinTuc
ADD file_dinh_kem NVARCHAR(255) NULL;
ALTER TABLE TinTuc
DROP COLUMN noi_dung;

CREATE TABLE DanhGia (
    ma_danh_gia INT PRIMARY KEY,
    noi_dung NVARCHAR(MAX),
    ho_ten_khach NVARCHAR(50),
    thong_tin_lien_lac NVARCHAR(255),
    ghi_chu NVARCHAR(255)
);
ALTER TABLE DanhGia
ALTER COLUMN noi_dung NVARCHAR(MAX) NOT NULL;

ALTER TABLE DanhGia
ADD ThoiGianGui DATETIME NOT NULL DEFAULT GETDATE();
ALTER TABLE DanhGia
ADD SoSao INT NOT NULL DEFAULT 5;
ALTER TABLE DanhGia
ADD TrangThai INT NOT NULL DEFAULT 0;
GO
-- Đặt các cột bắt buộc thành NOT NULL
ALTER TABLE DanhGia ALTER COLUMN noi_dung NVARCHAR(MAX) NOT NULL;
ALTER TABLE DanhGia ALTER COLUMN ho_ten_khach NVARCHAR(50) NOT NULL;
ALTER TABLE DanhGia ALTER COLUMN thong_tin_lien_lac NVARCHAR(255) NOT NULL;
ALTER TABLE DanhGia ALTER COLUMN SoSao INT NOT NULL;
select * from DanhGia

select * from TinTuc
select * from CheBien
drop table CTCBNuyenLieu

CREATE TABLE DieuKienKhuyenMai (
    ma_chi_tiet INT IDENTITY(1,1) PRIMARY KEY,
    ma_su_kien INT NOT NULL,
    kieu_ap_dung NVARCHAR(50) NULL,
    gia_tri_don_hang FLOAT NULL,
    gia_tri_don_hang_tu FLOAT NULL,
    gia_tri_don_hang_den FLOAT NULL,
    kieu_giam NVARCHAR(50) NULL,
    gia_tri_giam FLOAT NULL,
    so_luong_yeu_cau INT NULL,

    -- Khóa ngoại đến bảng SuKien
    CONSTRAINT FK_DieuKienKhuyenMai_SuKien FOREIGN KEY (ma_su_kien)
        REFERENCES SuKien(ma_su_kien)
        ON DELETE CASCADE
);
CREATE TABLE DieuKien_SanPham (
    id INT IDENTITY(1,1) PRIMARY KEY,
    ma_chi_tiet INT NOT NULL,
    ma_san_pham INT NOT NULL,

    CONSTRAINT FK_DieuKien_SanPham_DieuKienKhuyenMai FOREIGN KEY (ma_chi_tiet)
        REFERENCES DieuKienKhuyenMai(ma_chi_tiet)
        ON DELETE CASCADE,

    CONSTRAINT FK_DieuKien_SanPham_SanPham FOREIGN KEY (ma_san_pham)
        REFERENCES SanPham(ma_san_pham)
        ON DELETE CASCADE
);
CREATE TABLE DieuKien_QuaTang (
    id INT IDENTITY(1,1) PRIMARY KEY,
    ma_chi_tiet INT NOT NULL,
    ma_san_pham INT NOT NULL,
    so_luong INT NULL ,

    CONSTRAINT FK_DieuKien_QuaTang_DieuKienKhuyenMai FOREIGN KEY (ma_chi_tiet)
        REFERENCES DieuKienKhuyenMai(ma_chi_tiet)
        ON DELETE CASCADE,

    CONSTRAINT FK_DieuKien_QuaTang_SanPham FOREIGN KEY (ma_san_pham)
        REFERENCES SanPham(ma_san_pham)
        ON DELETE CASCADE
);



DELETE FROM __EFMigrationsHistory;

use F_ROASTERY
go
