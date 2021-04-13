using System;

namespace app.Tabaldi.PACT.LibraryModels.AttendanceRecurrenceModule.Models
{
    public class AttendancesCurrentMonthModel : IAttendancesModelBase
    {
        public string ClientName { get; set; }
        public DateTime DayOfAttendance { get; set; }
        public string DayOffWeekAttendance { get; set; }
        public DateTime StartAttendance { get; set; }
        public DateTime EndAttendance { get; set; }
    }
}
