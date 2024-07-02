using Microsoft.EntityFrameworkCore;
ApplicationDbContext context = new();

#region What is Sequence ?
//Sequence is a database object that generates a sequence of numbers.
//It is used to generate unique numbers for the primary key column of a table. It is used when we need
#endregion
#region Sequence Defining
//Sequence is defined in the OnModelCreating method of the DbContext class. 
//The HasSequence method takes the name of the sequence as a parameter.
//The sequence name is used to generate the SQL code for the sequence.

//the sequence is dependent on the database provider.  
//For example, the SQL Server provider generates the SQL code for the sequence as "CREATE SEQUENCE EC_Sequence START WITH 100 INCREMENT BY 5". 
//But the SQLite provider does not support the sequence.

#region HasSequence

#endregion
#region HasDefaultValueSql

#endregion
#endregion

await context.Employees.AddAsync(new() { Name = "Alperen", Surname = "Gunes", Salary = 1000 });
await context.Employees.AddAsync(new() { Name = "Mustafa", Surname = "gunes", Salary = 1000 });


await context.Customers.AddAsync(new() { Name = "sunnrise" });
await context.SaveChangesAsync();

#region Sequence Configuration

#region StartsAt

#endregion
#region IncrementsBy

#endregion
#endregion
#region Sequence Identity Difference
//Sequence is not dependent on any table.
//Identity takes the next value from the disk, while the Sequence takes the next value from the RAM. Therefore, Sequence is significantly faster, more efficient, and less costly than Identity.
// Sequence is a database object, while Identity is a column property.


#endregion

class Employee
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public int Salary { get; set; }
}
class Customer
{
    public int Id { get; set; }
    public string? Name { get; set; }
}
class ApplicationDbContext : DbContext
{
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Customer> Customers { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasSequence("EC_Sequence")
            .StartsAt(100)
            .IncrementsBy(5);


        modelBuilder.Entity<Employee>()
            .Property(e => e.Id)
            .HasDefaultValueSql("NEXT VALUE FOR EC_Sequence");

        modelBuilder.Entity<Customer>()
            .Property(c => c.Id)
            .HasDefaultValueSql("NEXT VALUE FOR EC_Sequence");
    }

protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost, 1433;Database=ApplicationDb;User Id=sa;Password=Password1;TrustServerCertificate=True");
    }
}