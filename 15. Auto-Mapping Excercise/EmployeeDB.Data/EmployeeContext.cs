using EmployeeDB.Data.Configurations;
using EmployeeDB.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace EmployeeDB.Data
{
    public class EmployeeContext : DbContext
    {
        public EmployeeContext()
        { }

        public EmployeeContext(DbContextOptions options) : base(options)
        { }

        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration<Employee>(new EmployeeConfiguration());
        }
    }
}
