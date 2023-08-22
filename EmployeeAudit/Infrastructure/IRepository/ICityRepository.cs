using EmployeeAudit.Models;
namespace EmployeeAudit.Infrastructure.IRepository
{
  public interface ICityRepository : IRepository<City>
  {
    Task<IEnumerable<City>> GetAllCities();
    Task<IEnumerable<City>> GetCitiesByState(string stateName);
  }
}