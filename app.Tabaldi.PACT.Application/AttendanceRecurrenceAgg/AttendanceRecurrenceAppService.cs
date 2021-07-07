using app.Tabaldi.PACT.Crosscutting.NetCore.Extensions;
using app.Tabaldi.PACT.Application.AttendanceRecurrenceAgg.Models;
using app.Tabaldi.PACT.Crosscutting.NetCore.AuthenticatedUser;
using app.Tabaldi.PACT.Domain.AttendanceModule.AttendanceAgg;
using app.Tabaldi.PACT.Domain.AttendanceModule.AttendanceRecurrenceAgg;
using app.Tabaldi.PACT.Domain.ClientsModule.ClientAgg;
using app.Tabaldi.PACT.Domain.UsersModule.UserAgg;
using app.Tabaldi.PACT.LibraryModels.AttendanceRecurrenceModule.Commands;
using app.Tabaldi.PACT.LibraryModels.AttendanceRecurrenceModule.Enums;
using app.Tabaldi.PACT.LibraryModels.AttendanceRecurrenceModule.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace app.Tabaldi.PACT.Application.AttendanceRecurrenceAgg
{
    public interface IAttendanceRecurrenceAppService
    {
        IQueryable<AttendancesCurrentDayModel> GetCurrentDayAttendances();
        IQueryable<AttendancesCurrentWeekModel> GetCurrentWeekAttendances();
        IQueryable<AttendancesCurrentMonthModel> GetCurrentMonthAttendances();
        IQueryable<IAttendancesModelBase> RetrieveAttendancesByDate(AttendancesByDateCommand command);
        Task<IEnumerable<AttendancesCurrentDayModel>> RetrieveAttendancesAlertsAsync();
    }

    public class AttendanceRecurrenceAppService : AppServiceBase<IAttendanceRecurrenceRepository>, IAttendanceRecurrenceAppService
    {
        private readonly IUserRepository _userRepository;
        private readonly IAttendanceRepository _attendanceRepository;
        private readonly Lazy<IAuthenticatedUser> _authenticatedUser;

        public AttendanceRecurrenceAppService(
            IAttendanceRecurrenceRepository repository,
            IUserRepository userRepository,
            IAttendanceRepository attendanceRepository,
            Lazy<IAuthenticatedUser> authenticatedUser)
            : base(repository)
        {
            _userRepository = userRepository;
            _attendanceRepository = attendanceRepository;
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

        public async Task<IEnumerable<AttendancesCurrentDayModel>> RetrieveAttendancesAlertsAsync()
        {
            var altersToSend = new List<AttendancesCurrentDayModel>();

            var user = await _userRepository.SingleOrDefaultAsync(UserSpecification.RetrieveByID(_authenticatedUser.Value.User.ID), false, x => x.Clients);
            if (!user.SendAlerts) { return altersToSend; }

            var dtNow = DateTime.Now;

            foreach (var client in user.Clients)
            {
                var attendanceToday = client.Recurrences.FirstOrDefault(p => p.WeekDay == (WeekDay)dtNow.DayOfWeek);
                if (attendanceToday == null)
                {
                    return altersToSend;
                }

                var attendanceDate = new DateTime(dtNow.Year, dtNow.Month, dtNow.Day, 0, 0, 0);
                var attendance = client.Attendances.FirstOrDefault(p => p.Date.Between(attendanceDate));

                if (attendance == null)
                {
                    attendance = new Attendance(client.ID, attendanceDate, attendanceToday.StartTime, attendanceToday.EndTime, string.Empty);
                    _attendanceRepository.Create(attendance);
                }

                var startDate = new DateTime(dtNow.Year, dtNow.Month, dtNow.Day, attendanceToday.StartTime.Hour, attendanceToday.StartTime.Minute, 0);

                if (!attendance.AlertHasSend && startDate.AddMinutes(-15) <= dtNow)
                {
                    attendance.SetAlertSended();
                    altersToSend.Add(new AttendancesCurrentDayModel()
                    {
                        ClientID = client.ID,
                        ClientName = client.Name,
                        StartAttendance = attendance.HourInitial,
                        EndAttendance = attendance.HourFinish,
                    });
                }
            }

            await CommitAsync();

            return altersToSend;
        }

        public IQueryable<IAttendancesModelBase> RetrieveAttendancesByDate(AttendancesByDateCommand command)
        {
            return Repository.RetrieveMapper(new AttendancesCurrentDayModelMapper(_authenticatedUser.Value.User.ID, command.DateFilter.DayOfWeek));
        }
    }
}
