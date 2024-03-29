﻿using Microsoft.EntityFrameworkCore;
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
    public DbSet<DepartmentEntity> Departments { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EmployeeEntity>().ToTable("Employees").HasKey(x => x.Id);;
        modelBuilder.Entity<EmployeeEntity>().Property(x => x.EmployeeNumber).ValueGeneratedOnAddOrUpdate();
        modelBuilder.Entity<DepartmentEntity>().ToTable("Departments").HasKey(x => x.Id);;
        modelBuilder.Entity<ShiftEntity>().ToTable("Shift").HasKey(x => x.Id);
        modelBuilder.Entity<ShiftEntity>().HasOne(x => x.Employee).WithMany().HasForeignKey(x => x.EmployeeId);

        

        modelBuilder.Entity<DepartmentEntity>().HasData(
            new DepartmentEntity
            {
                Id = 1,
                DepartmentName = "Lager"
            },
            new DepartmentEntity
            {
                Id = 2,
                DepartmentName = "Bager"
            }
        );
    }
}