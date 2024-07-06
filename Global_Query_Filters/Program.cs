using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

ApplicationDbContext context = new();

#region What is Global Query Filters ?
//It is a feature that allows us to create general/default conditions at the application level for an entity and thus filter the data globally.

//Thus, it allows us to query quickly by automatically applying filters without the need for an additional condition expression in all queries made on the specified entity.

//It can be used when working with data such as active (IsActive) at the application level,
//It can be used when defining TenantId in MultiTenancy applications, etc.
#endregion
#region How can we apply Global Query Filters?
//await context.Persons.Where(p => p.IsActive).ToListAsync();
//await context.Persons.ToListAsync();
//await context.Persons.FirstOrDefaultAsync(p => p.Name.Contains("a") || p.PersonId == 3);
#endregion
#region Global Query Filters using Navigation Property
//var p1 = await context.Persons
//    .AsNoTracking()
//    .Include(p => p.Orders)
//    .Where(p => p.Orders.Count > 0)
//    .ToListAsync();

//var p2 = await context.Persons.AsNoTracking().ToListAsync();
//Console.WriteLine();
#endregion
#region How can Ignore Global Query Filters? - IgnoreQueryFilters
//var person1 = await context.Persons.ToListAsync();
//var person2 = await context.Persons.IgnoreQueryFilters().ToListAsync();

//Console.WriteLine();
#endregion
#region Be Careful! 
//Global Query Filter uygulanmış bir kolona farkında olmaksızın şart uygulanabilmektedir. Bu duruma dikkat edilmelidir.
//A condition can be applied to a column that has a Global Query Filter applied without realizing it. Be careful of this.

await context.Persons.Where(p => p.IsActive).ToListAsync();
#endregion

public class Person
{
    public int PersonId { get; set; }
    public string Name { get; set; }
    public bool IsActive { get; set; }

    public List<Order> Orders { get; set; }
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

        modelBuilder.Entity<Person>().HasQueryFilter(p => p.IsActive);
        //modelBuilder.Entity<Person>().HasQueryFilter(p => p.Orders.Count > 0);
    }


protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost, 1433;Database=ApplicationDb;User Id=sa;Password=Password1;TrustServerCertificate=True");
    }
}



