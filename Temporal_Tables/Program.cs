﻿using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Security.Cryptography.X509Certificates;

ApplicationDbContext context = new() ;

#region What is Temporal Tables?
//Temporal Tables are tables that store records in the process of data change and are used for analyzing table data at different points in time and managed by the system.
//It is supported with EF Core 6.0.
#endregion
#region How can we work with Temporal Tables feature?
//Temporal tables can be created and generated in the database through migration structures in EF Core.
//We can convert the existing tables to Temporal Tables with the migration structure.
//We can easily query the past of any table data.
//The state/data of a data in any table at any T time in the past can be retrieved.
#endregion
#region How can apply Temporal Table?

#region IsTemoral structuring
//Ef Core 6.0 has a new method called IsTemporal() that can be used to create a Temporal Table. If that table already exists, it can be converted to a Temporal Table.
#endregion
#region Examining the migration generated for the Temporal Table

#endregion
#endregion
#region Let's test the Temporal Table

#region Data Inserting
//When we insert a record into the table, history table cant keep the record.We only see the between specific time range values not start and end time.
//var persons =new List<Person>
//{
//    new Person { Name = "John", Surname = "Doe", BirthDate = new DateTime(1990, 1, 1) },
//    new Person { Name = "Jane", Surname = "Doe", BirthDate = new DateTime(1991, 1, 1) },
//    new Person { Name = "Jack", Surname = "Doe", BirthDate = new DateTime(1992, 1, 1) }
//};
//await context.Persons.AddRangeAsync(persons);
//await context.SaveChangesAsync();
#endregion
#region Data Update
// When we update a record in the table, the history table keeps the old record. We only see the between specific time range values not start and end time.

//var person =await context.Persons.FindAsync(2);
//person.Name = "Janeee";
//await context.SaveChangesAsync();

#endregion
#region Data Deleting
//var person = await context.Persons.FindAsync(2);
//context.Persons.Remove(person);
//await context.SaveChangesAsync();
#endregion
#endregion
#region Querying Historical Data with Temporal Table

#region TemporalAsOf
//It returns the updated data of the table at the specified time.

//var datas =await context.Persons.TemporalAsOf(DateTime.UtcNow).Select(p => new
//{
//    p.Id,
//    p.Name,
//    PeriodStart = EF.Property<DateTime>(p, "PeriodStart"),
//    PeriodEnd = EF.Property<DateTime>(p, "PeriodEnd")
//}).ToListAsync();

//foreach (var data in datas)
//{
//    System.Console.WriteLine($"Id: {data.Id}, Name: {data.Name}, PeriodStart: {data.PeriodStart}, PeriodEnd: {data.PeriodEnd}");
//}
#endregion

#region TemporalAll
//It returns all the data(present and past) of the table at the specified time.

//var datas = await context.Persons.TemporalAll().Select(p => new
//{
//    p.Id,
//    p.Name,
//    PeriodStart = EF.Property<DateTime>(p, "PeriodStart"),
//    PeriodEnd = EF.Property<DateTime>(p, "PeriodEnd")
//}).ToListAsync();

//foreach (var data in datas)
//{
//    System.Console.WriteLine($"Id: {data.Id}, Name: {data.Name}, PeriodStart: {data.PeriodStart}, PeriodEnd: {data.PeriodEnd}");
//}
#endregion

#region TemporalFromTo
//It returns the data of the table between the specified time range. Start and End time are not included.

//var StartDate = new DateTime(2024, 07, 07, 09, 58, 58);
//var EndDate = new DateTime(2024, 07, 07, 10, 01, 03);

////2024-07-07 09:58:58.2379530
////2024-07-07 10:01:03.4062148

//var datas = await context.Persons.TemporalFromTo(StartDate,EndDate).Select(p => new
//{
//    p.Id,
//    p.Name,
//    PeriodStart = EF.Property<DateTime>(p, "PeriodStart"),
//    PeriodEnd = EF.Property<DateTime>(p, "PeriodEnd")
//}).ToListAsync();

//foreach (var data in datas)
//{
//    System.Console.WriteLine($"Id: {data.Id}, Name: {data.Name}, PeriodStart: {data.PeriodStart}, PeriodEnd: {data.PeriodEnd}");
//}
#endregion
#region TemporalBetween
//It returns the data of the table between the specified time range. Start time is included however End time is not included.

//var StartDate = new DateTime(2024, 07, 07, 09, 58, 58);
//var EndDate = new DateTime(2024, 07, 07, 10, 01, 03);

////2024-07-07 09:58:58.2379530
////2024-07-07 10:01:03.4062148

//var datas = await context.Persons.TemporalBetween(StartDate, EndDate).Select(p => new
//{
//    p.Id,
//    p.Name,
//    PeriodStart = EF.Property<DateTime>(p, "PeriodStart"),
//    PeriodEnd = EF.Property<DateTime>(p, "PeriodEnd")
//}).ToListAsync();

//foreach (var data in datas)
//{
//    System.Console.WriteLine($"Id: {data.Id}, Name: {data.Name}, PeriodStart: {data.PeriodStart}, PeriodEnd: {data.PeriodEnd}");
//}
#endregion
#region TemporalContainedIn
//It returns the data of the table between the specified time range. Start time and End time are included.

//var StartDate = new DateTime(2024, 07, 07, 09, 58, 58);
//var EndDate = new DateTime(2024, 07, 07, 10, 01, 03);

////2024-07-07 09:58:58.2379530
////2024-07-07 10:01:03.4062148

//var datas = await context.Persons.TemporalContainedIn(StartDate, EndDate).Select(p => new
//{
//    p.Id,
//    p.Name,
//    PeriodStart = EF.Property<DateTime>(p, "PeriodStart"),
//    PeriodEnd = EF.Property<DateTime>(p, "PeriodEnd")
//}).ToListAsync();

//foreach (var data in datas)
//{
//    System.Console.WriteLine($"Id: {data.Id}, Name: {data.Name}, PeriodStart: {data.PeriodStart}, PeriodEnd: {data.PeriodEnd}");
//}
#endregion
#endregion
#region Bringing Back the Deleted Data with Temporal Table 
//Fistly, We have to detect the date of the deleted data and then we can bring back the data with the Temporal Table feature. Then, we can get the past value of the deleted data with TemporalAsOf method. If we want, we can carry to the physical table with the Add method.

//Deleting time: 2024-07-07 10:01:03.4062148
var dateOfDelete=await context.Persons.TemporalAll()
    .Where(p=>p.Id == 2)
    .OrderByDescending(p => EF.Property<DateTime>(p, "PeriodEnd"))
    .Select(p=>EF.Property<DateTime>(p, "PeriodEnd"))
    .FirstAsync();

var deletedPerson = await context.Persons.TemporalAsOf(dateOfDelete.AddMilliseconds(-1))
    .FirstOrDefaultAsync(p=>p.Id==2);

await context.Persons.AddAsync(deletedPerson);
await context.Database.OpenConnectionAsync();
await context.Database.ExecuteSqlInterpolatedAsync($"SET IDENTITY_INSERT dbo.Persons ON");
await context.SaveChangesAsync();
await context.Database.ExecuteSqlInterpolatedAsync($"SET IDENTITY_INSERT dbo.Persons OFF");

#region SET IDENTITY_INSERT Configuration
// If we want to insert the deleted data with Id to into the table, we have to set the IDENTITY_INSERT configuration to true. 
#endregion
#endregion

class Person
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public DateTime BirthDate { get; set; }
}
class Employee
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
}
class ApplicationDbContext : DbContext
{
    public DbSet<Person> Persons { get; set; }
    public DbSet<Employee> Employees { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>().ToTable("Employees",builder=> builder.IsTemporal());
        modelBuilder.Entity<Person>().ToTable("Persons", builder => builder.IsTemporal());
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost, 1433;Database=ApplicationDb;User Id=sa;Password=Password1;TrustServerCertificate=True");
    }
}