using app.Tabaldi.PACT.Application.AttendanceRecurrenceAgg;
using app.Tabaldi.PACT.Domain.AttendanceModule.AttendanceAgg.Models;
using app.Tabaldi.PACT.Domain.AttendanceModule.AttendanceRecurrenceAgg.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

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
    }

}
