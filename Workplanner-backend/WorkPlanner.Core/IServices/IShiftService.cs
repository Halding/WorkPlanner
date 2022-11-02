using Workplanner_Core.Models;

namespace Workplanner_Core.IServices;

public interface IShiftService
{
    Task<List<Shift>> GetAllShift();

    Task<Shift> CreateShift(Shift shift);

    Task<Shift> UpdateShift(Shift shift);

    Task<Shift> GetShiftById(int id);

    Task<Shift> DeleteShiftById(int id);
}