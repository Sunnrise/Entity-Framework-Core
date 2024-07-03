
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Reflection;
using System.Runtime.CompilerServices;

ApplicationDbContext context = new();

#region What is Lazy Loading?
//Lazy Loading is an approach that allows the loading of related data on demand. When an operation is performed on a navigation property, a query is created and executed for the corresponding table and the data is loaded.
#endregion



//var employee = await context.Employees.FindAsync(2);
//Console.WriteLine(employee.Region.Name);

#region Lazy Loading with Proxies
//Microsoft.EntityFrameworkCore.Proxies

#region Virtual Properties

//If we use proxies for lazy loading, Navigation Properties must be marked with virtual. Otherwise, an exception will be thrown.
#endregion
#endregion

#region Lazy Loading without Proxy

//All platforms may not support proxies. In such a case, we may need to implement lazy loading manually.

//Navigation Properties do not need to be marked with virtual in manually implemented Lazy Loading operations!
#region Lazy Loading with ILazyLoader Interface
//Microsoft.EntityFrameworkCore.Abstractions
//var employee = await context.Employees.FindAsync(2);
#endregion
#region Lazy Loading with Delegate
//var employee = await context.Employees.FindAsync(2);
#endregion
#endregion

#region N+1 Problem
//var region = await context.Regions.FindAsync(1);
//foreach (var employee in region.Employees)
//{
//    var orders = employee.Orders;
//    foreach (var order in orders)
//    {
//        Console.WriteLine(order.OrderDate);
//    }
//}
#endregion

//Lazy Loading is a method that has a very costly and performance-reducing effect in terms of usage. Therefore, when using it, we should be careful as much as possible and focus on not preferring lazy loading in cases where navigation properties are circularly triggered. Otherwise, it will generate and execute the same queries for each trigger. We call this situation the N+1 Problem.

//Console.WriteLine();

#region Lazy Loading with proxy
public class Employee
{
    public int Id { get; set; }
    public int RegionId { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public int Salary { get; set; }
    public virtual List<Order> Orders { get; set; }
    public virtual Region Region { get; set; }
}
public class Region
{
    public int Id { get; set; }
    public string Name { get; set; }
    public virtual ICollection<Employee> Employees { get; set; }
}
public class Order
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public DateTime OrderDate { get; set; }
    public virtual Employee Employee { get; set; }
}
#endregion
#region Lazy Loading with ILazyLoader Interface
//var employees = await context.Employees.FindAsync(2);

//public class Employee
//{
//    ILazyLoader _lazyLoader;
//    Region _region;
//    private List<Order> _orders;
//    public Employee() { }
//    public Employee(ILazyLoader lazyLoader)
//        => _lazyLoader = lazyLoader;
//    public int Id { get; set; }
//    public int RegionId { get; set; }
//    public string? Name { get; set; }
//    public string? Surname { get; set; }
//    public int Salary { get; set; }
//    public List<Order> Orders {
//        get=>_lazyLoader.Load(this,ref _orders); 
//        set=> _orders=value; 
//    }
//    public Region Region
//    {
//        get => _lazyLoader.Load(this, ref _region);
//        set => _region = value;
//    }
//}
//public class Region
//{
//    ILazyLoader _lazyLoader;
//    ICollection<Employee> _employees;
//    public Region() { }
//    public Region(ILazyLoader lazyLoader)
//        => _lazyLoader = lazyLoader;
//    public int Id { get; set; }
//    public string Name { get; set; }
//    public ICollection<Employee> Employees
//    {
//        get => _lazyLoader.Load(this, ref _employees);
//        set => _employees = value;
//    }
//}
//public class Order
//{
//    ILazyLoader _lazyLoader;
//    Employee _employee;
//    public Order() { }
//    public Order(ILazyLoader lazyLoader)
//        =>_lazyLoader = lazyLoader;

//    public int Id { get; set; }
//    public int EmployeeId { get; set; }
//    public DateTime OrderDate { get; set; }
//    public Employee Employee {
//        get => _lazyLoader.Load(this, ref _employee);  
//        set=> _employee = value;
//    }
//}

#endregion
#region Lazy Loading with Delegate
//public class Employee
//{
//    Action<object, string> _lazyLoader;
//    Region _region;
//    public Employee() { }
//    public Employee(Action<object, string> lazyLoader)
//        => _lazyLoader = lazyLoader;
//    public int Id { get; set; }
//    public int RegionId { get; set; }
//    public string? Name { get; set; }
//    public string? Surname { get; set; }
//    public int Salary { get; set; }
//    public List<Order> Orders { get; set; }
//    public Region Region
//    {
//        get => _lazyLoader.Load(this, ref _region);
//        set => _region = value;
//    }
//}
//public class Region
//{
//    Action<object, string> _lazyLoader;
//    ICollection<Employee> _employees;
//    public Region() { }
//    public Region(Action<object, string> lazyLoader)
//        => _lazyLoader = lazyLoader;
//    public int Id { get; set; }
//    public string Name { get; set; }
//    public ICollection<Employee> Employees
//    {
//        get => _lazyLoader.Load(this, ref _employees);
//        set => _employees = value;
//    }
//}
//public class Order
//{
//    public int Id { get; set; }
//    public int EmployeeId { get; set; }
//    public DateTime OrderDate { get; set; }
//    public Employee Employee { get; set; }
//}

//static class LazyLoadingExtension
//{
//    public static TRelated Load<TRelated>(this Action<object, string> loader, object entity, ref TRelated navigation, [CallerMemberName] string navigationName = null)
//    {
//        loader.Invoke(entity, navigationName);
//        return navigation;
//    }
//}
#endregion

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
        optionsBuilder.UseLazyLoadingProxies()
            .UseSqlServer("Server=localhost, 1433;Database=ApplicationDb;User Id=sa;Password=Password1;TrustServerCertificate=True");
        //optionsBuilder.UseLazyLoadingProxies();
    }
}
