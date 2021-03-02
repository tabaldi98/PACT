using app.Tabaldi.PACT.Domain.AttendanceModule.AttendanceRecurrenceAgg;
using app.Tabaldi.PACT.Domain.Seedwork.Contracts.Mappers;
using app.Tabaldi.PACT.Domain.Seedwork.Specification;
using System;
using System.Linq.Expressions;

namespace app.Tabaldi.PACT.Domain.AttendanceModule.AttendanceRecurrenceAgg.Models
{
    public class AttendancesCurrentDayModel : IAttendancesModelBase
    {
        public string ClientName { get; set; }
        public DateTime StartAttendance { get; set; }
        public DateTime EndAttendance { get; set; }
    }

    public class AttendancesCurrentDayModelMapper : IHaveMapper<AttendanceRecurrence, AttendancesCurrentDayModel>
    {
        public AttendancesCurrentDayModelMapper()
        { }

        public Expression<Func<AttendanceRecurrence, AttendancesCurrentDayModel>> Selector => p => new AttendancesCurrentDayModel()
        {
            ClientName = p.Client.Name,
            StartAttendance = p.StartTime,
            EndAttendance = p.EndTime,
        };

        public ISpecification<AttendanceRecurrence> Specification => AttendanceRecurrenceSpecifications.RetrieveAttendanceToCurrentDay();
    }
}
