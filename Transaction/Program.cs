
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using System.Reflection.Metadata;
using System.Transactions;

ApplicationDbContext context = new();
#region What is Transaction?
//Transaction is a feature that allows us to perform cumulative operations in the database atomically.

//All operations within a transaction will be physically reflected in the database if they are committed. Or if it is rolled back, all operations will be rolled back and there will be no data change in the database.

//The general purpose of the Transaction is to maintain the consistency of the database. Or in other words, it is to take precautions against inconsistent situations in the database.
#endregion

#region Default Transaction Behaviour
//By default, all operations are applied to the database physically with the SaveChanges function in EF Core.
//Because SaveChanges has a transaction by default.
//If there is a problem/failure during this process, all operations are rolled back and none of the operations are applied to the database.
//Thus, SaveChanges indicates that all operations will either be completely successful or if an error occurs, the operations will be terminated without changing the database.
#endregion

#region Manuel Transaction Control
//IDbContextTransaction transaction =await context.Database.BeginTransactionAsync();

////In EF Core, we can start a transaction manually with the BeginTransaction function.

//Person p = new() { Name = "Alperen" };
//await context.Persons.AddAsync(p);
//await context.SaveChangesAsync();
//await transaction.CommitAsync();
#endregion  

#region Savepoints
//It comes with EF Core 5.0.
//Savepoints are a feature that allows us to create a point in the transaction and roll back to that point if necessary.
//Savepoints are not supported in all databases. It is supported in SQL Server, PostgreSQL, and SQLite.
//We can use whatever we want in the database that supports Savepoints.
#region CreateSavepoint
//We can create a savepoint with the CreateSavepoint function.
#endregion

#region RollbackToSavepoint
//We can roll back to the savepoint with the RollbackToSavepoint function.
#endregion

//IDbContextTransaction transaction =await context.Database.BeginTransactionAsync();
//Person p10 =await context.Persons.FindAsync(10);
//Person p12 = await context.Persons.FindAsync(12);
//context.Persons.RemoveRange(p10, p12);
//await context.SaveChangesAsync();

//await transaction.CreateSavepointAsync("Savepoint1");
//Person p9= await context.Persons.FindAsync(9);
//context.Persons.Remove(p9);
//await context.SaveChangesAsync();
//transaction.RollbackToSavepoint("Savepoint1");
//await transaction.CommitAsync();
#endregion

#region TransactionScope  
// It provides to manage database processes as a group.
//We use too with ADO.NET,
using TransactionScope transactionScope = new();
//Database Processes
//.....
//....
transactionScope.Complete();// It is used to commit the transaction.
//If the Complete function is not called, the transaction is rolled back. 
#region Complete

#endregion
#endregion

public class Person
{
    public int PersonId { get; set; }
    public string Name { get; set; }

    public ICollection<Order> Orders { get; set; }
}
public class Order
{
    public int OrderId { get; set; }
    public int PersonId { get; set; }
    public string Description { get; set; }

    public Person Person { get; set; }
}
class ApplicationDbContext : DbContext
{
    public DbSet<Person> Persons { get; set; }
    public DbSet<Order> Orders { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        modelBuilder.Entity<Person>()
            .HasMany(p => p.Orders)
            .WithOne(o => o.Person)
            .HasForeignKey(o => o.PersonId);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost, 1433;Database=ApplicationDb;User Id=sa;Password=Password1;TrustServerCertificate=True");
    }
}