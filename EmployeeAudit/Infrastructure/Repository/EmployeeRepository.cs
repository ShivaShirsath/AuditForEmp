using EmployeeAudit.Data;
using EmployeeAudit.Infrastructure.IRepository;
using EmployeeAudit.Models;

namespace EmployeeAudit.Infrastructure.Repository
{
  public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
  {
    private AppDbContext _context;
    public EmployeeRepository(AppDbContext context) : base(context)
    {
      _context = context;
    }
    public void Update(Employee entity)
    {
      _context.Employees.Update(entity);
    }
  }
}
