using Audit.Core;
using EmployeeAudit.Data;
using EmployeeAudit.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeAudit.Controllers
{
  public class EmployeesController : Controller
  {
    private readonly AppDbContext _context;
    public EmployeesController(AppDbContext context)
    {
      _context = context;
    }
    // GET: Employees
    public IActionResult Index()
    {
      var employees = _context.Employees.Include(e => e.Address);
      ViewData["Title"] = "Employees";
      return View(employees.ToList());
    }
    // GET: Employees/Details/5
    public IActionResult Details(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }
      var employee = _context.Employees
          .Include(e => e.Address)
          .FirstOrDefault(e => e.EmployeeId == id
      );
      if (employee == null)
      {
        return NotFound();
      }
      var scope = AuditScope.CreateAsync(_ => _
        .EventType("Employee:Details")
        .ExtraFields(new { MyProperty = "Show" })
        .Target(() => employee)
      );
      ViewData["Title"] = "Details";
      return View(employee);
    }
    // GET: Employee/Create
    public IActionResult Create()
    {
      ViewData["Title"] = "Create";
      return View();
    }
    // POST: Employees/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Employee employee)
    {
      try
      {
        if (ModelState.IsValid)
        {
          var address = new Address
          {
            City = employee.Address.City,
            State = employee.Address.State,
            ZipCode = employee.Address.ZipCode,
            Country = employee.Address.Country
          };
          employee.Address = address;
          _context.Employees.Add(employee);
          _context.SaveChanges();

          var scope = AuditScope.CreateAsync(_ => _
            .EventType("Employee:Create")
            .Target(() => employee)
          );
        }
      return RedirectToAction(nameof(Index));
      }
      catch (Exception e)
      {
        Console.WriteLine(e.Message);
      }
      return View(employee);
    }
    // GET: Employees/Edit/5
    public IActionResult Edit(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }
      var employee = _context.Employees.Include(e => e.Address).FirstOrDefault(e => e.EmployeeId == id);
      if (employee == null)
      {
        return NotFound();
      }
      ViewData["Title"] = "Edit";
      return View(employee);
    }
    // POST: Employees/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, Employee employee)
    {
      if (id != employee.EmployeeId)
      {
        return NotFound();
      }
      if (ModelState.IsValid)
      {
        try
        {
          var existingEmployee = _context.Employees.Include(e => e.Address).FirstOrDefault(e => e.EmployeeId == id);
          try
          {
            if (existingEmployee == null)
            {
              return NotFound();
            }

            existingEmployee.Name = employee.Name;
            existingEmployee.Phone = employee.Phone;
            existingEmployee.Address.City = employee.Address.City;
            existingEmployee.Address.State = employee.Address.State;
            existingEmployee.Address.ZipCode = employee.Address.ZipCode;
            existingEmployee.Address.Country = employee.Address.Country;
            var scope = AuditScope.CreateAsync(_ => _
                .EventType("Employee:Edit")
                .ExtraFields(new { MyProperty = "Update" })
                .Target(() => employee)
            );
            _context.Update(existingEmployee);
            _context.SaveChanges();
          }
          catch (DbUpdateConcurrencyException)
          {
            if (!EmployeesExists(employee.EmployeeId))
            {
              return NotFound();
            }
            else
            {
              ModelState.AddModelError("EmployeeId", "The record has been updated by another user. Please refresh the page and try again.");
              return View(employee);
            }
          }
        }
        catch (DbUpdateConcurrencyException)
        {
          if (!EmployeesExists(employee.EmployeeId))
          {
            return NotFound();
          }
          else
          {
            throw;
          }
        }
        return RedirectToAction(nameof(Index));
      }
      return View(employee);
    }
    // GET: Employees/Delete/5
    public IActionResult Delete(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }
      var employee = _context.Employees
          .Include(e => e.Address)
          .FirstOrDefault(e => e.EmployeeId == id);
      if (employee == null)
      {
        return NotFound();
      }
      ViewData["Title"] = "Delete";
      return View(employee);
    }
    // POST: Employees/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
      var employee = _context.Employees.Find(id);

      var scope = AuditScope.CreateAsync(_ => _
        .EventType("Employee:Details")
        .ExtraFields(new { MyProperty = "Show" })
        .Target(() => employee)
      );
      if (employee == null)
      {
        return NotFound();
      }
      _context.Employees.Remove(employee);
      _context.SaveChanges();
      return RedirectToAction(nameof(Index));
    }
    private bool EmployeesExists(int id)
    {
      return _context.Employees.Any(e => e.EmployeeId == id);
    }
  }
}