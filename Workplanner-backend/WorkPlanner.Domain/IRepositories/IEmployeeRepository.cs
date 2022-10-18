using Workplanner_Core.Models;

namespace Workplanner_Domain.IRepositories;

public interface IEmployeeRepository
{
    List<Employee> ReadAll();
}