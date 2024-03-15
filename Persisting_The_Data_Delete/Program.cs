
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;





#region How can we delete data basicly
//ECommerceDbContext context = new ECommerceDbContext();
//Product product = await context.Products.FirstOrDefaultAsync(p => p.Id == 4);
//context.Products.Remove(product);
//await context.SaveChangesAsync();



#endregion
#region What is the ChangeTracker role in Delete process
//The ChangeTracker is a feature of the DbContext class that keeps track of the state of the entities that are being tracked by the context.
//It helps EF Core to understand the state of an object when we update it.
//So EF Core decide whether to execute an insert, update, or delete query based on the state of the object.
#endregion
#region How can untracked entities be deleted
//ECommerceDbContext context = new ECommerceDbContext();
//Product product = new Product
//{
//    Id = 2
//};
//context.Products.Remove(product);
//await context.SaveChangesAsync();
#region Deleting with EntityState method
//Product p = new Product
//{
//    Id = 1
//};
//context.Entry(p).State= EntityState.Deleted;
//await context.SaveChangesAsync();
#endregion
#endregion
#region Important things when we delete more than one instance to the database
#region Using efficiently savechanges method
// İf you want to delete more than one instance, you can use SaveChanges method efficiently. You have to use SaveChanges method after all instances are deleted.
#endregion
#region RemoveRange
ECommerceDbContext context = new();
List <Product>products =await context.Products.Where(u=>u.Id>=7&&u.Id<=9).ToListAsync();
context.Products.RemoveRange(products);
await context.SaveChangesAsync();


#endregion
#region  Using EntityState for deleting more than one instance

#endregion
#endregion
public class ECommerceDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Customer> Customers { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //Provider, ConnectionString, LazyLoading
        optionsBuilder.UseSqlServer("Server=localhost, 1433;Database=ECommerceDb;User Id=sa;Password=Password1;TrustServerCertificate=True");
    }
}
//Entity
public class Product
{
    //EF Core will consider Id or ProductId as primary key, if you use any other name, you need to use [Key]
    //attribute, to specify it as primary key, Property name can be Id,ID,ProductId or
    //ProductID so that EF Core can consider it as primary key
    public int Id { get; set; }
    public string Name { get; set; }
    public int Quantity { get; set; }
    public float Price { get; set; }
}
// Entity
public class Customer
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}