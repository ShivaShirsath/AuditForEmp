using EmpAddrAudit.Models;
using Microsoft.EntityFrameworkCore;

namespace EmpAddrAudit.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Address> Address { get; set; }
    }
}
