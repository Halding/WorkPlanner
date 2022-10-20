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
            Role = e.Role,
            FirstName = e.FirstName,
            LastName = e.LastName
            
        }).ToListAsync();
    }

    public async Task<Employee> PostEmployee(Employee employee)
    {
        var newEmployee = new EmployeeEntity
        {
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            Role = employee.Role,
            DepartmentId = employee.DepartmentId,
            Password = employee.Password
        };
        _ctx.Employees.Add(newEmployee);
        await _ctx.SaveChangesAsync();
        
        return employee;
    }
}