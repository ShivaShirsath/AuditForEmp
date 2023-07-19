namespace EmployeeAudit.Models
{
  public class Event
  {
    public long EventId { get; set; }
    public DateTime? LastUpdatedDate { get; set; } = DateTime.Now;
    public string EventType { get; set; }
    public string User { get; set; }
    public string JsonData { get; set; }
  }
}