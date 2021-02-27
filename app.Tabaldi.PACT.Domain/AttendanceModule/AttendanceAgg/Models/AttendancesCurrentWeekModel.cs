using System;
using System.Linq.Expressions;
using app.Tabaldi.PACT.Domain.ClientsModule.ClientAgg;
using app.Tabaldi.PACT.Domain.Seedwork.Contracts.Mappers;
using app.Tabaldi.PACT.Domain.Seedwork.Specification;

namespace app.Tabaldi.PACT.Domain.AttendanceModule.AttendanceAgg.Models
{
    public class AttendancesCurrentWeekModel : IAttendancesModelBase
    {
        public string ClientName { get; set; }
        public string DayOfAttendance { get; set; }
        public DateTime StartAttendance { get; set; }
        public DateTime EndAttendance { get; set; }
    }

    public class AttendancesCurrentWeekModelMapper : IHaveMapper<Client, AttendancesCurrentWeekModel>
    {
        public AttendancesCurrentWeekModelMapper()
        { }

        public Expression<Func<Client, AttendancesCurrentWeekModel>> Selector => p => new AttendancesCurrentWeekModel()
        {
            ClientName = p.Name,
            StartAttendance =
                    p.HasServiceOnSunday ? p.StartSunday.Value
                    : p.HasServiceOnMonday ? p.StartMonday.Value
                    : p.HasServiceOnTuesday ? p.StartThursday.Value
                    : p.HasServiceOnWednesday ? p.StartWednesday.Value
                    : p.HasServiceOnThursday ? p.StartThursday.Value
                    : p.HasServiceOnFriday ? p.StartFriday.Value
                    : p.HasServiceOnSaturday ? p.StartSaturday.Value
                    : p.StartMonday.Value,
            EndAttendance =
                     p.HasServiceOnSunday ? p.EndSunday.Value
                    : p.HasServiceOnMonday ? p.EndMonday.Value
                    : p.HasServiceOnTuesday ? p.EndThursday.Value
                    : p.HasServiceOnWednesday ? p.EndWednesday.Value
                    : p.HasServiceOnThursday ? p.EndThursday.Value
                    : p.HasServiceOnFriday ? p.EndFriday.Value
                    : p.HasServiceOnSaturday ? p.EndSaturday.Value
                    : p.StartMonday.Value,
            DayOfAttendance =
                     p.HasServiceOnSunday ? "Domingo"
                    : p.HasServiceOnMonday ? "Segunda-feira"
                    : p.HasServiceOnTuesday ? "Terça-feira"
                    : p.HasServiceOnWednesday ? "Quarta-feira"
                    : p.HasServiceOnThursday ? "Quinta-feira"
                    : p.HasServiceOnFriday ? "Sexta-feira"
                    : p.HasServiceOnSaturday ? "Sábado"
                    : "-",
        };

        public ISpecification<Client> Specification => null;
    }
}
