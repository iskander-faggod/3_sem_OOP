using System;
using System.Collections.Generic;
using ReportsApi.Models;

namespace ReportsApi.DTO
{
    public class ReportDTO
    {
        public Guid Id;
        public List<WorkTask> Tasks { get; set; }
        public Guid Writer{ get; set; }
    }
}