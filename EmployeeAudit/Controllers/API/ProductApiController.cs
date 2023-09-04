using Audit.WebApi;
using EmployeeAudit.Infrastructure.IRepository;
using EmployeeAudit.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeAudit.Controllers.API
{
  [Route("api/product")]
  [ApiController]
  [AuditApi(EventTypeName = "{controller}/{action}")]
  public class ProductApiController : ControllerBase
  {
    private readonly IUnitOfWork _unitOfWork;

    private const int PageSize = 10; // Number of items per page
    public ProductApiController(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    //[HttpGet]
    //[AuditIgnore]
    //public async Task<ActionResult<IEnumerable<Product?>>> GetProducts() => Ok(await _unitOfWork.Product.GetProductsAsync());
    [HttpGet]
    [AuditIgnore]
    public async Task<ActionResult<Dictionary<string, object>>> GetProducts(int page = 1)
    {
      IEnumerable<Product> prod = await _unitOfWork.Product.GetProductsAsync();
      // Sort the Products in descending order by ID
      prod = prod.OrderByDescending(e => e.ProductId);

      int totalCount = prod.Count();
      int totalPages = (int)Math.Ceiling(totalCount / (double)PageSize);

      if (page < 1)
      {
        page = 1;
      }
      else if (page > totalPages)
      {
        page = totalPages;
      }
      var paginatedEvents = prod
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
    public async Task<ActionResult<Product>> Create(Product product)
    {
      if (ModelState.IsValid)
      {
        _unitOfWork.Product.Add(product);
        await _unitOfWork.SaveChangesAsync();
        return CreatedAtAction(nameof(Details), new { id = product.ProductId }, product);
      }
      return BadRequest(ModelState);
    }

    [HttpGet("{id}")]
    [AuditIgnore]
    public async Task<ActionResult<Product>> Details(int id)
    {
      var product = await _unitOfWork.Product.GetProductAsync(x => x.ProductId == id);
      return product == null ? NotFound() : Ok(product);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Product[] product)
    {
      if (id == product[0].ProductId)
      {
        _unitOfWork.Product.Update(product[0]);
        await _unitOfWork.SaveChangesAsync();
        return NoContent();
      }
      return BadRequest();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
      var product = await _unitOfWork.Product.GetProductAsync(x => x.ProductId == id);
      if (product != null)
      {
        _unitOfWork.Product.Delete(product);
        await _unitOfWork.SaveChangesAsync();
        return NoContent();
      }
      return NotFound();
    }
  }
}
