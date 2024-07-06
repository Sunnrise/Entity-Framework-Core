
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

ApplicationDbContext context = new();
#region What is Query Log?
//We use the query log mechanism to be able to follow the queries generated as a result of LINQ queries and to debug possible technical errors.
#endregion
#region How can we configure?
//Microsoft.Extensions.Logging.Console
await context.Persons.ToListAsync();
await context.Persons.Include(p => p.Orders).Where(p => p.Name.Contains("a")).Select(p => new { p.Name, p.PersonId}).ToListAsync();
#endregion
#region How can we apply a filter?

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
    public int Price { get; set; }

    public Person Person { get; set; }
}
class ApplicationDbContext : DbContext
{
    public DbSet<Person> Persons { get; set; }
    public DbSet<Order> Orders { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        modelBuilder.Entity<Person>()
            .HasMany(p => p.Orders)
            .WithOne(o => o.Person)
            .HasForeignKey(o => o.PersonId);
    }

    //private readonly ILoggerFactory loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
    private readonly ILoggerFactory loggerFactory = LoggerFactory.Create(builder => builder.AddFilter((category,level)=>
    {
       return category == DbLoggerCategory.Database.Command.Name && level == LogLevel.Information;
    }).AddConsole());


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost, 1433;Database=ApplicationDb;User Id=sa;Password=Password1;TrustServerCertificate=True");
        optionsBuilder.UseLoggerFactory(loggerFactory);

    }
}



