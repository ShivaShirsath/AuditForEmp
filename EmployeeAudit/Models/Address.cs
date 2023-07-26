using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeAudit.Models
{
  public class Address
  {
    public int AddressId { get; set; }
    [Required]
    public string City { get; set; }
    [Required]
    public string State { get; set; }
    [Required]
    public string ZipCode { get; set; }
    [Required]
    public string Country { get; set; }
    [ForeignKey("Employee")]
    public int EmployeeId { get; set; }
  }
}
