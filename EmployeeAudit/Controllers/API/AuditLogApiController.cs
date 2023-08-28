using EmployeeAudit.Infrastructure.IRepository;
using EmployeeAudit.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeAudit.Controllers.API
{
  [Route("api/audit")]
  [ApiController]
  public class AuditLogApiController : ControllerBase
  {
    private readonly IUnitOfWork _unitOfWork;
    private const int PageSize = 10; // Number of items per page

    public AuditLogApiController(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    // Retrieve all audit events
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Event?>>> GetAudit(int page = 1)
    {
      var allEvents = await _unitOfWork.Event.GetAllAudits();

      int totalCount = allEvents.Count();
      int totalPages = (int)Math.Ceiling(totalCount / (double)PageSize);

      if (page < 1)
      {
        page = 1;
      }
      else if (page > totalPages)
      {
        page = totalPages;
      }

      var paginatedEvents = allEvents
          .Skip((page - 1) * PageSize)
          .Take(PageSize)
          .ToList();

      return Ok(paginatedEvents);
    }
  }
}