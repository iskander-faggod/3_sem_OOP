using Microsoft.EntityFrameworkCore;
using ReportsApi.Models;

namespace ReportsApi.Context
{
    public class EmployeeContext : DbContext
    {
        public EmployeeContext(DbContextOptions<EmployeeContext> options)
            : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; } = null!;
    }
}