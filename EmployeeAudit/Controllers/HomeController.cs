using EmployeeAudit.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EmployeeAudit.Controllers
{
  public class HomeController : Controller
  {
    private readonly ILogger<HomeController> _logger;
    public HomeController(ILogger<HomeController> logger)
    {
      _logger = logger;
    }
    public IActionResult Index()
    {
      ViewData["Title"] = "Home Page";
      return View();
    }
    public IActionResult Privacy()
    {
      ViewData["Title"] = "Privacy Policy";
      return View();
    }
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
      return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
  }
}