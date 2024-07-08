using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using System.Text.Json;

ApplicationDbContext context = new();
#region What is Value Conversions?

//Value conversions allow you to convert data in the database to and from the application data types.

//We can convert the data that will come in the SELECT queries.
//or
//We can also make conversions on the data we send to the database from the software in UPDATE or INSERT queries, and thus we can manipulate the data physically.
#endregion
#region How can use Value Converter?
//We can apply the Value Converter feature through the Value Converter structures in EF Core.

#region HasConversion 
//The HasConversion function is a function that is the simplest form of a function that acts as a value converter through EF Core.

//var persons = await context.Persons.ToListAsync();
//Console.WriteLine();
#endregion
#endregion
#region Value Converter using with Enum Values

//Normally, the transfer of properties held in Enum type to the database is realized in the form of int. With the help of the value converter, we can provide conversions of properties that are of type enum to the types we want, both set the type of the column to that type and provide data conversions in the relevant type during the working process via the enum.

//var person = new Person() { Name = "Rakıf", Gender2 = Gender.Male, Gender = "M" };
//await context.Persons.AddAsync(person);
//await context.SaveChangesAsync();
//var _person = await context.Persons.FindAsync(person.Id);
//Console.WriteLine();

#endregion
#region ValueConverter Class
//The ValueConverter class is a class that can take on the responsibilities of data transformations.
//So, with the instance of this class, we can take on the work done by the HasConvention function and directly perform our transformational work by giving this instance to the relevant function.

//var _person = await context.Persons.FindAsync(13);
//Console.WriteLine();
#endregion
#region Custom ValueConverter Class
//In EF Core, we can customly produce converter classes for data conversions. All that needs to be done is to ensure that the custom class inherits from the ValueConverter class.

//var _person = await context.Persons.FindAsync(13);
//Console.WriteLine();
#endregion
#region Built-in Converters Structures
//EF Core has built-in conversion classes for simple conversions.

#region BoolToZeroOneConverter
//It provides the storage of the boolean data as an integer.
#endregion
#region BoolToStringConverter
//It provides the storage of the boolean data as a string.
#endregion
#region BoolToTwoValuesConverter
//It provides the storage of the boolean data as a char.
#endregion

//You can observe other built-in converters Structures from the link below.

//https://learn.microsoft.com/en-us/ef/core/modeling/value-conversions?tabs=data-annotations#built-in-converters

#endregion
#region Serialization of Primitive Collections
//When we try to migrate models that contain collections created from primitive types, we encounter an error. To get rid of the error and be able to process the relevant data by serializing the data in the collection, we can perform a conversion operation that allows us to convert this collection to normal textual values.

//var person = new Person() { Name = "Filanca", Gender = "M", Gender2 = Gender.Male, Married = true, Titles = new() { "A", "B", "C" } };
//await context.Persons.AddAsync(person);

//await context.SaveChangesAsync();

//var _person = await context.Persons.FindAsync(person.Id);
//Console.WriteLine();
#endregion
#region .NET 6 - Value Converter For Nullable Fields
//Before .NET 6, value converters did not support the conversion of null values. With .NET 6, null values can now be converted.
#endregion


public class GenderConverter : ValueConverter<Gender, string>
{
    public GenderConverter() : base(
        //INSERT - UPDATE
        g => g.ToString()
        ,
        //SELECT
        g => (Gender)Enum.Parse(typeof(Gender), g)
        )
    {
    }
}

public class Person
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Gender { get; set; }
    public Gender Gender2 { get; set; }
    public bool Married { get; set; }
    public List<string>? Titles { get; set; }
}
public enum Gender
{
    Male,
    Famele
}
public class ApplicationDbContext : DbContext
{
    public DbSet<Person> Persons { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        #region How can we use Value Converter?
        //modelBuilder.Entity<Person>()
        //    .Property(p => p.Gender)
        //    .HasConversion(
        //        //INSERT - UPDATE
        //        g => g.ToUpper()
        //    ,
        //        //SELECT
        //        g => g == "M" ? "Male" : "Female"
        //    );
        #endregion
        #region Using Value Converter with Enum values
        //modelBuilder.Entity<Person>()
        //   .Property(p => p.Gender2)
        //   .HasConversion(
        //       //INSERT - UPDATE
        //       g => g.ToString()
        //       //g => (int)g
        //   ,
        //       //SELECT
        //       g => (Gender)Enum.Parse(typeof(Gender), g)
        //   );
        #endregion
        #region ValueConverter Class

        //ValueConverter<Gender, string> converter = new(
        //     //INSERT - UPDATE
        //     g => g.ToString()
        //     ,
        //     //SELECT
        //     g => (Gender)Enum.Parse(typeof(Gender), g)
        //    );

        //modelBuilder.Entity<Person>()
        // .Property(p => p.Gender2)
        // .HasConversion(converter);
        #endregion
        #region Custom ValueConverter Class
        //modelBuilder.Entity<Person>()
        // .Property(p => p.Gender2)
        // .HasConversion<GenderConverter>();
        #endregion
        #region BoolToZeroOneConverter
        //modelBuilder.Entity<Person>()
        // .Property(p => p.Married)
        // .HasConversion<BoolToZeroOneConverter<int>>();

        //Or we can directly declare the int type as below, the same behavior will be valid.

        //modelBuilder.Entity<Person>()
        // .Property(p => p.Married)
        // .HasConversion<int>();
        #endregion
        #region BoolToStringConverter
        //BoolToStringConverter converter = new("Single", "Married");

        //modelBuilder.Entity<Person>()
        // .Property(p => p.Married)
        // .HasConversion(converter);
        #endregion
        #region BoolToTwoValuesConverter
        //BoolToTwoValuesConverter<char> converter = new('S', 'M');

        //modelBuilder.Entity<Person>()
        // .Property(p => p.Married)
        // .HasConversion(converter);
        #endregion
        #region Serialization of Primitive Collections
        modelBuilder.Entity<Person>()
            .Property(p => p.Titles)
            .HasConversion(
            //INSERT - UPDATE
            t => JsonSerializer.Serialize(t, (JsonSerializerOptions)null)
            ,
            //SELECT
            t => JsonSerializer.Deserialize<List<string>>(t, (JsonSerializerOptions)null)
            );
        #endregion
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost, 1433;Database=ApplicationDb;User Id=sa;Password=Password1;TrustServerCertificate=True");
    }
}
