using EmployeeAudit.Data;
using EmployeeAudit.Infrastructure.IRepository;

namespace EmployeeAudit.Infrastructure.Repository
{
  public class UnitOfWork : IUnitOfWork
  {
    private AppDbContext _context;
    public IEmployeeRepository Employee { get; private set; }
    public UnitOfWork(AppDbContext context)
    {
      _context = context;
      Employee = new EmployeeRepository(context);
    }
    public void Save()
    {
      _context.SaveChanges();
    }
  }
}
