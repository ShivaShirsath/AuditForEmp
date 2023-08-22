using EmployeeAudit.Data;
using EmployeeAudit.Infrastructure.IRepository;
using EmployeeAudit.Models;
using Microsoft.EntityFrameworkCore;
namespace EmployeeAudit.Infrastructure.Repository
{
  public class StateRepository : Repository<State>, IStateRepository
  {
    private AppDbContext _context;
    public StateRepository(AppDbContext context) : base(context) => _context = context;
    public async Task<IEnumerable<State>> GetAllStates() => await _context.States.ToListAsync();
    public async Task<IEnumerable<State>> GetAllStatesByContry(string countryName) => await _context.States.Where(state => state.country_name == countryName).ToListAsync();
  }
}