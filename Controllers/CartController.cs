using GLOBAL_MART.Data; using GLOBAL_MART.Models; using Microsoft.AspNetCore.Mvc; using System.Text.Json;
namespace GLOBAL_MART.Controllers;
public class CartController(AppDbContext db) : Controller {
 const string Key="GM_CART";
 List<CartItem> Get(){return JsonSerializer.Deserialize<List<CartItem>>(HttpContext.Session.GetString(Key)??"[]")??new();}
 void Save(List<CartItem> c)=>HttpContext.Session.SetString(Key,JsonSerializer.Serialize(c));
 public IActionResult Index()=>View(Get());
 public IActionResult Add(int id){var p=db.Products.Find(id);if(p==null)return NotFound();var c=Get();var item=c.FirstOrDefault(x=>x.ProductId==id);if(item==null)c.Add(new CartItem{ProductId=id,Name=p.Name,Price=p.Price,ImageUrl=p.ImageUrl});else item.Quantity++;Save(c);return RedirectToAction("Index");}
 [HttpPost] public IActionResult Update(int id,int quantity){var c=Get();var i=c.FirstOrDefault(x=>x.ProductId==id);if(i!=null){if(quantity<=0)c.Remove(i);else i.Quantity=quantity;}Save(c);return RedirectToAction("Index");}
 public IActionResult Remove(int id){var c=Get();c.RemoveAll(x=>x.ProductId==id);Save(c);return RedirectToAction("Index");}
}
