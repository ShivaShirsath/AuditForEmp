using EmployeeAudit.Models;
namespace EmployeeAudit.Infrastructure.IRepository
{
  public interface IEventRepository : IRepository<Event>
  {
    Task<IEnumerable<Event>> GetAllAudits();
  }
}