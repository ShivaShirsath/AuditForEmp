using Audit.WebApi;
using EmployeeAudit.Infrastructure.IRepository;
using EmployeeAudit.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeAudit.Controllers.API
{ // Route and attribute settings for the controller
  [Route("api/emp")]
  [ApiController]
  [AuditApi(EventTypeName = "{controller}/{action}")]
  public class EmployeeApiController : ControllerBase
  {
    private readonly IUnitOfWork _unitOfWork;
    private const int PageSize = 10; // Number of items per page
    public EmployeeApiController(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    // Retrieve all employees with their addresses
    //[HttpGet]
    //[AuditIgnore]
    //public async Task<ActionResult<IEnumerable<Employee?>>> GetEmployees() => Ok(await _unitOfWork.Employee.GetEmployeesWithAddressAsync());
    [HttpGet]
    [AuditIgnore]
    public async Task<ActionResult<Dictionary<string, object>>> GetEmployees(int page = 1)
    {
      IEnumerable<Employee> emp = await _unitOfWork.Employee.GetEmployeesWithAddressAsync();
      // Sort the Employees in descending order by ID
      emp = emp.OrderByDescending(e => e.EmployeeId);

      int totalCount = emp.Count();
      int totalPages = (int)Math.Ceiling(totalCount / (double)PageSize);

      if (page < 1)
      {
        page = 1;
      }
      else if (page > totalPages)
      {
        page = totalPages;
      }
      var paginatedEvents = emp
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

    // Retrieve all countries
    [HttpGet("contries")]
    [AuditIgnore]
    public async Task<ActionResult<IEnumerable<Country?>>> GetContries() => Ok(await _unitOfWork.Country.GetAllContries());

    // Retrieve states by country name
    [HttpGet("states")]
    [AuditIgnore]
    public async Task<ActionResult<IEnumerable<State?>>> GetStates(string? country_name)
    {
      if (!string.IsNullOrEmpty(country_name))
      {
        var states = await _unitOfWork.State.GetAllStatesByContry(country_name);
        return Ok(states);
      }
      return Ok(await _unitOfWork.State.GetAllStates());
    }

    // Retrieve cities by state name
    [HttpGet("cities")]
    [AuditIgnore]
    public async Task<ActionResult<IEnumerable<City?>>> GetCities(string? state_name)
    {
      if (!string.IsNullOrEmpty(state_name))
      {
        var cities = await _unitOfWork.City.GetCitiesByState(state_name);
        return Ok(cities);
      }
      return Ok(await _unitOfWork.City.GetAllCities());
    }

    // Retrieve details of a specific employee by ID
    [HttpGet("{id}")]
    [AuditIgnore]
    public async Task<ActionResult<Employee>> Details(int id)
    {
      var employee = await _unitOfWork.Employee.GetEmployeeWithAddressAsync(x => x.EmployeeId == id, e => e.Address);
      return employee == null ? NotFound() : Ok(employee);
    }

    // Create a new employee
    [HttpPost]
    public async Task<ActionResult<Employee>> Create(Employee employee)
    {
      if (ModelState.IsValid)
      {
        _unitOfWork.Employee.Add(employee);
        await _unitOfWork.SaveChangesAsync();
        return CreatedAtAction(nameof(Details), new { id = employee.EmployeeId }, employee);
      }
      return BadRequest(ModelState);
    }

    // Update an existing employee by ID
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Employee[] employee)
    {
      if (id == employee[0].EmployeeId)
      {
        _unitOfWork.Employee.Update(employee[0]);
        await _unitOfWork.SaveChangesAsync();
        return NoContent();
      }
      return BadRequest();
    }

    // Delete an employee by ID
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id, [FromBody] Employee employee)
    {
      var Xemployee = await _unitOfWork.Employee.GetEmployeeWithAddressAsync(x => x.EmployeeId == id, e => e.Address);
      if (Xemployee != null)
      {
        _unitOfWork.Employee.Delete(Xemployee);
        await _unitOfWork.SaveChangesAsync();
        return NoContent();
      }
      return NotFound();
    }
  }
}