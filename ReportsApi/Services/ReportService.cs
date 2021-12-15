using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReportsApi.Context;
using ReportsApi.Models;

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

        public async Task<Report> UpdateReport(Guid id, Report report)
        {
            Report foundedReport = await _context.Reports.FindAsync(id);
            if (foundedReport is null) throw new ArgumentException(nameof(foundedReport) + "is invalid");
            foundedReport.ReportId = report.ReportId;
            foundedReport.TasksId = report.TasksId;
            foundedReport.WriterId = report.WriterId;
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
        
        private bool ReportExists(Guid id)
        {
            return _context.Reports.Any(e => e.ReportId == id);
        }
    }
}