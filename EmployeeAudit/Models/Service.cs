using System.ComponentModel.DataAnnotations;

namespace EmployeeAudit.Models
{
  public class Service
  {
    [Key]
    public int ServiceId { get; set; }
    [Required(ErrorMessage = "Please enter the service name.")]
    public string? Name { get; set; }
    [Required(ErrorMessage = "Please select the service type.")]
    public string? Type { get; set; }
    public string? Info { get; set; }
    public string? Provider { get; set; }
  }
}
