using EmployeeAudit.Data;
using EmployeeAudit.Infrastructure.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EmployeeAudit.Infrastructure.Repository
{
  public class Repository<T> : IRepository<T> where T : class
  {
    private readonly AppDbContext _context;
    private DbSet<T> _dbSet;

    public Repository(AppDbContext context)
    {
      _context = context;
      _dbSet = _context.Set<T>();
    }

    public void Add(T entity)
    {
      _dbSet.Add(entity);
    }

    public void Delete(T entity)
    {
      _dbSet.Remove(entity);
    }
    public void DeleteRange(IEnumerable<T> entities)
    {
      _dbSet.RemoveRange(entities);
    }

    public IEnumerable<T> GetAll()
    {
      return _dbSet.ToList();
    }
    public T GetT(Expression<Func<T, bool>> predicate)
    {
      return _dbSet.Where(predicate).FirstOrDefault();
    }
    public IEnumerable<T> GetTIncluding(Expression<Func<T, bool>> predicate, Expression<Func<T, T>> filter)
    {
      //IQueryable<T> query = _dbSet;

      //if (filter != null)
      //{
      //  query = query.Where(predicate);
      //  query = query.Include(filter);
      //}

      //return query.ToList();
      return _dbSet.Include(filter).Where(predicate);
    }
  }
}
