using EmployeeAudit.Data;
using EmployeeAudit.Infrastructure.IRepository;
using EmployeeAudit.Models;

namespace EmployeeAudit.Infrastructure.Repository
{
  public class EventRepository : Repository<Event>, IEventRepository
  {
    private AppDbContext _context;
    public EventRepository(AppDbContext context) : base(context)
    {
      _context = context;
    }
  }
}
