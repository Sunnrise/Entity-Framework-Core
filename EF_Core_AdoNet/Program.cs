using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Reflection;

ApplicationDbContext context = new();
#region Database Property'si
//Property of Database represents the database and allows us to access some details of EF Core functions.
#endregion
#region BeginTransaction
//EF Core, manage the transaction automatically. If we want to manage the transaction manually, we can use the BeginTransaction function.

//IDbContextTransaction transaction = context.Database.BeginTransaction();
#endregion
#region CommitTransaction
//It is a function that allows the work done on EF Core to be committed.

//context.Database.CommitTransaction();
#endregion
#region RollbackTransaction
//It is a function that allows the work done on EF Core to be rolled back.

//context.Database.RollbackTransaction();
#endregion
#region CanConnect
//It checks whether the database can be connected according to the given connection string.

//bool connect = context.Database.CanConnect();
//Console.WriteLine(connect);
#endregion
#region EnsureCreated
//It provides us to create the designed database in EF Core without using migration, in runtime in the code.

//context.Database.EnsureCreated();
#endregion
#region EnsureDeleted
// It is a function that allows us to delete the built database at runtime.

//context.Database.EnsureDeleted();
#endregion
#region GenerateCreateScript
//It is a method that gives us a SQL Script as a string according to the database design made in the Context object.

//var script = context.Database.GenerateCreateScript();
//Console.WriteLine(script);
#endregion
#region ExecuteSql
//ExecuteSql uses parameterized queries to protect against SQL injection attacks. It is a method that allows us to write Insert, Update, and Delete queries to the database.

//string name = Console.ReadLine();
//var result = context.Database.ExecuteSql($"INSERT Persons VALUES('{name}')");
#endregion
#region ExecuteSqlRaw
//It is a method that allows us to write Insert, Update, and Delete queries to the database. In this method, the responsibility of protecting the query against SQL Injection attacks is on the developer.

//string name = Console.ReadLine();
//var result = context.Database.ExecuteSqlRaw($"INSERT Persons VALUES('{name}')");
#endregion
#region SqlQuery
//SqlQuery function is accessible, but it is no longer supported. Instead, the FromSql function, which can be accessed via the DbSet property, has been introduced/is used.
#endregion
#region SqlQueryRaw
//SqlQueryRaw function is accessible, but it is no longer supported. Instead, the FromSqlRaw function, which can be accessed via the DbSet property, has been introduced/is used.
#endregion
#region GetMigrations
//It is a method that allows us to programmatically obtain all migrations produced in the application.

//var migs = context.Database.GetMigrations();
//Console.WriteLine();
#endregion
#region GetAppliedMigrations
// It is a function that allows us to obtain all migrations that have been migrated to the Database.

//var migs = context.Database.GetAppliedMigrations();
//Console.WriteLine();
#endregion
#region GetPendingMigrations
//It is a function that allows us to obtain all migrations that have not been migrated to the Database.

//var migs = context.Database.GetPendingMigrations();
//Console.WriteLine();
#endregion
#region Migrate
// It is a function used to migrate migrations programmatically at runtime.

//context.Database.Migrate();

//EnsureCreated Function does not include migrations. Therefore, the work done in migrations will not be valid in the relevant function.
//EnsureCreated cant send the migration to the database. It only creates the database from the context.
#endregion
#region OpenConnection
//It opens the database connection manually.

//context.Database.OpenConnection();
#endregion
#region CloseConnection
//It closes the database connection manually.

//context.Database.CloseConnection();
#endregion
#region GetConnectionString
//It allows you to obtain the connectionstring value used by the relevant context object at that moment.

//Console.WriteLine(context.Database.GetConnectionString());
#endregion
#region GetDbConnection
//It is a function that allows us to obtain the DbConnection object used by the Ado.NET infrastructure used by EF Core. In other words, it takes us to the Ado.NET side.


//SqlConnection connection = (SqlConnection)context.Database.GetDbConnection();
//Console.WriteLine();
#endregion
#region SetDbConnection
//A function that allows us to include customized connection objects in the EF Core architecture.
//For example; we can use it when we want to create a connection object and include this object in the EF Core architecture.

//context.Database.SetDbConnection();
#endregion
#region ProviderName Property
//It is a property that returns the information of the provider used by EF Core.

//Console.WriteLine(context.Database.ProviderName);
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
    public int Price { get; set; }

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


