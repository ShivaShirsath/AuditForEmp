using EmployeeAudit.Data;
using EmployeeAudit.Infrastructure.IRepository;

namespace EmployeeAudit.Infrastructure.Repository
{
  public class UnitOfWork : IUnitOfWork
  {
    private AppDbContext _context;
    public IEmployeeRepository Employee { get; private set; }
    public IProductRepository Product { get; private set; }
    public IServiceRepository Service { get; private set; }
    public ICountryRepository Country { get; private set; }
    public IEventRepository Event { get; private set; }
    public IStateRepository State { get; private set; }
    public ICityRepository City { get; private set; }
    public UnitOfWork(AppDbContext context)
    {
      _context = context;
      Employee = new EmployeeRepository(context);
      Product = new ProductRepository(context);
      Service = new ServiceRepository(context);
      Country = new CountryRepository(context);
      State = new StateRepository(context);
      City = new CityRepository(context);
      Event = new EventRepository(context);
    }
    public void Save() => _context.SaveChanges();
    public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
  }
}