using Workplanner_Core.IServices;
using Workplanner_Core.Models;
using Workplanner_Domain.IRepositories;

namespace Workplanner_Domain.Services;

public class DepartmentService : IDepartmentService
{
    private readonly IDepartmentRepository _departmentRepository;

    public DepartmentService(IDepartmentRepository departmentRepository)
    {
        _departmentRepository = departmentRepository;
    }
    public async Task<List<Department>> GetAllDepartments()
    {
        return await _departmentRepository.ReadAllDepartments();
    }

    public async Task<Department> CreateDepartment(Department department)
    {
        return await _departmentRepository.CreateDepartment(department);
    }

    public async Task<Department> UpdateDepartment(Department department)
    {
        return await _departmentRepository.UpdateDepartment(department);
    }

    public async Task<Department> GetDepartmentById(int id)
    {
        return await _departmentRepository.GetDepartmentById(id);
    }

    public async Task<Department> DeleteDepartmentById(int id)
    {
        return await _departmentRepository.DeleteDepartmentById(id);
    }
}