using System;
using ReportsApi.Models;

namespace ReportsApi.DTO
{
    public class TaskDTO
    {
        public Guid Id;
        public TaskType TaskState { get; set; }
        public DateTime TaskEditTime { get; set; } = DateTime.Now;
        public DateTime TaskCreationTime { get; set; } = DateTime.Now;
        public Guid ExecutorId { get; set; }
        public string Comment { get; set; }
    }
}