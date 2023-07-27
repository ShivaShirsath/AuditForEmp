using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EmployeeAudit.Models
{
  public class Employee
  {
    [Key]
    [DisplayName("Id")]
    public int EmployeeId { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string Phone { get; set; }
    [Required]
    public Address Address { get; set; }
  }
}