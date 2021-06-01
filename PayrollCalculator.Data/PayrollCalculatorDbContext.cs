using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using static System.Net.Mime.MediaTypeNames;

namespace PayrollCalculator.Data
{
    public sealed class PayrollCalculatorDbContext : DbContext
    {
        public DbSet<EmployeeEntity> Employees { get; set; }
        public DbSet<DependentEntity> Dependents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmployeeEntity>()
                .HasMany(employee => employee.Dependents)
                .WithOne(dependent => dependent.Employee);                
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {            
            optionsBuilder.UseSqlite(@"Data Source=C:\Users\javie\Documents\Projects\PayrollCalculator\PayrollCalculator.Data\PayrollCalculator.db");
        }
    }
}
