using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Logging;
using Workplanner_Core.IServices;
using Workplanner_Core.Models;

namespace WorkPlanner.Api.Controllers
{
    // [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        
        [HttpPost("create")]
        public async Task<ActionResult<Employee>> PostEmployee(Employee employee)
        {
            var result = await _employeeService.CreateEmployee(employee);
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<Employee>> ReadAllEmployee()
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

        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployeeById(int id)
        {
            var result = await _employeeService.GetEmployeeById(id);

            return Ok(result);
        }

        [HttpGet("employeeNumber/{employeeNumber}")]
        public async Task<ActionResult<Employee>> GetEmployeeByEmployeeNumber(int employeeNumber)
        {
            var result = await _employeeService.GetEmployeeByEmployeeNumber(employeeNumber);

            return Ok(result);
        }


        [HttpGet("user")]
        public async Task<IActionResult> GetUser()
        {
            var userId = User.FindFirstValue("UserId");

            Console.WriteLine(userId);
            
            var user = await _employeeService.GetEmployeeById(int.Parse(userId));

            return Ok(user);
        }
        
        [HttpPatch("update/{id}")]
        public async Task<ActionResult<Employee>> PatchEmployee(Employee employee)
        {
            var result = await _employeeService.UpdateEmployee(employee);

            return Ok(result);
        }
        
        [HttpDelete("delete/{id}")]
        public async Task<ActionResult<Employee>> DeleteEmployeeById(int id)
        {
            var result = await _employeeService.DeleteEmployeeById(id);

            return Ok(result);
        }
    }
}