using EmployeeAudit.Models;

namespace EmployeeAudit.Infrastructure.IRepository
{
  public interface IStateRepository : IRepository<State>
  {
    Task<IEnumerable<State>> GetAllStates();
    Task<IEnumerable<State>> GetAllStatesByContry(string countryName);
  }
}
