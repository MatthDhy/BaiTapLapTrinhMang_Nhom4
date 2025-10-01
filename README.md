# BaiTapLapTrinhMang_Nhom4_NT106.Q14_UIT
_Repo này dùng để lưu bài tập trên lớp theo từng tuần nhe._
**Tên project: Caro Vui Vẻ**

***Danh sách thành viên:**
1.	24520656	Đinh Võ Gia Huy
2.	24520514	Trần Lê Văn Hiếu
3.	24520661	Hoàng Gia Huy
4.	24520653	Cao Phan Đức Huy
5.	24521358	Nguyễn Hoàng Phú

______________

## Mô tả bài tập
Ứng dụng Quản lý người dùng với các tính năng:

- Đăng ký và Đăng nhập bằng giao diện Windows Forms (C#).

- Lưu trữ thông tin người dùng trong cơ sở dữ liệu SQL Server.

- Hỗ trợ kiểm tra logic và tính hợp lệ dữ liệu khi đăng ký:

- Mật khẩu và xác nhận mật khẩu phải trùng khớp.

- Email đúng định dạng.

- Username chưa tồn tại trong DB.

- Mã hóa mật khẩu bằng SHA-256 trước khi lưu vào DB.

- Đăng nhập kiểm tra thông tin với DB, hiển thị lỗi nếu thất bại, chuyển sang form chính nếu thành công.

Cơ sở dữ liệu SQL Server: bảng Users gồm:

UserId (int, PK)

Username (nvarchar)

Password (nvarchar, đã mã hóa)

Email (nvarchar)

Giao diện thân thiện, dễ sử dụng, xử lý exception hợp lý, hiển thị thông báo lỗi cho người dùng.

## Hướng dẫn cài đặt
1. Clone repo:  
   ```bash
   git clone https://github.com/MatthDhy/BaiTapLapTrinhMang_Nhom4.git
   cd BaiTapLapTrinhMang_Nhom4/BaiTapTuan03_Register,Login,DB/UserLoginApp
```

2. Mở project bằng Visual Studio.

Khởi tạo database trong SQL Server bằng cách chạy script:

```bash
 Mở file SQL/setup.sql và chạy trong SQL Server Management Studio


3. Kiểm tra và chỉnh lại connection string trong project nếu cần.

4. Build và chạy project trong Visual Studio.

***Hướng dẫn sử dụng

Chọn Đăng ký để tạo tài khoản mới.

Nhập Username, Password, Confirm Password, Email → lưu vào DB nếu hợp lệ.

Chọn Đăng nhập để vào hệ thống.

Đúng thông tin → chuyển sang form chính.

Sai thông tin → hiển thị thông báo lỗi.


***Giao diện ứng dụng

Màn hình Đăng nhập


Màn hình Đăng ký


Màn hình chính