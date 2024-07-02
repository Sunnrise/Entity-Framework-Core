using Microsoft.EntityFrameworkCore;

ApplicationDbContext context = new();

#region What is Index ?
// Index: if we have a table with a lot of rows, and we want to search for a specific row, it will take a long time to find it. To speed up the search process, we can create an index on the column that we want to search for. An index is a data structure that improves the speed of data retrieval operations on a database table at the cost of additional writes and storage space to maintain the index data structure.
#endregion
#region How can we create an Index in EF Core ?
// Pk, FK and Alternate Key are automatically indexed in EF Core.
// We can create an index on a column or multiple columns using the HasIndex method in the OnModelCreating method of the DbContext class
//context.Employees.Where(x => x.Name == "John");
#region Index Attribute

#endregion
#region HasIndex Method

#endregion
#endregion
#region Define index more than one column

#endregion
#region Index Uniqueness

#endregion
#region Index Sort Order - Sorting Type (EF Core 7.0)

#region AllDescending - Attribute
// AllDescending: If we want to create an index on multiple columns and we want to sort all columns in descending order, we can use the AllDescending attribute.
#endregion
#region IsDescending - Attribute
// IsDescending: If we want to create an index on multiple columns and we want to sort each column in a different order, we can use the IsDescending attribute.
#endregion
#region IsDescending Method

#endregion
#endregion
#region Index Name

#endregion
#region Index Filter

#region HasFilter Method

#endregion
#endregion
#region Included Columns

#region IncludeProperties Method

#endregion
#endregion

//[Index(nameof(Name))]
//[Index(nameof(Surname))]
//[Index(nameof(Name), nameof(Surname))]
//[Index(nameof(Name), AllDescending = true)]
//[Index(nameof(Name), nameof(Surname), IsDescending = new[] { true, false })]
//[Index(nameof(Name), Name = "name_index")]

class Employee
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public int Salary { get; set; }
}

class ApplicationDbContext : DbContext
{
    public DbSet<Employee> Employees { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //modelBuilder.Entity<Employee>()
        //.HasIndex(x => x.Name);
        //.HasIndex(x => new { x.Name, x.Surname });
        //.HasIndex(nameof(Employee.Name), nameof(Employee.Surname));
        //.HasIndex(x => x.Name)
        //.IsUnique();

        //modelBuilder.Entity<Employee>()
        //    .HasIndex(x => x.Name)
        //    .IsDescending();

        //modelBuilder.Entity<Employee>()
        //    .HasIndex(x => new { x.Name, x.Surname })
        //    .IsDescending(true, false);

        //modelBuilder.Entity<Employee>()
        //    .HasIndex(x => x.Name)
        //    .HasDatabaseName("name_index");

        //modelBuilder.Entity<Employee>()
        //    .HasIndex(x => x.Name)
        //    .HasFilter("[NAME] IS NOT NULL");

        modelBuilder.Entity<Employee>()
            .HasIndex(x => new { x.Name, x.Surname })
            .IncludeProperties(x => x.Salary);
    }

protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost, 1433;Database=ApplicationDb;User Id=sa;Password=Password1;TrustServerCertificate=True");
    }
}