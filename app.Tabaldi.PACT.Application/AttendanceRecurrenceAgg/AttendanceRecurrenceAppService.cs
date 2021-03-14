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
        IQueryable<AttendancesCurrentMonthModel> GetCurrentMonthAttendances();
    }

    public class AttendanceRecurrenceAppService : AppServiceBase<IAttendanceRecurrenceRepository>, IAttendanceRecurrenceAppService
    {
        public AttendanceRecurrenceAppService(IAttendanceRecurrenceRepository repository)
            : base(repository)
        { }

        public IQueryable<AttendancesCurrentDayModel> GetCurrentDayAttendances()
        {
            return Repository.RetrieveMapper(new AttendancesCurrentDayModelMapper());
        }

        public IQueryable<AttendancesCurrentMonthModel> GetCurrentMonthAttendances()
        {
            var data = Repository.RetrieveMapper(new AttendancesCurrentMonthModelMapper());

            //data = data.Select(p => new AttendancesCurrentMonthModel()
            //{

            //});

            return data;
        }

        public IQueryable<AttendancesCurrentWeekModel> GetCurrentWeekAttendances()
        {
            return Repository.RetrieveMapper(new AttendancesCurrentWeekModelMapper());
        }
    }
}
