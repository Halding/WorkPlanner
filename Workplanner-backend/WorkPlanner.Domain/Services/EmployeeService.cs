using Workplanner_Core.IServices;
using Workplanner_Core.Models;
using Workplanner_Domain.IRepositories;

namespace Workplanner_Domain.Services;

public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository _employeeRepository;


    public EmployeeService(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public async Task<List<Employee>> GetAllEmployees()
    {
        return await _employeeRepository.ReadAll();
    }

    public async Task<Employee> CreateEmployee(Employee employee)
    {
         return await _employeeRepository.PostEmployee(employee);
    }
}