namespace EmployeeAudit.Infrastructure.IRepository
{
  public interface IUnitOfWork
  {
    IEmployeeRepository Employee { get; }
    void Save();
  }
}
