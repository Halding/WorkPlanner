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
    
    
    public async Task<List<Department>> GetAllDepartment()
    {
        return await _departmentRepository.ReadAllDepartment();
    }

    public async Task<Department> CreateDepartment(Department department)
    {
        throw new NotImplementedException();
    }

    public async Task<Department> UpdateDepartment(Department department)
    {
        return await _departmentRepository.PatchDepartment(department);
    }

    public async Task<Department> GetDepartmentById(int id)
    {
        return await _departmentRepository.ReadByDepartmentById(id);
    }

    public async Task<Department> DeleteDepartmentById(int id)
    {
        throw new NotImplementedException();
    }
}