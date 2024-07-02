using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

ApplicationDbContext context = new();

#region Primary Key Constraint
//From name convention ID, Id, EntityNameID, EntityNameId are recognized as primary key
//If you want to give a name to primary key constraint, you can use HasName method or Key Attribute
#region HasKey Func

#endregion
#region Key Attribute'u

#endregion
#region Alternate Keys - HasAlternateKey
// If you want to create an alternate key, you can use HasAlternateKey method or AlternateKey Attribute
// Alternate key is a unique constraint that is not a primary key constraint but can be used to uniquely identify a row in a table  
#endregion
#region Composite Alternate Key

#endregion

#region Give name to Primary Key Constraint with HasName Method

#endregion
#endregion

#region Foreign Key Constraint

#region HasForeignKey Method

#endregion
#region ForeignKey Attribute'u

#endregion
#region Composite Foreign Key

#endregion

#region Shadow Property on Foreign Key

#endregion

#region Give name to Primary Key Constraint with HasConstraintName method

#endregion
#endregion

#region Unique Constraint

#region HasIndex - IsUnique Method

#endregion

#region Index, IsUnique Attributes

#endregion

#region Alternate Key

#endregion
#endregion

#region Check Constratint

#region HasCheckConstraint

#endregion
#endregion

//[Index(nameof(Blog.Url), IsUnique = true)]
class Blog
{
    public int Id { get; set; }
    //[Key]
    public string BlogName { get; set; }
    public string Url { get; set; }

    public ICollection<Post> Posts { get; set; }
}
class Post
{
    public int Id { get; set; }
    //[ForeignKey(nameof(Blog))]
    //public int BlogId { get; set; }
    public string Title { get; set; }
    public string BlogUrl { get; set; }
    public int A { get; set; }
    public int B { get; set; }

    public Blog Blog { get; set; }
}


class ApplicationDbContext : DbContext
{
    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Post> Posts { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //modelBuilder.Entity<Blog>()
        //    .HasKey(b => b.Id)
        //    .HasName("sample");
        //modelBuilder.Entity<Blog>()
        //    .HasAlternateKey(b => new { b.Url, b.BlogName });
        //modelBuilder.Entity<Blog>()
        //    .Property<int>("BlogForeignKeyId");

        //modelBuilder.Entity<Blog>()
        //    .HasMany(b => b.Posts)
        //    .WithOne(b => b.Blog)
        //    .HasForeignKey("BlogForeignKeyId")// Shadow Property
        //    .HasConstraintName("ornekforeignkey");

        //modelBuilder.Entity<Blog>()
        //    .HasIndex(b => b.Url)
        //    .IsUnique();
        //modelBuilder.Entity<Blog>()
        //    .HasAlternateKey(b => b.Url);

        modelBuilder.Entity<Post>()
            .HasCheckConstraint("a_b_check_const", "[A] > [B]");
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost, 1433;Database=ApplicationDb;User Id=sa;Password=Password1;TrustServerCertificate=True");
    }
}