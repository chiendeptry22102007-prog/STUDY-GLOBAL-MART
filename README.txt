STUDY MARK - UI UPGRADE
ASP.NET Core MVC / C# / SQL Server

Admin:
Email admin@globalmart.vn
Password Admin@123

Run:
1) Extract ZIP.
2) Open GLOBAL_MART.csproj in Visual Studio 2022.
3) Ensure workload ASP.NET and web development installed.
4) Update appsettings.json connection string to your SQL Server instance.
5) Restore NuGet packages.
6) Build: Ctrl+Shift+B.
7) Run: F5.

UI refresh: dark glassmorphism, animated ambient background, responsive navigation, modern product cards, responsive login/register forms.
Registration/login improvements: email validation, duplicate-email check case-insensitive, password min 6, confirm password, clear error messages, success message after registration.

Feature status:
- Customer homepage/search/category/product details/cart/checkout/account/orders included.
- Admin dashboard/product/category/order/customer/revenue included.
- Promotions and product reviews are not fully implemented as interactive CRUD modules yet.
- Demo passwords are stored in plaintext; do not deploy to production without password hashing and security hardening.
