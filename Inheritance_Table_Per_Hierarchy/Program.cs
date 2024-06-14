// See https://aka.ms/new-console-template for more information
using Microsoft.EntityFrameworkCore;
ApplicationDbContext context = new();

#region What is the Table Per Hierarchy (TPH) 

#endregion

#region How to implement Table Per Hierarchy (TPH) in EF Core

#endregion

#region What is the Discriminator Column

#endregion

#region How can we change the Discriminator Column Name

#endregion

#region How can we change the Discriminator Column Value

#endregion

#region Adding Data With TPH

#endregion

#region Deleting Data With TPH

#endregion

#region Updating Data With TPH

#endregion

#region Data Querying With TPH

#endregion

#region Same column name in the different entities

#endregion

#region IsComplete Configuration

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
    public int A { get; set; }
    public string? CompanyName { get; set; }
}
class Technician : Employee
{
    public int A { get; set; }
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
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost, 1433;Database=ApplicationDb;User Id=sa;Password=Password1;TrustServerCertificate=True");
    }
}
