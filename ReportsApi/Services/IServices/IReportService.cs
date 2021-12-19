using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ReportsApi.Models;

namespace ReportsApi.Services.IServices
{
    public interface IReportService
    {
        Task<ActionResult<IEnumerable<Report>>> GetAllReports();

        Task<ActionResult<Report>> GetReport(Guid id);

        Task<Report> UpdateReport(Guid id, [FromBody] Report report);

        Task Create([FromBody] Report report);

        Task<List<WorkTask>> GetTasksForAWeek();

        Task AddNewTaskInReport(Guid reportId, WorkTask task);

        Task Delete(Guid id);
    }
}