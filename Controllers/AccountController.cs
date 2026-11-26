using GLOBAL_MART.Data; using GLOBAL_MART.Models; using Microsoft.AspNetCore.Mvc; using Microsoft.AspNetCore.Authentication; using Microsoft.AspNetCore.Authentication.Cookies; using System.Security.Claims;
namespace GLOBAL_MART.Controllers;
public class AccountController(AppDbContext db) : Controller {
 [HttpGet] public IActionResult Login(string? returnUrl=null){ViewBag.ReturnUrl=returnUrl;return View();}
 [HttpPost] public async Task<IActionResult> Login(string email,string password,string? returnUrl=null) {
   email=(email??"").Trim();
   if(string.IsNullOrWhiteSpace(email)||string.IsNullOrEmpty(password)){ViewBag.Error="Vui lòng nhập email và mật khẩu.";ViewBag.ReturnUrl=returnUrl;return View();}
   var u=db.Users.FirstOrDefault(x=>x.Email.ToLower()==email.ToLower() && x.Password==password);
   if(u==null){ViewBag.Error="Email hoặc mật khẩu không chính xác. Hãy kiểm tra lại.";ViewBag.ReturnUrl=returnUrl;return View();}
   var claims=new List<Claim>{new(ClaimTypes.NameIdentifier,u.Id.ToString()),new(ClaimTypes.Name,u.FullName),new(ClaimTypes.Email,u.Email),new(ClaimTypes.Role,u.Role)};
   await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,new ClaimsPrincipal(new ClaimsIdentity(claims,CookieAuthenticationDefaults.AuthenticationScheme)));
   if(!string.IsNullOrEmpty(returnUrl)&&Url.IsLocalUrl(returnUrl))return Redirect(returnUrl);
   return Redirect(u.Role=="Admin"?"/Admin":"/");
 }
 [HttpGet] public IActionResult Register()=>View();
 [HttpPost] public IActionResult Register(string fullName,string email,string password,string confirmPassword,string phone) {
   fullName=(fullName??"").Trim();email=(email??"").Trim();
   if(string.IsNullOrWhiteSpace(fullName)||string.IsNullOrWhiteSpace(email)||string.IsNullOrEmpty(password)){ViewBag.Error="Vui lòng điền đầy đủ thông tin bắt buộc.";return View();}
   if(password.Length<6){ViewBag.Error="Mật khẩu cần tối thiểu 6 ký tự.";return View();}
   if(password!=confirmPassword){ViewBag.Error="Mật khẩu nhập lại chưa khớp.";return View();}
   if(db.Users.Any(x=>x.Email.ToLower()==email.ToLower())){ViewBag.Error="Email này đã được đăng ký. Hãy đăng nhập hoặc dùng email khác.";return View();}
   db.Users.Add(new User{FullName=fullName,Email=email,Password=password,Phone=(phone??"").Trim(),Role="Customer"});db.SaveChanges();
   TempData["Success"]="Đăng ký thành công! Hãy đăng nhập bằng tài khoản vừa tạo.";
   return RedirectToAction("Login");
 }
 public async Task<IActionResult> Logout(){await HttpContext.SignOutAsync();return RedirectToAction("Index","Home");}
}
