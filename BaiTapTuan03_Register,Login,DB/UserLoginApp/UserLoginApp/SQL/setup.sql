-- Tạo database nếu chưa có
IF DB_ID('CaroAppDB') IS NULL
BEGIN
    CREATE DATABASE CaroAppDB;
END
GO

USE CaroAppDB;
GO

-- Tạo bảng Users nếu chưa có
IF OBJECT_ID('dbo.Users', 'U') IS NULL
BEGIN
    CREATE TABLE Users
    (
        UserID INT IDENTITY(1,1) PRIMARY KEY,
        Username NVARCHAR(50) NOT NULL UNIQUE,
        Password NVARCHAR(255) NOT NULL,
        Email NVARCHAR(100) NULL,
        CreatedAt DATETIME DEFAULT GETDATE()
    );
END
GO

-- Chèn user mặc định nếu chưa có
IF NOT EXISTS (SELECT * FROM Users WHERE Username = N'Huy')
BEGIN
    INSERT INTO Users (Username, Password, Email)
    VALUES (N'Huy', 
            N'8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92', 
            N'1234@gmail.com');
END
GO

-- Test
SELECT * FROM Users;
GO
