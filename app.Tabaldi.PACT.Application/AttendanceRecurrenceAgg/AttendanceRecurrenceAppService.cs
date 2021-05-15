using app.Tabaldi.PACT.Application.AttendanceRecurrenceAgg.Models;
using app.Tabaldi.PACT.Crosscutting.NetCore.AuthenticatedUser;
using app.Tabaldi.PACT.Domain.AttendanceModule.AttendanceRecurrenceAgg;
using app.Tabaldi.PACT.LibraryModels.AttendanceRecurrenceModule.Models;
using System;
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
        private readonly Lazy<IAuthenticatedUser> _authenticatedUser;

        public AttendanceRecurrenceAppService(
            IAttendanceRecurrenceRepository repository,
            Lazy<IAuthenticatedUser> authenticatedUser)
            : base(repository)
        {
            _authenticatedUser = authenticatedUser;
        }

        public IQueryable<AttendancesCurrentDayModel> GetCurrentDayAttendances()
        {
            return Repository.RetrieveMapper(new AttendancesCurrentDayModelMapper(_authenticatedUser.Value.User.ID));
        }

        public IQueryable<AttendancesCurrentMonthModel> GetCurrentMonthAttendances()
        {
            return Repository.RetrieveMapper(new AttendancesCurrentMonthModelMapper());
        }

        public IQueryable<AttendancesCurrentWeekModel> GetCurrentWeekAttendances()
        {
            return Repository.RetrieveMapper(new AttendancesCurrentWeekModelMapper(_authenticatedUser.Value.User.ID));
        }
    }
}
