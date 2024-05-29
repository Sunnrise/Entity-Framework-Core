using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Emit;

EBookDbContext context = new();

#region Default Convention
////We should connect the two tables plurally (ICollection, List). This table will have two columns, one for the primary key of the first table and the other for the primary key of the second table.
////The primary key of the table will be the combination of these two columns.
////The table will be created automatically by the Entity Framework Core.
////Composite primary key is created by default.
//class Book
//{
//    public int Id { get; set; }
//    public string BookName { get; set; }
//    public ICollection<Author> Authors  { get; set; }
//}
//class Author
//{
//    public int Id { get; set; }
//    public string AuthorName { get; set; }
//    public ICollection<Book> Books { get; set; }
//}
#endregion
#region Data Annotations
//We should create Cross Table manually.
//We should connect the Entities with the Cross Table. one to many relationship.
//We cannot set composite primary key by default. We should set it manually but we have to use the Fluent API. 
// We have to use haskey method with an anonymous type for composite primary key.
//We should use the [Key] attribute for the primary key of the Cross Table.
//We dont have to assign Cross Table with DbSet. 


class Book
{
    public int Id { get; set; }
    public string BookName { get; set; }
    public ICollection<AuthorBook>Authors { get; set; }

}
//Cross Table
class AuthorBook
{
    [Key]
    public int BookId { get; set; }
    [Key]
    public int AuthorId { get; set; }
    public Book Book { get; set; }
    public Author Author { get; set; }
}
class Author
{
    public int Id { get; set; }
    public string AuthorName { get; set; }
    public ICollection<AuthorBook> Books { get; set; }

}
#endregion
#region Fluent API


#endregion


class EBookDbContext : DbContext
{
    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost, 1433;Database=EBook  Db;User Id=sa;Password=Password1;TrustServerCertificate=True");
    }
    //If we want to set composite primary key, we should use the Fluent API.
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AuthorBook>()
            .HasKey(ab => new { ab.AuthorId, ab.BookId });
    }
}