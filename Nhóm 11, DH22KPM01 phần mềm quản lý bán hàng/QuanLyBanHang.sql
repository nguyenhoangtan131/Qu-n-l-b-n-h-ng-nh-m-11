CREATE DATABASE QuanLyBanHang
GO
USE QuanLyBanHang;

-- HangHoa
CREATE TABLE HangHoa (
    MaHang INT IDENTITY PRIMARY KEY, -- MH001
    TenHang NVARCHAR(100) NOT NULL,
    DonGia DECIMAL(18, 2) NOT NULL CHECK (DonGia >=0),
);

-- NhanVien
CREATE TABLE NhanVien (
    MaNV INT IDENTITY PRIMARY KEY, -- NV001, AD001
    TenNV NVARCHAR(50) NOT NULL,																																															
    SoDienThoai NVARCHAR(15) NOT NULL,
    LuongCoBan DECIMAL(18, 2) NOT NULL CHECK (LuongCoBan >=0),
);
-- TaiKhoan
CREATE TABLE TaiKhoan (
	id INT IDENTITY PRIMARY KEY,
	TenHienThi NVARCHAR(50),
    TenDangNhap NVARCHAR(50),
    MatKhau NVARCHAR(1000) NOT NULL,
    QuyenHan INT NOT NULL DEFAULT 0, -- 0 STAFF, 1ADMIN 
);
Select TenDangNhap, TenHienThi, QuyenHan from TaiKhoan

-- HoaDon

CREATE TABLE HoaDon (
    MaHD INT IDENTITY PRIMARY KEY, -- HD001
    NgayLap DATE NOT NULL DEFAULT GETDATE(),
);

-- ChiTietHoaDon

CREATE TABLE ChiTietHoaDon (
    MaChiTietHD INT IDENTITY PRIMARY KEY,
    idProduct INT NOT NULL,
	idBill INT NOT NULL,
    SoLuong INT NOT NULL DEFAULT 0,
);

--insert nhanvien

INSERT INTO NhanVien (TenNV, SoDienThoai, LuongCoBan)  
VALUES  
(N'Nguyễn Văn Hồ', '0123456789', 4000000),  
(N'Nguyễn Ngọc Quân', '0123456739', 5000000),  
(N'Cao Thị Kim Chi', '0123456781', 6000000),  
(N'Lê Anh Tuấn', '0123456785', 9000000),  
(N'Nguyễn Thị Mai', '0123456786', 5200000),  
(N'Nguyễn Văn An', '0123456787', 5400000),  
(N'Lê Quang Huy', '0123456788', 5050000),  
(N'Phan Thiên Vương', '0123456789', 5030000);  
go


CREATE PROC USP_GetEmployeeList
AS SELECT * FROM NhanVien
GO	

EXEC dbo.USP_GetEmployeeList
GO




-- insert account
-- Chèn tài khoản ADMIN với quyền 1
INSERT INTO TaiKhoan (TenHienThi, TenDangNhap, QuyenHan, MatKhau)  
VALUES 
('Admin', 'Admin', 1, '123'); -- 1: ADMIN

-- Chèn dữ liệu cho các tài khoản nhân viên (mặc định QuyenHan là 0)
INSERT INTO TaiKhoan (TenHienThi, TenDangNhap, MatKhau)  
VALUES 
('Nhân viên 002', 'nhanvien002', '123'),
('Nhân viên 334', 'nhanvien334', '123'),
('Nhân viên 065', 'nhanvien065', '123'),
('Nhân viên 005', 'nhanvien005', '123'),
('Nhân viên 006', 'nhanvien006', '123'),
('Nhân viên 142', 'nhanvien142', '123'),
('Nhân viên 122', 'nhanvien122', '123')

--store proc usp_gettaccountbyusername
Create PROC USP_GetAccountByUserName
@TenDangNhap NVARCHAR(50)
AS
BEGIN
	SELECT * FROM TaiKhoan WHERE TenDangNhap = @TenDangNhap
END
GO

	
EXEC USP_GetAccountByUserName @TenDangNhap = N'nhanvien142'
GO

--store proc usp_login
Create PROC USP_Login
@TenDangNhap NVARCHAR(50), @MatKhau NVARCHAR(1000)
AS
BEGIN
	SELECT * FROM TaiKhoan WHERE TenDangNhap = @TenDangNhap AND MatKhau = @MatKhau
END
GO

INSERT INTO HangHoa (TenHang, DonGia) 
VALUES 
(N'Áo Thun', 120000), 
(N'Áo Khoác', 350000), 
(N'Giày Thể Thao', 500000), 
(N'Giày Lười', 450000), 
(N'Tai Nghe Bluetooth', 200000), 
(N'Ba Lô', 300000), 
(N'Sạc Dự Phòng', 250000), 
(N'Điện Thoại', 10000000), 
(N'Máy Tính Bảng', 8000000), 
(N'Sách Giáo Khoa', 50000), 
(N'Tủ lạnh Toshiba 200L', 5000000), 
(N'Máy giặt LG 8kg', 7200000), 
(N'Tivi Samsung 50 Inch', 9500000)
SELECT * FROM HangHoa 
GO

SELECT TenHang, DonGia FROM Hanghoa WHERE MaHang = '2';

--store proc hang hoa
CREATE PROC USP_GetProductList
AS SELECT * FROM HangHoa
GO	

EXEC dbo.USP_GetProductList
GO

--THÊM HÓA ĐƠN
INSERT INTO HoaDon (NgayLap)
VALUES
( GETDATE()),
( GETDATE()),
( GETDATE());
GO
INSERT INTO ChiTietHoaDon(idProduct, idBill, SoLuong) 
VALUES
(1,'1' , 1),
(3,'2' , 2),
(2, '3' , 3)
GO

SELECT * FROM HoaDon WHERE MaHD = '3'

SELECT * FROM ChiTietHoaDon WHERE MaChiTietHD = '3'; 
SELECT * FROM ChiTietHoaDon
SELECT * FROM HangHoa
SELECT * FROM HoaDon

CREATE FUNCTION [dbo].[fuConvertToUnsign1] ( @strInput NVARCHAR(4000) ) RETURNS NVARCHAR(4000)
AS
BEGIN
	IF @strInput IS NULL RETURN @strInput
	IF @strInput = '' RETURN @strInput

	DECLARE @RT NVARCHAR(4000)
	DECLARE @SIGN_CHARS NCHAR(136)
	DECLARE @UNSIGN_CHARS NCHAR (136)

	SET @SIGN_CHARS = N'ăâđêôơưàảãạáằẳẵặắầẩẫậấèẻẽẹéềểễệế ìỉĩịíòỏõọóồổỗộốờởỡợớùủũụúừửữựứỳỷỹỵý ĂÂĐÊÔƠƯÀẢÃẠÁẰẲẴẶẮẦẨẪẬẤÈẺẼẸÉỀỂỄỆẾÌỈĨỊÍ ÒỎÕỌÓỒỔỖỘỐỜỞỠỢỚÙỦŨỤÚỪỬỮỰỨỲỶỸỴÝ' + NCHAR(272)+ NCHAR(208)
	SET @UNSIGN_CHARS = N'aadeoouaaaaaaaaaaaaaaaeeeeeeeeee iiiiiooooooooooooooouuuuuuuuuuyyyyy AADEOOUAAAAAAAAAAAAAAAEEEEEEEEEEIIIII OOOOOOOOOOOOOOOUUUUUUUUUUYYYYYDD'
	
	DECLARE @COUNTER int
	DECLARE @COUNTER1 int
	SET @COUNTER = 1

	WHILE (@COUNTER <= LEN(@strInput))
	BEGIN
		SET @COUNTER1 = 1
		WHILE (@COUNTER1 <= LEN(@SIGN_CHARS) + 1)
			BEGIN
				IF UNICODE(SUBSTRING(@SIGN_CHARS, @COUNTER1,1)) = UNICODE(SUBSTRING(@strInput,@COUNTER ,1) )
				BEGIN
					IF @COUNTER=1
						SET @strInput = SUBSTRING(@UNSIGN_CHARS, @COUNTER1,1) + SUBSTRING(@strInput, @COUNTER+1,LEN(@strInput)-1)
					ELSE
						SET @strInput = SUBSTRING(@strInput, 1, @COUNTER-1) +SUBSTRING(@UNSIGN_CHARS, @COUNTER1,1) + SUBSTRING(@strInput, @COUNTER+1,LEN(@strInput)- @COUNTER)
					BREAK
				END
					SET @COUNTER1 = @COUNTER1 +1
			END
			SET @COUNTER = @COUNTER +1
	END
	SET @strInput = replace(@strInput,' ','-')
	RETURN @strInput
END

