﻿
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using System.Reflection.Emit;

ApplicationDbContext context = new();

#region Injecting Service into Entity
var persons = await context.Persons.ToListAsync();
foreach (var person in persons)
{
    person.ToString();
}
#endregion

public class PersonServiceInjectionInterceptor : IMaterializationInterceptor
{
    public object InitializedInstance(MaterializationInterceptionData materializationData, object instance)
    {
        if (instance is IHasPersonService hasPersonService)
        {
            hasPersonService.PersonService = new PersonLogService();
        }

        return instance;
    }
}
public interface IHasPersonService
{
    IPersonLogService PersonService { get; set; }
}
public interface IPersonLogService
{
    void LogPerson(string name);
}
public class PersonLogService : IPersonLogService
{
    public void LogPerson(string name)
    {
        Console.WriteLine($"{name} isimli kişi loglanmıştır.");
    }
}
public class Person : IHasPersonService
{
    public int PersonId { get; set; }
    public string Name { get; set; }

    public override string ToString()
    {
        PersonService?.LogPerson(Name);
        return base.ToString();
    }

    [NotMapped]
    public IPersonLogService? PersonService { get; set; }
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

        optionsBuilder.AddInterceptors(new PersonServiceInjectionInterceptor());
    }
}