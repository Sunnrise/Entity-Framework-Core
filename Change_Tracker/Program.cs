
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Emit;
ECommerceDbContext context = new();

#region What is Change Tracking

#endregion

#region Property of ChanceTracker

#endregion


public class ECommerceDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Component> Components { get; set; }
    public DbSet<ProductComponent> ProductComponents { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //Provider, ConnectionString, LazyLoading
        optionsBuilder.UseSqlServer("Server=localhost, 1433;Database=ECommerceDb;User Id=sa;Password=Password1;TrustServerCertificate=True");
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProductComponent>().HasKey(pc => new { pc.ProductId, pc.ComponentId });
    }
}
//Entity
public class Product
{
    //EF Core will consider Id or ProductId as primary key, if you use any other name, you need to use [Key]
    //attribute, to specify it as primary key, Property name can be Id,ID,ProductId or
    //ProductID so that EF Core can consider it as primary key
    public int Id { get; set; }
    public string ProductName { get; set; }
    public float Price { get; set; }
    public ICollection<Component> Components { get; set; }
}
// Entity
public class Component
{
    public int Id { get; set; }
    public string ComponentName { get; set; }
}
public class ProductComponent
{
    public int ProductId { get; set; }
    public Product Product { get; set; }
    public int ComponentId { get; set; }
    public Component Component { get; set; }
}
