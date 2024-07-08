
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System;

ApplicationDbContext context = new();

//When working on the In-Memory database, there is no need to create a migration and migrate!

//In-Memory database, when the application is terminated/closed, it will be deleted from memory.

//Therefore, especially if you are using in-memory database in real applications, REMEMBER that it is not permanent but temporary, i.e. deletable!

#region Why do we need to work on In-Memory Database in EF Core
//Lecturer Gençay Yıldız, uses this feature to test new EF Core features.
//EF Core, instead of physical databases, allows us to create a Database in-memory and perform many operations on it. With this feature, we can quickly perform operations such as tests outside of real applications.
#endregion

#region What are advantages?
//Instead of creating and structuring real/physical databases in test and pre-prod applications, we can model all the data in memory and perform the necessary operations as if we were working on a real database.

//In-memory database, as it is a temporary experience, will prevent test databases created on database servers from occupying unnecessary space.

//Modeling the database in memory will allow the code to be tested quickly
#endregion

#region What are disadvantages?
//In-Memory database does not support relational modeling. This situation may cause data inconsistency and incorrect results statistically.
#endregion

#region Sample 
//Microsoft.EntityFrameworkCore.InMemory package must be installed to work with In-Memory Database.

await context.People.AddAsync(new Person { Name = "Alperen", Surname = "Güneş" });
await context.SaveChangesAsync();
var people=await context.People.ToListAsync();
Console.WriteLine();
#endregion

class Person
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
}

class ApplicationDbContext : DbContext
{
    public DbSet<Person> People { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
       optionsBuilder.UseInMemoryDatabase("InMemoryDatabase");
    }
}