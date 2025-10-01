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

- Username (nvarchar)

- Password (nvarchar, đã mã hóa)

- Email (nvarchar)

Giao diện thân thiện, dễ sử dụng, xử lý exception hợp lý, hiển thị thông báo lỗi cho người dùng.

## Hướng dẫn cài đặt
1. Clone repo:  
   ```bash
   git clone https://github.com/MatthDhy/BaiTapLapTrinhMang_Nhom4.git
   cd BaiTapLapTrinhMang_Nhom4/BaiTapTuan03_Register,Login,DB/UserLoginApp


2. Mở project bằng Visual Studio.

Khởi tạo database trong SQL Server bằng cách chạy script:

```bash
 Mở file SQL/setup.sql và chạy trong SQL Server Management Studio

```
3. Cấu hình connection string

Trong file App.config của project, kiểm tra và chỉnh sửa connectionString cho phù hợp với SQL Server trên máy bạn. Ví dụ:

<connectionStrings>
  <add name="CaroAppDB" 
       connectionString="Server=.;Database=CaroAppDB;Trusted_Connection=True;" 
       providerName="System.Data.SqlClient" />
</connectionStrings>


Server=. → kết nối SQL Server local mặc định.

- Nếu bạn dùng SQL Express thì đổi thành:
Server=.\SQLEXPRESS

- Nếu dùng login bằng tài khoản SQL (sa, password) thì đổi thành:
Server=localhost;Database=CaroAppDB;User Id=sa;Password=your_password;

4. Build và chạy project trong Visual Studio.

----
***📖 Hướng dẫn sử dụng ứng dụng
# CaroApp - Đăng ký, đăng nhập

Repo này sử dụng **SQL Server** để lưu dữ liệu người dùng.  
Để chạy được project, bạn cần tạo database và bảng theo hướng dẫn phía trên.


1. Đăng ký tài khoản

Chọn Đăng ký trên giao diện chính.

Nhập thông tin theo yêu cầu:

Username: tên đăng nhập (không được trùng với người dùng đã tồn tại).

Password & Confirm Password: phải trùng khớp.

Email: đúng định dạng email.

Sau khi hợp lệ → thông tin được mã hóa mật khẩu bằng SHA-256 rồi lưu vào DB.

Nếu có lỗi (username/email đã tồn tại, mật khẩu không khớp, email sai định dạng) → hiển thị thông báo cho người dùng.

2. Đăng nhập

Nhập Username và Password đã đăng ký.

Ứng dụng sẽ kiểm tra thông tin trong CaroAppDB.Users:

Nếu đúng → hiển thị thông báo Đăng nhập thành công, chuyển sang màn hình chính.

Nếu sai → hiển thị lỗi Sai tài khoản hoặc mật khẩu.

3. Màn hình chính

Sau khi đăng nhập thành công, người dùng được đưa đến Form chính của ứng dụng.

Đây là nơi có thể mở rộng thêm các tính năng khác (ví dụ: chơi Caro, quản lý tài khoản...).

4. Xử lý lỗi & thông báo

Ứng dụng luôn hiển thị thông báo rõ ràng khi xảy ra lỗi:

Nhập thiếu thông tin.

Mật khẩu không khớp.

Email sai định dạng.

Username hoặc Email đã tồn tại.

Mọi lỗi kết nối Database hoặc lỗi hệ thống đều được xử lý exception để không làm crash ứng dụng.


***Giao diện ứng dụng

Màn hình Đăng nhập
<img width="816" height="483" alt="image" src="https://github.com/user-attachments/assets/25091d38-271e-4f51-b759-feab681c3f0e" />

Màn hình Đăng ký
<img width="512" height="394" alt="image" src="https://github.com/user-attachments/assets/b74e2b0a-4186-469c-a59f-2e902ed7c3d3" />

Màn hình chính
<img width="811" height="495" alt="image" src="https://github.com/user-attachments/assets/43ad1197-7205-441a-a300-13c714c9ca17" />
