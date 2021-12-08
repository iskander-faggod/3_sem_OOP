using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReportsApi.Context;
using ReportsApi.Models;

namespace ReportsApi.Services
{
    public class WorkTaskService : IWorkTaskService
    {
        private readonly ReportsDbContext _context;

        public WorkTaskService(ReportsDbContext context)
        {
            _context = context;
        }

        public async Task Create(WorkTask workTask)
        {
            _context.WorkTasks.Add(workTask);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            WorkTask workTask = await _context.WorkTasks.FindAsync(id);
            if (workTask == null) throw new ArgumentException(nameof(workTask) + "is invalid");
            _context.WorkTasks.Remove(workTask);
            await _context.SaveChangesAsync();
        }

        public async Task<List<WorkTask>> GetAllTasks()
        {
            return await _context.WorkTasks.ToListAsync();
        }

        public async Task<WorkTask> GetTaskById(Guid taskId)
        {
            WorkTask workTask = await _context.WorkTasks.FindAsync(taskId);
            if (workTask is null) throw new ArgumentException(nameof(workTask) + "is invalid");
            return workTask;
        }

        public async Task<WorkTask> GetTaskByDate(DateTime dateTime)
        {
            WorkTask workTask = await _context.WorkTasks
                .FirstOrDefaultAsync(x => x.TaskCreationTime == dateTime);
            return workTask;
        }

        public async Task<WorkTask> GetTaskByEmployee(Employee employee)
        {
            WorkTask workTask = await _context.WorkTasks
                .FirstOrDefaultAsync(x => x.Executor == employee);
            return workTask;
        }

        public List<WorkTask> GetUnchangedTasks()
        {
            var workTasks =  _context.WorkTasks
                .Where(x => x.TaskEditTime == x.TaskCreationTime).ToList();
            return workTasks;
        }

        public async Task<WorkTask> UpdateTaskState(Guid id, TaskType state)
        {
            WorkTask workTask = await _context.WorkTasks.FindAsync(id);
            if (workTask is null) throw new ArgumentException(nameof(workTask) + "is invalid");
            workTask.TaskState = state;
            await _context.SaveChangesAsync();
            return workTask;
        }

        public async Task<WorkTask> UploadTaskComment(Guid id, string comment)
        {
            WorkTask workTask = await _context.WorkTasks.FindAsync(id);
            if (workTask is null) throw new ArgumentException(nameof(workTask) + "is invalid");
            workTask.Comment = comment;
            await _context.SaveChangesAsync();
            return workTask;
        }

        public async Task<WorkTask> ChangeTaskExecutor(Guid id, Employee employee)
        {
            WorkTask workTask = await _context.WorkTasks.FindAsync(id);
            if (workTask is null) throw new ArgumentException(nameof(workTask) + "is invalid");
            workTask.Executor = employee;
            await _context.SaveChangesAsync();
            return workTask;
        }

        public List<WorkTask> GetTaskByExecutorRole(EmployeeType role)
        {
            var tasks = _context.WorkTasks
                .Where(x => x.Executor.Type == role).ToList();
            return tasks;
        }

        public bool WorkTaskExists(Guid id)
        {
            return _context.WorkTasks.Any(e => e.TaskId == id);
        }
    }
}