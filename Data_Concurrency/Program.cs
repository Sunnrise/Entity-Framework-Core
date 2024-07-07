using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Reflection.Emit;
ApplicationDbContext context = new();

#region What is Data Concurrency?
//In the applications we develop, inconsistencies can occur in terms of data. For example; In cases where multiple applications or clients work synchronously on the same database, inconsistencies can occur in terms of data from application to application or from client to client

//The Data Concurrency concept is a concept that covers behaviors that will provide manageability in response to data inconsistency situations in applications.

//Having data inconsistency in an application means misleading the users of that application.
//In applications where data inconsistency occurs, statistically incorrect results can be obtained...
#endregion
#region What is Stale & Dirty Data?
//Stale Data: It refers to the data that has not been updated or has expired, which can cause data inconsistency. For example; If the stock status of a product is zero, but there is no update situation that indicates this on the interface, this is an example of stale data.

//Dirty Data :It refers to the data that is incorrect or wrong, which can cause data inconsistency. For example; If a user named 'Ahmet' is kept in the database as 'Mehmet', this is an example of dirty data.
#endregion
#region Last In Wins
//Last In Wins is a term that refers to the most recent action in a data structure being at the top/maintaining its existence.
#endregion
#region Pessimistic Lock

//Pessimistic lock is a method that provides resistance to change by locking the relevant data to prevent changes with different queries on the data obtained in a transaction process.

//The locking of this data is limited to the commit or rollback of the relevant transaction.

#region What is Deadlock?
//Deadlock is a term that refers to the inability to resolve the lock of a locked data due to a systemic error that occurs at the database level or the occurrence of a cyclic locking situation.

//In the Pessimistic Lock method, it is possible to experience a deadlock situation. Therefore, it is an approach that needs to be evaluated and should be preferred by thinking well.
#endregion
#region WITH (XLOCK)
//await using var transaction = await context.Database.BeginTransactionAsync();
//var data = await context.Persons.FromSql($"SELECT * FROM Persons WITH (XLOCK) WHERE PersonID = 5")
//    .ToListAsync();
//Console.WriteLine();
//await transaction.CommitAsync();
#endregion
#endregion
#region Optimistic Lock

//Optimistic lock is an approach that allows us to work in the version logic without any locking process to understand whether a data is stale or not.
//In the Optimistic lock approach, changes that may cause inconsistency in the data are not physically prevented. In other words, the data can be changed in a way that will cause inconsistency.

//However, in the Optimistic lock approach, we use the version information to track the inconsistency situation on these data. We use it like this;

//A version information is generated for each data. This information can be textual or numerical. This version information will be updated as a result of every change made on the data. Therefore, we prefer it to be numerical to make this update easier.

//When querying data through EF Core, we also take the version information of the relevant data into in-memory. Then, if a change is made to the data, we compare this in-memory version information with the version information in the database. If this comparison is verified, the action taken will be valid, otherwise, if it is not verified, it means that the value of the data has changed, which means that there is an inconsistency situation. In this case, an error will be thrown and the action will not be performed.

//EF Core has a structural feature for the Optimistic lock approach.

#region Property Based Configuration (ConcurrencyCheck Attribute)
//Properties that are desired to be checked for data consistency are marked with the ConcurrencyCheck attribute. As a result of this marking, a token value will be generated in-memory for each instance of the entity. This generated token value will be validated by EF Core in the action processes taken, and if there is no change, the action will be successfully completed. However, if there is any change in the transaction process on the relevant data (in the properties marked with the ConcurrencyCheck attribute), then the token generated will be changed and consequently it will be understood that it is not valid in the validation process, so it will be understood that there is a data inconsistency situation and an error will be thrown.

//var person = await context.Persons.FindAsync(3);
//context.Entry(person).State = EntityState.Modified;
//await context.SaveChangesAsync();

#endregion
#region RowVersion Column
//In this approach, a version information is physically created for each row in the database.
var person = await context.Persons.FindAsync(3);
context.Entry(person).State = EntityState.Modified;
await context.SaveChangesAsync();
#endregion
#endregion

public class Person
{
    public int PersonId { get; set; }
    //[ConcurrencyCheck]
    public string Name { get; set; }
    //[Timestamp]
    public byte[] RowVersion { get; set; }
}
class ApplicationDbContext : DbContext
{
    public DbSet<Person> Persons { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        //modelBuilder.Entity<Person>().Property(p => p.Name).IsConcurrencyToken();
        modelBuilder.Entity<Person>().Property(p => p.RowVersion).IsRowVersion();
    }
    readonly ILoggerFactory _loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {


        optionsBuilder.UseSqlServer("Server=localhost, 1433;Database=ApplicationDb;User Id=sa;Password=Password1;TrustServerCertificate=True")
            .UseLoggerFactory(_loggerFactory); 
    }
}