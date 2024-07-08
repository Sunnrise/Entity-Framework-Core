using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

ApplicationDbContext context = new();

#region Entity Splitting
// Entity Splitting is a feature that allows us to represent multiple physical tables with a single entity in Entity Framework Core.
#endregion
#region Example
#region Adding Data
Person person = new()
{
Name = "Alperen",
Surname = "Güneş",
City = "Antalya",
Country = "Türkiye",
PhoneNumber = "1234567890",
PostCode = "1234567890",
Street = "..."
};

//await context.Persons.AddAsync(person);
//await context.SaveChangesAsync();
#endregion
#region Reading Data
person = await context.People.FindAsync(2);
Console.WriteLine();
#endregion
#endregion
public class Person
{
    #region People Table
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }

    #endregion
    #region PhoneNumbers Table
    public string? PhoneNumber { get; set; }
    #endregion
    #region Addresses Tablosu
    public string Street { get; set; }
    public string City { get; set; }
    public string? PostCode { get; set; }
    public string Country { get; set; }
    #endregion
}
class ApplicationDbContext : DbContext
{
    public DbSet<Person> People { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Person>(entityBuilder =>
        {
            entityBuilder.ToTable("Persons")
                .SplitToTable("PhoneNumbers", tableBuilder =>
                {
                    tableBuilder.Property(person => person.Id).HasColumnName("PersonId");
                    tableBuilder.Property(person => person.PhoneNumber);
                })
                .SplitToTable("Addresses", tableBuilder =>
                {
                    tableBuilder.Property(person => person.Id).HasColumnName("PersonId");
                    tableBuilder.Property(person => person.Street);
                    tableBuilder.Property(person => person.City);
                    tableBuilder.Property(person => person.PostCode);
                    tableBuilder.Property(person => person.Country);
                });
        });
    }
protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost, 1433;Database=ApplicationDb;User Id=sa;Password=Password1;TrustServerCertificate=True");
    }
}