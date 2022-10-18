using Microsoft.EntityFrameworkCore;
using Workplanner_Core.Models;

namespace Workplanner_DataAccess;

public class MainDbContext : DbContext
{
    public MainDbContext(DbContextOptions<MainDbContext> options) : base(options)
    {
        
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>().HasData(
            new Employee
            {
                Id = 1,
                FirstName = "Peter",
                LastName = "Jensen",
                Department = null,
                EmployeeNumber = 1,
                Role = "User",
                PasswordHash = null,
                PasswordSalt = null
            },
            new Employee
            {
                Id = 2,
                FirstName = "Hans",
                LastName = "Peter",
                Department = null,
                EmployeeNumber = 2,
                Role = "Admin",
                PasswordHash = null,
                PasswordSalt = null
            }
        );
    }

    public DbSet<Employee> Employees { get; set; }
    public DbSet<Shift> Shifts { get; set; }
    
}