﻿using EmployeeAudit.Data;
using EmployeeAudit.Infrastructure.IRepository;

namespace EmployeeAudit.Infrastructure.Repository
{
  public class UnitOfWork : IUnitOfWork
  {
    private AppDbContext _context;
    public IEmployeeRepository Employee { get; private set; }
    public ICountryRepository Country { get; private set; }
    public IEventRepository Event { get; private set; }
    public UnitOfWork(AppDbContext context)
    {
      _context = context;
      Employee = new EmployeeRepository(context);
      Country = new CountryRepository(context);
      Event = new EventRepository(context);
    }
    public void Save()
    {
      _context.SaveChanges();
    }
  }
}
