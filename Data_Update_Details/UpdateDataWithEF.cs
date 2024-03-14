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

//ECommerceDbContext context = new ECommerceDbContext();
//Product product = new Product
//{

//    Name = "Desktop Computer",
//    Quantity = 15,
//    Price = 1500
//};
//await context.AddAsync(product);
//await context.SaveChangesAsync();
//Console.WriteLine(product.Id);


#endregion

#region How to update the data in the database using EF Core

//ECommerceDbContext context = new ECommerceDbContext();
//Product product= await context.Products.FirstOrDefaultAsync(p => p.Id == 3);
//product.Name = "Diswasher";
//product.Price = 4999;
//await context.SaveChangesAsync();
#endregion

#region What is the ChangeTracker in EF Core(basicly)
//The ChangeTracker is a feature of the DbContext class that keeps track of the state of the entities that are being tracked by the context.
//It helps EF Core to understand the state of an object when we update it.
//So EF Core decide whether to execute an insert, update, or delete query based on the state of the object.
#endregion
#region How can update untracked Objects
//ECommerceDbContext context = new ECommerceDbContext();
//Product product = new Product
//{
//    Id = 3,
//    Name = "new peoduct",
//    Price = 123
//};
#region Update Method
//ChangeTracker mechanizm will not track the object, so we need to use Update method to track the object.
//For tracking the object, We need specify the object Id, so that EF Core can understand which object to track.
//context.Products.Update(product);
//await context.SaveChangesAsync();
#endregion
#endregion
#region What is the EntityState in EF Core
////The EntityState is a entity instance's state in the context. It can be Added, Modified, Deleted, Unchanged, Detached.
//ECommerceDbContext context = new();
//Product p = new();
//Console.WriteLine(context.Entry(p).State);//Detached
#endregion
#region How can EF Core understand the state of an object when we update it
//ECommerceDbContext context = new ECommerceDbContext();
//Product product=await context.Products.FirstOrDefaultAsync(p=>p.Id == 3);
//Console.WriteLine(context.Entry(product).State);//Unchanged

//product.Name = "new product";
//Console.WriteLine(context.Entry(product).State);//Modified

//await context.SaveChangesAsync();
//Console.WriteLine(context.Entry(product).State);//Unchanged:Because last changes are saved to the database and then no changes are made to the object
#endregion
#region Important things when we update more than one instance to the database
ECommerceDbContext context = new ECommerceDbContext();
var products = await context.Products.ToListAsync();
foreach(var product in products)
{
    product.Name += "*";
}
await context.SaveChangesAsync();

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