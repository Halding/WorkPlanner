namespace Workplanner_Core.Models;

public class Shift
{
    public int Id { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public DateTime ClockInTime { get; set; }
    public DateTime ClockOutTime { get; set; }
    public int EmployeeId { get; set; }
    public int DepartmentId { get; set; }
}