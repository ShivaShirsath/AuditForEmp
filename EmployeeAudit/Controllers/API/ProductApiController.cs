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

    public ProductApiController(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    [HttpGet]
    [AuditIgnore]
    public async Task<ActionResult<IEnumerable<Product?>>> GetProducts() => Ok(await _unitOfWork.Product.GetProductsAsync());

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
