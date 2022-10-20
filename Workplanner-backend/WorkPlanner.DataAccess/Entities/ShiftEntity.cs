namespace Workplanner_DataAccess.Entities;

public class ShiftEntity
{
    public int Id { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public DateTime ClockInTime { get; set; }
    public DateTime ClockOutTime { get; set; }
    public int EmployeeId { get; set; }
    public int DepartmenId { get; set; }
}