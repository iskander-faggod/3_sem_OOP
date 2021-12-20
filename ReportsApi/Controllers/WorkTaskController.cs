using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReportsApi.Context;
using ReportsApi.DTO;
using ReportsApi.Models;
using ReportsApi.Services;
using ReportsApi.Services.IServices;

namespace ReportsApi.Controllers
{
    [Route("api/work-task")]
    [ApiController]
    public class WorkTaskController : ControllerBase
    {
        private readonly IWorkTaskService _workTaskService;

        public WorkTaskController(IWorkTaskService workTaskService)
        {
            _workTaskService = workTaskService;
        }

        [HttpGet]
        public async Task<List<WorkTask>> GetWorkTasks()
        {
            return await _workTaskService.GetAllTasks();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<WorkTask>> GetWorkTask(Guid id)
        {
            WorkTask workTask = await _workTaskService.GetTaskById(id);
            return workTask;
        }
        
        [HttpGet("tasks/{time:datetime}")]
        public async Task<ActionResult<WorkTask>> GetWorkTaskByDate([FromBody]DateTime time)
        {
            WorkTask workTask = await _workTaskService.GetTaskByDate(time);
            return workTask;
        }
        
        [HttpGet("tasks/unchanged")]
        public List<WorkTask> GetWorkTaskByEmployee()
        {
            List<WorkTask> workTasks =  _workTaskService.GetUnchangedTasks();
            return workTasks;
        }
        
        [HttpGet("tasks/{role}")]
        public List<WorkTask> GetWorkTaskByEmployee([FromBody] EmployeeType role)
        {
            List<WorkTask> workTasks =  _workTaskService.GetTaskByExecutorRole(role);
            return workTasks;
        }

        [HttpPost]
        public async Task<IActionResult> PostWorkTask([FromBody] CreateTaskArguments workTask)
        {
            var newTask = new WorkTask()
            {
                TaskId = Guid.NewGuid(),
                TaskState = workTask.TaskState,
                TaskEditTime = workTask.TaskEditTime,
                TaskCreationTime = workTask.TaskCreationTime,
                Comment = workTask.Comment,
            };
            
            await _workTaskService.Create(workTask.ExecutorId, newTask);
            return Ok(newTask);
        }
        
        [HttpPatch("update/task/{id}")]
        public async Task PostWorkTask(Guid id, [FromBody]TaskType state)
        {
            await _workTaskService.UpdateTaskState(id, state);
        }
        
        [HttpPatch("update/comment/{id}")]
        public async Task PostWorkTask(Guid id, [FromBody]string comment)
        {
            await _workTaskService.UploadTaskComment(id, comment);
        }

        [HttpDelete("{id}")]
        public async Task DeleteTask(Guid id)
        {
            await _workTaskService.Delete(id);
        }

        private bool WorkTaskExists(Guid id)
        {
            return _workTaskService.WorkTaskExists(id);
        }
    }
}
