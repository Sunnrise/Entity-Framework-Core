using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Emit;

EBookDbContext context = new();

#region Default Convention
//We should connect the two tables plurally (ICollection, List). This table will have two columns, one for the primary key of the first table and the other for the primary key of the second table.
//The primary key of the table will be the combination of these two columns.
//The table will be created automatically by the Entity Framework Core.
//Composite primary key is created by default.
class Book
{
    public int Id { get; set; }
    public string BookName { get; set; }
    public ICollection<Author> Authors  { get; set; }
}
class Author
{
    public int Id { get; set; }
    public string AuthorName { get; set; }
    public ICollection<Book> Books { get; set; }
}
#endregion
#region Data Annotations

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
}