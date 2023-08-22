using EmployeeAudit.Models;
namespace EmployeeAudit.Infrastructure.IRepository
{
  public interface ICountryRepository : IRepository<Country>
  {
    Task<IEnumerable<Country>> GetAllContries();
  }
}