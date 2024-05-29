using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

ECommerceDbContext context = new();

#region Default Convention
////in Default Convention, EF Core will create a foreign key column in the dependent entity (Employee) with the name DepartmentId (Navigation Property Name + Primary Key Name), if we don't specify the foreign key explicitly.
//class Employee //Dependent Entity 
//{
//    public int Id { get; set; }
//    public int DepartmentId { get; set; } //Foreign Key
//    public string Name { get; set; }

//    public Department Department { get; set; } //Navigation Property
//}
//class Department
//{
//    public int Id { get; set; }
//    public string DepartmentName { get; set; }

//    public ICollection<Employee> Employees { get; set; } //Navigation Property
//}
#endregion
#region Data Annotations
////We can use the ForeignKey attribute to specify the foreign key property in the dependent entity (Employee) explicitly.
//// The ForeignKey attribute is applied to the navigation property in the dependent entity (Employee) and specifies the foreign key property (DId) in the dependent entity (Employee).
//class Employee //Dependent Entity 
//{
//    public int Id { get; set; }
//    [ForeignKey(nameof(Department))]
//    public int DId { get; set; } //Foreign Key
//    public string Name { get; set; }

//    public Department Department { get; set; } //Navigation Property
//}
//class Department
//{
//    public int Id { get; set; }
//    public string DepartmentName { get; set; }

//    public ICollection<Employee> Employees { get; set; } //Navigation Property
//}


#endregion
#region Fluent API
class Employee //Dependent Entity 
{
    public int Id { get; set; }
    public int DId { get; set; } //Foreign Key
    public string Name { get; set; }

    public Department Department { get; set; } //Navigation Property
}
class Department
{
    public int Id { get; set; }
    public string DepartmentName { get; set; }

    public ICollection<Employee> Employees { get; set; } //Navigation Property
}

#endregion


class ECommerceDbContext : DbContext
{
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Department> Departments { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost, 1433;Database=ECommerceDb;User Id=sa;Password=Password1;TrustServerCertificate=True");
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>()
             .HasOne(e => e.Department)
             .WithMany(d => d.Employees)
             .HasForeignKey(d=>d.DId);      
    }


}