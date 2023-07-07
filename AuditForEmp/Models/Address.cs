
using System.ComponentModel.DataAnnotations.Schema;

namespace AuditForEmp.Models
{
    public class Address
    {
        [ForeignKey("Employee")]
        public int Id { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public Employee Employee { get; set; }
    }
}