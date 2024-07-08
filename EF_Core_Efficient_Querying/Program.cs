
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using System.Reflection.Emit;

ApplicationDbContext context = new();

#region EF Core Efficient Querying Techniques

#region IQueryable - IEnumerable Difference

//IQueryable, operations performed on this interface will be reflected directly on the query to be generated.

//IEnumerable, operations performed on this interface are performed on instances that are returned as a result of the basic query and loaded into memory. So it is not reflected in the query.

//In query studies made with IQueryable, the sql query will be generated to obtain the target data, while in query studies made with IEnumerable, the sql is executed to bring wider data and the target data is filtered in-memory.

//IQueryable brings the target data, brings more than the target data and filters in-memory.

#region IQueryable
//var persons1 = await context.People.Where(p => p.Name.Contains("a"))
//                             .Take(3)
//                             .ToListAsync();


//var persons2 = await context.People.Where(p => p.Name.Contains("a"))
//                             .Where(p => p.PersonId > 3)
//                             .Skip(3)
//                             .Take(3)
//                             .ToListAsync();

#endregion

#region IEnumerable
//var persons1 =  context.People.Where(p => p.Name.Contains("a"))
//                             .AsEnumerable()
//                             .Take(3)
//                             .ToList();
#endregion

#region AsQueryable

#endregion

#region AsEnumerable

#endregion
#endregion

#region Just Select The Required Columns/ Select
//var people =await context.People.Select(p => new
//{
//    p.PersonId,
//    p.Name
//}).ToListAsync();
////SELECT [p].[PersonId], [p].[Name]
#endregion

#region Limited Results- Take
//await context.People.Take(3).ToListAsync();// Get the first 3 people
#endregion

#region Filtering Data in Eager Loading Process in Join Queries
//await context.People.Include(p => p.Orders
//                                        .Where(o => o.Description
//                                        .Contains("a")))
//                                        .OrderByDescending(p => p.PersonId)
//                                        .Take(3)
//.ToListAsync();
#endregion

#region Using Explicit Loading for Conditional Joins

//var person = await context.People.Include(p=>p.Orders).FirstOrDefaultAsync(p => p.PersonId == 1); /Cancel the order loading process
//var person = await context.People.FirstOrDefaultAsync(p => p.PersonId == 1); //Just bring the person and then check the condition, if true, bring the orders of the person
//if (person.Name=="Alperen")
//{
//    //bring the orders of the person
//    await context.Entry(person).Collection(p => p.Orders).LoadAsync();
//}


#endregion

#region Be Careful When Using Lazy Loading

#region Risky State 
//var people = await context.People.ToListAsync();
//foreach (var person in people)
//{
//    foreach (var order in person.Orders)
//    {
//        Console.WriteLine($"{person.Name}-{order.OrderId}");

//        //Do something
//    }
//    Console.WriteLine("-/-/-/-/-/-/-/-/-//-");
//}
////It will make a query for each person to bring the orders of the person. It will make a query for each order to bring the person of the order.
#endregion
#region İdeal State
//var people = await context.People.Select(p=> new {p.Name, p.Orders}).ToListAsync();
//foreach (var person in people)
//{
//    foreach (var order in person.Orders)
//    {
//        Console.WriteLine($"{person.Name}-{order.OrderId}");

//        //Do something
//    }
//    Console.WriteLine("-/-/-/-/-/-/-/-/-//-");
//}
////It will make a query to bring the people and orders of the people. It will not make a query for each person to bring the orders of the person. It will not make a query for each order to bring the person of the order.
#endregion
#endregion

#region  If you need to use raw SQL at certain points / FromSql

#endregion

#region Prefer Asynchronous Functions

#endregion

#endregion

public class Person
{
    public int PersonId { get; set; }
    public string Name { get; set; }

    public virtual ICollection<Order> Orders { get; set; }
}
public class Order
{
    public int OrderId { get; set; }
    public int PersonId { get; set; }
    public string Description { get; set; }

    public virtual Person Person { get; set; }
}
class ApplicationDbContext : DbContext
{
    public DbSet<Person> People { get; set; }
    public DbSet<Order> Orders { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        modelBuilder.Entity<Person>()
            .HasMany(p => p.Orders)
            .WithOne(o => o.Person)
            .HasForeignKey(o => o.PersonId);
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost, 1433;Database=ApplicationDb;User Id=sa;Password=Password1;TrustServerCertificate=True")
            .UseLazyLoadingProxies();
    }
}
