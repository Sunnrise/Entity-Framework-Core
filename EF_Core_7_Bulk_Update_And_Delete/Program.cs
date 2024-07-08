
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

ApplicationDbContext context = new();

#region Bulk Data Update Before EF Core 7
//var persons = await context.Persons.Where(p => p.PersonId > 5).ToListAsync();
//foreach (var person in persons)
//{
//    person.Name = $"{person.Name}...";
//}
//await context.SaveChangesAsync();
#endregion
#region Bulk Data Delete Before EF Core 7
//var persons = await context.Persons.Where(p => p.PersonId > 5).ToListAsync();
//context.RemoveRange(persons);
//await context.SaveChangesAsync();
#endregion


#region ExecuteUpdate
//await context.Persons.Where(p => p.PersonId > 3).ExecuteUpdateAsync(p => p.SetProperty(p => p.Name, v => v.Name + " yeni"));
//await context.Persons.Where(p => p.PersonId > 3).ExecuteUpdateAsync(p => p.SetProperty(p => p.Name, v => $"{v.Name} yeni"));
#endregion
#region ExecuteDelete
//await context.Persons.Where(p => p.PersonId > 3).ExecuteDeleteAsync();
#endregion

//When performing bulk update and delete operations with the ExecuteUpdate and ExecuteDelete functions, you do not need to call the

//SaveChanges

//function.
//Because these functions are Execute... functions. That is, they directly affect the database.

//If you want, you can also handle the functions of these functions in the process by taking the transaction control.

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
        optionsBuilder.UseSqlServer("Server=localhost, 1433;Database=ApplicationDb;User Id=sa;Password=Password1;TrustServerCertificate=True");
    }
}