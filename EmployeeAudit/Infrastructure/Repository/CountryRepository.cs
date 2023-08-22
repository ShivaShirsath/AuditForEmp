using EmployeeAudit.Data;
using EmployeeAudit.Infrastructure.IRepository;
using EmployeeAudit.Models;
using Microsoft.EntityFrameworkCore;
namespace EmployeeAudit.Infrastructure.Repository
{
  public class CountryRepository : Repository<Country>, ICountryRepository
  {
    private AppDbContext _context;
    public CountryRepository(AppDbContext context) : base(context) => _context = context;
    public async Task<IEnumerable<Country>> GetAllContries() => await _context.Countries.ToListAsync();
  }
}