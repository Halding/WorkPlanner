using System.IdentityModel.Tokens.Jwt;
using Workplanner_Core.Models;

namespace Workplanner_Domain.IRepositories;

public interface IEmployeeRepository
{
    Task<List<Employee>> ReadAllEmployee();

    Task<Employee> PostEmployee(Employee employee);
    Task<Employee> PatchEmployee(Employee employee);
    Task<Employee> ReadByEmployeeById(int id);
    Task<Employee> DeleteEmployeeId(int id);
    Task<Employee> ReadEmployeeByEmployeeNumber(int employeeNumber);

}