using System.ComponentModel.DataAnnotations;

namespace EmployeeAudit.Models
{
  public class Product
  {
    [Key]
    public int ProductId { get; set; }
    [Required(ErrorMessage = "Please enter the product name.")]
    public string? Name { get; set; }
    [Required(ErrorMessage = "Please select the product category.")]
    public string? Category { get; set; }
    public string? Description { get; set; }
    public string? Compatibility { get; set; }
  }
}
