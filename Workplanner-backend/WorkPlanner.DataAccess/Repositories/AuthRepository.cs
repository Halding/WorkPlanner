using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Http;
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
        Console.WriteLine(employee.EmployeeNumber);


        if (employee == null)
        {
            response.Success = false;
            response.Message = "User not Found";
        }
        else if (!VerifyPasswordHash(password, employee.PasswordHash, employee.PasswordSalt))
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
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, _configuration["AppSettings:Token:Subject"]),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
            new Claim("UserId", employee.Id.ToString()),
            new Claim("EmployeeNumber", employee.EmployeeNumber.ToString()),
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["AppSettings:Token:Key"]));
        var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            _configuration["AppSettings:Token:Issuer"],
            _configuration["AppSettings:Token:Audience"],
            claims,
            expires: DateTime.UtcNow.AddMinutes(360),
            signingCredentials: signIn);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }


    private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
    {
        using (var hmac = new HMACSHA512(passwordSalt))
        {
            var cumputedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            return cumputedHash.SequenceEqual(passwordHash);
        }
    }


    
}