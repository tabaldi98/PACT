using app.Tabaldi.PACT.Domain.AttendanceModule.AttendanceRecurrenceAgg;
using app.Tabaldi.PACT.Domain.Seedwork.Contracts.Mappers;
using app.Tabaldi.PACT.Domain.Seedwork.Specification;
using app.Tabaldi.PACT.LibraryModels.AttendanceRecurrenceModule.Enums;
using app.Tabaldi.PACT.LibraryModels.AttendanceRecurrenceModule.Models;
using System;
using System.Linq.Expressions;

namespace app.Tabaldi.PACT.Application.AttendanceRecurrenceAgg.Models
{
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
