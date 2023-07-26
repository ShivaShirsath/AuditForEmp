using Audit.Mvc;
using EmployeeAudit.Infrastructure.IRepository;
using EmployeeAudit.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeAudit.Controllers
{
  public class EmployeesController : Controller
  {
    private IUnitOfWork _unitOfWork;
    public EmployeesController(IUnitOfWork unitOfWork)
    {
      _unitOfWork = unitOfWork;
    }
    // GET: Employees
    public IActionResult Index()
    {
      var employees = _unitOfWork.Employee.GetAll();
      ViewData["Title"] = "Employees";
      return View(employees.ToList());
    }
    // GET: Employees/Details/5
    [Audit]
    public IActionResult Details(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }
      var employee = _unitOfWork.Employee.GetEmployeeIncludingAddress(x => x.EmployeeId == id, e => e.Address);
      if (employee == null)
      {
        return NotFound();
      }
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
    [Audit]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Employee employee)
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
        _unitOfWork.Employee.Add(employee);
        _unitOfWork.Save();
        TempData["success"] = "Employee Details Added !";
        return RedirectToAction(nameof(Index));
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
      var employee = _unitOfWork.Employee.GetT(x => x.EmployeeId == id);
      if (employee == null)
      {
        return NotFound();
      }
      ViewData["Title"] = "Edit";
      return View(employee);
    }
    // POST: Employees/Edit/5
    [Audit]
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
        var existingEmployee = _unitOfWork.Employee.GetT(x => x.EmployeeId == id);
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
          _unitOfWork.Employee.Update(existingEmployee);
          _unitOfWork.Save();
          TempData["success"] = "Employee Details Edited !";
        }
        catch (DbUpdateConcurrencyException)
        {

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
      var employee = _unitOfWork.Employee.GetT(x => x.EmployeeId == id);
      if (employee == null)
      {
        return NotFound();
      }
      ViewData["Title"] = "Delete";
      return View(employee);
    }
    // POST: Employees/Delete/5
    [Audit]
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
      var employee = _unitOfWork.Employee.GetT(x => x.EmployeeId == id);

      if (employee == null)
      {
        return NotFound();
      }
      _unitOfWork.Employee.Delete(employee);
      _unitOfWork.Save();
      TempData["success"] = "Employee Details Removed !";
      return RedirectToAction(nameof(Index));
    }
  }
}