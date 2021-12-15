using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ReportsApi.Models;

namespace ReportsApi.Services
{
    public interface IReportService
    {
        Task<ActionResult<IEnumerable<Report>>> GetAllReports();

        Task<ActionResult<Report>> GetReport(Guid id);

        Task<Report> UpdateReport(Guid id, [FromBody] Report report);

        Task Create([FromBody] Report report);

        Task Delete(Guid id);
    }
}