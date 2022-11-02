using System.Text.Encodings.Web;
using System.Text.Json;
using MicroElements.Swashbuckle.NodaTime;
using Microsoft.EntityFrameworkCore;
using Workplanner_Core.IServices;
using Workplanner_DataAccess;
using Workplanner_DataAccess.Repositories;
using Workplanner_Domain.IRepositories;
using Workplanner_Domain.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NodaTime;
using NodaTime.Serialization.SystemTextJson;
using Swashbuckle.AspNetCore.SwaggerGen;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.




builder.Services.AddControllers().AddJsonOptions(option =>
{
    option.JsonSerializerOptions.ConfigureForNodaTime(DateTimeZoneProviders.Tzdb);
});
void ConfigureSystemTextJsonSerializerSettings(JsonSerializerOptions serializerOptions)
{
    // Configures JsonSerializer to properly serialize NodaTime types.
    serializerOptions.ConfigureForNodaTime(DateTimeZoneProviders.Tzdb);

    serializerOptions.Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
}

// builder.Services.AddSwaggerGen(c =>
//
// {
//
//     c.SwaggerDoc("v1", new OpenApiInfo {Title = "WorkPlanner.Api", Version = "v1"});
//     
//     
//     var jsonSerializerOptions = new JsonSerializerOptions();
//
//     ConfigureSystemTextJsonSerializerSettings(jsonSerializerOptions);
//
//     c.ConfigureForNodaTimeWithSystemTextJson(jsonSerializerOptions);
//
// });



builder.Services.AddDbContext<MainDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IDepartmentService, DepartmentService>();
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
builder.Services.AddScoped<IShiftService, ShiftService>();
builder.Services.AddScoped<IShiftRepository, ShiftRepository>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey =
                new SymmetricSecurityKey(
                    System.Text.Encoding.UTF8.GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

app.UseCors(o => o
    .AllowAnyMethod()
    .AllowAnyHeader()
    .AllowCredentials()
    .WithOrigins("http://localhost:3000")
);

app.UseAuthorization();

app.MapControllers();

app.Run();
