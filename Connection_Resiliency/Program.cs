using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Storage;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

ApplicationDbContext context = new();

#region What is Connection Resiliency?
//During the database operations in EF Core, disconnections/interruptions may occur in the database connection.

//With Connection Resiliency, we can make the necessary reconnection requests to reconnect the disconnected connection and, on the other hand, we can trigger the queries that will be repeated from the beginning to the end in case of disconnection by determining the behavior models called execution strategy.
#endregion

#region EnableRetryOnFailure

//while (true)
//{
//    await Task.Delay(1000);
//    var persons= await context.Persons.ToListAsync();
//    persons.ForEach(p => Console.WriteLine(p.Name));
//    Console.WriteLine("***************");
//}

#region MaxRetryCount
//It is the maximum number of retries that can be made when a connection is lost. 
//Default value is 6.
#endregion

#region MaxRetryDelay
//It is the maximum time to wait between retries.
//Default value is 30 seconds.
#endregion
#endregion

#region Execution Strategies
//Execution strategies are the behavior models that determine how the queries that will be repeated from the beginning to the end in case of disconnection will be triggered.

//There are two types of execution strategies in EF Core: Default Execution Strategy and Custom Execution Strategy. 

#region Default Execution Strategy
//Default Execution Strategy is the behavior model that determines how the queries that will be repeated from the beginning to the end in case of disconnection will be triggered. 
//If we use EnableRetryOnFailure, the default execution strategy will be used.
//MaxRetryCount : 6
//MaxRetryDelay : 30 seconds
//If we want to use default values , we should use parameterless EnableRetryOnFailure method.

#endregion

#region Custom Execution Strategy

#region Initilization

#endregion
#region Using - ExecutionStrategy
//while (true)
//{
//    await Task.Delay(1000);
//    var persons = await context.Persons.ToListAsync();
//    persons.ForEach(p => Console.WriteLine(p.Name));
//    Console.WriteLine("***************");
//}
#endregion

#endregion

#region When the connection is lost, all the work that needs to be executed must be reprocessed
//If we want to reprocess all the work that needs to be executed when the connection is lost,
// Execute and ExecuteAsync methods should be overridden in the CustomExecutionStrategy class. 

//Execute method processes the work that needs to be executed synchronously until commit. If the connection is lost before commit, it will be reprocessed.
var strategy = context.Database.CreateExecutionStrategy();
await strategy.ExecuteAsync(async () =>
{
    using var transaction=  await context.Database.BeginTransactionAsync();
    await context.Persons.AddAsync(new Person { Name = "Halil" });
    await context.SaveChangesAsync();

    await context.Persons.AddAsync(new Person { Name = "Kadir" });
    await context.SaveChangesAsync();
    
    await transaction.CommitAsync();
});

#endregion

#region Which Cases Should Execution Strategy Be Used?
// Some databases password changes, network problems, and database server restarts can cause connection interruptions.
#endregion
#endregion

public class Person
{
    public int PersonId { get; set; }
    public string Name { get; set; }
}
class ApplicationDbContext : DbContext
{
    public DbSet<Person> Persons { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        #region Default Execution Strategy
        //optionsBuilder.UseSqlServer("Server=localhost, 1433;Database=ApplicationDb;User Id=sa;Password=Password1;TrustServerCertificate=True", builder => builder.EnableRetryOnFailure(
        //    maxRetryCount: 4,
        //    maxRetryDelay: TimeSpan.FromSeconds(20),
        //    errorNumbersToAdd: new[] { 4060 }))
        //    .LogTo(
        //    filter: (eventId, level) => eventId.Id == CoreEventId.ExecutionStrategyRetrying,
        //    logger: eventData =>
        //    Console.WriteLine($"Trying to connect again..."));
        #endregion
        #region Custom Execution Strategy
        optionsBuilder.UseSqlServer("Server=localhost, 1433;Database=ApplicationDb;User Id=sa;Password=Password1;TrustServerCertificate=True",builder=>
        builder.ExecutionStrategy(dependencies => new CustomExecutionStrategy(dependencies, 4, TimeSpan.FromSeconds(20))));
        #endregion

    }
}
class CustomExecutionStrategy : ExecutionStrategy
{
    public CustomExecutionStrategy(DbContext context, int maxRetryCount, TimeSpan maxRetryDelay) 
        : base(context, maxRetryCount, maxRetryDelay)
    {
    }

    public CustomExecutionStrategy(ExecutionStrategyDependencies dependencies, int maxRetryCount, TimeSpan maxRetryDelay) 
        : base(dependencies, maxRetryCount, maxRetryDelay)
    {
    }
    int retryCount = 0;
    protected override bool ShouldRetryOn(Exception exception)
    {
        //When the connection is lost, all the work that needs to be executed must be reprocessed
        Console.WriteLine($"Try to connect again {++retryCount}");
        return true;
    }
}


