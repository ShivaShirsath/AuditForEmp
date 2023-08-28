using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeAudit.Models
{
  public class Address
  {
    public int AddressId { get; set; }
    [Required(ErrorMessage = "Please Enter a City.")]
    public string? City { get; set; }
    [Required(ErrorMessage = "Please Enter a State.")]
    public string? State { get; set; }
    [Required(ErrorMessage = "Please Enter the ZipCode.")]
    [RegularExpression(@"^\d{6}$", ErrorMessage = "ZipCode must be a 6 digit number.")]
    public string? ZipCode { get; set; }
    [Required(ErrorMessage = "Please Select a Country.")]
    public string? Country { get; set; }
    [ForeignKey("Employee")]
    public int EmployeeId { get; set; }
  }
}