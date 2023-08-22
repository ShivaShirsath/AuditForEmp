using EmployeeAudit.Data;
using EmployeeAudit.Infrastructure.IRepository;
using System.Data.Entity;
using System.Linq.Expressions;
namespace EmployeeAudit.Infrastructure.Repository
{
  public class Repository<T> : IRepository<T> where T : class
  {
    private readonly AppDbContext _context;
    private Microsoft.EntityFrameworkCore.DbSet<T> _dbSet;
    public Repository(AppDbContext context)
    {
      _context = context;
      _dbSet = _context.Set<T>();
    }
    public void Add(T entity) => _dbSet.Add(entity);
    public void Delete(T entity) => _dbSet.Remove(entity);
    public void DeleteRange(IEnumerable<T> entities) => _dbSet.RemoveRange(entities);
    public IEnumerable<T> All => _dbSet.ToList();
    public T Get(Expression<Func<T, bool>> predicate) => _dbSet.Where(predicate).FirstOrDefault();
    public async Task<IEnumerable<T>> GetAllAsync() => await _dbSet.ToListAsync();
  }
}