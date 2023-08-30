using EmployeeAudit.Data;
using EmployeeAudit.Infrastructure.IRepository;
using EmployeeAudit.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeAudit.Infrastructure.Repository
{
  public class EventRepository : Repository<Event>, IEventRepository
  {
    private AppDbContext _context;

    public EventRepository(AppDbContext context) : base(context) => _context = context;

    public async Task<IEnumerable<Event>> GetAllAudits() => await _context.Events.ToListAsync();

    public async Task<IEnumerable<Event>> GetEventsByEventType(string eventType) => await _context.Events
               .Where(e => e.EventType.StartsWith(eventType == "all" ? "" : eventType))
               .ToListAsync();
    public async Task<IEnumerable<string>> GetEventTypes() => await _context.Events
          .Select(e => e.EventType)
          .Distinct()
          .ToListAsync();
  }
}
