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
    public async Task<List<Department>> ReadAllDepartments()
    {
        return await _ctx.Departments.Select(e => new Department
        {
            Id = e.Id,
            DepartmentName = e.DepartmentName,
        }).ToListAsync();
    }

    public async Task<Department> CreateDepartment(Department department)
    {
        var newDepartment = new DepartmentEntity
        {
            DepartmentName = department.DepartmentName
        };
        _ctx.Departments.Add(newDepartment);
        await _ctx.SaveChangesAsync();

        return department;
    }

    public async Task<Department> UpdateDepartment(Department department)
    {
        var foundDepartmentEntity = await _ctx.Departments.FirstOrDefaultAsync(x => x.Id == department.Id);

        if (foundDepartmentEntity != null)
        {
            foundDepartmentEntity.DepartmentName = department.DepartmentName;

            await _ctx.SaveChangesAsync();
            return department;
        }

        return null;
    }

    public async Task<Department> GetDepartmentById(int id)
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

    public async Task<Department> DeleteDepartmentById(int id)
    {
        var foundDepartment = await _ctx.Departments.FindAsync(id);
        if (foundDepartment!= null)
        {
            var newDepartment = new Department
            {
              Id = foundDepartment.Id,
              DepartmentName = foundDepartment.DepartmentName
            };
            
            _ctx.Remove(foundDepartment);
            await _ctx.SaveChangesAsync();
            return newDepartment;

        }
        return null;
    }
}