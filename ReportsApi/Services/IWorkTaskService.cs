using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ReportsApi.Models;

namespace ReportsApi.Services
{
    public interface IWorkTaskService
    {
        Task Create(WorkTask workTask);

        Task Delete(Guid id);

        Task<List<WorkTask>> GetAllTasks();

        Task<WorkTask> GetTaskById(Guid taskId);

        Task<WorkTask> GetTaskByDate(DateTime dateTime);

        Task<WorkTask> GetTaskByEmployee(Employee employee);

        List<WorkTask> GetUnchangedTasks();

        Task<WorkTask> UpdateTaskState(Guid id, TaskType state);

        Task<WorkTask> UploadTaskComment(Guid id, string comment);

        Task<WorkTask> ChangeTaskExecutor(Guid id, Employee employee);

        List<WorkTask> GetTaskByExecutorRole(EmployeeType role);

        bool WorkTaskExists(Guid id);
    }
}