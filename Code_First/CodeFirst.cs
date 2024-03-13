// See https://aka.ms/new-console-template for more information
using Microsoft.EntityFrameworkCore;

ECommerceDbContext context = new ECommerceDbContext();
await context.Database.MigrateAsync();
public class  ECommerceDbContext:DbContext
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
    //attribute, to specify it as primary key, Property name can be Id,ID,ProductId or ProductID so that EF Core can consider it as primary key
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