using Workplanner_Core.Models;

namespace Workplanner_Domain.IRepositories;

public interface IShiftRepository
{
    Task<List<Shift>> ReadAllShift();
    Task<Shift> PostShift(Shift shift);
    Task<Shift> PatchShift(Shift shift);
    Task<Shift> ReadByShiftId(int id);
    Task<List<Shift>> ReadShiftByEmployeeId(int employeeId);
    Task<List<Shift>> ReadShiftByDepartmentId(int departmentId);
}