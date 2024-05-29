using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Emit;

EBookDbContext context = new();

#region Default Convention
class Book
{
    public int Id { get; set; }
    public string BookName { get; set; }
}
class Author
{
    public int Id { get; set; }
    public string AuthorName { get; set; }
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
        optionsBuilder.UseSqlServer("Server=localhost, 1433;Database=EBookDb;User Id=sa;Password=Password1;TrustServerCertificate=True");
    }
}