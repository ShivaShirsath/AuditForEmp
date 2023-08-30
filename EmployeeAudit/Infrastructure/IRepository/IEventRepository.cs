using EmployeeAudit.Models;
namespace EmployeeAudit.Infrastructure.IRepository
{
  public interface IEventRepository : IRepository<Event>
  {
    Task<IEnumerable<Event>> GetAllAudits();
    Task<IEnumerable<Event>> GetEventsByEventType(string eventType);
    Task<IEnumerable<string>> GetEventTypes();
  }
}