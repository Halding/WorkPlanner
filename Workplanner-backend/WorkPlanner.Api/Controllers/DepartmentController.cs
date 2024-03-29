using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Workplanner_Core.IServices;
using Workplanner_Core.Models;
using Workplanner_Domain.IRepositories;

namespace WorkPlanner.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }
        
        
        [HttpPost ("create")]
        public async Task<ActionResult<Department>> PostDepartment(Department department)
        {
            var result = await _departmentService.CreateDepartment(department);
            return Ok(result);
        }
        
        [HttpGet]
        public async Task<ActionResult<Department>> ReadAllDepartment()
        {
            try
            {
                var result = await _departmentService.GetAllDepartment();
                return Ok(result);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<Department>> GetDepartmentById(int id)
        {
            var result = await _departmentService.GetDepartmentById(id);

            return Ok(result);
        }

        [HttpPatch("update/{id}")]
        public async Task<ActionResult<Department>> UpdateDepartment(Department department)
        {
            var result = await _departmentService.UpdateDepartment(department);

            return Ok(result);
        }
        
        [HttpDelete("delete/{id}")]
        public async Task<ActionResult<Employee>> DeleteDepartmentById(int id)
        {
            var result = await _departmentService.DeleteDepartmentById(id);

            return Ok(result);
        }


    }
}
