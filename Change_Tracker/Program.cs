﻿
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Emit;
ECommerceDbContext context = new();

#region What is Change Tracking 
//the data and obejcts which are being tracked by the context, are called change tracking. Change Tracker tracks the data and objects and keeps the record of the changes made to the objects. and then Sql Queries are generated based on the changes made to the objects and data.
#endregion

#region Property of Change Tracker
//We can access the Change Tracker using the ChangeTracker property of the DbContext class.  Change Tracker has the following properties and methods.
Product product = new Product { ProductName = "Laptop", Price = 1000 };
context.Products.Add(product);
context.SaveChanges();


var products =await context.Products.ToListAsync();

products[0].Price = 2000;
context.Products.Remove(products[7]); // delete the product at index 7
products[3].ProductName = "Desktop";// update the product at index 3

var datas =context.ChangeTracker.Entries(); //Returns all the entities being tracked by the context
Console.WriteLine();

#region DetectChanges Method

#endregion

#region AutoDetectChangesEnabled Property

#endregion

#region Entries Method

#endregion

#region AcceptAllChanges Method

#endregion

#region HasChanges Method

#endregion

#endregion

#region Entity States

#region Detached



#endregion

#region Added



#endregion

#region Unchanged



#endregion

#region Modified



#endregion

#region Deleted



#endregion

#endregion

#region Using Change Tracker for Interceptor

#endregion

#region Context object with Change Tracker 

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
