using GLOBAL_MART.Data; using Microsoft.AspNetCore.Mvc; using Microsoft.EntityFrameworkCore; using System.Security.Claims;
namespace GLOBAL_MART.Controllers;
public class OrdersController(AppDbContext db):Controller {
 
 [HttpGet] public IActionResult Payment(int id){
  if(!User.Identity!.IsAuthenticated)return Redirect("/Account/Login");
  int uid=int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
  var order=db.Orders.Include(x=>x.Items).FirstOrDefault(x=>x.Id==id && x.UserId==uid);
  if(order==null)return NotFound();
  if(order.PaymentMethod!="Bank")return Redirect("/Orders");
  return View(order);
 }
 public IActionResult Index(){if(!User.Identity!.IsAuthenticated)return Redirect("/Account/Login");int uid=int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);return View(db.Orders.Include(x=>x.Items).Where(x=>x.UserId==uid).OrderByDescending(x=>x.CreatedAt).ToList());}
}
