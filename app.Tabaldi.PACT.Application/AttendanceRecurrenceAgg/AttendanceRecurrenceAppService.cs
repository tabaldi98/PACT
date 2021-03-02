using app.Tabaldi.PACT.Domain.AttendanceModule.AttendanceAgg.Models;
using app.Tabaldi.PACT.Domain.AttendanceModule.AttendanceRecurrenceAgg;
using app.Tabaldi.PACT.Domain.AttendanceModule.AttendanceRecurrenceAgg.Models;
using System.Linq;

namespace app.Tabaldi.PACT.Application.AttendanceRecurrenceAgg
{
    public interface IAttendanceRecurrenceAppService
    {
        IQueryable<AttendancesCurrentDayModel> GetCurrentDayAttendances();
        IQueryable<AttendancesCurrentWeekModel> GetCurrentWeekAttendances();
    }

    public class AttendanceRecurrenceRepository : AppServiceBase<IAttendanceRecurrenceRepository>, IAttendanceRecurrenceAppService
    {
        public AttendanceRecurrenceRepository(IAttendanceRecurrenceRepository repository)
            : base(repository)
        { }

        public IQueryable<AttendancesCurrentDayModel> GetCurrentDayAttendances()
        {
            return Repository.RetrieveMapper(new AttendancesCurrentDayModelMapper());
        }

        public IQueryable<AttendancesCurrentWeekModel> GetCurrentWeekAttendances()
        {
            return Repository.RetrieveMapper(new AttendancesCurrentWeekModelMapper());
        }
    }
}
