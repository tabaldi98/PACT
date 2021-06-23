using app.Tabaldi.PACT.Domain.AttendanceModule.AttendanceRecurrenceAgg;
using app.Tabaldi.PACT.Domain.Seedwork.Contracts.Mappers;
using app.Tabaldi.PACT.Domain.Seedwork.Specification;
using app.Tabaldi.PACT.LibraryModels.AttendanceRecurrenceModule.Models;
using System;
using System.Linq.Expressions;

namespace app.Tabaldi.PACT.Application.AttendanceRecurrenceAgg.Models
{
    public class AttendancesCurrentDayModelMapper : IHaveMapper<AttendanceRecurrence, AttendancesCurrentDayModel>
    {
        public AttendancesCurrentDayModelMapper(int userId, DayOfWeek? weekDay = null)
        {
            Specification = weekDay == null 
                ? AttendanceRecurrenceSpecifications.RetrieveAttendanceToCurrentDay(userId)
                : AttendanceRecurrenceSpecifications.RetrieveAttendanceByWeekDay(userId, weekDay.Value);
        }

        public Expression<Func<AttendanceRecurrence, AttendancesCurrentDayModel>> Selector => p => new AttendancesCurrentDayModel()
        {
            ClientID = p.Client.ID,
            ClientName = p.Client.Name,
            StartAttendance = p.StartTime,
            EndAttendance = p.EndTime,
        };

        public ISpecification<AttendanceRecurrence> Specification { get; }
    }
}
