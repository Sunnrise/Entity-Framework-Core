using Complex_Relational_Queries.Configurations;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

ApplicationDbContext context = new();

#region Complex Query Operators

#region Join

#region Query Syntax
//var query1= from photo in context.Photos
//           join person in context.Persons
//           on photo.PersonId equals person.PersonId
//           select new
//           {
//               person.Name,
//               photo.Url
//           };
//var datas=await query1.ToListAsync();
#endregion

#region Method Syntax
//var query2 = context.Photos
//    .Join(context.Persons,
//    photo => photo.PersonId,
//    person => person.PersonId,
//    (photo, person) => new
//    {
//        person.Name,
//        photo.Url
//    });
//var datas2 = await query2.ToListAsync();
#endregion

#region Multiple Columns Join

#region Query Syntax
var query1= from photo in context.Photos
            join person in context.Persons
                on new {photo.PersonId, photo.Url }equals new {person.PersonId,Url=person.Name }
            select new
            {
               person.Name,
               photo.Url
            };
var datas1 = await query1.ToListAsync();
#endregion

#region Method Syntax
var query2 = context.Photos
    .Join(context.Persons,
    photo=> new 
    { 
        photo.PersonId, 
        photo.Url 
    },
    person=> new
    {
        person.PersonId,
        Url = person.Name
    },
    (photo, person) =>new
    {
        person.Name,
        photo.Url
    });
var datas2 = await query2.ToListAsync();
#endregion
#endregion

#region Tabloyla Join with more than 2 tables

#region Query Syntax

#endregion
#region Method Syntax

#endregion
#endregion

#region Group Join - Not GroupBy!

#endregion
#endregion

#region Left Join

#endregion

#region Right Join

#endregion

#region Full Join

#endregion

#region Cross Join

#endregion

#region Where using on Collection Selector

#endregion

#region Cross Apply

#endregion

#region Outer Apply

#endregion
#endregion
Console.WriteLine();
public class Photo
{
    public int PersonId { get; set; }
    public string Url { get; set; }

    public Person Person { get; set; }
}
public enum Gender { Man, Woman }
public class Person
{
    public int PersonId { get; set; }
    public string Name { get; set; }
    public Gender Gender { get; set; }

    public Photo Photo { get; set; }
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
    public DbSet<Photo> Photos { get; set; }
    public DbSet<Person> Persons { get; set; }
    public DbSet<Order> Orders { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        modelBuilder.Entity<Photo>()
            .HasKey(p => p.PersonId);

        modelBuilder.Entity<Person>()
            .HasOne(p => p.Photo)
            .WithOne(p => p.Person)
            .HasForeignKey<Photo>(p => p.PersonId);

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
