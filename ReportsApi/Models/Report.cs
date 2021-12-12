using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ReportsApi.Models
{
    public class Report
    {
        [Key] public Guid ReportId { get; set; }
        public List<WorkTask> Tasks { get; set; }
        public Employee Writer { get; set; }

        public Report() { }

        public Report
        (
            Guid reportId,
            List<WorkTask> tasks,
            Employee writer
        )
        {
            if (reportId == Guid.Empty) throw new ArgumentException($"{nameof(reportId)} is invalid");
            ReportId = reportId;
            Tasks = tasks ?? throw new ArgumentException($"{nameof(tasks)} is invalid");
            Writer = writer ?? throw new ArgumentException($"{nameof(writer)} is invalid");
        }
    }
}