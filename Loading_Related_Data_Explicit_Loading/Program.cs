﻿
using Microsoft.EntityFrameworkCore;
using System.Reflection;
ApplicationDbContext context = new();
#region Explicit Loading

//An approach that allows the data to be loaded according to conditions/needs to be added to the created query.

//var employee=await context.Employees.FirstOrDefaultAsync(e => e.Id == 1);

//if (employee.Name == "Alperen") 
//{ 
// var orders= await context.Orders.Where(o=> o.EmployeeId == employee.Id).ToListAsync();
//}
#region Reference

//In the Explicit Loading process, if the navigation property of the table to be added to the query relationally is a singular type, we can add this table to the query with reference.

//var employee = await context.Employees.FirstOrDefaultAsync(e => e.Id == 2);
//...
//...
//...
//await context.Entry(employee).Reference(e => e.Region).LoadAsync();
//second query will be added to the main query

//Console.WriteLine();
#endregion

#region Collection

//In the Explicit Loading process, if the navigation property of the table to be added to the query relationally is a plural/collectional type, we can add this table to the query with Collection.

//var employee = await context.Employees.FirstOrDefaultAsync(e => e.Id == 2);
////...
////...
////...
//await context.Entry(employee).Collection(e => e.Orders).LoadAsync();

//Console.WriteLine();
#endregion

#region Apply Aggregate operator on Collections
//var employee = await context.Employees.FirstOrDefaultAsync(e => e.Id == 2);
//...
//...
//...
//var count = await context.Entry(employee).Collection(e => e.Orders).Query().CountAsync();
Console.WriteLine();
#endregion 
#region Apply Filter on Collections
var employee = await context.Employees.FirstOrDefaultAsync(e => e.Id == 2);
//...
//...
//...
var orders = await context.Entry(employee).Collection(e => e.Orders).Query().Where(q => q.OrderDate.Day == DateTime.Now.Day).ToListAsync();
#endregion
#endregion

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
