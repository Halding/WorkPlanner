namespace Workplanner_DataAccess.Entities;

public class EmployeeEntity
{
    public int Id { get; set; }
    public int EmployeeNumber { get; set; }
    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";
    
    public string? Title { get; set; }
    public int? DepartmentId { get; set; }
    public string Role { get; set; } = "";
    public byte[]? PasswordSalt { get; set; }
    public byte[]? PasswordHash { get; set; }
}