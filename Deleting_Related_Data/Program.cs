using System.Collections.Generic;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore; 

ApplicationDbContext context = new ();

#region Data Delete in One to One relational scenarios 
//Person? person = await context.Persons
//    .Include(p => p.Address)
//    .FirstOrDefaultAsync(p => p.Id == 1);
//context.Addresses.Remove(person.Address);
//await context.SaveChangesAsync();
#endregion

#region Data Delete in One to Many relational scenarios 
//Blog? blog = await context.Blogs
//    .Include(b => b.Posts)
//    .FirstOrDefaultAsync(b => b.Id == 1);
//Post? post=blog.Posts.FirstOrDefault(p => p.Id == 2);
//context.Posts.Remove(post);
//await context.SaveChangesAsync();
#endregion

#region Data Delete in Many to Many relational scenarios 
//Book? book=await context.Books
//    .Include(b => b.Authors)
//    .FirstOrDefaultAsync(b => b.Id == 1);
//Author? author = book.Authors.FirstOrDefault(a => a.Id == 2);
////context.Authors.Remove(author);// Remove author from the book !!!! we need to delete relation not the author
//book.Authors.Remove(author);
//await context.SaveChangesAsync();
#endregion

#region Cascade Delete
// this is the default behavior of EF Core. Fluent api provides the following options to configure the delete behavior.
#region Cascade
//Deleted data which belongs to the principal entity, deletes the related data in the dependent entity.
//Blog? blog=await context.Blogs.FindAsync(1);
//context.Blogs.Remove(blog);
//await context.SaveChangesAsync();

#endregion

#region SetNull
//Deleted data which belongs to the principal entity, sets the  dependent entity values to null.
//In one to one scenarios, if primary key and foreign key are the same, We cannot use SetNull.
Blog? blog = await context.Blogs.FindAsync(1);
context.Blogs.Remove(blog);
await context.SaveChangesAsync();
#endregion

#region Restrict
//Deleted data which belongs to the principal entity, if there is data in the dependent entity, Restrict will throw an exception.
#endregion
#endregion

#region Saving Data

#endregion
class Person
{
    public int Id { get; set; }
    public string Name { get; set; }

    public Address Address { get; set; }
}
class Address
{
    public int? Id { get; set; }
    public string PersonAddress { get; set; }

    public Person Person { get; set; }
}
class Blog
{
    public Blog()
    {
        Posts = new HashSet<Post>();
    }
    public int Id { get; set; }
    public string Name { get; set; }

    public ICollection<Post> Posts { get; set; }
}
class Post
{
    public int Id { get; set; }
    public int? BlogId { get; set; }
    public string Title { get; set; }

    public Blog Blog { get; set; }
}
class Book
{
    public Book()
    {
        Authors = new HashSet<Author>();
    }
    public int Id { get; set; }
    public string BookName { get; set; }

    public ICollection<Author> Authors { get; set; }
}
class Author
{
    public Author()
    {
        Books = new HashSet<Book>();
    }
    public int Id { get; set; }
    public string AuthorName { get; set; }

    public ICollection<Book> Books { get; set; }
}
class ApplicationDbContext : DbContext
{
    public DbSet<Person> Persons { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost, 1433;Database=ApplicationDb;User Id=sa;Password=Password1;TrustServerCertificate=True");
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Address>()
            .HasOne(a => a.Person)
            .WithOne(p => p.Address)
            .HasForeignKey<Address>(a => a.Id);
            

        modelBuilder.Entity<Post>()
            .HasOne(p => p.Blog)
            .WithMany(b => b.Posts)
            .OnDelete(DeleteBehavior.SetNull)
            .IsRequired(false);
    }

}
