using System;

namespace app.Tabaldi.PACT.Domain.AttendanceModule.AttendanceRecurrenceAgg.Models
{
    public class AttendancesCurrentMonthModel : IAttendancesModelBase
    {
        public string ClientName { get; set; }
        public string DayOfAttendance { get; set; }
        public DateTime DayOffWeekAttendance { get; set; }
        public DateTime StartAttendance { get; set; }
        public DateTime EndAttendance { get; set; }
    }
}
