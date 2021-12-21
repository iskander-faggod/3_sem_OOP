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
    [Route("api/reports")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;

        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Report>>> GetReports()
        {
            return await _reportService.GetAllReports();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Report>> GetReport(Guid id)
        {
            return await _reportService.GetReport(id);
        }
        
        [HttpGet("week")]
        public async Task<List<WorkTask>> GetReportByCurrentWeek()
        {
            return await _reportService.GetTasksForAWeek();
        }

        [HttpPut("{id}")]
        public async Task PutReport([FromBody] ReportDTO report)
        {
            await _reportService.UpdateReport(report);
        }

        [HttpPost]
        public async Task PostReport([FromBody] CreateReportArguments report)
        {
            var newReport = new Report()
            {
                ReportId = Guid.NewGuid(),
                Writer = report.Writer
            };
             await _reportService.Create(newReport);
        }
        
        [HttpPost("add-task/{reportId}/{taskId}")]
        public async Task PostReportTask(Guid reportId,[FromBody] TaskDTO task)
        {
            await _reportService.AddNewTaskInReport(reportId, task);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReport(Guid id)
        {
            await _reportService.Delete(id);
            return NoContent();
        }
    }
}
