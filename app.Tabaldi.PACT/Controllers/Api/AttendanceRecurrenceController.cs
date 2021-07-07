using app.Tabaldi.PACT.Application.AttendanceRecurrenceAgg;
using app.Tabaldi.PACT.LibraryModels.AttendanceModule.Enums;
using app.Tabaldi.PACT.LibraryModels.AttendanceRecurrenceModule.Commands;
using app.Tabaldi.PACT.LibraryModels.AttendanceRecurrenceModule.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace app.Tabaldi.PACT.Api.Controllers.Api
{
    [Authorize]
    [Route("api/attendance-recurrence")]
    [ApiController]
    public class AttendanceRecurrenceController : ControllerBase
    {
        private readonly IAttendanceRecurrenceAppService _attendanceAppService;

        public AttendanceRecurrenceController(IAttendanceRecurrenceAppService attendanceAppService)
        {
            _attendanceAppService = attendanceAppService;
        }

        [HttpGet]
        [Route("{id:int}/by-type")]
        public IQueryable<IAttendancesModelBase> RetrieveAttendancesByType(int id)
        {
            return (ViewPeriodType)id switch
            {
                ViewPeriodType.Today => _attendanceAppService.GetCurrentDayAttendances(),
                ViewPeriodType.CurrentWeek => _attendanceAppService.GetCurrentWeekAttendances(),
                _ => null,
            };
        }

        [HttpPost]
        [Route("by-date")]
        public IQueryable<IAttendancesModelBase> RetrieveAttendancesByDate(AttendancesByDateCommand command)
        {
            return _attendanceAppService.RetrieveAttendancesByDate(command);
        }

        [HttpGet]
        [Route("alerts")]
        public async Task<IEnumerable<AttendancesCurrentDayModel>> RetrieveAttendancesAlertsAsync()
        {
            return await _attendanceAppService.RetrieveAttendancesAlertsAsync();
        }
    }
}
