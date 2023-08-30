using System.ComponentModel;

namespace EmployeeAudit.Models
{
  public class Event
  {
    [DisplayName("Id")]
    public long EventId { get; set; }
    [DisplayName("Updated Date & Time")]
    public string? LastUpdatedDate { get; set; }
    [DisplayName("Request & View")]
    public string? EventType { get; set; }
    public string? User { get; set; }
    [DisplayName("JSON Data")]
    public string? JsonData { get; set; }
  }
}