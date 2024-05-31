using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

BackingFieldDbContext context = new();
var person = await context.People.FindAsync(1);

Console.Read();

#region Backing Fields
// we represent a entity with a backing field not a property
//class Person 
//{
//    public string Id { get; set; }
//    public string name;

//    public string Name { get=>name.Substring(0,3); set=>name= value.Substring(0, 3); }
//    public string Department { get; set; }

//}
#endregion

#region BackingField Attributes
//class Person
//{
//    public string Id { get; set; }
//    public string name;
//    [BackingField(nameof(name))]
//    public string Name { get; set; }
//    public string Department { get; set; }

//}
#endregion

#region HasField Fluent API 
// In fluent API, we can use the HasField method to specify the backing field for a property.
class Person
{
    public string Id { get; set; }
    public string name;
    public string Name { get; set; }
    public string Department { get; set; }

}
#endregion

#region Field And Property Access
// By default, EF Core uses the property access mode to access the properties of an entity.
// However, we can change this behavior to use the field access mode by setting the -"UsePropertyAccessMode"- method on the model builder.

#endregion

#region Field-Only Properties

#endregion

class BackingFieldDbContext : DbContext
{
    public DbSet<Person> People { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost, 1433;Database=ApplicationDb;User Id=sa;Password=Password1;TrustServerCertificate=True");
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Person>()
            .Property(p => p.Name)
            .HasField(nameof(Person.name))
            .UsePropertyAccessMode(PropertyAccessMode.Field);

        //Field, if we want to access the data we have to use the field, if we cannot access the field, an exception will be thrown.

        //FieldDuringConstruction, if we want to access the data while constructing the object, we have to use the field, if we cannot access the field, an exception will be thrown.

        //Property, if we want to access the data, we have to use the property, if we cannot access the property,(readonly, writeonly) an exception will be thrown.

        //PreferField,

        //PreferFieldDuringConstruction,

        //PreferProperty
    }

}
