using EmployeeAudit.Data;
using EmployeeAudit.Infrastructure.IRepository;
using EmployeeAudit.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EmployeeAudit.Infrastructure.Repository
{
  internal class ServiceRepository : Repository<Service>, IServiceRepository
  {
    private AppDbContext _context;
    public ServiceRepository(AppDbContext context) : base(context) => _context = context;
    public void Update(Service entity) => _context.Services.Update(entity);
    public async Task<IEnumerable<Service>> GetServicesAsync() => await _context.Services.ToListAsync();
    public async Task<Service?> GetServiceAsync(Expression<Func<Service, bool>> predicate) => await _context.Services.FirstOrDefaultAsync(predicate);
  }
}