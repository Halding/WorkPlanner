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

    //Gets all employees in the list
    public async Task<List<Employee>> GetAllEmployees()
    {
        return await _employeeRepository.ReadAll();
    }

    public async Task<Employee> CreateEmployee(Employee employee)
    {
        return await _employeeRepository.PostEmployee(employee);
    }

    public async Task<Employee> UpdateEmployee(Employee employee)
    {
        return await _employeeRepository.PatchEmployee(employee);
    }
    
    public async Task<Employee> GetEmployeeById(int id)
    {
        return await _employeeRepository.ReadByEmployeeById(id);
    }

    public Task<Employee> DeleteEmployeeById(int id)
    {
        return _employeeRepository.DeleteEmployeeId(id);
    }
}