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
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }
        [HttpGet]
        public async Task<ActionResult<Department>> ReadAll()
        {
            try
            {
                var result = await _departmentService.GetAllDepartments();
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

        [HttpPost ("create")]
        public async Task<ActionResult<Department>> PostDepartment(Department department)
        {
            var result = await _departmentService.CreateDepartment(department);
            return Ok(result);
        }
        
        [HttpPatch("update/{id}")]
        public async Task<ActionResult<Department>> PatchDepartment(Department department)
        {
            var result = await _departmentService.UpdateDepartment(department);

            return Ok(result);
        }

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult<Department>> DeleteDepartmentById(int id)
        {
            var result = await _departmentService.DeleteDepartmentById(id);

            return Ok(result);
        }
    }
}
