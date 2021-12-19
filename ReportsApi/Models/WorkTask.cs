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
        public DateTime TaskEditTime { get; set; } = DateTime.Now;
        public DateTime TaskCreationTime { get; set; } = DateTime.Now;
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