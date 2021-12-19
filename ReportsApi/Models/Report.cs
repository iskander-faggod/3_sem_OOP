using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ReportsApi.Models
{
    public class Report
    {
        [Key] public Guid ReportId { get; set; }
        public List<WorkTask> Tasks { get; set; }
        public Guid Writer{ get; set; }

        public Report() { }

        public Report
        (
            Guid reportId,
            List<WorkTask> tasks,
            Guid writerId
        )
        {
            if (reportId == Guid.Empty) throw new ArgumentException($"{nameof(reportId)} is invalid");
            if (writerId == Guid.Empty) throw new ArgumentException($"{nameof(writerId)} is invalid");
            ReportId = new Guid();
            Tasks = new List<WorkTask>();
            Writer = writerId;
        }
    }
}