using app.Tabaldi.PACT.Application.AttendanceAgg;
using app.Tabaldi.PACT.Domain.AttendanceModule.AttendanceAgg.Commands;
using app.Tabaldi.PACT.Domain.AttendanceModule.AttendanceAgg.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace app.Tabaldi.PACT.Api.Controllers.Api
{
    [Route("api/attendance")]
    [ApiController]
    public class AttendanceController : ControllerBase
    {
        private readonly IAttendanceAppService _attendanceAppService;

        public AttendanceController(IAttendanceAppService attendanceAppService)
        {
            _attendanceAppService = attendanceAppService;
        }

        [HttpPost]
        public async Task<int> CreateAsync(AttendanceAddCommand command)
        {
            return await _attendanceAppService.CreateAsync(command);
        }

        [HttpGet]
        public IQueryable<AttendanceModel> GetByClientId(int clientId)
        {
            return _attendanceAppService.RetrieveAllByClientID(clientId);
        }

        [HttpPost]
        [Route("remove")]
        public async Task<bool> RemoveAsync(AttendanceRemoveCommand command)
        {
            return await _attendanceAppService.RemoveAsync(command);
        }

        [HttpPost]
        [Route("edit")]
        public async Task<bool> EditAsync(AttendanceEditCommand command)
        {
            return await _attendanceAppService.UpdateAsync(command);
        }

        [HttpGet]
        [Route("{id:int}/attendances-by-type")]
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