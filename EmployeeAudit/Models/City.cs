using System.ComponentModel.DataAnnotations;

namespace EmployeeAudit.Models
{
  public class City
  {
    [Key]
    public string city_name { get; set; }
    public string state_name { get; set; }
  }
}
