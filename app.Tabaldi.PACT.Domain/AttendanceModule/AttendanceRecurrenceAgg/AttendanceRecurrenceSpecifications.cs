using app.Tabaldi.PACT.Domain.Seedwork.Specification;
using app.Tabaldi.PACT.LibraryModels.AttendanceRecurrenceModule.Enums;
using System;

namespace app.Tabaldi.PACT.Domain.AttendanceModule.AttendanceRecurrenceAgg
{
    public static class AttendanceRecurrenceSpecifications
    {
        public static ISpecification<AttendanceRecurrence> RetrieveAttendanceToCurrentDay(int userId)
        {
            var weekDay = (WeekDay)DateTime.Now.DayOfWeek;

            return new DirectSpecification<AttendanceRecurrence>(p => p.Client.UserID == userId && p.WeekDay == weekDay);
        }

        public static ISpecification<AttendanceRecurrence> RetrieveAttendanceByWeekDay(int userId, DayOfWeek weekDay)
        {
            return new DirectSpecification<AttendanceRecurrence>(p => p.Client.UserID == userId && p.WeekDay == (WeekDay)weekDay);
        }

        public static ISpecification<AttendanceRecurrence> RetrieveByUserID(int userId)
        {
            return new DirectSpecification<AttendanceRecurrence>(p => p.Client.UserID == userId);
        }
    }
}
