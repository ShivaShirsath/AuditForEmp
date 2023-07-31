using System.ComponentModel.DataAnnotations;

namespace EmployeeAudit.Models
{
  public class Country
  {
    [Key]
    public string country_name { get; set; }
    public string country_short_name { get; set; }
    public int country_phone_code { get; set; }
  }
}
