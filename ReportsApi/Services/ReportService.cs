using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReportsApi.Context;
using ReportsApi.DTO;
using ReportsApi.Extensions;
using ReportsApi.Models;
using ReportsApi.Services.IServices;

namespace ReportsApi.Services
{
    public class ReportService : IReportService
    {
        private readonly ReportsDbContext _context;

        public ReportService(ReportsDbContext context)
        {
            _context = context;
        }

        public async Task<ActionResult<IEnumerable<Report>>> GetAllReports()
        {
            return await _context.Reports.ToListAsync();
        }

        public async Task<ActionResult<Report>> GetReport(Guid id)
        {
            Report report = await _context.Reports.FindAsync(id);
            if (report is null) throw new ArgumentNullException($"{nameof(report)} is null");
            return report;
        }
        
        public async Task<Report> UpdateReport(ReportDTO report)
        {
            Report foundedReport = await _context.Reports.FindAsync(report.Id);
            if (foundedReport is null) throw new ArgumentException(nameof(foundedReport) + "is invalid");
            foundedReport.Tasks = report.Tasks;
            foundedReport.Writer = report.Writer;
            await _context.SaveChangesAsync();
            return foundedReport;
        }

        public async Task Create(Report report)
        {
            _context.Reports.Add(report);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            Report report = await _context.Reports.FindAsync(id);
            if (report is null) throw new ArgumentException(nameof(report) + "is invalid");
            _context.Reports.Remove(report);
            await _context.SaveChangesAsync();
        }

        public Task<List<WorkTask>> GetTasksForAWeek()
        {
            DateTime startOfWeek = DateTime.Now.StartOfWeek();
            var workTasks =  _context.WorkTasks
                .Where(task =>
                task.TaskCreationTime >= startOfWeek && task.TaskCreationTime <= DateTime.Now)
                .ToList();
            if (workTasks is null) throw new ArgumentException($"{nameof(workTasks)} is null");
            return Task.FromResult(workTasks);
        }
        
        public async Task AddNewTaskInReport(Guid reportId, TaskDTO task)
        {
            Report report = await _context.Reports.FindAsync(reportId);
            if (task is null) throw new ArgumentException($"{nameof(task)} is null");
            var newTask = new WorkTask()
            {
                Comment = task.Comment,
                TaskCreationTime = DateTime.Now,
                TaskEditTime = DateTime.Now,
                TaskId = task.Id,
                TaskState = task.TaskState,
            };
            report.Tasks.Add(newTask);
        }

        private bool ReportExists(Guid id)
        {
            return _context.Reports.Any(e => e.ReportId == id);
        }
    }
}