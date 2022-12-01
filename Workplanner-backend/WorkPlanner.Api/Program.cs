using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using Azure.Identity;
using MicroElements.Swashbuckle.NodaTime;
using Microsoft.EntityFrameworkCore;
using Workplanner_Core.IServices;
using Workplanner_DataAccess;
using Workplanner_DataAccess.Repositories;
using Workplanner_Domain.IRepositories;
using Workplanner_Domain.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.DataProtection;
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


builder.Services.AddDbContext<MainDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("AzureConnection"));
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

// builder.Services.AddDataProtection()
//     .PersistKeysToAzureBlobStorage(new Uri("<blobUriWithSasToken>"))
//     .ProtectKeysWithAzureKeyVault(new Uri("<keyIdentifier>"), new DefaultAzureCredential());


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["AppSettings:Token:Audience"],
        ValidIssuer = builder.Configuration["AppSettings:Token:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["AppSettings:Token:Key"]))
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

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
