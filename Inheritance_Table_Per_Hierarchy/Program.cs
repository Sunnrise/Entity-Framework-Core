﻿// See https://aka.ms/new-console-template for more information
using Microsoft.EntityFrameworkCore;
ApplicationDbContext context = new();

#region What is the Table Per Hierarchy (TPH) 
//Inheritance is a mechanism in which a new class is created (derived class) that inherits the properties from the existing class (base class).
#endregion

#region How to implement Table Per Hierarchy (TPH) in EF Core
//In EF Core, the Table Per Hierarchy (TPH) pattern is implemented by default when we use the inheritance in the entity classes.
// So we don't need to do anything to implement the TPH pattern in EF Core.
#endregion

#region What is the Discriminator Column
//The Discriminator column is used to store the type of the entity.
//The Discriminator column is added to the table by default when we use the inheritance in the entity classes.
#endregion

#region How can we change the Discriminator Column Name
//Fistly we need to override the OnModelCreating method in the DbContext class.
//Then we can use the HasDiscriminator method in base class to change the Discriminator column name.
#endregion
//Employee Employee = new()
//{
//    Name = "Employee Name",
//    Surname = "Employee Surname",
//    Department = "Employee Department"
//};
//await context.Employees.AddAsync(Employee);
//await context.SaveChangesAsync();

#region How can we change the Discriminator Column Value
//We can use the HasValue method in the OnModelCreating method to change the Discriminator column value.
#endregion

#region Adding Data With TPH
// we can add data to the database using the TPH pattern in the same way as we add data to the database without using the TPH pattern.
//Employee e1 = new()
//{
//    Name = "Employee Name",
//    Surname = "Employee Surname",
//    Department = "Employee Department"
//};
//Employee e2 = new()
//{
//    Name = "Employee Name2",
//    Surname = "Employee Surname2",
//    Department = "Employee Department2"
//};
//Customer c1 = new()
//{
//    Name = "Customer Name",
//    Surname = "Customer Surname",
//    CompanyName = "Customer CompanyName"
//};
//Customer c2 = new()
//{
//    Name = "Customer Name2",
//    Surname = "Customer Surname2",
//    CompanyName = "Customer CompanyName2"
//};
//Technician t1 = new()
//{
//    Name = "Technician Name",
//    Surname = "Technician Surname",
//    Department = "Technician Department",
//    Branch = "Technician Branch"
//};
//Technician t2 = new()
//{
//    Name = "Technician Name2",
//    Surname = "Technician Surname2",
//    Department = "Technician Department2",
//    Branch = "Technician Branch2"
//};
//await context.Employees.AddAsync(e1);
//await context.Employees.AddAsync(e2);
//await context.Customers.AddAsync(c1);
//await context.Customers.AddAsync(c2);
//await context.Technicians.AddAsync(t1);
//await context.Technicians.AddAsync(t2);
//await context.SaveChangesAsync();
#endregion

#region Deleting Data With TPH
//// we can delete data from the database using the TPH pattern in the same way as we delete data from the database without using the TPH pattern.
//var employee = await context.Employees.FirstOrDefaultAsync();
//context.Employees.Remove(employee);
//await context.SaveChangesAsync();

//var customers=await context.Customers.ToListAsync();
//context.Customers.RemoveRange(customers);
//await context.SaveChangesAsync();
#endregion

#region Updating Data With TPH
//Employee updateEmployee= await context.Employees.FindAsync(4);
//updateEmployee.Name = "Updated Employee Name";
//updateEmployee.Surname = "Updated Employee Surname";
//updateEmployee.Department = "Updated Employee Department";
//await context.SaveChangesAsync();

#endregion

#region Data Querying With TPH
var employees = await context.Employees.ToListAsync();
var technicians = await context.Technicians.ToListAsync();
// we can query data from the database using the TPH pattern in the same way as we query data from the database without using the TPH pattern.

//If the query is based on the base class, the result will include all the derived classes

//EF core will automatically add the WHERE clause to the query to filter the result based on the Discriminator column value.

#endregion

#region Same column name in the different entities
// we can have the same column name in the different entities when we use the TPH pattern.
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
        //modelBuilder.Entity<Person>()
        //    .HasDiscriminator<string>("DiscriminatorColumn")
        //    .HasValue<Person>("A")
        //    .HasValue<Employee>("B")
        //    .HasValue<Customer>("C")
        //    .HasValue<Technician>("D");
            

    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost, 1433;Database=ApplicationDb;User Id=sa;Password=Password1;TrustServerCertificate=True");
    }
}
