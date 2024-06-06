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
//The Table attribute is used to specify the table name for the entity. It is used to map the entity to a specific table in the database.
//The ToTable method is used to specify the table name for the entity using the Fluent API.
#endregion

#region Column - HasColumnName, HasColumnType, HasColumOrder
//The Column attribute is used to specify the column name, data type, and order for the property in the database table.
//The HasColumnName method is used to specify the column name for the property using the Fluent API.
//The HasColumnType method is used to specify the data type for the property using the Fluent API.
//The HasColumnOrder method is used to specify the order of the column in the database table using the Fluent API.

#endregion

#region ForeingKey - HasForeignKey
// in relational table design, a foreign key is a field in a relational table that matches the primary key column of another table.
// EF Core uses the default convention to create foreign key relationships between entities based on the navigation properties.

// we can use the ForeignKey attribute to specify the foreign key property in the dependent entity.
//But if we want to configure the foreign key relationship using the Fluent API, we can use the HasForeignKey method
#endregion

#region NotMapped - Ignore
// EF Core uses the default convention to map the entity properties to the database columns.
// If we want to ignore a property in the entity and not map it to the database table, we can use the NotMapped attribute.
// The Ignore method is used to ignore a property in the entity and not map it to the database table using the Fluent API.

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

//[Table("PersonFirst")]
class Person
{
    public int Id { get; set; }
    [ForeignKey(nameof(Department))]
    public int AlperenId { get; set; }
    //public string DepartmentId { get; set; }
    //[Column("FullName",TypeName ="Text",Order =7)]
    public string Name { get; set; }
    public string Surname { get; set; }
    public decimal Salary { get; set; }
    [NotMapped] 
    public int NotMappedProperty { get; set; }
    //we create a property that is not mapped to the database table using the NotMapped attribute
    //for the aim of using it in the application.

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
        #region GetEntityTypes
        //var entities = modelBuilder.Model.GetEntityTypes();
        //foreach (var entity in entities)
        //{
        //    Console.WriteLine(entity.Name);
        //}
        #endregion
        #region ToTable
        //modelBuilder.Entity<Person>().ToTable("newPersonTableName2");
        #endregion
        #region Column
        //modelBuilder.Entity<Person>()
        //    .Property(p => p.Name)
        //    .HasColumnName("FullName2")
        //    .HasColumnType("Text2")
        //    .HasColumnOrder(7);
        #endregion
        #region ForeignKey
        //modelBuilder.Entity<Person>()
        //    .HasOne(p => p.Department)
        //    .WithMany(d => d.Persons)
        //    .HasForeignKey(p => p.AlperenId);
        #endregion
        #region Ignore
        modelBuilder.Entity<Person>()
             .Ignore(p => p.NotMappedProperty);




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



