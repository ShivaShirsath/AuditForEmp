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
    private const int PageSize = 6; // Number of items per page

    public AuditLogApiController(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    [HttpGet]
    public async Task<ActionResult<Dictionary<string, object>>> GetAudit(string eventType = "all", int page = 1)
    {
      IEnumerable<Event> events;

      if (string.IsNullOrEmpty(eventType))
      {
        events = await _unitOfWork.Event.GetAllAudits();
      }
      else
      {
        events = await _unitOfWork.Event.GetEventsByEventType(eventType);
      }

      int totalCount = events.Count();
      int totalPages = (int)Math.Ceiling(totalCount / (double)PageSize);

      if (page < 1)
      {
        page = 1;
      }
      else if (page > totalPages)
      {
        page = totalPages;
      }

      var paginatedEvents = events
          .Skip((page - 1) * PageSize)
          .Take(PageSize)
          .ToList();

      var result = new Dictionary<string, object>
        {
            { "total", totalCount },
            { "page", page },
            { "pageSize", PageSize },
            { "totalPages", totalPages },
            { "events", paginatedEvents }
        };

      return Ok(result);
    }

    [HttpGet("Tables")]
    public async Task<ActionResult<IEnumerable<string>>> GetEventTypes()
    {
      var eventTypes = await _unitOfWork.Event.GetEventTypes();
      return Ok(eventTypes);
    }
  }
}
