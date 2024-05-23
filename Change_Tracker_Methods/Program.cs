
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Internal;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Reflection.Emit;
ECommerceDbContext context = new();

#region AsNoTracking Method
// Data which they come from context to the memory, they are tracked by the context.
// Change Tracker Costs are paralel to the tracked entities number. So, if we don't do any operation on the entities, change tracker will be unnecessary. It is important for the performance.

//AsNoTracking method is used to prevent the entities from being tracked by the context.

//with AsNoTracking method, we can get the entities without tracking them. So, we can get the entities without any cost of change tracker.

//The Queries with AsNoTracking method are read-only. We can't update or delete the entities which are retrieved by AsNoTracking method.

//var users=await context.Users.AsNoTracking().ToListAsync();
//foreach(var user in users)
//{
//    Console.WriteLine(user.Name);
//    user.Name= $"Updated {user.Name}";
//}
//await context.SaveChangesAsync();// No update operation will be done because of AsNoTracking method.
#endregion

#region AsNoTrackingWithIdentityResolution
// Change Tracker Mechanism is bring the repeated entities from the database as the same instance. So, if we get the same entity from the database, the change tracker will return the same instance of the entity.

//If we use AsNoTracking method, the entities will be returned as different instances. So, if we want to get the same instance of the entity, we can use AsNoTrackingWithIdentityResolution method.

//AsNoTracking method is used to prevent the entities from being tracked by the context. So if we use AsNoTracking method, the entities will be returned as different instances.

//In these condition we can use AsNoTrackingWithIdentityResolution method to get the same instance of the entity. At the same time we save the cost of change tracker.

var books = await context.Books.Include(b => b.Authors).AsNoTrackingWithIdentityResolution().ToListAsync();

//AsNoTrackingWithIdentityResolution method is used to get the same instance of the entities which are retrieved by AsNoTracking method.
//Its performance is slower than AsNoTracking method but faster than Change Tracker.
// It should be better if we use much  relational data.


#endregion

#region AsTracking

#endregion

#region UseQueryTrackingBehavior

#endregion

Console.WriteLine("BreakPoint for runtime values");


public class ECommerceDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //Provider, ConnectionString, LazyLoading
        optionsBuilder.UseSqlServer("Server=localhost, 1433;Database=ECommerceDb;User Id=sa;Password=Password1;TrustServerCertificate=True");
    }
}
//Entity
public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public ICollection<Role> Roller { get; set; }
}
public class Role
{
    public int Id { get; set; }
    public string RoleName { get; set; }
    public ICollection<User> Users { get; set; }
}
public class Book
{
    public Book() => Console.WriteLine("Book instance is created.");
    public int Id { get; set; }
    public string BookName { get; set; }
    public int PageCount { get; set; }
    public ICollection<Author> Authors { get; set; }
}
public class Author
{
    public Author() => Console.WriteLine("Author instance is created.");
    public int Id { get; set; }
    public string AuthorName { get; set; }
    public ICollection<Book> Books { get; set; }
}