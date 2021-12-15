using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ReportsApi.Models
{
    public class Report
    {
        [Key] public Guid ReportId { get; set; }
        public List<Guid> TasksId { get; set; }
        public Guid WriterId { get; set; }

        public Report() { }

        public Report
        (
            Guid reportId,
            List<Guid> tasksId,
            Guid writerId
        )
        {
            if (reportId == Guid.Empty) throw new ArgumentException($"{nameof(reportId)} is invalid");
            if (writerId == Guid.Empty) throw new ArgumentException($"{nameof(writerId)} is invalid");
            ReportId = reportId;
            TasksId = tasksId ?? throw new ArgumentException($"{nameof(reportId)} is invalid");
            WriterId = writerId;
        }
    }
}