using Microsoft.EntityFrameworkCore;
using Workplanner_Core.Models;
using Workplanner_DataAccess.Entities;
using Workplanner_Domain.IRepositories;

namespace Workplanner_DataAccess.Repositories;

public class DepartmentRepository : IDepartmentRepository
{
    private readonly MainDbContext _ctx;

    public DepartmentRepository(MainDbContext ctx)
    {
        _ctx = ctx;
    }

    public async Task<List<Department>> ReadAllDepartment()
    {
        return await _ctx.Departments.Select(d => new Department
        {
            Id = d.Id,
            DepartmentName = d.DepartmentName
        }).ToListAsync();
    }

    public async Task<Department> PostDepartment(Department department)
    {
        throw new NotImplementedException();
    }

    public async Task<Department> PatchDepartment(Department department)
    {
        var foundDepartment = await _ctx.Departments.FirstOrDefaultAsync(x => x.Id == department.Id);

        if (foundDepartment != null)
        {
            foundDepartment.Id = department.Id;
            foundDepartment.DepartmentName = department.DepartmentName;
            await _ctx.SaveChangesAsync();
            return department;
        }

        return null;
    }

    public async Task<Department> ReadByDepartmentById(int id)
    {
        var foundDepartment = await _ctx.Departments.FindAsync(id);

        if (foundDepartment != null)
        {
            var newDepartment = new Department
            {
                Id = foundDepartment.Id,
                DepartmentName = foundDepartment.DepartmentName
            };
            return newDepartment;
        }

        return null;
    }

    public async Task<Department> DeleteDepartmentId(int id)
    {
        var foundDepartment = await _ctx.Departments.FindAsync(id);

        if (foundDepartment != null)
        {
            var departmentToreturn = new Department
            {
                Id = foundDepartment.Id,
                DepartmentName = foundDepartment.DepartmentName
            };

            _ctx.Remove(foundDepartment);
            await _ctx.SaveChangesAsync();


            return departmentToreturn;
        }

        return null;
    }
}