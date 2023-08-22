namespace EmployeeAudit.Infrastructure.IRepository
{
  public interface IUnitOfWork
  {
    IEmployeeRepository Employee { get; }
    ICountryRepository Country { get; }
    IStateRepository State { get; }
    ICityRepository City { get; }
    IEventRepository Event { get; }
    void Save();
    Task SaveChangesAsync();
  }
}