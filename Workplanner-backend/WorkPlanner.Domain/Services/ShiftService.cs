using Workplanner_Core.IServices;
using Workplanner_Core.Models;
using Workplanner_Domain.IRepositories;

namespace Workplanner_Domain.Services;

public class ShiftService : IShiftService
{
    private readonly IShiftRepository _shiftRepository;

    public ShiftService(IShiftRepository shiftRepository)
    {
        _shiftRepository = shiftRepository;
    }
    
    public async Task<List<Shift>> GetAllShift()
    {
        return await _shiftRepository.ReadAllShift();
    }

    public async Task<Shift> CreateShift(Shift shift)
    {
        return await _shiftRepository.PostShift(shift);
    }

    public async Task<Shift> UpdateShift(Shift shift)
    {
        return await _shiftRepository.PatchShift(shift);
    }

    public async Task<Shift> GetShiftById(int id)
    {
        return await _shiftRepository.ReadByShiftId(id);
    }

    public async Task<List<Shift>> GetShiftByEmployeeId(int employeeId)
    {
        return await _shiftRepository.ReadShiftByEmployeeId(employeeId);
    }

    public async Task<List<Shift>> GetShiftByDepartmentId(int departmentId)
    {
        return await _shiftRepository.ReadShiftByDepartmentId(departmentId);
    }

    public async Task<Shift> DeleteShiftById(int id)
    {
        return await _shiftRepository.DeleteById(id);
    }
}