using Microsoft.EntityFrameworkCore;
using ReportsApi.Models;

namespace ReportsApi.Context
{
    public class TaskContext : DbContext
    {
        public TaskContext(DbContextOptions<TaskContext> options)
            : base(options)
        {
        }

        public DbSet<Employee> Tasks { get; set; } = null!;

        public DbSet<ReportsApi.Models.Task> Task { get; set; }
    }
}