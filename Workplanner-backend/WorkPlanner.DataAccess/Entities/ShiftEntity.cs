using NodaTime;

namespace Workplanner_DataAccess.Entities;

public class ShiftEntity
{
    public int Id { get; set; }
    public DateTimeOffset StartTime { get; set; }
    public DateTimeOffset EndTime { get; set; }
    public DateTimeOffset? ClockInTime { get; set; }
    public DateTimeOffset? ClockOutTime { get; set; }
    public int? EmployeeId { get; set; }
    public int? DepartmenId { get; set; }
    public EmployeeEntity? Employee { get; set; }
}