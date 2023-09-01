using EmployeeAudit.Models;
using System.Linq.Expressions;

namespace EmployeeAudit.Infrastructure.IRepository
{
  public interface IProductRepository : IRepository<Product>
  {
    void Update(Product entity);
    Task<Product?> GetProductAsync(Expression<Func<Product, bool>> predicate);
    Task<IEnumerable<Product>> GetProductsAsync();
  }
}