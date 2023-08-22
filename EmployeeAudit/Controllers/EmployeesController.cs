using Audit.Mvc;
using EmployeeAudit.Infrastructure.IRepository;
using EmployeeAudit.Models;
using Microsoft.AspNetCore.Mvc;
namespace EmployeeAudit.Controllers
{
  public class EmployeesController : Controller
  {
    private readonly IUnitOfWork _unitOfWork;
    public EmployeesController(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;
    // GET: Employees
    public IActionResult Index()
    {
      var employees = _unitOfWork.Employee.All;
      ViewData["Title"] = "Employees";
      return View(employees.ToList());
    }
    // GET: Employees/Details/5
    [Audit]
    public IActionResult Details(int? id)
    {
      if (id != null)
      {
        var employee = _unitOfWork.Employee.GetEmployeeWithAddress(x => x.EmployeeId == id, filter: e => e.Address);
        if (employee != null)
        {
          ViewData["Title"] = "Details";
          return View(employee);
        }
        return NotFound();
      }
      return NotFound();
    }
    // GET: Employee/Create
    public IActionResult Create()
    {
      ViewData["Title"] = "Create";
      var countries = _unitOfWork.Country.All;
      ViewBag.Countries = countries;
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
          City = employee.Address?.City,
          State = employee.Address?.State,
          ZipCode = employee.Address?.ZipCode,
          Country = employee.Address?.Country
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
      if (id != null)
      {
        var employee = _unitOfWork.Employee.GetEmployeeWithAddress(x => x.EmployeeId == id, filter: e => e.Address);
        if (employee != null)
        {
          ViewData["Title"] = "Edit";
          var countries = _unitOfWork.Country.All;
          ViewBag.Countries = countries;
          return View(employee);
        }
        return NotFound();
      }
      return NotFound();
    }
    // POST: Employees/Edit/5
    [Audit]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, Employee employee)
    {
      if (id == employee.EmployeeId)
      {
        if (ModelState.IsValid)
        {
          var existingEmployee = _unitOfWork.Employee.GetEmployeeWithAddress(x => x.EmployeeId == id, filter: e => e.Address);
          if (existingEmployee != null)
          {
            if (existingEmployee != null && existingEmployee.Address != null)
            {
              existingEmployee.Name = employee.Name;
              existingEmployee.Phone = employee.Phone;
              existingEmployee.Address.City = employee.Address?.City;
              existingEmployee.Address.State = employee.Address?.State;
              existingEmployee.Address.ZipCode = employee.Address?.ZipCode;
              existingEmployee.Address.Country = employee.Address?.Country;
              _unitOfWork.Employee.Update(existingEmployee);
              _unitOfWork.Save();
              TempData["success"] = "Employee Details Edited !";
            }
            return RedirectToAction(nameof(Index));
          }
          return NotFound();
        }
        return View(employee);
      }
      return NotFound();
    }
    // GET: Employees/Delete/5
    public IActionResult Delete(int? id)
    {
      if (id != null)
      {
        var employee = _unitOfWork.Employee.GetEmployeeWithAddress(x => x.EmployeeId == id, filter: e => e.Address);
        if (employee != null)
        {
          ViewData["Title"] = "Delete";
          return View(employee);
        }
        return NotFound();
      }
      return NotFound();
    }
    // POST: Employees/Delete/5
    [Audit]
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
      var employee = _unitOfWork.Employee.GetEmployeeWithAddress(x => x.EmployeeId == id, filter: e => e.Address);
      if (employee != null)
      {
        _unitOfWork.Employee.Delete(employee);
        _unitOfWork.Save();
        TempData["success"] = "Employee Details Removed !";
        return RedirectToAction(nameof(Index));
      }
      return NotFound();
    }
  }
}