using Workplanner_Core.Models;

namespace Workplanner_Domain.IRepositories;

public interface IEmployeeRepository
{
    Task<List<Employee>> ReadAll();

    Task<Employee> PostEmployee(Employee employee);
    Task<Employee> PatchEmployee(Employee employee);
    Task<Employee> ReadByEmployeeById(int id);
    Task<Employee> DeleteEmployeeId(int id);
}