using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;
using System.Reflection.Emit;

ApplicationDbContext context = new();

#region Owned Entity Types Nedir?
//EF Core allows us to split entity classes by hosting their properties in different classes as a set and combining all these classes in the relevant entity to work holistically.

//Thus, an entity can be formed by the combination of more than one owned sub-class.
#endregion
#region Why we use Owned Entity Types?
//https://www.gencayyildiz.com/blog/wp-content/uploads/2020/12/Entity-Framework-Core-Owned-Entities-and-Table-Splitting.png

//Owned Entity Types are used in the Domain Driven Design (DDD) approach as counterparts to Value Objects!
#endregion
#region How Owned Entity Types are appyling?
//Referencing different classes in a normal entity will cause errors such as primary key, etc.

//Because taking a class directly as a reference is perceived by ef core as a relational design. It is especially necessary for us to declare that the classes that contain the properties of the entity as a set are part of that entity.

#region OwnsOne Method

#endregion
#region Owned Attribute
//We can use the Owned attribute to define a class as an owned
#endregion
#region IEntityTypeConfiguration<T> Interface

#endregion

#region OwnsMany Method

//The OwnsMany method has a function that allows us to access the different properties of the entity from another class through a Navigation Property of type ICollection in a relational way.

//The basic difference of this relationship, which can normally be established as a Has relationship, is that while the Has relationship requires a DbSet property, the OwnsMany method allows us to do it without the need for DbSet.

//var d = await context.Employees.ToListAsync();
//Console.WriteLine();
#endregion
#endregion
#region Nested Owned Entity Types

#endregion
#region Constraints

#endregion

class Employee
{
    public int Id { get; set; }
    //public string Name { get; set; }
    //public string MiddleName { get; set; }
    //public string LastName { get; set; }
    //public string StreetAddress { get; set; }
    //public string Location { get; set; }
    public bool IsActive { get; set; }

    public EmployeeName EmployeeName { get; set; }
    public Address Adress { get; set; }

}
//[Owned]
class EmployeeName
{
    public string Name { get; set; }
    public string MiddleName { get; set; } 
    public string LastName { get; set; }

}
//[Owned]
class Address
{
    public string StreetAddress { get; set; }
    public string Location { get; set; }
}
class ApplicationDbContext : DbContext
{
    public DbSet<Employee> Employees { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        #region OwnsOne
        //modelBuilder.Entity<Employee>().OwnsOne(e => e.EmployeeName, builder =>
        //{
        //    builder.Property(e => e.Name).HasColumnName("Name");
        //});
        //modelBuilder.Entity<Employee>().OwnsOne(e => e.Adress);
        #endregion
        #region IEntityTypeConfiguration<T> Interface
        modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
        #endregion

    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost, 1433;Database=ApplicationDb;User Id=sa;Password=Password1;TrustServerCertificate=True");
    }
    class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.OwnsOne(e => e.EmployeeName,builder=>
            {
                builder.Property(e => e.Name).HasColumnName("Name");
            });
            builder.OwnsOne(e => e.Adress);
        }
            
    }
}




