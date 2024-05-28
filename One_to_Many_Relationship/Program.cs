using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

ECommerceDbContext context = new();

#region Default Convention
//in Default Convention, EF Core will create a foreign key column in the dependent entity (Employee) with the name DepartmentId (Navigation Property Name + Primary Key Name), if we don't specify the foreign key explicitly.
class Employee //Dependent Entity 
{
    public int Id { get; set; }
    public int DepartmentId { get; set; } //Foreign Key
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
#region Data Annotations



#endregion
#region Fluent API

#endregion


class ECommerceDbContext : DbContext
{
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Department> Departments { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost, 1433;Database=ECommerceDb;User Id=sa;Password=Password1;TrustServerCertificate=True");
    }


}