using EmployeeAudit.Models;
using System.Linq.Expressions;

namespace EmployeeAudit.Infrastructure.IRepository
{
  public interface IServiceRepository : IRepository<Service>
  {
    void Update(Service entity);
    Task<Service?> GetServiceAsync(Expression<Func<Service, bool>> predicate);
    Task<IEnumerable<Service>> GetServicesAsync();
  }
}