using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EmpAddrAudit.Models;
using EmpAddrAudit.Data;
using Audit.Core;
using Newtonsoft.Json;

namespace EmpAddrAudit.Controllers
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
            ViewData["Title"] = "Employee";
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
                .FirstOrDefault(e => e.EmployeeId == id);

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

                using (var scope = AuditScope.Create(_ =>
                {
                    _.EventType("Create Employee");
                    _.Target(() => employee);
                    _.ExtraFields(new { MyProperty = "value", CreatedBy = HttpContext.User.Identity.Name });
                    _.JsonData(JsonConvert.SerializeObject(new { Event = "Create Employee", Employee = employee }));
                }))
                {
                    _context.Employees.Add(employee);
                    _context.SaveChanges();
                }
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
                    using (var scope = AuditScope.Create(_ =>
                    {
                        _.EventType("Create Employee");
                        _.Target(() => employee);
                        _.ExtraFields(new { MyProperty = "value", CreatedBy = HttpContext.User.Identity.Name });
                        _.JsonData(JsonConvert.SerializeObject(new { Event = "Create Employee", Employee = employee }));
                    }))
                    {
                        var existingEmployee = _context.Employees.Include(e => e.Address).FirstOrDefault(e => e.EmployeeId == id);

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

                        _context.Update(existingEmployee);
                        _context.SaveChanges();
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
            using (var scope = AuditScope.Create(_ =>
            {
                _.EventType("Create Employee");
                _.Target(() => employee);
                _.ExtraFields(new { MyProperty = "value", CreatedBy = HttpContext.User.Identity.Name });
                _.JsonData(JsonConvert.SerializeObject(new { Event = "Create Employee", Employee = employee }));
            }))
            {
                _context.Employees.Remove(employee);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeesExists(int id)
        {
            return _context.Employees.Any(e => e.EmployeeId == id);
        }
    }
}