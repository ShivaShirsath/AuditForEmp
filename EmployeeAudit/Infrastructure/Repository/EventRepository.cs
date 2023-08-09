using EmployeeAudit.Data;
using EmployeeAudit.Infrastructure.IRepository;
using EmployeeAudit.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeAudit.Infrastructure.Repository
{
  public class EventRepository : Repository<Event>, IEventRepository
  {
    private AppDbContext _context;
    public EventRepository(AppDbContext context) : base(context)
    {
      _context = context;
    }

    public async Task<IEnumerable<Event>> GetAllAudits()
    {
      return await _context.Events.ToListAsync();
    }
  }
}
