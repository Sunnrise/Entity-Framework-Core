using Microsoft.EntityFrameworkCore;


ApplicationDbContext context = new();
#region Adding Data in One to One Relational scenarios 
#region 1. Method => Add Dependent Entity data on Principal Entity
Person person = new();
person.Name = "Mustafa";
person.Address = new() { PersonAddress="Eskişehir"};

await context.AddAsync(person);
await context.SaveChangesAsync();
#endregion
//if you want to add data to the dependent entity, we need to add the principal entity.
//if you want to add data to the principal entity, we needn't add the dependent entity.
#region 2. Method => Add Principal Entity  data on Dependent Entity
Address address = new() {
    
    PersonAddress="Antalya/manavgat",
    Person=new() { Name = "Alperengunes" }

};
await context.AddAsync(address);
await context.SaveChangesAsync();
#endregion
class Person
{
    public int Id { get; set; }
    public string Name { get; set; }
    public Address Address { get; set; }
}
class Address
{
    public int Id { get; set; }
    public string PersonAddress { get; set; }
    public Person Person { get; set; }
}
class ApplicationDbContext : DbContext
{
    public DbSet<Person> Persons { get; set; }
    public DbSet<Address> Addresses { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost, 1433;Database=ECommerceDb;User Id=sa;Password=Password1;TrustServerCertificate=True");
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Address>()
            .HasOne(a => a.Person)
            .WithOne(p => p.Address)
            .HasForeignKey<Address>(a => a.Id);

    }

}
#endregion
#region Adding Data in One to Many Relational scenarios 
#endregion
#region Adding Data in Many to Many Relational scenarios 

#endregion

