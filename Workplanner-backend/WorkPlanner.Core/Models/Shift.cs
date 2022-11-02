using NodaTime;

namespace Workplanner_Core.Models;

public class Shift
{
    public int Id { get; set; }
    public DateTimeOffset StartTime { get; set; }
    public DateTimeOffset EndTime { get; set; }
    public DateTimeOffset? ClockInTime { get; set; }
    public DateTimeOffset? ClockOutTime { get; set; }
    public int? EmployeeId { get; set; }
    public int? DepartmentId { get; set; }
}