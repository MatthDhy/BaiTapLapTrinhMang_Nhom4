# BaiTapTuan03 - (Login & Register)

Ứng dụng **Windows Forms C#** với các chức năng **Đăng nhập** và **Đăng ký**, dữ liệu lưu trong **SQL Server**, mật khẩu được mã hóa bằng **SHA-256**.

---

## 🎯 Mục tiêu
- Tạo form **Đăng nhập** và **Đăng ký**.
- Kiểm tra dữ liệu nhập vào (validate).
- Lưu thông tin user vào **SQL Server**.
- Đăng nhập thành công → chuyển đến **MainForm** hiển thị thông tin user.
- Mã hóa mật khẩu trước khi lưu vào DB.

---

## 📂 Cấu trúc thư mục
└── UserLoginApp/

├── Program.cs

├── DatabaseHelper.cs

├── Models/
│ └── User.cs

├── Utils/
│ └── Security.cs

├── Forms/
│ ├── LoginForm.cs
│ ├── RegisterForm.cs
│ └── MainForm.cs

└── README.md

---

## Phân công việc
Đinh Võ Gia Huy: Setup repo + DatabaseHelper + User.cs + SQL script

Member 1: LoginForm UI + logic gọi DB

Member 2: RegisterForm UI + logic gọi DB

Member 3: Security.cs (SHA-256) + MainForm

Member 4: Validation + Error handling
