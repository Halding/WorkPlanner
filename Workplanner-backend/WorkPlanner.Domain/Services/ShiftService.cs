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
        throw new NotImplementedException();
    }

    public async Task<Shift> GetShiftById(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<Shift> DeleteShiftById(int id)
    {
        throw new NotImplementedException();
    }
}