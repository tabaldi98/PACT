using app.Tabaldi.PACT.Application.ReportsModule;
using app.Tabaldi.PACT.LibraryModels.ReportsModule.Commands;
using app.Tabaldi.PACT.LibraryModels.ReportsModule.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace app.Tabaldi.PACT.Api.Controllers.Api.ReportsModule
{
    [Authorize]
    [Route("api/reports")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly Lazy<IReportsAppService> _reportsAppService;

        public ReportsController(Lazy<IReportsAppService> reportsAppService)
        {
            _reportsAppService = reportsAppService;
        }

        [HttpPost]
        [Route("financial")]
        public async Task<IEnumerable<ReportsFinancialModel>> RetrieveFinancialReportAsync(ReportsDefaultFilterCommand command)
        {
            return await _reportsAppService.Value.RetrieveFinancialReportAsync(command);
        }

        [HttpPost]
        [Route("attendance")]
        public IQueryable<ReportsAttendancesModel> RetrieveFinancialReport(ReportClientIDFilterCommand command)
        {
            return _reportsAppService.Value.RetrieveAttendanceReport(command);
        }
    }
}