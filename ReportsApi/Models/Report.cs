using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ReportsApi.Models
{
    public class Report
    {
        [Key] public Guid ReportId { get; set; }
        public List<WorkTask> Tasks { get; set; } = new List<WorkTask>();
        public Guid Writer{ get; set; }
    }
}