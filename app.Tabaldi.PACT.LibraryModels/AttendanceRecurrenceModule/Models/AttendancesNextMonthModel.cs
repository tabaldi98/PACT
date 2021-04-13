using System;

namespace app.Tabaldi.PACT.LibraryModels.AttendanceRecurrenceModule.Models
{
    public class AttendancesNextMonthModel : IAttendancesModelBase
    {
        public string ClientName { get; set; }
        public string DayOfAttendance { get; set; }
        public DateTime DayOffWeekAttendance { get; set; }
        public DateTime StartAttendance { get; set; }
        public DateTime EndAttendance { get; set; }
    }
}
