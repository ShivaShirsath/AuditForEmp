using System.ComponentModel;

namespace EmployeeAudit.Models
{
  public class Event
  {
    [DisplayName("Id")]
    public long EventId { get; set; }
    [DisplayName("Updated Date & Time")]
    public DateTime? LastUpdatedDate { get; set; } = DateTime.Now;
    [DisplayName("Request & View")]
    public string EventType { get; set; }
    public string User { get; set; }
    [DisplayName("JSON Data")]
    public string JsonData { get; set; }
  }
}