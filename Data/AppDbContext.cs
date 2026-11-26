using GLOBAL_MART.Models;
using Microsoft.EntityFrameworkCore;
namespace GLOBAL_MART.Data;
public class AppDbContext : DbContext {
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}
    public DbSet<User> Users => Set<User>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderItem> OrderItems => Set<OrderItem>();
    public DbSet<Review> Reviews => Set<Review>();
    protected override void OnModelCreating(ModelBuilder b) {
        b.Entity<User>().HasIndex(x => x.Email).IsUnique();
        b.Entity<Product>().Property(x => x.Price).HasPrecision(18,2);
        b.Entity<Order>().Property(x => x.Total).HasPrecision(18,2);
        b.Entity<OrderItem>().Property(x => x.UnitPrice).HasPrecision(18,2);
        b.Entity<Category>().HasMany(x=>x.Products).WithOne(x=>x.Category).HasForeignKey(x=>x.CategoryId);
        b.Entity<Order>().HasMany(x=>x.Items).WithOne(x=>x.Order).HasForeignKey(x=>x.OrderId);
    }
}
