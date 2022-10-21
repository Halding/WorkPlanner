using Workplanner_Core.Models;

namespace Workplanner_Domain.IRepositories;

public interface IDepartmentRepository
{
    Task<List<Department>> ReadAllDepartments();

    Task<Department> CreateDepartment(Department department);

    Task<Department> UpdateDepartment(Department department);

    Task<Department> GetDepartmentById(int id);

    Task<Department> DeleteDepartmentById(int id);
}