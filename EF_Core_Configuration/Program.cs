using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

ApplicationDbContext context = new ApplicationDbContext();
#region Why do we need Configuration in EF Core
// Sometimes, the default conventions are not enough to define the database schema as we want. In this case, we need to use the Fluent API or Data Annotations to configure the model.
// We need to configure the model in EF Core to define the database schema, table names, column names, data types, and relationships between entities.
#endregion

#region OnModelCreating Method
// The OnModelCreating method is used to configure the model using the Fluent API. This method is called when the model is created for the first time.
// The OnModelCreating method is defined in the DbContext class as Virtual and is used to configure the model using the "Fluent API" so we need to override this method in the DbContext class.
#region GetEntityTypes
// The GetEntityTypes method is used to get all the entity types in the model. It returns a collection of all the entity types in the model.
#endregion
#endregion

#region Configurations | Data Annotations & Fluent API
#region Table - ToTable

#endregion

#region Column - HasColumnName, HasColumnType, HasColumOrder

#endregion

#region ForeingKey - HasForeignKey

#endregion

#region NotMapped - Ignore

#endregion

#region Key - HasKey

#endregion

#region Timestamp - IsRowVersion

#endregion

#region Required - IsRequired

#endregion

#region MaxLength - HasMaxLength

#endregion

#region StringLength - HasMaxLength

#endregion

#region Precision - HasPrecision
#endregion

#region Unicode - IsUnicode
#endregion

#region Comment - HasComment
#endregion

#region ConcurrencyCheck - IsConcurrencyToken

#endregion

#region InverseProperty

#endregion

#endregion

#region Configurations| Fluent API

#region Composite Key
#endregion

#region HasDefaultSchema

#endregion

#region Property

#region HasDefaultValue

#endregion

#region HasDefaultValueSql

#endregion

#endregion

#region HasComputedColumnSql
#endregion

#region HasConstraintName

#endregion

#region HasData
#endregion

#region HasDiscriminator
#endregion

#region HasField
#endregion

#region HasIndex
#endregion

#region HasIndex
#endregion

#region HasQueryFilter
#endregion

#region HasValue
#endregion

#region DatabaseGenerated - ValueGeneratedOnAddOrUpdate, ValueGeneratedOnAdd, ValueGeneratedNever
#endregion

#endregion

class Person
{
    public int Id { get; set; }
    public string DepartmentId { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Salary { get; set; }

    public DateTime CreatedDate { get; set; }
    public Department Department { get; set; }
}
class Department
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<Person> Persons { get; set; }
}
class ApplicationDbContext: DbContext
{
    public DbSet<Person> Persons { get; set; }
    public DbSet<Department> Departments { get; set; }
    //public Dbset<Flight> Flights { get; set; }
    //public Dbset<Airport> Airports { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var entities=modelBuilder.Model.GetEntityTypes();
        foreach (var entity in entities)
        {
            Console.WriteLine(entity.Name);
        }
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost, 1433;Database=ApplicationDb;User Id=sa;Password=Password1;TrustServerCertificate=True");
    }  
}
public class Flight
{
    public int FlightID { get; set; }
    public int DepartureAirportId { get; set; }
    public int ArrivalAirportId { get; set; }
    public string Name { get; set; }
    public Airport DepartureAirport { get; set; }
    public Airport ArrivalAirport { get; set; }
}
public class Airport
{
    public int AirportID { get; set; }
    public string Name { get; set; }
    [InverseProperty(nameof(Flight.DepartureAirport))]
    public virtual ICollection<Flight> DepartingFlights { get; set; }

    [InverseProperty(nameof(Flight.ArrivalAirport))]
    public virtual ICollection<Flight> ArrivingFlights { get; set; }
}



