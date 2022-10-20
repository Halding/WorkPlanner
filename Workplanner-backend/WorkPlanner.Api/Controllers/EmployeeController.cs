using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Workplanner_Core.IServices;
using Workplanner_Core.Models;

namespace WorkPlanner.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }


        [HttpGet]
        public async Task<ActionResult<Employee>> ReadAll()
        {
            try
            {
                var result = await _employeeService.GetAllEmployees();
                return Ok(result);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        //Lav try catch, error handling?
        [HttpPost("Create")]
        public async Task<ActionResult<Employee>> PostEmployee(Employee employee)
        {
            var result = await _employeeService.CreateEmployee(employee);
            return Ok(result);
        }
        //Lav try catch, error handling?
        [HttpPatch("Update/{id}")]
        public async Task<ActionResult<Employee>> PatchEmployee(Employee employee)
        {
            var result = await _employeeService.UpdateEmployee(employee);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> ReadEmployeeById(int id)
        {
            var result = await _employeeService.GetEmployeeById(id);
            return Ok(result);
        }
    }
}
