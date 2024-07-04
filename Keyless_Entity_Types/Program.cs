using Microsoft.EntityFrameworkCore;
using System.Reflection;

ApplicationDbContext context = new();


#region Keyless Entity Types
//Keyless Entity Types is a feature that allows you to map entities to queries that are defined in your model but do not have keys defined on them.

//Generally aggregate operations do not have primary key columns. We can execute these queries as if they are entities with the Keyless Entity Types feature.
#endregion

#region Keyless Entity Types Tanımlama
//1. Design a entity class that represents the query result.
//2. add DbSet property to the DbContext class.
//3. we have to override the OnModelCreating method and configure the entity type as keyless with hasnokey. then we specify the view name with toview method.

//var datas = await context.PersonOrderCounts.ToListAsync();
//Console.WriteLine();
#region Keyless Attribute'u

#endregion
#region HasNoKey Fluent API'ı

#endregion
#endregion
#region Keyless Entity Types Features?
//No primary key column

//Change Tracker mechanism will not be active.

//It can be used in entity hierarchy as TPH but cannot be used in other inheritance relationships!
#endregion
[Keyless]
public class PersonOrderCount
{
    public string Name { get; set; }
    public int OrderCount { get; set; }
}
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
    public int Price { get; set; }

    public Person Person { get; set; }
}
class ApplicationDbContext : DbContext
{
    public DbSet<Person> Persons { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<PersonOrderCount> PersonOrderCounts { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        modelBuilder.Entity<Person>()
            .HasMany(p => p.Orders)
            .WithOne(o => o.Person)
            .HasForeignKey(o => o.PersonId);
        modelBuilder.Entity<PersonOrderCount>().HasNoKey().ToView("vw_PersonOrderCount");
    }
protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost, 1433;Database=ApplicationDb;User Id=sa;Password=Password1;TrustServerCertificate=True");
    }
}


