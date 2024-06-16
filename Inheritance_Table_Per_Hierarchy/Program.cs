// See https://aka.ms/new-console-template for more information
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
Employee Employee = new()
{
    Name = "Employee Name",
    Surname = "Employee Surname",
    Department = "Employee Department"
};
await context.Employees.AddAsync(Employee);
await context.SaveChangesAsync();

#region How can we change the Discriminator Column Value
//We can use the HasValue method in the OnModelCreating method to change the Discriminator column value.
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
        modelBuilder.Entity<Person>()
            .HasDiscriminator<string>("DiscriminatorColumn")
            .HasValue<Person>("A")
            .HasValue<Employee>("B")
            .HasValue<Customer>("C")
            .HasValue<Technician>("D");
            

    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost, 1433;Database=ApplicationDb;User Id=sa;Password=Password1;TrustServerCertificate=True");
    }
}
