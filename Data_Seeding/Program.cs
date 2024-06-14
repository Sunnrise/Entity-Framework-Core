// See https://aka.ms/new-console-template for more information
using Microsoft.EntityFrameworkCore;
ApplicationDbContext context = new ();
#region What is the Data Seeding
//Data seeding is the process of populating a database with an initial set of data. This is often used to provide test data for development or to populate the database with data that is required by the application to function properly.
#endregion

#region Adding Data Seed
//The data seeding can be done by overriding the OnModelCreating method of the DbContext class and using the HasData method of the EntityTypeBuilder class to add the seed data.

//We have to set the primary key of the entity in the seed data. If the primary key is an auto-incremented value, then we have to set the primary key value explicitly in the seed data.
#endregion

#region Adding Data Seed for Related Entities
//If the entities are related to each other, then we have to set the foreign key value explicitly in the seed data.
#endregion

#region Changing the Primary Key of the Seed Data
//If the primary key of the entity is changed after the migration is applied, then we have to update the seed data with the new primary key value.
#endregion

class Post
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public int BlogId { get; set; }
    public Blog Blog { get; set; }
}
class Blog
{
    public int Id { get; set; }
    public string Url { get; set; }
    public ICollection<Post> Posts { get; set; }
}
class ApplicationDbContext : DbContext
{
    public DbSet<Post> Posts { get; set; }
    public DbSet<Blog> Blogs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Blog>()
            .HasData(
                new Blog() { Id = 1, Url = "http://sample.com" },
                new Blog() { Id = 2, Url = "http://sample2.com" }

            );
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost, 1433;Database=ApplicationDb;User Id=sa;Password=Password1;TrustServerCertificate=True");
    }
}