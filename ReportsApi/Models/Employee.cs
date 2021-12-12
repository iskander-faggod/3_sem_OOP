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
        public Guid LeaderId { get; set; }
        public Guid TeamLeaderId { get; set; }
        public List<WorkTask> Tasks { get; set; }

        private Employee() { }

        public Employee
        (
            Guid employeeId,
            EmployeeType type,
            string name,
            string surname,
            string positionName,
            Guid leaderId,
            Guid teamLeaderId,
            List<WorkTask> tasks
        )
        {
            if (employeeId == Guid.Empty) throw new ArgumentException("employeeId is invalid", nameof(employeeId));
            if (leaderId == Guid.Empty) throw new ArgumentException("leaderId is invalid", nameof(leaderId));
            if (teamLeaderId == Guid.Empty)
                throw new ArgumentException("teamLeaderId is invalid", nameof(teamLeaderId));
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("name is invalid", nameof(name));
            if (string.IsNullOrWhiteSpace(surname)) throw new ArgumentException("surname is invalid", nameof(surname));
            if (string.IsNullOrWhiteSpace(positionName))
                throw new ArgumentException("positionName is invalid", nameof(positionName));
            EmployeeId = employeeId;
            Type = type;
            Name = name;
            Surname = surname;
            PositionName = positionName;
            LeaderId = leaderId;
            TeamLeaderId = teamLeaderId;
            Tasks = tasks ?? throw new ArgumentException($"{nameof(tasks)} is invalid");
        }
    }

    public enum EmployeeType
    {
        TeamLeader = 0,
        Leader = 1,
        Slave = 2,
    }
}