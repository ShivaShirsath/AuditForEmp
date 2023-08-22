using EmployeeAudit.Data;
using EmployeeAudit.Infrastructure.IRepository;
using EmployeeAudit.Models;
using Microsoft.EntityFrameworkCore;
namespace EmployeeAudit.Infrastructure.Repository
{
  public class CityRepository : Repository<City>, ICityRepository
  {
    private AppDbContext _context;
    public CityRepository(AppDbContext context) : base(context) => _context = context;
    public async Task<IEnumerable<City>> GetAllCities() => await _context.Cities.ToListAsync();
    public async Task<IEnumerable<City>> GetCitiesByState(string stateName) => await _context.Cities.Where(city => city.state_name == stateName).ToListAsync();
  }
}