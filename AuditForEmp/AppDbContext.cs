using AuditForEmp.Models;
using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){}
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Address> Addresses { get; set; }
}
