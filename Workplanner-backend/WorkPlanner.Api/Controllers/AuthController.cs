using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WorkPlanner.Api.Dtos;
using Workplanner_Core.IServices;
using Workplanner_Core.Models.Utils;

namespace WorkPlanner.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        
        [HttpPost("login")]
        public async Task<ActionResult<ServiceResponse<string>>> Login(LoginDto request)
        {
            var response = await _authService.Login(request.EmployeeNumber, request.Password);
            if (!response.Success)
            {
                return BadRequest(response);
            }

            Response.Cookies.Append("OurJwt", response.Data, new CookieOptions
                           {
                               HttpOnly = false
                           });
            return Ok("success");

            // return Ok(response);
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("OurJwt");

            return Ok("logout success");
        }

        
        
    }
}
