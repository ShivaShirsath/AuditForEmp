using EmployeeAudit.Infrastructure.IRepository;
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

    // Retrieve all audit events
    [HttpGet]
    public async Task<ActionResult<Dictionary<string, object>>> GetAudit(int page = 1)
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
  }
}
