
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
//int ProductId = 5;
//var products = from p in context.Products
//               where p.Id > ProductId
//               select p;
//ProductId = 10;
//foreach(Product product in products)
//{
//    Console.WriteLine(product.ProductName);
//}

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
#region ToListAsync
//: It is used to execute the query and load the data into memory.
#region Method Syntax
//var products =await context.Products.ToListAsync();
#endregion
#region Query Syntax
//var products = await (from p in context.Products
//                      select p).ToListAsync();
#endregion
#endregion

#region Where
//It is a method which used to filter the data based on where condition.
#region Method Syntax
//var products =await context.Products.Where(p=>p.Price>100).ToListAsync();
//var products=await context.Products.Where(p=>p.ProductName.StartsWith("A")).ToListAsync();
#endregion
#region Query Syntax
//var products =  (from p in context.Products
//                      where p.Price > 100&&p.ProductName.StartsWith("A")
//                      select p);
//var data =await products.ToListAsync();
#endregion
#endregion

#region OrderBy
//It is a method which used to sort the data based on the given condition with query.(Ascending)

#region Method Syntax
//var products =await context.Products.Where(products=>products.Price>100).OrderBy(p=>p.Price).ToListAsync();
#endregion
#region Query Syntax
//var products2 = await (from p in context.Products
//                       where p.Price>100
//                        orderby p.Price //ascending with default
//                        select p).ToListAsync();              
//#endregion
#endregion
#endregion

#region Thenby
//It is a method which used to sort the data after the OrderBy method with query.(Ascending)
//var products =context.Products.Where(p=>p.Id>10||p.ProductName.EndsWith("A")).OrderBy(p=>p.Price).ThenBy(p=>p.ProductName).ThenBy(p=>p.Id);

#endregion

#region OrderByDescending
#region Method Syntax
//var products =await context.Products.OrderByDescending(u=>u.Price).ToListAsync();
#endregion
#region Query Syntax
//var products = await (from p in context.Products
//                      orderby p.Price descending
//                      select p).ToListAsync();
#endregion
#endregion

#region ThenByDescending
//It is a method which used to sort the data after the OrderByDescending method with query.(Descending)
var products=context.Products.OrderByDescending(p=>p.Price).ThenByDescending(p=>p.ProductName);
#endregion
#endregion

#region Query methods which brings singular data 
//These methods are used to bring only one data from the query result. single,SingleOrDefault
#region SingleAsync
// It is a method which used to bring only one data from the query result, if there is more than one data, it throws an exception.
#region Only one data
//var product= await context.Products.SingleAsync(p=>p.Id==1);
#endregion
#region No data
//var product = await context.Products.SingleAsync(p => p.Id == 5555);
#endregion
#region more than one data
//var product = await context.Products.SingleAsync(p => p.Id > 1);
#endregion
#endregion

#region SingleOrDefaultAsync
// It is a method which used to bring only one data from the query result, if there is more than one data, it throws an exception.
// If there is no data, it returns default value.(null for reference types, 0 for numeric types)
#region Only one data
//var product= await context.Products.SingleOrDefaultAsync(p=>p.Id==1);
#endregion
#region No data
//var product = await context.Products.SingleOrDefaultAsync(p => p.Id == 5555);
#endregion
#region more than one data
//var product = await context.Products.SingleOrDefaultAsync(p => p.Id > 1);
#endregion

#endregion

//It is a method which used to bring only one data from the query result, if there is more than one data, it throws an exception.
#region FirstAsync 
//the first data from the query result is returned, if there is no data, it throws an exception.

#region Only one data
//var product= await context.Products.FirstAsync(p=>p.Id==1);
#endregion
#region No data
//var product = await context.Products.FirstAsync(p => p.Id == 5555);
#endregion
#region More than one data
//var product = await context.Products.FirstAsync(p => p.Id > 1);
#endregion

#endregion

#region FirstOrDefaultAsync
// the first data from the query result is returned, if there is no data, it returns default value.(null for reference types, 0 for numeric types)

#region Only one data
//var product= await context.Products.FirstOrDefaultAsync(p=>p.Id==1);
#endregion
#region No data
//var product = await context.Products.FirstOrDefaultAsync(p => p.Id == 5555);
#endregion
#region More than one data
//var product = await context.Products.FirstOrDefaultAsync(p => p.Id > 1);
#endregion

#endregion

#region SingleAsync,SingleOrDefaultAsync,FirstAsync,FirstOrDefaultAsync comparison
//SingleAsync: It is used to bring only one data from the query result, if there is more than one data, it throws an exception.
//SingleOrDefaultAsync: It is used to bring only one data from the query result, if there is more than one data, it throws an exception.
//If there is no data, it returns default value.(null for reference types, 0 for numeric types)
//FirstAsync: the first data from the query result is returned, if there is no data, it throws an exception.
//FirstOrDefaultAsync: the first data from the query result is returned, if there is no data, it returns default value.(null for reference types, 0 for numeric types)
#endregion

#region FindAsync
//It is used to bring the data from the database based on the primary key.
//Product product = await context.Products.FirstOrDefaultAsync(p=>p.Id==1);
//Product product= await context.Products.FindAsync(1);
#region Composite Primary Key state
//ProductComponent pc = await context.ProductComponents.FindAsync(1, 1);
#endregion
#endregion

#region FindAsync and SingleAsync,SingleOrDefaultAsync,FirstAsync,FirstOrDefaultAsync methods comparison 
//FindAsync: It is used to bring the data from the database based on the primary key.
//SingleAsync: It is used to bring only one data from the query result, if there is more than one data, it throws an exception.
//SingleOrDefaultAsync: It is used to bring only one data from the query result, if there is more than one data, it throws an exception.
//If there is no data, it returns default value.(null for reference types, 0 for numeric types)
//FirstAsync: the first data from the query result is returned, if there is no data, it throws an exception.
//FirstOrDefaultAsync: the first data from the query result is returned, if there is no data, it returns default value.(null for reference types, 0 for numeric types)
#endregion

#region LastAsync
//It is used to bring the last data from the query result, if there is no data, it throws an exception. It requires OrderBy method before it.
//var product = await context.Products.OrderBy(p=>p.ProductName).LastAsync(p => p.Id > 1);
#endregion

#region LastOrDefaultAsync
//It is used to bring the last data from the query result, if there is no data, it returns default value.(null for reference types, 0 for numeric types)
//It requires OrderBy method before it.
var product = await context.Products.OrderBy(p=>p.ProductName).LastOrDefaultAsync(p => p.Id > 1);

#endregion

#region MyRegion

#endregion

#endregion

#region Other querying methods

#region CountAsync

#endregion

#region LongCountAsync

#endregion

#region AnyAsync

#endregion

#region MaxAsync

#endregion

#region MinAsync

#endregion

#region Distinc

#endregion

#region AllAsync

#endregion

#region SumAsync

#endregion

#region AverageAsync

#endregion

#region ContainsAsync

#endregion
#endregion

#region Transform methods after query results

#region ToDictionaryAsync

#endregion

#region ToArrayAsync

#endregion

#region Select

#endregion

#region SelectMany

#endregion
#endregion,

#region GroupBy method
#endregion

#region Foreach method
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
    public ICollection<Component>Components { get; set; }
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
