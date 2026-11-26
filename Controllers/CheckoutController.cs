using GLOBAL_MART.Data; using GLOBAL_MART.Models; using Microsoft.AspNetCore.Mvc; using System.Text.Json; using System.Security.Claims;
namespace GLOBAL_MART.Controllers;
public class CheckoutController(AppDbContext db):Controller {
 [HttpGet] public IActionResult Index(){if(!User.Identity!.IsAuthenticated)return Redirect("/Account/Login");var cart=JsonSerializer.Deserialize<List<CartItem>>(HttpContext.Session.GetString("GM_CART")??"[]")??new();return View(cart);}
 [HttpPost] public IActionResult Place(string name,string phone,string address,string payment){if(!User.Identity!.IsAuthenticated)return Redirect("/Account/Login");var cart=JsonSerializer.Deserialize<List<CartItem>>(HttpContext.Session.GetString("GM_CART")??"[]")??new();if(cart.Count==0)return Redirect("/Cart");var order=new Order{UserId=int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!),CustomerName=name,Phone=phone,Address=address,PaymentMethod=payment,Total=cart.Sum(x=>x.Price*x.Quantity),Items=cart.Select(x=>new OrderItem{ProductId=x.ProductId,ProductName=x.Name,Quantity=x.Quantity,UnitPrice=x.Price}).ToList()};db.Orders.Add(order);db.SaveChanges();HttpContext.Session.Remove("GM_CART");
 if(payment=="Bank") return RedirectToAction("Payment","Orders",new{id=order.Id});
 return Redirect("/Orders");}
}
