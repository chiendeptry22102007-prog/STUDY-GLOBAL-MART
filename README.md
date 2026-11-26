<img width="959" height="540" alt="{0AA8B723-1CAC-4DEF-A1C8-9554E9E6CABF}" src="https://github.com/user-attachments/assets/77a98d6f-4406-4719-b1f8-ca77e22a7478" />"# Du an STUDY MARK - ASP.NET Core MVC" 
# 🛒 STUDY MARK (GLOBAL MART) - Hệ Thống Thương Mại Điện Tử

Dự án xây dựng website bán hàng trực tuyến **STUDY MARK** áp dụng quy trình phát triển phần mềm linh hoạt (Agile/Scrum) chia thành **5 Sprint**.

---

## 🛠 Công Nghệ Sử Dụng
* **Backend:** ASP.NET Core MVC (.NET 10.0), C#
* **Database & ORM:** Microsoft SQL Server (`SQLEXPRESS`), Entity Framework Core
* **Frontend:** HTML5, CSS3, Bootstrap, JavaScript, Razor Views
* **Quản lý mã nguồn:** Git & GitHub (Workflow theo từng Sprint & Pull Request)

---

## 📅 Lộ Trình Phát Triển (5 Sprints)

| Giai đoạn | Nhánh (Branch) | Nội dung thực hiện | Pull Request |
| :--- | :---: | :--- | :---: |
| **Sprint 1** | `sprint-1` | Thiết kế Database, cấu hình EF Core kết nối SQL Server và xây dựng giao diện Trang chủ (`HomeController`). | `#1` (Merged) |
| **Sprint 2** | `sprint-2` | Xây dựng chức năng Đăng ký, Đăng nhập và phân quyền người dùng/Admin (`AccountController`). | `#2` (Merged) |
| **Sprint 3** | `sprint-3` | Xây dựng tính năng Giỏ hàng: thêm, cập nhật số lượng và xóa sản phẩm (`CartController`). | `#3` (Merged) |
| **Sprint 4** | `sprint-4` | Xây dựng quy trình Đặt hàng, Thanh toán và quản lý đơn hàng (`CheckoutController`, `OrdersController`). | `#4` (Merged) |
| **Sprint 5** | `sprint-5` | Hoàn thiện trang quản trị Admin (CRUD sản phẩm), tích hợp trợ lý AI (`AdminController`, `AIController`) và tối ưu giao diện. | `#5` (Merged) |

---

## ⚙️ Hướng Dẫn Cài Đặt & Chạy Dự Án
1. **Khôi phục cơ sở dữ liệu:** Mở SQL Server Management Studio (SSMS), kết nối tới server `.\SQLEXPRESS` (bật *Trust Server Certificate*) và chạy script/restore database của dự án.
2. **Cấu hình chuỗi kết nối:** Kiểm tra `ConnectionStrings` trong file `appsettings.json` khớp với tên Server trên máy.
3. **Chạy ứng dụng:** Mở file `GLOBAL_MART.csproj` bằng **Visual Studio**, nhấn **`F5`** (hoặc chạy lệnh `dotnet run`) để khởi động website.
