using System.IdentityModel.Tokens.Jwt;
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
        return await _employeeRepository.ReadAllEmployee();
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

    public async Task<Employee> GetEmployeeByEmployeeNumber(int employeeNumber)
    {
        return await _employeeRepository.ReadEmployeeByEmployeeNumber(employeeNumber);
    }

    public Task<Employee> DeleteEmployeeById(int id)
    {
        return _employeeRepository.DeleteEmployeeId(id);
    }

    public JwtSecurityToken VerifyKey(string jwt)
    {
        return _employeeRepository.ValidateTokenByJwt(jwt);
    }
}