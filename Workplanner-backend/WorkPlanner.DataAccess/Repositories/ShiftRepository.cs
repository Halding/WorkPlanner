using Microsoft.EntityFrameworkCore;
using NodaTime;
using Workplanner_Core.Models;
using Workplanner_DataAccess.Entities;
using Workplanner_Domain.IRepositories;

namespace Workplanner_DataAccess.Repositories;

public class ShiftRepository : IShiftRepository
{
    private readonly MainDbContext _ctx;

    public ShiftRepository(MainDbContext ctx)
    {
        _ctx = ctx;
    }
    
    
    
    public async Task<List<Shift>> ReadAllShift()
    {
        
        return await _ctx.Shifts.Select(s => new Shift
        {
            Id = s.Id,
            StartTime = s.StartTime,
            EndTime = s.EndTime,
            ClockInTime = s.ClockInTime,
            ClockOutTime = s.ClockOutTime,
            EmployeeId = s.EmployeeId,
            DepartmentId = s.DepartmenId
        }).ToListAsync();
    }

    public async Task<Shift> PostShift(Shift shift)
    {
        var newShift = new ShiftEntity
        {
            StartTime = shift.StartTime,
            EndTime = shift.EndTime,
            ClockInTime = shift.ClockInTime,
            ClockOutTime = shift.ClockOutTime,
            EmployeeId = shift.EmployeeId,
            DepartmenId = shift.DepartmentId
        };

        _ctx.Add(newShift);
        await _ctx.SaveChangesAsync();
        return shift;

    }

    public async Task<Shift> PatchShift(Shift shift)
    {
        var foundShiftEntity = await _ctx.Shifts.FirstOrDefaultAsync(x => x.Id == shift.Id);

        if (foundShiftEntity != null)
        {
            foundShiftEntity.DepartmenId = shift.DepartmentId;
            foundShiftEntity.EmployeeId = shift.EmployeeId;
            foundShiftEntity.EndTime = shift.EndTime;
            foundShiftEntity.StartTime = shift.StartTime;
            foundShiftEntity.ClockInTime = shift.ClockInTime;
            foundShiftEntity.ClockOutTime = shift.ClockOutTime;
            
            await _ctx.SaveChangesAsync();
            return shift;
        }

        return null;
    }
}