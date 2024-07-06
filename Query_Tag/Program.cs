
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Reflection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

ApplicationDbContext context = new();

#region What is Query Tags?
//Query Tags allows us to add comments to the generated queries with EF Core, allowing us to observe the queries with these comments in SQL Profiler, Query Log, etc.

//await context.Persons.ToListAsync();

#endregion
#region TagWith Method
//await context.Persons.TagWith("Sample Explaining for logging...").ToListAsync();
#endregion
#region Multiple TagWith
//await context.Persons.TagWith("All people pulled.r")
//    .Include(p => p.Orders).TagWith("People's sellings added to query.")
//    .Where(p => p.Name.Contains("a")).TagWith(" Names which they have a character are,Filtered .")
//    .ToListAsync();
#endregion
#region TagWithCallSite Method
//It adds a comment line to the generated query and additionally provides information about which line in this file (.cs) the query was generated.
await context.Persons.TagWithCallSite("All people pulled.r")
    .Include(p => p.Orders).TagWithCallSite("People's sellings added to query.")
    .Where(p => p.Name.Contains("a")).TagWithCallSite("Names which they have a character are,Filtered .")
    .ToListAsync();
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
    readonly ILoggerFactory loggerFactory = LoggerFactory.Create(builder => builder
    .AddFilter((category, level) =>
    {
        return category == DbLoggerCategory.Database.Command.Name && level == LogLevel.Information;
    })
    .AddConsole());

protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost, 1433;Database=ApplicationDb;User Id=sa;Password=Password1;TrustServerCertificate=True");
        optionsBuilder.UseLoggerFactory(loggerFactory);

    }
}



