using Loading_Related_Data_Explicit_Loading.Configurations;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

ApplicationDbContext context = new();
#region Loading Related Data

#region Eager Loading
//Eager Loading, a method that loads the main entity and its related entities in a single query. 
#region Include
// Include method is used to load related entities in the same query.

////var employees = await context.Employees.Include("Orders").ToListAsync();
//var employees = await context.Employees
//    .Include(e=> e.Orders)
//    .Where(e => e.Orders.Count > 2)
//    .Include(e=> e.Region)
//    .ToListAsync();
//// in this sceneario it does not matter the order of Include method. If we change the order of Include or where methods, the result will be the same. 
#endregion
#region ThenInclude
//var orders = await context.Orders
//    .Include(o => o.Employee)
//    .Include(o=>o.Employee.Region)
//    .ToListAsync();

//ThenInclude method is used to load related entities of related entities.


//var regions = await context.Regions
//    .Include(r => r.Employees)
//    .ThenInclude(e => e.Orders)
//    .ToListAsync();
#endregion
#region Filtered Include
//In querying , we can filter the related entities with the Where method.  

//var regions = await context.Regions.Include(r => r.Employees.Where(e => e.Name.Contains("a")).OrderByDescending(e=> e.Surname)).ToListAsync();

//Supported Methods: where, OrderBy, Thenby, Skip, Take

//If change tracker is enabled, the filtered include method will be applied to the entities that are already loaded in the context. It causes to load all entities from the database. If we want to apply the filtered include method to the entities that are already loaded in the context, we should use AsNoTracking method.
#endregion
#region Critical Info for Eager Loading
//EF Core, can use the queries which  generated before 
//var orders = await context.Orders.ToListAsync();

//var employees= await context.Employees.ToListAsync();
// It gives the employees order list from the memory. It does not make a query to the database.
#endregion
#region AutoInclude - EF Core 6
//AutoInclude method is used to load related entities automatically. It is a new feature of EF Core 6.0.

//var employees2 = await context.Employees.ToListAsync();

#endregion
#region IgnoreAutoIncludes
// It provides to ignore the AutoInclude method for a specific query.

//var employees3 = await context.Employees.IgnoreAutoIncludes().ToListAsync();
#endregion
#region Include Between Derived Entities

#region Include with Cast Operand
var persons1 = await context.Persons.Include(p => ((Employee)p).Orders).ToListAsync();
#endregion
#region Include with as Operand
var persons2 = await context.Persons.Include(p => (p as Employee).Orders).ToListAsync();
#endregion
#region Include with 2. Overload 
var persons3 = await context.Persons.Include("Orders").ToListAsync();
#endregion
#endregion

Console.WriteLine();
#endregion

#region Explicit Loading

#region Collection Method

#endregion
#region Reference Method

#endregion
#endregion

#region Lazy Loading

#region N + 1 Problem

#endregion
#endregion
#endregion



public class Person
{
    public int Id { get; set; }

}
public class Employee : Person
{
    //public int Id { get; set; }
    public int RegionId { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public int Salary { get; set; }

    public List<Order> Orders { get; set; }
    public Region Region { get; set; }
}
public class Region
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<Employee> Employees { get; set; }
}
public class Order
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public DateTime OrderDate { get; set; }

    public Employee Employee { get; set; }
}


class ApplicationDbContext : DbContext
{
    public DbSet<Person> Persons { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Region> Regions { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        modelBuilder.Entity<Employee>()
            .Navigation(e => e.Region)
            .AutoInclude();

    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost, 1433;Database=ApplicationDb;User Id=sa;Password=Password1;TrustServerCertificate=True");
    }
}
