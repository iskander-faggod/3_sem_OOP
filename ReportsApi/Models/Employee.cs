using System;

namespace ReportsApi.Models
{
    public class Employee
    {
        public EmployeeType Type;
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PositionName { get; set; }
        public Guid Id { get; set; }
        public Guid LeaderId { get; set; }
        public Guid TeamLeaderId { get; set; }
    }
    
    public enum EmployeeType
    {
        TeamLeader = 0,
        Leader = 1,
        Slave = 2, 
    }
}