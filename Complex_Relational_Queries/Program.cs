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
//var query1= from photo in context.Photos
//            join person in context.Persons
//                on new {photo.PersonId, photo.Url }equals new {person.PersonId,Url=person.Name }
//            select new
//            {
//               person.Name,
//               photo.Url
//            };
//var datas1 = await query1.ToListAsync();
#endregion

#region Method Syntax
//var query2 = context.Photos
//    .Join(context.Persons,
//    photo=> new 
//    { 
//        photo.PersonId, 
//        photo.Url 
//    },
//    person=> new
//    {
//        person.PersonId,
//        Url = person.Name
//    },
//    (photo, person) =>new
//    {
//        person.Name,
//        photo.Url
//    });
//var datas2 = await query2.ToListAsync();
#endregion
#endregion

#region Join with more than 2 tables

#region Query Syntax
//var query1 = from photo in context.Photos
//            join person in context.Persons
//                on photo.PersonId equals person.PersonId
//            join order in context.Orders
//                on person.PersonId equals order.PersonId
//            select new
//                {
//                    person.Name,
//                    photo.Url,
//                    order.Description
//                };
//var datas1 = await query1.ToListAsync();
#endregion
#region Method Syntax
//var query2 = context.Photos
//    .Join(context.Persons,
//    photo=> photo.PersonId,
//    person=> person.PersonId,
//    (photo,person)=>new
//    {
//        person.PersonId,
//        person.Name,
//        photo.Url 
//    })
//    .Join(context.Orders,
//    personPhotos => personPhotos.PersonId,
//    order => order.PersonId,
//    (personPhotos, order) => new
//    {
//        personPhotos.Name,
//        personPhotos.Url,
//        order.Description
//    });
//var datas2=await query2.ToListAsync();
#endregion
#endregion

#region Group Join - Not GroupBy!
//var query = from person in context.Persons
//            join order in context.Orders
//                on person.PersonId equals order.PersonId into personOrders
//            //from order in personOrders
//            select new
//            {
//                person.Name,
//                Count =personOrders.Count()

//                //order.Description
//            };
//var datas = await query.ToListAsync();  
#endregion
#endregion

//DefaultIfEmpty() is used for Left Join operation

#region Left Join
var query1 = from person in context.Persons
            join order in context.Orders
                on person.PersonId equals order.PersonId into personOrders
            from order in personOrders.DefaultIfEmpty()
            select new
            {
                person.Name,
                order.Description
            };
var datas1= await query1.ToListAsync();
#endregion
//In EF Core, Right Join is not supported. But you can use Left Join and change the order of the tables.
#region Right Join
var query2 = from order in context.Orders
            join person in context.Persons
                on order.PersonId equals person.PersonId into orderPeople
            from person in orderPeople.DefaultIfEmpty()
            select new
            {
                person.Name,
                order.Description
            };
var datas2 = await query2.ToListAsync();
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
