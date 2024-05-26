using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Emit;

ECommerceDbContext context = new();

#region Default Convention

class Employee
{
    public int Id { get; set; }
    public string Name { get; set; }

    public EmployeeAddress EmployeeAddress { get; set; }
}
class EmployeeAddress
{
    public int Id { get; set; }//Primary Key
    public int EmployeeId { get; set; }//Foreign Key
    public string Address { get; set; }

    public Employee Employee { get; set; }
}
#endregion
#region Data Annotations

//class Employee
//{
//    public int Id { get; set; }
//    public string Name { get; set; }

//    public EmployeeAddress EmployeeAddress { get; set; }
//}
//class EmployeeAddress
//{
//    [Key, ForeignKey(nameof(Employee))]
//    public int Id { get; set; }
//    public string Address { get; set; }

//    public Employee Employee { get; set; }
//}
#endregion
#region Fluent API

//class Employee
//{
//    public int Id { get; set; }
//    public string Name { get; set; }

//    public EmployeeAddress EmployeeAddress { get; set; }
//}
//class EmployeeAddress
//{
//    public int Id { get; set; }
//    public string Address { get; set; }

//    public Employee Employee { get; set; }
//}
#endregion
class ECommerceDbContext : DbContext
{
    public DbSet<Employee> Employees { get; set; }
    public DbSet<EmployeeAddress> EmployeeAddresses { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost, 1433;Database=ECommerceDb;User Id=sa;Password=Password1;TrustServerCertificate=True");
    }
   
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
