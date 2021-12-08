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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .HasMany<WorkTask>(x => x.Tasks)
                .WithOne(x => x.Executor);
            modelBuilder.Entity<WorkTask>()
                .HasOne<Employee>(x => x.Executor)
                .WithMany(x => x.Tasks);
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }
    }
}