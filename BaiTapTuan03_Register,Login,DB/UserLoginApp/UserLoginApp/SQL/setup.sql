IF DB_ID('CaroAppDB') IS NULL
BEGIN
	CREATE DATABASE CaroAppDB;
END
GO

USE CaroAppDB;
GO

IF OBJECT_ID('dbo.Users', 'U') IS NOT NULL
    DROP TABLE dbo.Users;
GO

CREATE TABLE USERS
(
	UserID INT IDENTITY(1,1) PRIMARY KEY,   
    Username NVARCHAR(50) NOT NULL UNIQUE,  
    PasswordHash NVARCHAR(255) NOT NULL,    
    Email NVARCHAR(100) NULL,               
    CreatedAt DATETIME DEFAULT GETDATE()
);
GO

INSERT INTO USERS (Username, PasswordHash, Email)
VALUES (N'Huy', N'savdsjfwoik', N'1234@gmail.com');
GO

SELECT * FROM USERS;
GO