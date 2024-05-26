using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Emit;

ECommerceDbContext context = new();
#region Default Convention
//Each 2 entities refer to each other by a navigation property
//In one to one relationship, which entity is the principal and which is the dependent? In the default convention, the entity that contains the foreign key is the dependent entity.
//class Employee
//{
//    public int Id { get; set; }
//    public string Name { get; set; }

//    public EmployeeAddress EmployeeAddress { get; set; }
//}
//class EmployeeAddress
//{
//    public int Id { get; set; }//Primary Key
//    public int EmployeeId { get; set; }//Foreign Key
//    public string Address { get; set; }

//    public Employee Employee { get; set; }
//}
#endregion
#region Data Annotations
//Navigation property must be defined.
//Foreing Column name can be except the default convention, we use ForeignKey attribute to specify the foreign key column name.
//Foreign key column is not necessary
//In 1 to 1 relationship, we dont need to extra foreign key column in the dependent entity. so we can use ıd column as a primary key and foreign key.

//class Employee
//{
//    public int Id { get; set; }
//    public string Name { get; set; }

//    public EmployeeAddress EmployeeAddress { get; set; }
//}
//class EmployeeAddress
//{
//    [Key,ForeignKey(nameof(Employee))] 
//    public int Id { get; set; }
//    public string Address { get; set; }

//    public Employee Employee { get; set; }
//}
#endregion
#region Fluent API
// Navigation property must be defined.
// onModelCreating method is used to configure the relationship between entities. We desing the relationship between entities by using the HasOne, WithOne, HasForeignKey methods.
class Employee
{
    public int Id { get; set; }
    public string Name { get; set; }

    public EmployeeAddress EmployeeAddress { get; set; }
}
class EmployeeAddress
{
    public int Id { get; set; }
    public string Address { get; set; }

    public Employee Employee { get; set; }
}
#endregion
class ECommerceDbContext : DbContext
{
    public DbSet<Employee> Employees { get; set; }
    public DbSet<EmployeeAddress> EmployeeAddresses { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost, 1433;Database=ECommerceDb;User Id=sa;Password=Password1;TrustServerCertificate=True");
    }
    //Model's(entity's) configuration is done in OnModelCreating method.
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EmployeeAddress>()
            .HasKey(c => c.Id);

        modelBuilder.Entity<Employee>()
             .HasOne(c => c.EmployeeAddress)
             .WithOne(c => c.Employee)
             .HasForeignKey<EmployeeAddress>(c => c.Id);
    }
}
