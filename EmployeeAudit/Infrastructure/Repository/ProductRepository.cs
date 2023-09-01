using EmployeeAudit.Data;
using EmployeeAudit.Infrastructure.IRepository;
using EmployeeAudit.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EmployeeAudit.Infrastructure.Repository
{
  internal class ProductRepository : Repository<Product>, IProductRepository
  {
    private AppDbContext _context;
    public ProductRepository(AppDbContext context) : base(context) => _context = context;
    public void Update(Product entity) => _context.Products.Update(entity);
    public async Task<IEnumerable<Product>> GetProductsAsync() => await _context.Products.ToListAsync();
    public async Task<Product?> GetProductAsync(Expression<Func<Product, bool>> predicate) => await _context.Products.FirstOrDefaultAsync(predicate);
  }
}