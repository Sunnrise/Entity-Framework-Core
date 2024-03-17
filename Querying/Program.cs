
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
ECommerceDbContext context = new ();
#region How can create a basic query?
#region Method Syntax
//var products =await context.Products.ToListAsync();
#endregion
#region Query Syntax
//var products2= await (from p in context.Products
//                      select p).ToListAsync();
#endregion
#endregion
#region What needs to be done to execute a query?
#region ToListAsync
#region Method Syntax
//var products =await context.Products.ToListAsync();
#endregion
#region Query Syntax
//var products = await (from p in context.Products
//                      select p).ToListAsync();
#endregion
#endregion
int ProductId = 5;
var products = from p in context.Products
               where p.Id > ProductId
               select p;
ProductId = 10;
foreach(Product product in products)
{
    Console.WriteLine(product.ProductName);
}

#region Foreach
//foreach(Product product in products)
//{
//    Console.WriteLine(product.ProductName);
//}
#region Deferred Execution(delay the execution)
//The query is not executed(IQueryable) until the data is requested

#endregion
#endregion
#endregion

#region what are IQueryable and IEnumerable basicly?
//var products = await (from p in context.Products
//                              select p).ToListAsync();
#region IQueryable
//IQueryable is a queryable data source, it is a collection of data that can be queried.
//The Query which is the form of execution is not executed until the data is requested.
#endregion
#region IEnumerable
//IEnumerable is a collection of data that can be enumerated, it is a collection of data that can be enumerated.
//The Query which is the form of executed and data is loaded into memory.
#endregion
#endregion

#region Query methods which brings plural data
#endregion

#region Query methods which brings singular data 
#endregion
#region Other query methods
#endregion

#region Transform methods after query results

#endregion,

#region GroupBy method
#endregion

#region Foreach method
#endregion


public class ECommerceDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Component> Components { get; set; }
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
    public string ProductName { get; set; }
    public float Price { get; set; }
    public ICollection<Component>Components { get; set; }
}
// Entity
public class Component
{
    public int Id { get; set; }
    public string ComponentName { get; set; }
}
