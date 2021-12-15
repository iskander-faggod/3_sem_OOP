using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ReportsApi.Models
{
    public class WorkTask
    {
        [Key]
        public Guid TaskId { get; set; }
        public TaskType TaskState { get; set; }
        public DateTime TaskEditTime { get; set; }
        public DateTime TaskCreationTime { get; set; } = DateTime.Now;
        public Guid ExecutorId { get; set; }
        public Guid ReportId { get; set; }
        public Employee Executor { get; set; }
        public string Comment { get; set; }

        private WorkTask(){}
        public WorkTask
        (
            TaskType taskState,
            DateTime taskEditTime,
            Guid executorId,
            Guid reportId, 
            string comment
        )
        {
            if (string.IsNullOrWhiteSpace(comment)) throw new ArgumentException("comment is invalid", nameof(comment));
            TaskId = new Guid();
            TaskState = taskState;
            TaskEditTime = taskEditTime;
            ExecutorId = executorId;
            Comment = comment;
            ReportId = reportId;
        }
    }
    public enum TaskType
    {
        Open = 0,
        Active = 1,
        Resolved = 2, 
    }
}