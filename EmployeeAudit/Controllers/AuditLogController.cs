using EmployeeAudit.Data;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeAudit.Controllers
{
  public class AuditLogController : Controller
  {
    private readonly AppDbContext _context;
    public AuditLogController(AppDbContext context)
    {
      _context = context;
    }
    public IActionResult Index()
    {
      ViewData["Title"] = "Audit Logs";
      return View(_context.Events.ToList());
    }
  }
}
