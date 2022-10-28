using System.IdentityModel.Tokens.Jwt;
using Workplanner_Core.Models;

namespace Workplanner_Core.IServices;

public interface IEmployeeService
{
    Task<List<Employee>> GetAllEmployees();

    Task<Employee> CreateEmployee(Employee employee);

    Task<Employee> UpdateEmployee(Employee employee);

    Task<Employee> GetEmployeeById(int id);
    
    Task<Employee> GetEmployeeByEmployeeNumber(int employeeNumber);
    

    Task<Employee> DeleteEmployeeById(int id);

    JwtSecurityToken VerifyKey(string jwt);


}