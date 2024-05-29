using Microsoft.EntityFrameworkCore;


ApplicationDbContext context = new();
#region Adding Data in One to One Relational scenarios 
#region 1. Method => Add Dependent Entity data on Principal Entity
//Person person = new();
//person.Name = "Mustafa";
//person.Address = new() { PersonAddress="Eskişehir"};

//await context.AddAsync(person);
//await context.SaveChangesAsync();
#endregion
//if you want to add data to the dependent entity, we need to add the principal entity.
//if you want to add data to the principal entity, we needn't add the dependent entity.
#region 2. Method => Add Principal Entity  data on Dependent Entity
//Address address = new() {

//    PersonAddress="Antalya/manavgat",
//    Person=new() { Name = "Alperengunes" }

//};
//await context.AddAsync(address);
//await context.SaveChangesAsync();
#endregion
//class Person
//{
//    public int Id { get; set; }
//    public string Name { get; set; }
//    public Address Address { get; set; }
//}
//class Address
//{
//    public int Id { get; set; }
//    public string PersonAddress { get; set; }
//    public Person Person { get; set; }
//}
//class ApplicationDbContext : DbContext
//{
//    public DbSet<Person> Persons { get; set; }
//    public DbSet<Address> Addresses { get; set; }
//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//    {
//        optionsBuilder.UseSqlServer("Server=localhost, 1433;Database=ECommerceDb;User Id=sa;Password=Password1;TrustServerCertificate=True");
//    }
//    protected override void OnModelCreating(ModelBuilder modelBuilder)
//    {
//        modelBuilder.Entity<Address>()
//            .HasOne(a => a.Person)
//            .WithOne(p => p.Address)
//            .HasForeignKey<Address>(a => a.Id);

//    }

//}
#endregion
#region Adding Data in One to Many Relational scenarios 
//#region 1. Method => Add Dependent Entity data on Principal Entity
//#region Adding with Instance Reference
//Blog blog = new() { Name = "alperengunes.blog" };
//blog.Posts.Add(new Post { Title = "Post1" });
//blog.Posts.Add(new Post { Title = "Post2" });
//await context.AddAsync(blog);
//await context.SaveChangesAsync();
//#endregion
//#region Adding with Object Initializer
//Blog blog2 = new() { 

//    Name= "alperengunes",
//    Posts=new HashSet<Post>
//    {
//        new Post { Title = "Post3" },
//        new Post { Title = "Post4" }
//    }
//};
//await context.AddAsync(blog2);
//await context.SaveChangesAsync();
//#endregion
//#endregion
//#region 2. Method => Add Principal Entity data on Dependent Entity
//Post post = new() { Title = "Post6", Blog = new () { Name = "Z blog" } };
//#endregion
//#region 3. Method => Add data with foreign key column
//Post post3 = new() { 
//    BlogId = 1,
//    Title = "Post7"
//};
//await context.AddAsync(post3);
//await context.SaveChangesAsync();
//#endregion
//class Blog
//{
//    public Blog()
//    {
//        Posts=new HashSet<Post>();
//    }
//    public int Id { get; set; }
//    public string Name { get; set; }
//    public ICollection<Post> Posts { get; set; }
//}
//class Post
//{
//    public int Id { get; set; }
//    public int BlogId { get; set; }
//    public string Title { get; set; }

//    public Blog Blog { get; set; }
//}
//class ApplicationDbContext : DbContext
//{
//    public DbSet<Blog> Persons { get; set; }
//    public DbSet<Post> Addresses { get; set; }
//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//    {
//        optionsBuilder.UseSqlServer("Server=localhost, 1433;Database=ECommerceDb;User Id=sa;Password=Password1;TrustServerCertificate=True");
//    }
//}
#endregion
#region Adding Data in Many to Many Relational scenarios 
#region 1. Method
//n to n relationship is created by default convention
//Book book = new() { 
//    BookName = "Book1",
//    Authors=new HashSet<Author>()
//    {
//        new () { AuthorName = "Author1" },
//        new () { AuthorName = "Author2" },
//        new () { AuthorName = "Author3" }
//    }

//};
//await context.AddAsync(book);
//await context.SaveChangesAsync();
//class Book
//{
//    public Book()
//    {
//        Authors = new HashSet<Author>();
//    }
//    public int Id { get; set; }
//    public string BookName { get; set; }
//    public ICollection<Author> Authors { get; set; }
//}

//class Author
//{
//    public Author()
//    {
//        Books = new HashSet<Book>();
//    }
//    public int Id { get; set; }
//    public string AuthorName { get; set; }
//    public ICollection<Book> Books { get; set; }
//}
#endregion
#region 2. Method
//n to n relationship is created by fluent api
Author author = new() 
{ 
    AuthorName = "Author4",
    Books = new HashSet<AuthorBook>()
    {
        new () { BookId=1 },
        new () { Book = new Book { BookName = "Book4" } }
    }
};
class Book
{
    public Book()
    {
        Authors = new HashSet<AuthorBook>();
    }
    public int Id { get; set; }
    public string BookName { get; set; }
    public ICollection<AuthorBook> Authors { get; set; }
}
class AuthorBook
{
    public int BookId { get; set; }
    public int AuthorId { get; set; }
    public Book Book { get; set; }
    public Author Author { get; set; }
}
class Author
{
    public Author()
    {
        Books = new HashSet<AuthorBook>();
    }
    public int Id { get; set; }
    public string AuthorName { get; set; }
    public ICollection<AuthorBook> Books { get; set; }
}

#endregion

class ApplicationDbContext : DbContext
{
    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost, 1433;Database=ECommerceDb;User Id=sa;Password=Password1;TrustServerCertificate=True");
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AuthorBook>()
            .HasKey(ba => new { ba.BookId, ba.AuthorId });
        modelBuilder.Entity<AuthorBook>()
            .HasOne(ba => ba.Book)
            .WithMany(b => b.Authors)
            .HasForeignKey(ba => ba.BookId);
        modelBuilder.Entity<AuthorBook>()
            .HasOne(ba => ba.Author)
            .WithMany(a => a.Books)
            .HasForeignKey(ba => ba.AuthorId);
    }
}
#endregion

