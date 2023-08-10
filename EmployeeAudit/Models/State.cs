using System.ComponentModel.DataAnnotations;

namespace EmployeeAudit.Models
{
  public class State
  {
    [Key]
    public string state_name { get; set; }
    public string country_name { get; set; }
  }
}
