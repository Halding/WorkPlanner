using Microsoft.EntityFrameworkCore;
using Workplanner_Core.Models;
using Workplanner_DataAccess.Entities;
using Workplanner_Domain.IRepositories;

namespace Workplanner_DataAccess.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly MainDbContext _ctx;

    public EmployeeRepository(MainDbContext ctx)
    {
        _ctx = ctx;
    }
    
    public async Task<List<Employee>> ReadAll()
    {
        return await _ctx.Employees.Select(e => new Employee
        {
            Id = e.Id,
            EmployeeNumber = e.EmployeeNumber,
            DepartmentId = e.DepartmentId,
            FirstName = e.FirstName,
            Role = e.Role,
            LastName = e.LastName
        }).ToListAsync();
    }

    public async Task<Employee> PostEmployee(Employee employee)
    {
        var newEmployee = new EmployeeEntity
        {
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            DepartmentId = employee.DepartmentId,
            Password = employee.Password,
            Role = employee.Role
        };
        _ctx.Employees.Add(newEmployee);
        await _ctx.SaveChangesAsync();
        return employee;
    }
    
}