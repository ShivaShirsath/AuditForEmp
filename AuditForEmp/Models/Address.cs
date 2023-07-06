namespace AuditForEmp.Models
{
    public class Address
    {
        public int Id { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public ICollection<Employee> Employees { get; set; }
    }
}