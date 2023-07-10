﻿using System.ComponentModel.DataAnnotations;

namespace EmpAddrAudit.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public Address Address { get; set; }
    }
}
