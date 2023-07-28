using Audit.Mvc;
using EmployeeAudit.Infrastructure.IRepository;
using EmployeeAudit.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace EmployeeAudit.Controllers
{
  public class EmployeesController : Controller
  {
    private readonly IUnitOfWork _unitOfWork;
    private readonly HttpClient _httpClient;
    public EmployeesController(IUnitOfWork unitOfWork, IHttpClientFactory httpClientFactory)
    {
      _unitOfWork = unitOfWork;
      _httpClient = httpClientFactory.CreateClient(); // Inject the IHttpClientFactory
    }
    // GET: Employees
    public IActionResult Index()
    {
      var employees = _unitOfWork.Employee.GetAll();
      ViewData["Title"] = "Employees";
      return View(employees.ToList());
    }
    // GET: Employees/Details/5
    [Audit]
    public IActionResult Details(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }
      var employee = _unitOfWork.Employee.GetTEmpWithAddress(x => x.EmployeeId == id, e => e.Address);
      if (employee == null)
      {
        return NotFound();
      }
      ViewData["Title"] = "Details";
      return View(employee);
    }
    // Helper method to get countries from the API
    private async Task<List<Country>> GetCountriesFromApi()
    {
      var apiEndpoint = "https://www.universal-tutorial.com/api/countries";
      var token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VyIjp7InVzZXJfZW1haWwiOiJTaGl2YS5TLlNoaXJzYXRoQGdtYWlsLmNvbSIsImFwaV90b2tlbiI6IkIwSW15NVVhR1FER0lSZ0tQVXJPSVB4VTljaTlkS1NKMV9XNlFWOE0weDBwTDFoVjNEMGVENjU5RmhISV84RUo4TmdCMElteTVVYUdRREdJUmdLUFVyT0lQeFU5Y2k5ZEtTSjFfVzZRVjhNMHgwcEwxaFYzRDBlRDY1OUZoSElfOEVKOE5nIn0sImV4cCI6MTY5MDYzNTEyMX0.9zNKJQnpgB8rsi8NA3VZfEIsuPwbo4apJD6kfkhyRio"; // Replace with your actual API token
      _httpClient.DefaultRequestHeaders.Clear();
      _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
      _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
      var response = await _httpClient.GetAsync(apiEndpoint);
      if (response.IsSuccessStatusCode)
      {
        var countriesJson = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<List<Country>>(countriesJson);
      }
      // Handle API error if necessary
      return new List<Country>();
    }
    // Helper method to get States from the API
    private async Task<List<State>> GetStatesFromApi(String country)
    {
      var apiEndpoint = "https://www.universal-tutorial.com/api/states/" + country;
      var token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VyIjp7InVzZXJfZW1haWwiOiJTaGl2YS5TLlNoaXJzYXRoQGdtYWlsLmNvbSIsImFwaV90b2tlbiI6IkIwSW15NVVhR1FER0lSZ0tQVXJPSVB4VTljaTlkS1NKMV9XNlFWOE0weDBwTDFoVjNEMGVENjU5RmhISV84RUo4TmdCMElteTVVYUdRREdJUmdLUFVyT0lQeFU5Y2k5ZEtTSjFfVzZRVjhNMHgwcEwxaFYzRDBlRDY1OUZoSElfOEVKOE5nIn0sImV4cCI6MTY5MDYzNTEyMX0.9zNKJQnpgB8rsi8NA3VZfEIsuPwbo4apJD6kfkhyRio"; // Replace with your actual API token
      _httpClient.DefaultRequestHeaders.Clear();
      _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
      _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
      var response = await _httpClient.GetAsync(apiEndpoint);
      if (response.IsSuccessStatusCode)
      {
        var countriesJson = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<List<State>>(countriesJson);
      }
      // Handle API error if necessary
      return new List<State>();
    }
    // Helper method to get Cities from the API
    private async Task<List<City>> GetCitiesFromApi(String state)
    {
      var apiEndpoint = "https://www.universal-tutorial.com/api/cities/" + state;
      var token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VyIjp7InVzZXJfZW1haWwiOiJTaGl2YS5TLlNoaXJzYXRoQGdtYWlsLmNvbSIsImFwaV90b2tlbiI6IkIwSW15NVVhR1FER0lSZ0tQVXJPSVB4VTljaTlkS1NKMV9XNlFWOE0weDBwTDFoVjNEMGVENjU5RmhISV84RUo4TmcifSwiZXhwIjoxNjkwNTQ1NDA4fQ.00l5sye5_bHNlSAt4uctJ8JH3TMBVLYmwXgK5MG2vtI"; // Replace with your actual API token
      _httpClient.DefaultRequestHeaders.Clear();
      _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
      _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
      var response = await _httpClient.GetAsync(apiEndpoint);
      if (response.IsSuccessStatusCode)
      {
        var countriesJson = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<List<City>>(countriesJson);
      }
      // Handle API error if necessary
      return new List<City>();
    }
    // Action to get states based on the selected country
    [HttpGet]
    public async Task<IActionResult> GetStates(string country)
    {
      var states = await GetStatesFromApi(country);
      return Json(states);
    }
    [HttpGet]
    public async Task<IActionResult> GetCities(string state)
    {
      var cities = await GetCitiesFromApi(state);
      return Json(cities);
    }
    // GET: Employee/Create
    public IActionResult Create()
    {
      ViewData["Title"] = "Create";
      // Fetch countries from the API
      var countries = GetCountriesFromApi().Result;
      ViewBag.Countries = countries;
      return View();
    }
    // POST: Employees/Create
    [Audit]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Employee employee)
    {
      if (ModelState.IsValid)
      {
        var address = new Address
        {
          City = employee.Address.City,
          State = employee.Address.State,
          ZipCode = employee.Address.ZipCode,
          Country = employee.Address.Country
        };
        employee.Address = address;
        _unitOfWork.Employee.Add(employee);
        _unitOfWork.Save();
        TempData["success"] = "Employee Details Added !";
        return RedirectToAction(nameof(Index));
      }
      return View(employee);
    }
    // GET: Employees/Edit/5
    public IActionResult Edit(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }
      var employee = _unitOfWork.Employee.GetTEmpWithAddress(x => x.EmployeeId == id, e => e.Address);
      if (employee == null)
      {
        return NotFound();
      }
      ViewData["Title"] = "Edit";
      return View(employee);
    }
    // POST: Employees/Edit/5
    [Audit]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, Employee employee)
    {
      if (id != employee.EmployeeId)
      {
        return NotFound();
      }
      if (ModelState.IsValid)
      {
        var existingEmployee = _unitOfWork.Employee.GetTEmpWithAddress(x => x.EmployeeId == id, e => e.Address);

        try
        {
          if (existingEmployee == null)
          {
            return NotFound();
          }
          existingEmployee.Name = employee.Name;
          existingEmployee.Phone = employee.Phone;
          existingEmployee.Address.City = employee.Address.City;
          existingEmployee.Address.State = employee.Address.State;
          existingEmployee.Address.ZipCode = employee.Address.ZipCode;
          existingEmployee.Address.Country = employee.Address.Country;
          _unitOfWork.Employee.Update(existingEmployee);
          _unitOfWork.Save();
          TempData["success"] = "Employee Details Edited !";
        }
        catch (DbUpdateConcurrencyException)
        {

        }
        return RedirectToAction(nameof(Index));
      }
      return View(employee);
    }
    // GET: Employees/Delete/5
    public IActionResult Delete(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }
      var employee = _unitOfWork.Employee.GetTEmpWithAddress(x => x.EmployeeId == id, e => e.Address);
      if (employee == null)
      {
        return NotFound();
      }
      ViewData["Title"] = "Delete";
      return View(employee);
    }
    // POST: Employees/Delete/5
    [Audit]
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
      var employee = _unitOfWork.Employee.GetTEmpWithAddress(x => x.EmployeeId == id, e => e.Address);
      if (employee == null)
      {
        return NotFound();
      }
      _unitOfWork.Employee.Delete(employee);
      _unitOfWork.Save();
      TempData["success"] = "Employee Details Removed !";
      return RedirectToAction(nameof(Index));
    }
  }
}