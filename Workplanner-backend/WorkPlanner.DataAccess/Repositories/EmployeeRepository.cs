using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Workplanner_Core.Models;
using Workplanner_DataAccess.Entities;
using Workplanner_Domain.IRepositories;

namespace Workplanner_DataAccess.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly MainDbContext _ctx;

    public EmployeeRepository(MainDbContext ctx)
    {
        _ctx = ctx;
    }

    public async Task<List<Employee>> ReadAllEmployee()
    {
        return await _ctx.Employees.Select(e => new Employee
        {
            Id = e.Id,
            Title = e.Title,
            EmployeeNumber = e.EmployeeNumber,
            DepartmentId = e.DepartmentId,
            Role = e.Role,
            FirstName = e.FirstName,
            LastName = e.LastName
        }).ToListAsync();
    }

    public async Task<Employee> PostEmployee(Employee employee)
    {

        
        CreatePasswordHash(employee.Password, out byte[] passwordHash, out byte[] passwordSalt);

        
        var newEmployee = new EmployeeEntity
        {
            Title = employee.Title,
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            Role = employee.Role,
            DepartmentId = employee.DepartmentId,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt
        };

        _ctx.Employees.Add(newEmployee);
        await _ctx.SaveChangesAsync();
        
        return employee;
    }

    public async Task<Employee> PatchEmployee(Employee employee)
    {
        var foundEmployeeEntity = await _ctx.Employees.FirstOrDefaultAsync(x => x.Id == employee.Id);

        if (foundEmployeeEntity != null)
        {
            foundEmployeeEntity.Title = employee.Title;
            foundEmployeeEntity.FirstName = employee.FirstName;
            foundEmployeeEntity.LastName = employee.LastName;
            foundEmployeeEntity.DepartmentId = employee.DepartmentId;
            foundEmployeeEntity.Role = employee.Role;
            

            await _ctx.SaveChangesAsync();
            return employee;
        }

        return null;
    }

    public async Task<Employee> ReadByEmployeeById(int id)
    {
        var testEmployee = await _ctx.Employees.FindAsync(id);

        if (testEmployee != null)
        {
            var newEmployee = new Employee
            {
                Title = testEmployee.Title,
                Id = testEmployee.Id,
                FirstName = testEmployee.FirstName,
                LastName = testEmployee.LastName,
                DepartmentId = testEmployee.DepartmentId,
                EmployeeNumber = testEmployee.EmployeeNumber,
                Role = testEmployee.Role
            };
            return newEmployee;
        }

        return null;
    }

    public async Task<Employee> DeleteEmployeeId(int id)
    {
        var testEmployee = await _ctx.Employees.FindAsync(id);
        if (testEmployee != null)
        {
            var newEmployee = new Employee
            {
                Title = testEmployee.Title,
                Id = testEmployee.Id,
                FirstName = testEmployee.FirstName,
                LastName = testEmployee.LastName,
                DepartmentId = testEmployee.DepartmentId,
                EmployeeNumber = testEmployee.EmployeeNumber,
                Role = testEmployee.Role
            };
            
            _ctx.Remove(testEmployee);
            await _ctx.SaveChangesAsync();
            return newEmployee;

        }
        return null;
    }

    public async Task<Employee> ReadEmployeeByEmployeeNumber(int employeeNumber)
    {
        var testEmployee = await _ctx.Employees.FirstOrDefaultAsync(x => x.EmployeeNumber == employeeNumber);

        if (testEmployee != null)
        {
            var newEmployee = new Employee
            {
                Title = testEmployee.Title,
                Id = testEmployee.Id,
                FirstName = testEmployee.FirstName,
                LastName = testEmployee.LastName,
                DepartmentId = testEmployee.DepartmentId,
                EmployeeNumber = testEmployee.EmployeeNumber,
                Role = testEmployee.Role
            };
            return newEmployee;
        }

        return null;
    }
    
    
    private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using (var hmac = new HMACSHA512())
        {
            passwordSalt = hmac.Key;
            passwordHash = hmac
                .ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }
    }
    
   
    
    
}