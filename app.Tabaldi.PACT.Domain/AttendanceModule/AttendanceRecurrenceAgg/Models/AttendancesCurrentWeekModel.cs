using app.Tabaldi.PACT.Domain.Seedwork.Contracts.Mappers;
using app.Tabaldi.PACT.Domain.Seedwork.Specification;
using System;
using System.Linq.Expressions;

namespace app.Tabaldi.PACT.Domain.AttendanceModule.AttendanceRecurrenceAgg.Models
{
    public class AttendancesCurrentWeekModel : IAttendancesModelBase
    {
        public int ClientID { get; set; }
        public string ClientName { get; set; }
        public string DayOfAttendance { get; set; }
        public DateTime StartAttendance { get; set; }
        public DateTime EndAttendance { get; set; }
    }

    public class AttendancesCurrentWeekModelMapper : IHaveMapper<AttendanceRecurrence, AttendancesCurrentWeekModel>
    {
        public AttendancesCurrentWeekModelMapper(int userId)
        {
            Specification = AttendanceRecurrenceSpecifications.RetrieveByUserID(userId);
        }

        public Expression<Func<AttendanceRecurrence, AttendancesCurrentWeekModel>> Selector => p => new AttendancesCurrentWeekModel()
        {
            ClientID = p.Client.ID,
            ClientName = p.Client.Name,
            StartAttendance = p.StartTime,
            EndAttendance = p.EndTime,
            DayOfAttendance =
                p.WeekDay == WeekDay.Monday ? "Segunda-feira" :
                p.WeekDay == WeekDay.Tuesday ? "Terça-feira" :
                p.WeekDay == WeekDay.Wednesday ? "Quarta-feira" :
                p.WeekDay == WeekDay.Thursday ? "Quinta-feira" :
                p.WeekDay == WeekDay.Friday ? "Sexta-feira" :
                p.WeekDay == WeekDay.Saturday ? "Sábado" :
                p.WeekDay == WeekDay.Sunday ? "Domingo"
            : "-"
        };

        public ISpecification<AttendanceRecurrence> Specification { get; }
    }
}
