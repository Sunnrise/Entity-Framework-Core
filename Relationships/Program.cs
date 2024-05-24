using Microsoft.EntityFrameworkCore;
Console.WriteLine();

#region Relationships Term
#region Principal Entity
//If the table can be itself, then it is called the principal entity. Department is the principal entity in the below example. But if the table cannot be itself, then it is called the dependent entity. Employee is the dependent entity in the below example.
#endregion

#region Dependent Entity
// If the table cannot be itself, then it is called the dependent entity. Employee is the dependent entity in the below example.

#endregion

#region Foreign Key
//Principle entity and dependent entity are connected with the foreign key. DepartmentId is the foreign key in the below example.
#endregion

#region Principal Key
// The primary key of the principal entity is called the principal key. Id is the principal key in the below example. 
#endregion

class Employee
{
    public int Id { get; set; }
    public string EmployeeName { get; set; }
    public int DepartmentId { get; set; }

    public Department Department { get; set; }// Navigation Property
}
class Department
{
    public int Id { get; set; }
    public string DepartmentName { get; set; }

    public List<Employee> Employees { get; set; }// Navigation Property
}
//Navigation properties are used to define the relationship between two entities. In the above example, the Department property in the Employee entity is a navigation property. 
#endregion

#region What is an Navigation Property 
//Navigation property is a property in an entity that allows you to navigate from one entity to another entity. In the above example, the Department property in the Employee entity is a navigation property.
// Navigation properties are used to define the relationship between two entities.
//It's type must be the same as the related entity.
#endregion

#region Relationships
#region One to One

#endregion

#region One to Many

#endregion

#region Many to Many
#endregion
#endregion

#region Relationship conf. in Entity Framework Core
#region Default Conventions


#endregion

#region Data Annotations Attributes
#endregion

#region Fluent API


#region HasOne
#endregion

#region HasMany
#endregion

#region WithOne
#endregion

#region WithMany
#endregion
#endregion
#endregion