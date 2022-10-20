namespace Workplanner_DataAccess.Entities;

public class EmployeeEntity
{
    public int Id { get; set; }
    public int EmployeeNumber { get; set; }
    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";
    public int? DepartmentId { get; set; }
    public string Role { get; set; } = "";
    public string Password { get; set; } = "";
}