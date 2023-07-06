using AuditForEmp.Models;
using Microsoft.EntityFrameworkCore;

namespace AuditForEmp
{
    public class AppDbContext: DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Address)
                .WithOne()
                .HasForeignKey<Employee>(e => e.Id);
        }
    }
}
