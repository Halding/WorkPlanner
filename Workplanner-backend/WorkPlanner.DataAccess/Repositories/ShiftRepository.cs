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

    public async Task<Shift> ReadByShiftId(int id)
    {
        var foundShiftEntity = await _ctx.Shifts.FirstOrDefaultAsync(x => x.Id == id);

        if (foundShiftEntity != null)
        {
            var newShiftModel = new Shift
            {
                Id = foundShiftEntity.Id,
                ClockInTime = foundShiftEntity.ClockInTime,
                ClockOutTime = foundShiftEntity.ClockOutTime,
                DepartmentId = foundShiftEntity.DepartmenId,
                EmployeeId = foundShiftEntity.EmployeeId,
                EndTime = foundShiftEntity.EndTime,
                StartTime = foundShiftEntity.StartTime
            };

            return newShiftModel;
        }

        return null;
    }

    public async Task<List<Shift>> ReadShiftByEmployeeId(int employeeId)
    {
        var listOfShift = await _ctx.Shifts.Where(s => s.EmployeeId == employeeId).ToListAsync();


        var newList = new List<Shift>();

        listOfShift.ForEach(e => newList.Add(new Shift
        {
            Id = e.Id,
            ClockInTime = e.ClockInTime,
            ClockOutTime = e.ClockOutTime,
            DepartmentId = e.DepartmenId,
            EmployeeId = e.EmployeeId,
            EndTime = e.EndTime,
            StartTime = e.StartTime
        }));

        return newList;
    }

    public async Task<List<Shift>> ReadShiftByDepartmentId(int departmentId)
    {
        var listOfShift = await _ctx.Shifts.Where(s => s.DepartmenId == departmentId).ToListAsync();


        var newList = new List<Shift>();

        listOfShift.ForEach(e => newList.Add(new Shift
        {
            Id = e.Id,
            ClockInTime = e.ClockInTime,
            ClockOutTime = e.ClockOutTime,
            DepartmentId = e.DepartmenId,
            EmployeeId = e.EmployeeId,
            EndTime = e.EndTime,
            StartTime = e.StartTime
        }));

        return newList;
    }
}