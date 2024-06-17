// See https://aka.ms/new-console-template for more information
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;
ApplicationDbContext context = new();

#region What is the Table Per Type (TPT) 
//In the Table Per Type (TPT) pattern, each class in the inheritance hierarchy is mapped to its table in the database.
//The base class table contains the properties of the base class and the derived class tables contain the properties of the derived classes.

//The base class table and the derived class tables are connected by a foreign key relationship.
//The base class table and the derived class tables have a one-to-one relationship.

#endregion

#region How to implement Table Per Type (TPT) in EF Core
//To implement the Table Per Type (TPT) pattern in EF Core, we need to create a base class and derived
//classes that inherit from the base class. Then, we need to create a DbSet property for each class in the DbContext class.
//Finally, we need to configure the table mapping for each class in the OnModelCreating method of the DbContext class with the ToTable method.

#endregion

#region Adding Data With TPT
//Technician technician = new()
//{
//    Name = "Technician Name",
//    Surname = "Technician Surname",
//    Department = "Technician Department",
//    Branch = "Technician Branch"
//};
//Customer customer = new()
//{
//    Name = "Customer Name",
//    Surname = "Customer Surname",
//    CompanyName = "Customer Company Name"
//};
//context.Technicians.Add(technician);
//context.Customers.Add(customer);
//await context.SaveChangesAsync();
#endregion

#region Deleting Data With TPT
//Employee deleteEmployee = await context.Employees.FindAsync(1);
//context.Employees.Remove(deleteEmployee);
//await context.SaveChangesAsync();

//Person? deletePerson = await context.Persons.FindAsync(1);
//context.Persons.Remove(deletePerson);
#endregion

#region Updating Data With TPT
//Technician technician = await context.Technicians.FindAsync(1);
//technician.Name = "Updated Technician Name";
//context.SaveChanges();

#endregion

#region Data Querying With TPT

var data= context.Technicians.ToList();
var data2 = context.Employees.ToList();
#endregion

abstract class Person
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
}
class Employee : Person
{
    public int A { get; set; }
    public string? Department { get; set; }
}
class Customer : Person
{

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
        modelBuilder.Entity<Person>().ToTable("Persons");
        modelBuilder.Entity<Employee>().ToTable("Employees");
        modelBuilder.Entity<Customer>().ToTable("Customers");
        modelBuilder.Entity<Technician>().ToTable("Technicians");



    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost, 1433;Database=ApplicationDb;User Id=sa;Password=Password1;TrustServerCertificate=True");
    }
}
