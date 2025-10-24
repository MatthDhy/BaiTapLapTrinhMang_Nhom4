-- Tạo database nếu chưa có
IF DB_ID('UserManageApp') IS NULL
BEGIN
    CREATE DATABASE UserManageApp;
END
GO

USE UserManageApp;
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
        FullName NVARCHAR(100) NULL,
        Birthday DATE NULL,
    );
END
GO

-- Chèn user mặc định nếu chưa có
IF NOT EXISTS (SELECT * FROM Users WHERE Username = N'Huy')
BEGIN
    INSERT INTO Users (Username, Password, Email, FullName, Birthday)
    VALUES (N'Huy', 
            N'8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92', 
            N'1234@gmail.com', 'Đinh Võ Gia Huy' ,'29/11/2006');
END
GO

-- Test
SELECT * FROM Users;
GO
