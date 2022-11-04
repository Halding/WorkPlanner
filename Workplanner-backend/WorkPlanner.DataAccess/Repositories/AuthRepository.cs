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
        Console.WriteLine("testtesttesttest");
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
        // var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token:Key").Value));
        //
        // var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
        //
        // var header = new JwtHeader(creds);
        //
        // var payload = new JwtPayload(employee.Id.ToString(), null, null, null, DateTime.Now.AddMinutes(5));
        // var securityToken = new JwtSecurityToken(header, payload);
        //
        // return new JwtSecurityTokenHandler().WriteToken(securityToken);


        //create claims details based on the user information
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
            expires: DateTime.UtcNow.AddMinutes(1),
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


    private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using (var hmac = new HMACSHA512())
        {
            passwordSalt = hmac.Key;
            passwordHash = hmac
                .ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }
    }

    private JwtSecurityToken VerifyKey(string jwt)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes("AppSettings:Token:Key");

        tokenHandler.ValidateToken(jwt, new TokenValidationParameters
        {
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuerSigningKey = true,
            ValidateIssuer = false,
            ValidateAudience = false,
        }, out SecurityToken validatedToken);
        return (JwtSecurityToken)validatedToken;
    }
}