using System.Linq.Expressions;

namespace EmployeeAudit.Infrastructure.IRepository
{
  public interface IRepository<T> where T : class
  {
    IEnumerable<T> GetAll();
    T Get(Expression<Func<T, bool>> predicate);
    void Add(T entity);
    void Delete(T entity);
    void DeleteRange(IEnumerable<T> entities);
  }
}
