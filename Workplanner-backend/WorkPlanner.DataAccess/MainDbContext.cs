using Microsoft.EntityFrameworkCore;
using Workplanner_Core.Models;
using Workplanner_DataAccess.Entities;

namespace Workplanner_DataAccess;

public class MainDbContext : DbContext
{
    public MainDbContext(DbContextOptions<MainDbContext> options) : base(options)
    {
        
    }
    public DbSet<EmployeeEntity> Employees { get; set; }
    public DbSet<ShiftEntity> Shifts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EmployeeEntity>().ToTable("Employees");
        modelBuilder.Entity<EmployeeEntity>().Property(x => x.EmployeeNumber).ValueGeneratedOnAddOrUpdate();
        
        modelBuilder.Entity<EmployeeEntity>().HasData(
        new EmployeeEntity
        {
            Id = 1,
            FirstName = "Peter",
            LastName = "Jensen",
            DepartmentId = 2,
            EmployeeNumber = 1001,
            Role = "User",
            Password = "Password123"
            // PasswordHash = null,
            // PasswordSalt = null
        },
        new EmployeeEntity
        {
            Id = 2,
            FirstName = "Hans",
            LastName = "Peter",
            DepartmentId = 1,
            EmployeeNumber = 1000,
            Role = "Admin",
            Password = "Password123"
            // PasswordHash = null,
            // PasswordSalt = null
        }
    );
    }
    
    
}