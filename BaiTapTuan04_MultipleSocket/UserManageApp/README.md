# Tên project: UserManageApp

## 🎯 Mục tiêu bài tập

Xây dựng ứng dụng đăng ký – đăng nhập người dùng theo mô hình Client – Server, sử dụng socket TCP để giao tiếp, dữ liệu định dạng JSON, và SQL Server để lưu trữ thông tin người dùng.
Ứng dụng được phát triển bằng C# WinForms, chia tách rõ ràng giữa Client App và Server App để dễ bảo trì, demo và mở rộng.

Yêu cầu chính:

- Giao tiếp giữa Client và Server thông qua TCP socket.

- Dữ liệu gửi nhận được tuần tự hóa (serialize) sang JSON.

- Mật khẩu người dùng được mã hóa SHA-256 trước khi lưu.

Server xử lý các yêu cầu cơ bản:

 + REGISTER – Đăng ký tài khoản mới

 + LOGIN – Đăng nhập

 + GETINFO – Lấy thông tin người dùng

 + LOGOUT – Đăng xuất

Server hiển thị log kết nối trực quan bằng WinForms UI (FormServer).
Client có 3 form: LoginForm, RegisterForm, MainForm.

## 📂 Cấu trúc thư mục

UserManageApp/

├─ Client/       (WinForms – giữ phần code cũ của UserLoginApp)

│  ├─ Forms/		→ thêm một số đoạn code giao tiếp với server từ TcpClientHelper

│  │   ├─ LoginForm.cs

│  │   ├─ RegisterForm.cs

│  │   └─ MainForm.cs

│  ├─ Networking/

│  │   └─ TcpClientHelper.cs 

│  ├─ Utils/

│  │   └─ Security.cs → tái sử dụng được, sửa tí cho hợp với database mới

│  └─ Program.cs

│
└─ Server/       (WinForms – hiển thị log, xử lý TCP và DB)

   ├─ Forms/			→ Phần code mới cần giải quyết
   
   │   └─ FormServer.cs
   
   ├─ Core/
   
   │   ├─ ServerCore.cs
   
   │   └─ DbHelper.cs
   
   ├─ Models/
   
   │   ├─ User.cs
   
   │   ├─ RequestMessage.cs
   
   │   └─ ResponseMessage.cs
   
   └─ Program.cs
   
__________

## Phân công việc

Đinh Võ Gia Huy: Setup repo + database, Core, Models và hoàn thiện app

Cao Phan Đức Huy: LoginForm UI + viết logic và gọi hàm giao tiếp với server từ file TcpClientHelper 

Hoàng Gia Huy: RegisterForm UI + viết logic và gọi hàm giao tiếp với server từ file TcpClientHelper

Nguyễn Hoàng Phú: Security.cs (SHA-256) + MainForm + viết logic và gọi hàm giao tiếp với server từ file TcpClientHelper

Trần Lê Trung Hiếu: UI của FormServer + FormServer.cs, TcpClientHelper.cs 

___________

## Hướng dẫn cài đặt

1. Clone repo:  
   ```bash
   git clone https://github.com/MatthDhy/BaiTapLapTrinhMang_Nhom4.git
   cd BaiTapLapTrinhMang_Nhom4/BaiTapTuan03_Register,Login,DB/UserLoginApp
   ```
2. Mở project bằng Visual Studio.

Khởi tạo database trong SQL Server bằng cách chạy script:

```bash
 Mở file SQL/CreateUserTable.sql và chạy trong SQL Server Management Studio
```

3. Cấu hình connection string

Trong file App.config của project, kiểm tra và chỉnh sửa connectionString cho phù hợp với SQL Server trên máy bạn. Ví dụ:

<connectionStrings>
  <add name="UserManageApp" 
       connectionString="Server=.;Database=UserManageApp;Trusted_Connection=True;" 
       providerName="System.Data.SqlClient" />
</connectionStrings>

Server=. → kết nối SQL Server local mặc định.

- Nếu bạn dùng SQL Express thì đổi thành:
Server=.\SQLEXPRESS

- Nếu dùng login bằng tài khoản SQL (sa, password) thì đổi thành:
Server=localhost;Database=UserManageApp;User Id=sa;Password=your_password;

____________

 **Luồng hoạt động **
 
+-------------+         TCP JSON          +-------------+         SQL Query         +-------------------+

|  Client App | ────────────────────────▶ | Server App  | ───────────────────────▶ |   SQL Server DB   |

| (WinForms)  | ◀──────────────────────── | (WinForms)  | ◀─────────────────────── |  (Users table)    |

+-------------+                          +-------------+                           +-------------------+

