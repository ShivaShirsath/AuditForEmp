﻿using EmployeeAudit.Data;
using EmployeeAudit.Infrastructure.IRepository;
using EmployeeAudit.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
namespace EmployeeAudit.Infrastructure.Repository
{
  public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
  {
    private AppDbContext _context;
    public EmployeeRepository(AppDbContext context) : base(context) => _context = context;
    public Employee GetEmployeeWithAddress(Expression<Func<Employee, bool>> predicate, Expression<Func<Employee, Address>> filter) => _context.Employees.Include(filter).FirstOrDefault(predicate);
    public void Update(Employee entity) => _context.Employees.Update(entity);
    public async Task<IEnumerable<Employee>> GetEmployeesWithAddressAsync() => await _context.Employees.Include(e => e.Address).ToListAsync();
    public async Task<Employee?> GetEmployeeWithAddressAsync(Expression<Func<Employee, bool>> predicate, Expression<Func<Employee, Address>> filter) => await _context.Employees.Include(filter).FirstOrDefaultAsync(predicate);
  }
}