using System;
using ReportsApi.Models;

namespace ReportsApi.DTO
{
    public class CreateEmployeeArguments
    {
        public EmployeeType Type { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PositionName { get; set; }
        public Guid? LeaderId { get; set; }
    }
}