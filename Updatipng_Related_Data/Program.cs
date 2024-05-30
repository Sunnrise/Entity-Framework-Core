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
Person? person=await context.Persons
    .Include(p => p.Address)
    .FirstOrDefaultAsync(p=>p.Id==1);

context.Addresses.Remove(person.Address);
person.Address = new()
{
    PersonAddress = "Antalya/Döşemealtı"
};
await context.SaveChangesAsync();
#endregion

#region 2. State => Change the principal entity data which connected to the dependent entity
//Address? address = await context.Addresses.FindAsync(1);
//address.Id = 2;// we can not change the primary key value

Address? address = await context.Addresses.FindAsync(1);
context.Addresses.Remove(address);
await context.SaveChangesAsync();
Person? person1 = await context.Persons.FindAsync(2);
address.Person = person1;
context.Addresses.Add(address);
await context.SaveChangesAsync();


#endregion

#endregion

#region Updating Data in One to Many Relational scenarios 
#region Saving

#endregion
#region 1. State => Dependent Entity data 
#endregion

#region 2. State => 

#endregion
#endregion

#region Updating Data in Many to Many Relational scenarios 
#region Saving

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
