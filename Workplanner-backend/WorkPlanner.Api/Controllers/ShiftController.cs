using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Workplanner_Core.IServices;
using Workplanner_Core.Models;

namespace WorkPlanner.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShiftController : ControllerBase
    {
        private readonly IShiftService _shiftService;

        public ShiftController(IShiftService shiftService)
        {
            _shiftService = shiftService;
        }
        
        [HttpPost("create")]
        public async Task<ActionResult<Shift>> PostEmployee(Shift shift)
        {
            var result = await _shiftService.CreateShift(shift);
            return Ok(result);
        }
        
        [HttpGet]
        public async Task<ActionResult<Shift>> ReadAllShift()
        {
            try
            {
                var result = await _shiftService.GetAllShift();
                return Ok(result);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        
        
        [HttpGet("{id}")]
        public async Task<ActionResult<Shift>> GetShiftById(int id)
        {
            var result = await _shiftService.GetShiftById(id);

            return Ok(result);
        }

        [HttpGet("employee/{employeeId}")]
        public async Task<ActionResult<Shift>> GetShiftByEmployeeId(int employeeId)
        {
            var result = await _shiftService.GetShiftByEmployeeId(employeeId);

            return Ok(result);
        }
        
        [HttpGet("department/{departmentId}")]
        public async Task<ActionResult<Shift>> GetShiftByDepartmentId(int departmentId)
        {
            var result = await _shiftService.GetShiftByDepartmentId(departmentId);

            return Ok(result);
        }
        
        [HttpGet("employeeId")]
        public async Task<IActionResult> GetUser()
        {
            var employeeId = User.FindFirstValue("UserId");

            Console.WriteLine(employeeId);

            var shift = await _shiftService.GetShiftByEmployeeId(int.Parse(employeeId));

            return Ok(shift);
        }
        
        
        
        [HttpPatch("update/{id}")]
        public async Task<ActionResult<Shift>> PatchShift(Shift shift)
        {
            var result = await _shiftService.UpdateShift(shift);

            return Ok(result);
        }
        
        [HttpDelete("delete/{id}")]
        public async Task<ActionResult<Employee>> DeleteShiftById(int id)
        {
            var result = await _shiftService.DeleteShiftById(id);

            return Ok(result);
        }

    }
}
