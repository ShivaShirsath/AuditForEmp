using EmployeeAudit.Models;
using System.Linq.Expressions;

namespace EmployeeAudit.Infrastructure.IRepository
{
  public interface IEmployeeRepository : IRepository<Employee>
  {
    void Update(Employee entity);
    Employee GetEmployeeWithAddress(Expression<Func<Employee, bool>> predicate, Expression<Func<Employee, Address>> filter);
  }
}
