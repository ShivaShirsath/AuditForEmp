using Audit.WebApi;
using EmployeeAudit.Infrastructure.IRepository;
using EmployeeAudit.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeAudit.Controllers.API
{
  [Route("api/emp")]
  [ApiController]
  [AuditApi(EventTypeName = "{controller}/{action}")]
  public class EmployeeApiController : ControllerBase
  {
    private readonly IUnitOfWork _unitOfWork;
    public EmployeeApiController(IUnitOfWork unitOfWork)
    {
      _unitOfWork = unitOfWork;
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Employee?>>> GetEmployees()
    {
      return Ok(await _unitOfWork.Employee.GetEmployeesWithAddressAsync());
    }
    [HttpGet("contries")]
    public async Task<ActionResult<IEnumerable<Country?>>> GetContries()
    {
      return Ok(await _unitOfWork.Country.GetAllContries());
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<Employee>> GetEmployee(int id)
    {
      var employee = await _unitOfWork.Employee.GetEmployeeWithAddressAsync(x => x.EmployeeId == id, e => e.Address);
      if (employee == null)
      {
        return NotFound();
      }
      return Ok(employee);
    }
    [HttpPost]
    public async Task<ActionResult<Employee>> CreateEmployee(Employee employee)
    {
      if (ModelState.IsValid)
      {
        _unitOfWork.Employee.Add(employee);
        await _unitOfWork.SaveChangesAsync();
        return CreatedAtAction(nameof(GetEmployee), new { id = employee.EmployeeId }, employee);
      }
      return BadRequest(ModelState);
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateEmployee(int id, Employee employee)
    {
      if (id != employee.EmployeeId)
      {
        return BadRequest();
      }
      _unitOfWork.Employee.Update(employee);
      await _unitOfWork.SaveChangesAsync();
      return NoContent();
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEmployee(int id)
    {
      var employee = await _unitOfWork.Employee.GetEmployeeWithAddressAsync(x => x.EmployeeId == id, e => e.Address);
      if (employee == null)
      {
        return NotFound();
      }
      _unitOfWork.Employee.Delete(employee);
      await _unitOfWork.SaveChangesAsync();
      return NoContent();
    }
  }
}
