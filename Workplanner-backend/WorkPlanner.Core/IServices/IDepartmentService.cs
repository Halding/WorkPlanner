using Workplanner_Core.Models;

namespace Workplanner_Core.IServices;

public interface IDepartmentService
{
    Task<List<Department>> GetAllDepartments();

    Task<Department> CreateDepartment(Department department);

    Task<Department> UpdateDepartment(Department department);

    Task<Department> GetDepartmentById(int id);

    Task<Department> DeleteDepartmentById(int id);
}