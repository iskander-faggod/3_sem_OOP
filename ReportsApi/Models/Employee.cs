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
        public List<WorkTask> Tasks { get; set; }

        private Employee() { }

        public Employee
        (
            EmployeeType type,
            string name,
            string surname,
            string positionName,
            Guid? leaderId,
            List<WorkTask> tasks
        )
        {
            
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("name is invalid", nameof(name));
            if (string.IsNullOrWhiteSpace(surname)) throw new ArgumentException("surname is invalid", nameof(surname));
            if (string.IsNullOrWhiteSpace(positionName))
                throw new ArgumentException("positionName is invalid", nameof(positionName));
            EmployeeId = new Guid();
            Type = type;
            Name = name;
            Surname = surname;
            PositionName = positionName;
            LeaderId = leaderId;
            Tasks = tasks ?? new List<WorkTask>();
        }
    }

    public enum EmployeeType
    {
        TeamLeader = 0,
        Leader = 1,
        Slave = 2,
    }
}