﻿using Audit.WebApi;
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
    [AuditIgnore]
    public async Task<ActionResult<IEnumerable<Employee?>>> GetEmployees()
    {
      return Ok(await _unitOfWork.Employee.GetEmployeesWithAddressAsync());
    }
    [HttpGet("contries")]
    [AuditIgnore]
    public async Task<ActionResult<IEnumerable<Country?>>> GetContries()
    {
      return Ok(await _unitOfWork.Country.GetAllContries());
    }
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

    [HttpGet("{id}")]
    public async Task<ActionResult<Employee>> Details(int id)
    {
      var employee = await _unitOfWork.Employee.GetEmployeeWithAddressAsync(x => x.EmployeeId == id, e => e.Address);
      if (employee == null)
      {
        return NotFound();
      }
      return Ok(employee);
    }
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
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Employee employee)
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
    public async Task<IActionResult> Delete(int id)
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
