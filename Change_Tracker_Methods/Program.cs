
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Emit;
ECommerceDbContext context = new();

#region AsNoTracking Method
// Data which they come from context to the memory, they are tracked by the context.
// Change Tracker Costs are paralel to the tracked entities number. So, if we don't do any operation on the entities, change tracker will be unnecessary. It is important for the performance.

//AsNoTracking method is used to prevent the entities from being tracked by the context.

//with AsNoTracking method, we can get the entities without tracking them. So, we can get the entities without any cost of change tracker.

//The Queries with AsNoTracking method are read-only. We can't update or delete the entities which are retrieved by AsNoTracking method.



#endregion

#region AsNoTrackingWithIdentifyResolution

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