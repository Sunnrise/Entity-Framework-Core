
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Microsoft.Extensions.Logging;

ApplicationDbContext context = new();
var datas = await context.Persons.ToListAsync();
#region Why we need logging?
//We need logging to understand what is happening in our application in Runtime.
#endregion

#region What do we log?
//Queries, Errors, Warnings, Informations, Debug Informations
//Special Data, Exception Details
#endregion

#region How can do logging basicly?
//Minumum Configuration needed to log.
//No need to add any additional package.

#endregion

#region How can do logging in Debug Window?
//We cant see the logs easily in Debug Window. So we need to log in a file.
#endregion

#region How can do logging in File?


#endregion

#region Sensitive Data Logging- EnableSensitiveDataLogging
//Sensitive Data is not logged by default. Because it may contain sensitive data. It causes security problems. It is not recommended to log sensitive data. But if you want to log sensitive data, you can enable it. 
#endregion

#region Exception Detail Logging - EnableDetailedErrors
//By default, EF Core logs only the exception message. If you want to log the exception details, you can enable it.
#endregion

#region Log Levels
//By default, EF Core logs Debug level and above level logs. If you want to log the logs with different levels, you can set the log level.
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
    StreamWriter _log = new("logs.txt", append: true);


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost, 1433;Database=ApplicationDb;User Id=sa;Password=Password1;TrustServerCertificate=True");
        //optionsBuilder.LogTo(Console.WriteLine);
        //optionsBuilder.LogTo(message=> Debug.WriteLine(message));
        optionsBuilder.LogTo(async message => await _log.WriteLineAsync(message),LogLevel.Information)
        .EnableSensitiveDataLogging()
        .EnableDetailedErrors();
    }
    public override void Dispose()
    {
        base.Dispose();
        _log.Dispose();
    }

    public override async ValueTask DisposeAsync()
    {
        await base.DisposeAsync();
        await _log.DisposeAsync();
    }
}



