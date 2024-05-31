using Microsoft.EntityFrameworkCore;
using System;

ApplicationbDbContext context = new();



Console.Read();


#region Shadow Properties
//Shadow properties are properties that are not defined in your .NET entity class but are defined in the EF Core model.
//Shadow properties are not part of your entity class and are not saved to the database. They are used to store temporary data that is only needed during the execution of the application.
// Change Tracker, check the state of the Shadow Property
#endregion


#region Foreign Key-Shadow Properties

#endregion

#region Create Shadow Property
#endregion

#region Accessing Shadow Properties

#region  Access with ChangeTracker 

#endregion

#region Access with EF. Property
#endregion

#endregion

class Blog
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<Post> Posts { get; set; }
}

class Post
{
    public int Id { get; set; }
    public string Title { get; set; }
    public bool lastUpdated { get; set; }
    public Blog Blog { get; set; }
}
class ApplicationbDbContext : DbContext
{
    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Post> Posts { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost, 1433;Database=ApplicationDb;User Id=sa;Password=Password1;TrustServerCertificate=True");
    }
}