using EmployeeAudit.Data;
using EmployeeAudit.Infrastructure.IRepository;
using EmployeeAudit.Models;

namespace EmployeeAudit.Infrastructure.Repository
{
  public class CountryRepository : Repository<Country>, ICountryRepository
  {
    private AppDbContext _context;
    public CountryRepository(AppDbContext context) : base(context)
    {
      _context = context;
    }
  }
}
