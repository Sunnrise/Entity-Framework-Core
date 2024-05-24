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

#endregion

#region Principal Key

#endregion

class Employee
{
    public int Id { get; set; }
    public string EmployeeName { get; set; }
    public int DepartmentId { get; set; }

    public Department Departments { get; set; }
}
class Department
{
    public int Id { get; set; }
    public string DepartmentName { get; set; }

    public List<Employee> Employees { get; set; }
}
#endregion

#region What is an Navigation Property 
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