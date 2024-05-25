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
//Employee and EmployeeAddress are related to each other in a one-to-one relationship.
//for example, one employee has only one address and one address belongs to only one employee.
#endregion

#region One to Many
//Employee and Department are related to each other in a one-to-many relationship. 
// Mom and children relationship 
#endregion

#region Many to Many
//employees and projects are related to each other in a many-to-many relationship.
//siblings relationship
#endregion
#endregion

#region Relationship conf. in Entity Framework Core
#region Default Conventions
// default entity framework core conventions are used to configure the relationship between two entities.

//It use Navigation property for the relationship.
#endregion

#region Data Annotations Attributes
//Entities can be configured using data annotation attributes.[ForeignKey] attribute is used to configure the relationship between two entities.
//[key]
#endregion

#region Fluent API
//Fluent API is used to configure the relationship more flexibly than data annotation attributes.
//More control over the relationship.

#region HasOne
// HasOne method is used to configure the start one-to-one or one-to-many relationship.
#endregion

#region HasMany
// HasMany method is used to configure the start many to one or many-to-many relationship.
#endregion

#region WithOne
// HasOne or HasMany method is used to configure the end of the relationship.
#endregion

#region WithMany
// HasOne or HasMany method is used to configure the end of the relationship.
#endregion
#endregion
#endregion