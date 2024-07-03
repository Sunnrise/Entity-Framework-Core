using Loading_Related_Data_Eager_Loading.Configurations;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

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


var regions = await context.Regions
    .Include(r => r.Employees)
    .ThenInclude(e => e.Orders)
    .ToListAsync();
#endregion
#region Filtered Include

#endregion
#region Critical Info for Eager Loading


#endregion
#region AutoInclude - EF Core 6

#endregion
#region IgnoreAutoIncludes

#endregion
#region Include Between Derived Entities

#region Include with Cast Operand

#endregion
#region Include with as Operand

#endregion
#region Include with 2. Overload 

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
public class Employee
{
    public int Id { get; set; }
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
    //public DbSet<Person> Persons { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Region> Regions { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
       
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost, 1433;Database=ApplicationDb;User Id=sa;Password=Password1;TrustServerCertificate=True");
    }
}
