using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;


ApplicationDbContext context = new();
#region Updating Data in One to One Relational scenarios 
#region Saving
//Person person = new()
//{
//    Name = "Alperen",
//    Address = new()
//    {
//        PersonAddress = "Antalya/Manavgat"
//    }
//};
//Person person1 = new()
//{
//    Name = "Alperengunes"
//};
//await context.AddAsync(person);
//await context.AddAsync(person1);
//await context.SaveChangesAsync();
////we run the code once and see that the data is saved in the database.
#endregion
#region 1. State => Change the dependent entity data which connected to the principal entity
//Person? person=await context.Persons
//    .Include(p => p.Address)
//    .FirstOrDefaultAsync(p=>p.Id==1);

//context.Addresses.Remove(person.Address);
//person.Address = new()
//{
//    PersonAddress = "Antalya/Döşemealtı"
//};
//await context.SaveChangesAsync();
#endregion

#region 2. State => Change the principal entity data which connected to the dependent entity
//Address? address = await context.Addresses.FindAsync(1);
//address.Id = 2;// we can not change the primary key value

//Address? address = await context.Addresses.FindAsync(1);
//context.Addresses.Remove(address);
//await context.SaveChangesAsync();
//Person? person1 = await context.Persons.FindAsync(2);
//address.Person = person1;
//context.Addresses.Add(address);
//await context.SaveChangesAsync();
#endregion
#endregion

#region Updating Data in One to Many Relational scenarios 
#region Saving
//Blog blog = new()
//{
//    Name = "Blog1",
//    Posts = new List<Post>
//    {
//        new Post
//        {
//            Title = "Post1"
//        },
//        new Post
//        {
//            Title = "Post2"
//        },
//        new Post
//        {
//            Title = "Post3"
//        }
//    }
//};
//await context.Blogs.AddAsync(blog);
//await context.SaveChangesAsync();
////we run the code once and see that the data is saved in the database.
#endregion
#region 1. State => Change the dependent entity data which connected to the principal entity
//Blog? blog = await context.Blogs
//    .Include(b => b.Posts)
//    .FirstOrDefaultAsync(b => b.Id == 1);

//Post? deletePost = blog.Posts.FirstOrDefault(p => p.Id == 2);
//blog.Posts.Remove(deletePost);

//blog.Posts.Add(new() { Title = "Post3" });
//blog.Posts.Add(new() { Title = "Post" });
//await context.SaveChangesAsync();
#endregion
#region 2. State => Change the principal entity data which connected to the dependent entity
//Post? post = await context.Posts.FindAsync(4);
//post.Blog= new()
//{
//    Name = "Blog2"
//};
//await context.SaveChangesAsync();

//Post? post2=await context.Posts.FindAsync(5);
//Blog? blog = await context.Blogs.FindAsync(2);
//post2.Blog = blog;
#endregion
#endregion

#region Updating Data in Many to Many Relational scenarios 
#region Saving
//Book book1 = new() { BookName="1. Book"};
//Book book2 = new() { BookName = "2. Book" };
//Book book3 = new() { BookName = "3. Book" };
//Author author1 = new() { AuthorName = "1. Author" };
//Author author2 = new() { AuthorName = "2. Author" };
//Author author3 = new() { AuthorName = "3. Author" };

//book1.Authors.Add(author1);
//book1.Authors.Add(author2);

//book2.Authors.Add(author1);
//book2.Authors.Add(author2);
//book2.Authors.Add(author3);

//book3.Authors.Add(author3);

//await context.AddAsync(book1);
//await context.AddAsync(book2);
//await context.AddAsync(book3);
//await context.SaveChangesAsync();
////we run the code once and see that the data is saved in the database.
#endregion
#region 1. State => Add new author to the book
//Book? book= await context.Books.FindAsync(1);
//Author? author = await context.Authors.FindAsync(3);
//book.Authors.Add(author);
//await context.SaveChangesAsync();
#endregion
#region 2. State => Remove author from the book
//Author? author = await context.Authors
//    .Include(a => a.Books)
//    .FirstOrDefaultAsync(a => a.Id == 3);
//foreach (var book in author.Books)
//{
//    if (book.Id != 1)
//       author.Books.Remove(book);
        
//}
//await context.SaveChangesAsync();
#endregion
#region Last Sample
Book? book = await context.Books
    .Include(b => b.Authors)
    .FirstOrDefaultAsync(b => b.Id == 2);
book.Authors.Remove(book.Authors.FirstOrDefault(a => a.Id == 1));

book.Authors.Add(context.Authors.FirstOrDefault(a => a.Id == 3));
book.Authors.Add(new() { AuthorName = "4. Author" });
await context.SaveChangesAsync();
#endregion
#endregion

class Person
{
    public int Id { get; set; }
    public string Name { get; set; }

    public Address Address { get; set; }
}
class Address
{
    public int Id { get; set; }
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
    public int BlogId { get; set; }
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
    }

}
