using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Workplanner_Core.IServices;
using Workplanner_Core.Models.Utils;
using Workplanner_DataAccess.Entities;
using Workplanner_Domain.IRepositories;

namespace Workplanner_DataAccess.Repositories;

public class AuthRepository : IAuthRepository
{
    private readonly MainDbContext _ctx;
    private readonly IConfiguration _configuration;

    public AuthRepository(MainDbContext ctx, IConfiguration configuration)
    {
        _ctx = ctx;
        _configuration = configuration;
    }
    
    public async Task<ServiceResponse<string>> Login(int employeeNumber, string password)
    {
        var response = new ServiceResponse<string>();
        var employee = await _ctx.Employees.FirstOrDefaultAsync(x => x.EmployeeNumber.Equals(employeeNumber));
        Console.WriteLine("testtesttesttest");
        Console.WriteLine(employee.EmployeeNumber);
        

        if (employee == null)
        {
            response.Success = false;
            response.Message = "User not Found";
        }
        else if(!VerifyPasswordHash(password, employee.PasswordHash, employee.PasswordSalt))
        {
            response.Success = false;
            response.Message = "Employee number or Password is incorrect (this is password)";
        }
        
        response.Data = CreateToken(employee);
        response.Success = true;
        response.Message = "seems to work";
            
        return response;
    }

    private string? CreateToken(EmployeeEntity employee)
    {
        List<Claim> claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, employee.Id.ToString()),
            new Claim(ClaimTypes.Name, employee.FirstName + " " + employee.LastName)
        };

        var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddMinutes(10),
            signingCredentials: creds);

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);

        return jwt;

    }


    private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
    {
        using (var hmac = new HMACSHA512(passwordSalt))
        {
            var cumputedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            return cumputedHash.SequenceEqual(passwordHash);
        }
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