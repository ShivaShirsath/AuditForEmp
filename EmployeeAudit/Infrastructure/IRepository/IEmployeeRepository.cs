using EmployeeAudit.Models;

namespace EmployeeAudit.Infrastructure.IRepository
{
  public interface IEmployeeRepository : IRepository<Employee>
  {
    Employee GetEmployeeIncludingAddress(Func<Employee, bool> predicate, Func<Employee, Address> filter);
    void Update(Employee entity);
  }
}
