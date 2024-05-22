
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
//Product product = new Product { ProductName = "Laptop", Price = 1000 };
//context.Products.Add(product);
//context.SaveChanges();


//var products =await context.Products.ToListAsync();

//products[0].Price = 2000;
//context.Products.Remove(products[7]); // delete the product at index 7
//products[3].ProductName = "Desktop";// update the product at index 3

//var datas =context.ChangeTracker.Entries(); //Returns all the entities being tracked by the context
//Console.WriteLine();

#region DetectChanges Method
//EF Core automatically detects the changes made to the objects being tracked by the context. 
//However, if you want to manually detect the changes, you can use the DetectChanges method of the Change Tracker. So we can sure that the changes are being tracked by the context.

//var product =await context.Products.FirstOrDefaultAsync(u=>u.Id==3);
//product.Price = 987;
//context.ChangeTracker.DetectChanges();
//await context.SaveChangesAsync();
#endregion

#region AutoDetectChangesEnabled Property
// that methods(SaveChanges,Entries) automatically detect the changes made to the objects being tracked by the context. 
//However, if you want to disable the automatic detection of changes, you can set the AutoDetectChangesEnabled property of the Change Tracker to false.
//We can configure that detect changes automatically or manually by setting the AutoDetectChangesEnabled property of the Change Tracker.
//It will be useful when we are working with a large number of entities and we want to improve the performance of the application.
#endregion

#region Entries Method
//The version of collection, Entry method which belongs to Context class.
//Entity instances are tracked by the context and the state of the entity is stored in the EntityEntry object.
//Entries method trigger the DetectChanges method to detect the changes made to the objects being tracked by the context. It causes some performance overhead.
//So, if you want to manually detect the changes, you can use the DetectChanges method of the Change Tracker.

//var products =await context.Products.ToListAsync();
//products.FirstOrDefault(u=>u.Id==7).Price = 2001;
//context.Products.Remove(products.FirstOrDefault(u => u.Id == 8)); // delete the product at index 7
//products.FirstOrDefault(u => u.Id == 9).ProductName = "Sunnrise";// update the product at index 3

//context.ChangeTracker.Entries().ToList().ForEach(e=>
//{
//    if (e.State==EntityState.Unchanged)
//    {

//    }
//    else if (e.State == EntityState.Added)
//    {

//    }
//    else if (e.State == EntityState.Modified)
//    {

//    }
//    else if (e.State == EntityState.Deleted)
//    {

//    }
//    else if (e.State == EntityState.Detached)
//    {

//    }

//}); //Returns all the entities being tracked by the context

#endregion

#region AcceptAllChanges Method
//SaveChanges or SaveChanges(true) method will accept all the changes made to the objects being tracked by the context. Then Ef core will not track the changes made to the objects. So if there is any error while saving the changes, the changes will not be rolled back.

//If you want to accept all the changes made to the objects being tracked by the context, you can use the AcceptAllChanges method of the Change Tracker.

//If you are sure about process with SaveChanges(false) method and you want to accept all the changes made to the objects being tracked by the context, you can use the AcceptAllChanges method of the Change Tracker.

//var products = await context.Products.ToListAsync();
//products.FirstOrDefault(u => u.Id == 7).Price = 2001;
//context.Products.Remove(products.FirstOrDefault(u => u.Id == 8)); // delete the product at index 7
//products.FirstOrDefault(u => u.Id == 9).ProductName = "Sunnrise";// update the product at index 3

////await context.SaveChangesAsync();
////await context.SaveChangesAsync(true);
////they are same
//await context.SaveChangesAsync(false);
//context.ChangeTracker.AcceptAllChanges();

#endregion

#region HasChanges Method
////If you want to check whether there are any changes made to the objects being tracked by the context, you can use the HasChanges method of the Change Tracker.
////This method have to use detectChanges method to detect the changes made to the objects being tracked by the context.
//var result =context.ChangeTracker.HasChanges();

#endregion

#endregion

#region Entity States
// Entity States are the states of the entity being tracked by the context. Entity States are used to determine the state of the entity and based on the state of the entity, EF Core generates the SQL queries to perform the CRUD operations.
#region Detached
////It is not being tracked by the context.
//Product product = new();
//Console.WriteLine(context.Entry(product).State);
//product.ProductName = "Laptop";
//await context.SaveChangesAsync();
////The state of the entity is Detached because the entity is not being tracked by the context. if we want to track the entity, we need to add the entity to the context.
#endregion

#region Added
//// The entity is being tracked by the context and it is marked as Added. The entity is not present in the database and it will be inserted into the database when SaveChanges method is called.
//Product product = new() { Price=2300, ProductName="SmartPhone" };
//Console.WriteLine(context.Entry(product).State);
//await context.Products.AddAsync(product);
//Console.WriteLine(context.Entry(product).State);
//await context.SaveChangesAsync();
//product.Price = 2500;
//Console.WriteLine(context.Entry(product).State);
//await context.SaveChangesAsync();
#endregion

#region Unchanged
//// The entity is being tracked by the context and it is marked as Unchanged. The entity is present in the database and it has not been modified.
//Product product =await context.Products.FirstOrDefaultAsync();
//Console.WriteLine(context.Entry(product).State);
#endregion

#region Modified
//// The entity is being tracked by the context and it is marked as Modified. The entity is present in the database and it has been modified. The modified entity will be updated in the database when SaveChanges method is called. It creates an update query.
//var product = await context.Products.FirstOrDefaultAsync(u=>u.Id==3);
//Console.WriteLine(context.Entry(product).State);
//product.ProductName = "update";
//Console.WriteLine(context.Entry(product).State);
//await context.SaveChangesAsync(false);// it will not accept the changes, so the state will be modified
//Console.WriteLine(context.Entry(product).State);
#endregion

#region Deleted
// The entity is being tracked by the context and it is marked as Deleted. The entity is present in the database and it will be deleted from the database when SaveChanges method is called. It creates a delete query.
var product = await context.Products.FirstOrDefaultAsync(u => u.Id == 4);
context.Products.Remove(product);
Console.WriteLine(context.Entry(product).State);
context.SaveChanges();
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
