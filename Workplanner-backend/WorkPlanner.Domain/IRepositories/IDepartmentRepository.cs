using Workplanner_Core.Models;

namespace Workplanner_Domain.IRepositories;

public interface IDepartmentRepository
{
    Task<List<Department>> ReadAllDepartment();

    Task<Department> PostDepartment(Department department);
    Task<Department> PatchDepartment(Department department);
    Task<Department> ReadByDepartmentById(int id);
    Task<Department> DeleteDepartmentId(int id);
    
}