using System.Linq;
using Microsoft.EntityFrameworkCore;
using ReportsApi.Models;

namespace ReportsApi.Context
{
    public sealed class ReportsDbContext : DbContext
    {
        public ReportsDbContext(DbContextOptions<ReportsDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<WorkTask> WorkTasks { get; set; } = null!;
        public DbSet<Employee> Employees { get; set; } = null!;
        public DbSet<Report> Reports { get; set; } = null;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .HasMany(x => x.Tasks)
                .WithOne(x => x.Executor)
                .HasForeignKey(x => x.ExecutorId);

            modelBuilder.Entity<Report>()
                .HasOne(x => x.Writer);
            base.OnModelCreating(modelBuilder);
        }
 
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }
    }
}