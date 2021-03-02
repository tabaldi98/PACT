using app.Tabaldi.PACT.Domain.Seedwork.Specification;
using System;

namespace app.Tabaldi.PACT.Domain.AttendanceModule.AttendanceRecurrenceAgg
{
    public static class AttendanceRecurrenceSpecifications
    {
        public static ISpecification<AttendanceRecurrence> RetrieveAttendanceToCurrentDay()
        {
            var weekDay = (WeekDay)DateTime.Now.DayOfWeek;

            return new DirectSpecification<AttendanceRecurrence>(p => p.WeekDay == weekDay);
        }
    }
}
