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

    public ServiceApiController(IUnitOfWork unitOfWork)
    {
      _unitOfWork = unitOfWork;
    }

    [HttpGet]
    [AuditIgnore]
    public async Task<ActionResult<IEnumerable<Service?>>> GetServices() => Ok(await _unitOfWork.Service.GetServicesAsync());

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
    public async Task<IActionResult> Delete(int id)
    {
      var service = await _unitOfWork.Service.GetServiceAsync(x => x.ServiceId == id);
      if (service != null)
      {
        _unitOfWork.Service.Delete(service);
        await _unitOfWork.SaveChangesAsync();
        return NoContent();
      }
      return NotFound();
    }
  }
}
