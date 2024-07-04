using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

ApplicationDbContext context = new();
#region What is Stored Procedure?
//Stored Procedures, are a set of SQL statements that are stored on the database server and can be executed on the database server. Stored procedures are used to encapsulate a set of operations or queries to execute on the database server. Stored procedures can accept input parameters and return multiple values in the form of output parameters to the calling program or script.
// Views act as virtual tables, but stored procedures act as virtual functions. Stored procedures can accept input parameters and return multiple values in the form of output parameters to the calling program or script.

#endregion

#region Stored Procedure Using with Entity Framework Core

#region Stored Procedure Creation
//1. Step: Create a empty migration
//2. Step: Configure the stored procedure in the migration file with up and down method
//3. Step: Update the database
#endregion
#region using Stored Procedure 
// For using stored procedure in Entity Framework Core, we can use the following methods.
// We create a entity and dbset  for the stored procedure and use the entity to get the data from the stored procedure.
// then we use dbset property and then FromSql method to get the data from the stored procedure.
#region FromSql
//var datas = await context.PersonOrders.FromSqlRaw($"EXECUTE sp_GetPersonOrders").ToListAsync();
#endregion
#endregion

#region Return value Stored Procedure using

//SqlParameter countParameter = new()
//{
//    ParameterName = "@count",
//    SqlDbType = System.Data.SqlDbType.Int,
//    Direction = System.Data.ParameterDirection.Output
//};
//await context.Database.ExecuteSqlRawAsync($"EXEC @count = sp_bestSellingStaff", countParameter);
//Console.WriteLine(countParameter.Value);
#endregion

#region Parameters with Stored Procedure 
#region Input Parameters with  Stored Procedure

#endregion

#region Output Parameters with Stored Procedure

#endregion
SqlParameter nameParameter = new()
{
    ParameterName = "@name",
    SqlDbType = System.Data.SqlDbType.NVarChar,
    Size = 100,
    Direction = System.Data.ParameterDirection.Output
};
await context.Database.ExecuteSqlRawAsync($"EXECUTE sp_PersonOrders2 5, @name OUTPUT", nameParameter);
Console.WriteLine(nameParameter.Value);
#endregion
#endregion
Console.WriteLine();
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


public class PersonOrder
{
    public string Name { get; set; }
    public int Count { get; set; }
}
class ApplicationDbContext : DbContext
{
    public DbSet<Person> Persons { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<PersonOrder> PersonOrders { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        modelBuilder.Entity<PersonOrder>()
            .HasNoKey();
        modelBuilder.Ignore<PersonOrder>();


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


