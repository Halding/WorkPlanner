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
    public class ShiftController : ControllerBase
    {
        private readonly IShiftService _shiftService;

        public ShiftController(IShiftService shiftService)
        {
            _shiftService = shiftService;
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
        
        
        [HttpPost("create")]
        public async Task<ActionResult<Shift>> PostEmployee(Shift shift)
        {
            var result = await _shiftService.CreateShift(shift);
            return Ok(result);
        }

    }
}
