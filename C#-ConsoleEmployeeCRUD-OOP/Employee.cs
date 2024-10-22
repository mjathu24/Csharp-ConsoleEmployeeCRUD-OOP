using System.Runtime.Intrinsics.X86;

namespace EmployeeCRUDApp;

public class Employee
{
    internal string newLastName;

    public string EmployeeId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string DateOfBirth { get; set; }
}
