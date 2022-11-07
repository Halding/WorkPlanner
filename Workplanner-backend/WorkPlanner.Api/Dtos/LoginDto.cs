using Microsoft.Build.Framework;

namespace WorkPlanner.Api.Dtos;

public class LoginDto
{
    [Required]
     public int EmployeeNumber { get; set; }
     
     [Required]
     public string Password { get; set; }
     
}