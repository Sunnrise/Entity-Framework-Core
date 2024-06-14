﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

ApplicationDbContext context = new();

#region What is the Generated Value ?
// Generated Value is a value that is automatically generated by the database or the application.
#endregion

#region Default Values
// Default values are the values that are automatically assigned to a column when a new row is inserted into the table.
#region HasDefaultValue
//Static value that will be used as the default value.
#endregion

#region HasDefaultValueSql
//SQL query that will be executed to get the default value.
#endregion

#endregion

#region Computed Columns

#region HasComputedColumnSql
//SQL query that will be executed to get the computed value.
#endregion

#endregion

#region Value Generation

#region Primary Keys
//Primary keys are the unique identifiers of the rows in a table.
#endregion

#region Identity
//Identity columns are the columns that are automatically incremented by the database when a new row is inserted into the table. We dont have to use Identity for the primary key. We can use only one column as Identity.
#endregion
//Identity is the default value generation strategy for primary keys in Entity Framework Core.
#region DatabaseGenerated

#region DatabaseGeneratedOption.None - ValueGeneratedNever
//The value is never generated by the database or the application.
//We use this option for disable the Identity's default value generation strategy for primary keys.
#endregion

#region DatabaseGeneratedOption.Identity - ValueGeneratedOnAdd
//The value is generated by the database when a new row is inserted into the table.
#region Numeric Types
//The value is automatically incremented by 1 for each new row. We have to use only one column as Identity. For this reason, should use ValueGeneratedNever for the primary key.(None) 
#endregion

#region Non-Numeric Types
//The value is generated by the database using a different strategy. For example, the value can be a GUID or a string. We can use multiple columns as Identity.
#endregion

#endregion

#region DatabaseGeneratedOption.Computed - ValueGeneratedOnAddOrUpdate
//The value is generated by the database when a new row is inserted into the table or an existing row is updated. The value is computed based on the values of other columns in the row. We can use multiple columns as Computed.
#endregion

#endregion

#endregion

Person o = new()
{
    PersonId = 1,
    Name = "Alperen",
    Surname = "Gunes",
    Premium = 100,
    TotalGain = 200
   

};
await context.Persons.AddAsync(o);
await context.SaveChangesAsync();


class Person
{
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int PersonId { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public int Premium { get; set; }
    public int Salary { get; set; }
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public int TotalGain { get; set; }
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid PersonCode { get; set; }
}

class ApplicationDbContext : DbContext
{
    public DbSet<Person> Persons { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
       modelBuilder.Entity<Person>()
            .Property(p => p.Salary)
            .HasDefaultValueSql("FLOOR(RAND() * 1000)");
        //.HasDefaultValue(100);
        modelBuilder.Entity<Person>()
            .Property(p => p.TotalGain)
            .HasComputedColumnSql("([Premium] + [Salary])*10")
            .ValueGeneratedOnAddOrUpdate();
        modelBuilder.Entity<Person>()
            .Property(p => p.PersonId)
            .ValueGeneratedNever();
        modelBuilder.Entity<Person>()
            .Property(p => p.PersonCode)
            .HasDefaultValueSql("NEWID()");
        modelBuilder.Entity<Person>()
            .Property(p => p.PersonCode)
            .ValueGeneratedOnAdd();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost, 1433;Database=ApplicationDb;User Id=sa;Password=Password1;TrustServerCertificate=True");
    }
}