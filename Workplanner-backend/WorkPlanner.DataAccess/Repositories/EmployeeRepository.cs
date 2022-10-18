using Workplanner_Core.Models;
using Workplanner_Domain.IRepositories;

namespace Workplanner_DataAccess.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly MainDbContext _ctx;

    public EmployeeRepository(MainDbContext ctx)
    {
        _ctx = ctx;
    }
    
    public List<Employee> ReadAll()
    {
        return _ctx.Employees.Select(e => new Employee
        {
            Id = e.Id,
            Department = e.Department,
            EmployeeNumber = e.EmployeeNumber,
            FirstName = e.FirstName,
            LastName = e.LastName
        }).ToList();
    }
}