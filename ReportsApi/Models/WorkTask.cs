using System;
using System.ComponentModel.DataAnnotations;
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
        public Employee Executor { get; set; }
        public string Comment { get; set; }

        private WorkTask(){}
        public WorkTask
        (
            Guid taskId,
            TaskType taskState,
            DateTime taskEditTime,
            Employee executor,
            string comment
        )
        {
            if (taskId == Guid.Empty) throw new ArgumentException("taskId is invalid", nameof(taskId));
            if (taskEditTime < TaskCreationTime) throw new ArgumentException("taskEditTime is invalid", nameof(taskEditTime));
            if (string.IsNullOrWhiteSpace(comment)) throw new ArgumentException("comment is invalid", nameof(comment));
            TaskId = taskId;
            TaskState = taskState;
            TaskEditTime = taskEditTime;
            Executor = executor ?? throw new ArgumentException("executor is invalid", nameof(executor));
            Comment = comment;
        }
    }
    public enum TaskType
    {
        Open = 0,
        Active = 1,
        Resolved = 2, 
    }
}