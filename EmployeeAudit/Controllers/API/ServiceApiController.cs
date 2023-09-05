using Audit.WebApi;
using EmployeeAudit.Infrastructure.IRepository;
using EmployeeAudit.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeAudit.Controllers.API
{
  [Route("api/service")]
  [ApiController]
  [AuditApi(EventTypeName = "{controller}/{action}")]
  public class ServiceApiController : ControllerBase
  {
    private readonly IUnitOfWork _unitOfWork;

    private const int PageSize = 10; // Number of items per page
    public ServiceApiController(IUnitOfWork unitOfWork)
    {
      _unitOfWork = unitOfWork;
    }

    //[HttpGet]
    //[AuditIgnore]
    //public async Task<ActionResult<IEnumerable<Service?>>> GetServices() => Ok(await _unitOfWork.Service.GetServicesAsync());
    [HttpGet]
    [AuditIgnore]
    public async Task<ActionResult<Dictionary<string, object>>> GetServices(int page = 1)
    {
      IEnumerable<Service> services = await _unitOfWork.Service.GetServicesAsync();
      // Sort the Services in descending order by ID
      services = services.OrderByDescending(e => e.ServiceId);

      int totalCount = services.Count();
      int totalPages = (int)Math.Ceiling(totalCount / (double)PageSize);

      if (page < 1)
      {
        page = 1;
      }
      else if (page > totalPages)
      {
        page = totalPages;
      }
      var paginatedEvents = services
          .Skip((page - 1) * PageSize)
          .Take(PageSize)
          .ToList();

      var result = new Dictionary<string, object>
    {
        { "total", totalCount },
    { "page", page },
        { "pageSize", PageSize },
        { "totalPages", totalPages },
        { "data", paginatedEvents }
    };

      return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<Service>> Create(Service service)
    {
      if (ModelState.IsValid)
      {
        _unitOfWork.Service.Add(service);
        await _unitOfWork.SaveChangesAsync();
        return CreatedAtAction(nameof(Details), new { id = service.ServiceId }, service);
      }
      return BadRequest(ModelState);
    }

    [HttpGet("{id}")]
    [AuditIgnore]
    public async Task<ActionResult<Service>> Details(int id)
    {
      var service = await _unitOfWork.Service.GetServiceAsync(x => x.ServiceId == id);
      return service == null ? NotFound() : Ok(service);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Service[] service)
    {
      if (id == service[0].ServiceId)
      {
        _unitOfWork.Service.Update(service[0]);
        await _unitOfWork.SaveChangesAsync();
        return NoContent();
      }
      return BadRequest();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id, [FromBody] Service service)
    {
      var Xservice = await _unitOfWork.Service.GetServiceAsync(x => x.ServiceId == id);
      if (Xservice != null)
      {
        _unitOfWork.Service.Delete(Xservice);
        await _unitOfWork.SaveChangesAsync();
        return NoContent();
      }
      return NotFound();
    }
  }
}
