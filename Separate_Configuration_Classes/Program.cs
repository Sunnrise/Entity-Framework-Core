using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection;
Console.WriteLine();
#region OnModelCreating
//We can configure the entity using the OnModelCreating method of the DbContext class.
#endregion

#region IEntityTypeConfiguration<T> Interface
//We can create a separate class that implements the IEntityTypeConfiguration<T> interface for each entity.
//We separate the configuration of each entity into a separate class. So we can manage the configuration of each entity separately.
//If the entity configuration is complex, we can manage it in a separate class. It makes the code clean and easy to manage.
#endregion

#region ApplyConfiguration Method
//We can apply the configuration to the entity using the ApplyConfiguration method of the ModelBuilder class.

#endregion

#region ApplyConfigurationsFromAssembly Method
// We can apply the configuration to the entity using the ApplyConfigurationsFromAssembly method of the ModelBuilder class.
// It applies all the configurations that are defined in the assembly.
//We dont have to call the ApplyConfiguration method for each entity configuration.
#endregion

class Order
{
    public int OrderId { get; set; }
    public string Description { get; set; }
    public DateTime OrderDate { get; set; }
}
class OrderConfiguration:IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(o => o.OrderId);
        builder.Property(o => o.Description)
            .HasMaxLength(100)
            .IsRequired();
        builder.Property(o => o.OrderDate)
            .HasDefaultValueSql("GETDATE()");
    }
}

class ApplicationDbContext : DbContext
{
    public DbSet<Order> Orders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //modelBuilder.ApplyConfiguration(new OrderConfiguration());
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost, 1433;Database=ApplicationDb;User Id=sa;Password=Password1;TrustServerCertificate=True");
    }
}