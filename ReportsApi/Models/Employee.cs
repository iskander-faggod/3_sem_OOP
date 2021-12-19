using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace ReportsApi.Models
{
    public class Employee
    {
        [Key] public Guid EmployeeId { get; set; }

        public EmployeeType Type;
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PositionName { get; set; }
        public Guid? LeaderId { get; set; }
        public List<WorkTask> Tasks { get; set; } = new List<WorkTask>();
    }

    public enum EmployeeType
    {
        TeamLeader = 0,
        Leader = 1,
        Slave = 2,
    }
}