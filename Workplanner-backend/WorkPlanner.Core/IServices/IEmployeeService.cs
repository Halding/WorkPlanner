﻿using Workplanner_Core.Models;

namespace Workplanner_Core.IServices;

public interface IEmployeeService
{
    Task<List<Employee>> GetAllEmployees();

    Task<Employee> CreateEmployee(Employee employee);

    Task<Employee> UpdateEmployee(Employee employee);

    Task<Employee> GetEmployeeById(int id);

    Task<Employee> DeleteEmployeeById(int id);


}