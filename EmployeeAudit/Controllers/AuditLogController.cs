using EmployeeAudit.Infrastructure.IRepository;
using Microsoft.AspNetCore.Mvc;
namespace EmployeeAudit.Controllers
{
  public class AuditLogController : Controller
  {
    private readonly IUnitOfWork _unitOfWork;
    public AuditLogController(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;
    public IActionResult Index()
    {
      ViewData["Title"] = "Audit Logs";
      return View(_unitOfWork.Event.All);
    }
  }
}
