using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeAudit.Models
{
  public class Address
  {
    public int AddressId { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string ZipCode { get; set; }
    public string Country { get; set; }
    [ForeignKey("Employee")]
    public int EmployeeId { get; set; }
  }
}
