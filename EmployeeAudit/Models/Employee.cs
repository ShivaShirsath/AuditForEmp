using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EmployeeAudit.Models
{
  public class Employee
  {
    [Key]
    [DisplayName("Id")]
    public int EmployeeId { get; set; }
    [Required(ErrorMessage = "Please Enter your Good Name.")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 100 characters.")]
    public string? Name { get; set; }
    [Required(ErrorMessage = "Please Enter the Phone number.")]
    [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number must be a 10 digit number.")]
    public string? Phone { get; set; }
    [Required]
    [ValidateNever]
    public Address? Address { get; set; }
  }
}