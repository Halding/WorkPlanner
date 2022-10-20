namespace Workplanner_Core.Models;

public class Employee
{
    public int Id { get; set; }
    public int EmployeeNumber { get; set; }
    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";
    public int? DepartmentId { get; set; }
    public string Role { get; set; } = "";

    public string Password { get; set; } = string.Empty;
    // public byte[]? PasswordSalt { get; set; }
    // public byte[]? PasswordHash { get; set; }

}