using GLOBAL_MART.Data; using Microsoft.AspNetCore.Mvc; using Microsoft.EntityFrameworkCore;
namespace GLOBAL_MART.Controllers;
public class HomeController(AppDbContext db) : Controller {
 public async Task<IActionResult> Index(string? q, int? categoryId) {
   var products=db.Products.Include(p=>p.Category).AsQueryable();
   if(!string.IsNullOrWhiteSpace(q)) products=products.Where(p=>p.Name.Contains(q));
   if(categoryId.HasValue) products=products.Where(p=>p.CategoryId==categoryId);
   ViewBag.Categories=await db.Categories.ToListAsync(); ViewBag.Query=q; ViewBag.CategoryId=categoryId;
   return View(await products.OrderByDescending(p=>p.IsFeatured).ToListAsync());
 }
 public async Task<IActionResult> Details(int id) { var p=await db.Products.Include(x=>x.Category).FirstOrDefaultAsync(x=>x.Id==id); if(p==null)return NotFound(); return View(p); }
}
