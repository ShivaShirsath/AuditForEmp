using EmployeeAudit.Models;
using System.Linq.Expressions;

namespace EmployeeAudit.Infrastructure.IRepository
{
  public interface IEmployeeRepository : IRepository<Employee>
  {
    void Update(Employee entity);
    Employee GetTEmpWithAddress(Expression<Func<Employee, bool>> predicate, Expression<Func<Employee, Address>> filter);
  }
}
