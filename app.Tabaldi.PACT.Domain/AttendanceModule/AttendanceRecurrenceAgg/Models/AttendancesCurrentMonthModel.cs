using app.Tabaldi.PACT.Domain.Seedwork.Contracts.Mappers;
using app.Tabaldi.PACT.Domain.Seedwork.Specification;
using System;
using System.Linq.Expressions;

namespace app.Tabaldi.PACT.Domain.AttendanceModule.AttendanceRecurrenceAgg.Models
{
    public class AttendancesCurrentMonthModel : IAttendancesModelBase
    {
        public string ClientName { get; set; }
        public DateTime DayOfAttendance { get; set; }
        public string DayOffWeekAttendance { get; set; }
        public DateTime StartAttendance { get; set; }
        public DateTime EndAttendance { get; set; }
    }

    public class AttendancesCurrentMonthModelMapper : IHaveMapper<AttendanceRecurrence, AttendancesCurrentMonthModel>
    {
        public Expression<Func<AttendanceRecurrence, AttendancesCurrentMonthModel>> Selector => p => new AttendancesCurrentMonthModel()
        {
            ClientName = p.Client.Name,
            DayOffWeekAttendance = 
                p.WeekDay == WeekDay.Monday ? "Segunda-feira"
                : p.WeekDay == WeekDay.Tuesday ? "Terça-feira"
                : p.WeekDay == WeekDay.Wednesday ? "Quarta-feira"
                : p.WeekDay == WeekDay.Thursday ? "Quinta-feira"
                : p.WeekDay == WeekDay.Friday ? "Sexta-feira"
                : p.WeekDay == WeekDay.Saturday ? "Sábado"
                : p.WeekDay == WeekDay.Sunday ? "Sábado"
                : "",                
            StartAttendance = p.StartTime,
            EndAttendance = p.EndTime,
        };

        public ISpecification<AttendanceRecurrence> Specification => null;

    }
}
