using Microsoft.EntityFrameworkCore;
using System.Reflection;

ApplicationDbContext context = new();
#region What is the View?
//A database object that is a stored query. It helps to simplify complex queries that are used repeatedly in an application.
#endregion
#region View Using With EF Core

#region Create View
//1. Step : Empty migration created.
//2. Step : update migrations up and down functions With views update and delete scripts.
//3. Step : Migrate the database.
#endregion
#region Configure View as DbSet
//We have to create an entity that can hold the result of the view and add a DbSet property of this entity type.
#endregion
#region Declaring that DbSet is a View
// modelBuilder.Entity<PersonOrder>().ToView("vm_PersonOrders").HasNoKey(); // we have to declare that DbSet is a view with modelBuilder.
#endregion

//var personOrders = await context.PersonOrders
//    .Where(po => po.Count > 10)
//    .ToListAsync();

#region View Features in EF Core
//Views has no primary key. So, we have to declare that DbSet is a view with modelBuilder. HasNoKey() method is used to declare that DbSet is a view.
//Change tracking is not supported for views. So, we can't update or delete data from the view. 
#endregion
Console.WriteLine();
#endregion
public class Person
{
    public int PersonId { get; set; }
    public string Name { get; set; }

    public ICollection<Order> Orders { get; set; }
}
public class Order
{
    public int OrderId { get; set; }
    public int PersonId { get; set; }
    public string Description { get; set; }

    public Person Person { get; set; }
}
public class PersonOrder
{
    public string Name { get; set; }
    public int Count { get; set; }
}
class ApplicationDbContext : DbContext
{
    public DbSet<Person> Persons { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<PersonOrder> PersonOrders { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        modelBuilder.Entity<PersonOrder>()
            .ToView("vm_PersonOrders")
            .HasNoKey();

        modelBuilder.Entity<Person>()
            .HasMany(p => p.Orders)
            .WithOne(o => o.Person)
            .HasForeignKey(o => o.PersonId);
    }

protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost, 1433;Database=ApplicationDb;User Id=sa;Password=Password1;TrustServerCertificate=True");
    }
}


