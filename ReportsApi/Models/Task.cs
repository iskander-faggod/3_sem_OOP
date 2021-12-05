using System;

namespace ReportsApi.Models
{
    public class Task
    {
        public TaskType TaskState { get; set; }
        public Guid TaskId { get; set; }
        public DateTime TaskEditTime { get; set; }
        public Employee Executor { get; set; }
        public string Comment { get; set; }
    }
    public enum TaskType
    {
        Open = 0,
        Active = 1,
        Resolved = 2, 
    }
}