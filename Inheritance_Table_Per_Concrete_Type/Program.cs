// See https://aka.ms/new-console-template for more information
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;
ApplicationDbContext context = new();

#region What is Table Per Concrete Type (TPC)?
// TPC is a strategy to map the inheritance hierarchy to the database. It creates a table for each concrete class in the inheritance hierarchy. 
//TPC, more performant than TPT
#endregion
#region How can we apply TPC ?
//To apply TPC, we need to use the UseTpcMappingStrategy method in the OnModelCreating method of the DbContext class. 
#endregion
#region Adding data in TPC
await context.Technicians.AddAsync(new() { Name= "Alperen", Surname= "Güneş", Branch="CS", Department="IT"});
await context.SaveChangesAsync();
#endregion
#region Deleting data in TPC

#endregion
#region Update data in TPC

#endregion
#region Querying data in TPC

#endregion
abstract class Person
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
}
class Employee : Person
{
    public string? Department { get; set; }
}
class Customer : Person
{
    public string? CompanyName { get; set; }
}
class Technician : Employee
{
    public string? Branch { get; set; }
}

class ApplicationDbContext : DbContext
{
    public DbSet<Person> Persons { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Technician> Technicians { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //TPT
        //modelBuilder.Entity<Person>().ToTable("Persons");
        //modelBuilder.Entity<Employee>().ToTable("Employees");
        //modelBuilder.Entity<Customer>().ToTable("Customers");
        //modelBuilder.Entity<Technician>().ToTable("Technicians");
        modelBuilder.Entity<Person>().UseTpcMappingStrategy();
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost, 1433;Database=ApplicationDb;User Id=sa;Password=Password1;TrustServerCertificate=True");
    }
}
