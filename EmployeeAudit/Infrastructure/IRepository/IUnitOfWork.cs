namespace EmployeeAudit.Infrastructure.IRepository
{
  public interface IUnitOfWork
  {
    IEmployeeRepository Employee { get; }
    ICountryRepository Country { get; }
    IEventRepository Event { get; }
    void Save();
  }
}
