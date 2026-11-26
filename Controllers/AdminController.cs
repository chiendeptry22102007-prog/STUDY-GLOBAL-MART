using GLOBAL_MART.Data; using GLOBAL_MART.Models; using Microsoft.AspNetCore.Mvc; using Microsoft.EntityFrameworkCore;
namespace GLOBAL_MART.Controllers;
public class AdminController(AppDbContext db):Controller {
 bool IsAdmin()=>User.IsInRole("Admin");
 public IActionResult Index(){if(!IsAdmin())return Redirect("/Account/Login");ViewBag.Products=db.Products.Count();ViewBag.Users=db.Users.Count(x=>x.Role=="Customer");ViewBag.Orders=db.Orders.Count();ViewBag.Revenue=db.Orders.Where(x=>x.Status=="Đã giao").Sum(x=>(decimal?)x.Total)??0;return View();}
 public IActionResult Products(){if(!IsAdmin())return Redirect("/Account/Login");ViewBag.Categories=db.Categories.ToList();return View(db.Products.Include(x=>x.Category).ToList());}
 [HttpPost] public IActionResult SaveProduct(int id,string name,decimal price,int stock,int categoryId,string description,string imageUrl,bool featured){if(!IsAdmin())return Unauthorized();Product p=id==0?new Product():db.Products.Find(id)!;p.Name=name;p.Price=price;p.Stock=stock;p.CategoryId=categoryId;p.Description=description;p.ImageUrl=imageUrl;p.IsFeatured=featured;if(id==0)db.Products.Add(p);db.SaveChanges();return RedirectToAction("Products");}
 public IActionResult DeleteProduct(int id){if(!IsAdmin())return Unauthorized();var p=db.Products.Find(id);if(p!=null){db.Products.Remove(p);db.SaveChanges();}return RedirectToAction("Products");}
 public IActionResult Categories(){if(!IsAdmin())return Redirect("/Account/Login");return View(db.Categories.ToList());}
 [HttpPost] public IActionResult AddCategory(string name,string description){if(!IsAdmin())return Unauthorized();db.Categories.Add(new Category{Name=name,Description=description});db.SaveChanges();return RedirectToAction("Categories");}
 public IActionResult Orders(){if(!IsAdmin())return Redirect("/Account/Login");return View(db.Orders.Include(x=>x.Items).OrderByDescending(x=>x.CreatedAt).ToList());}
 [HttpPost] public IActionResult UpdateOrder(int id,string status){if(!IsAdmin())return Unauthorized();var o=db.Orders.Find(id);if(o!=null){o.Status=status;db.SaveChanges();}return RedirectToAction("Orders");}
 public IActionResult Customers(){if(!IsAdmin())return Redirect("/Account/Login");return View(db.Users.Where(x=>x.Role=="Customer").ToList());}
 public IActionResult Promotions(){if(!IsAdmin())return Redirect("/Account/Login");return View();}
 public IActionResult Revenue(){if(!IsAdmin())return Redirect("/Account/Login");ViewBag.Revenue=db.Orders.Where(x=>x.Status=="Đã giao").Sum(x=>(decimal?)x.Total)??0;return View(db.Orders.ToList());}
}
