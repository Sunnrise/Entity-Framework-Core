using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

ECommerceDbContext context = new();

#region Default Convention

class Employee //Dependent Entity
{
    public int Id { get; set; }
    public string Name { get; set; }
}
class Department
{
    public int Id { get; set; }
    public string DepartmentName { get; set; }
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