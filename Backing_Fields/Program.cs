using Microsoft.EntityFrameworkCore;

BackingFieldDbContext context = new();

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
class Person
{
    public string Id { get; set; }
    public string name;
    [BackingField(nameof(name))]
    public string Name { get; set; }
    public string Department { get; set; }

}
#endregion

#region HasField Fluent API 

#endregion

#region Field And Property Access

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
   
}
