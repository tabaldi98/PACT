using System;
using System.Linq.Expressions;
using app.Tabaldi.PACT.Domain.ClientsModule.ClientAgg;
using app.Tabaldi.PACT.Domain.Seedwork.Contracts.Mappers;
using app.Tabaldi.PACT.Domain.Seedwork.Specification;

namespace app.Tabaldi.PACT.Domain.AttendanceModule.AttendanceAgg.Models
{
    public class AttendancesCurrentDayModel : IAttendancesModelBase
    {
        public string ClientName { get; set; }
        public DateTime StartAttendance { get; set; }
        public DateTime EndAttendance { get; set; }
    }

    public class AttendancesCurrentDayModelMapper : IHaveMapper<Client, AttendancesCurrentDayModel>
    {
        private readonly DayOfWeek dayOfWeek = DateTime.Now.DayOfWeek;

        public AttendancesCurrentDayModelMapper()
        { }

        public Expression<Func<Client, AttendancesCurrentDayModel>> Selector => p => new AttendancesCurrentDayModel()
        {
            ClientName = p.Name,
            StartAttendance =
                    dayOfWeek == DayOfWeek.Sunday ? p.StartSunday.Value
                    : dayOfWeek == DayOfWeek.Monday ? p.StartMonday.Value
                    : dayOfWeek == DayOfWeek.Tuesday ? p.StartThursday.Value
                    : dayOfWeek == DayOfWeek.Wednesday ? p.StartWednesday.Value
                    : dayOfWeek == DayOfWeek.Thursday ? p.StartThursday.Value
                    : dayOfWeek == DayOfWeek.Friday ? p.StartFriday.Value
                    : dayOfWeek == DayOfWeek.Saturday ? p.StartSaturday.Value
                    : p.StartMonday.Value,
            EndAttendance =
                    dayOfWeek == DayOfWeek.Sunday ? p.EndSunday.Value
                    : dayOfWeek == DayOfWeek.Monday ? p.EndMonday.Value
                    : dayOfWeek == DayOfWeek.Tuesday ? p.EndThursday.Value
                    : dayOfWeek == DayOfWeek.Wednesday ? p.EndWednesday.Value
                    : dayOfWeek == DayOfWeek.Thursday ? p.EndThursday.Value
                    : dayOfWeek == DayOfWeek.Friday ? p.EndFriday.Value
                    : dayOfWeek == DayOfWeek.Saturday ? p.EndSaturday.Value
                    : p.StartMonday.Value,
        };

        public ISpecification<Client> Specification => ClientSpecifications.RetrieveAttendanceToCurrentDay(dayOfWeek);
    }
}
