// See https://aka.ms/new-console-template for more information
using Microsoft.EntityFrameworkCore;
#region Adding Data to the Database basicly

//ECommerceDbContext context = new ECommerceDbContext();
//Product product = new Product
//{
//    Name = "Laptop",
//    Quantity = 10,
//    Price = 1000
//};
#region Context.AddAsync Method
//await context.AddAsync(product);// object type 
#endregion
#region content.DbSet.AddAsync Method

//await context.Products.AddAsync(product); // Product type
#endregion
/*await context.SaveChangesAsync();*/ //SaveChanges; create insert, update, delete queries and execute them on the database. if the 
                                      //one of the queries fails, the whole transaction will be rolled back(ROLLBACK) and no changes will be made to the database.

#endregion
#region How to EF understands the insert query instead of the update query 
//ECommerceDbContext context = new ECommerceDbContext();
//Product product = new Product
//{

//    Name = "Desktop Computer",
//    Quantity = 15,
//    Price = 1500
//};
//Console.WriteLine(context.Entry(product).State); //Detached( Not tracked by the context)

//await context.Products.AddAsync(product);//: The product is added to the context and its state is changed to Added
//Console.WriteLine(context.Entry(product).State); //Added

//await context.SaveChangesAsync();//: The product is inserted into the database and its state is changed to Unchanged
//Console.WriteLine(context.Entry(product).State);// Unchanged( The product is tracked by the context but no changes are made to it)


#endregion

//await context.Database.MigrateAsync();
#region Important things when we add more than one product to the database

//ECommerceDbContext context = new ECommerceDbContext();
//Product product = new Product
//{

//    Name = "Desktop Computer",
//    Quantity = 15,
//    Price = 1500
//};
//Product product1 = new Product
//{

//    Name = "Computer",
//    Quantity = 1,
//    Price = 100
//};
//Product product2 = new Product
//{

//    Name = "Desktop",
//    Quantity = 5,
//    Price = 150
//};
//await context.AddAsync(product);
//await context.AddAsync(product1);
//await context.AddAsync(product2);
//await context.SaveChangesAsync();// we can use SaveChangesAsync() method to insert all the products into the database at once its useful
////because it will create a single transaction for all the products and execute them in a single batch,
////which is more efficient than executing them one by one.
#endregion

#region How to add multiple products to the database using AddRangeAsync() method
//ECommerceDbContext context = new ECommerceDbContext();
//Product product = new Product
//{

//    Name = "Desktop Computer",
//    Quantity = 15,
//    Price = 1500
//};
//Product product1 = new Product
//{

//    Name = "Computer",
//    Quantity = 1,
//    Price = 100
//};
//Product product2 = new Product
//{

//    Name = "Desktop",
//    Quantity = 5,
//    Price = 150
//};
//await context.AddRangeAsync(product, product1, product2);
//await context.SaveChangesAsync();


#endregion

#region getting Id of the product after inserting it into the database

ECommerceDbContext context = new ECommerceDbContext();
Product product = new Product
{

    Name = "Desktop Computer",
    Quantity = 15,
    Price = 1500
};
await context.AddAsync(product);
await context.SaveChangesAsync();
Console.WriteLine(product.Id);


#endregion

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