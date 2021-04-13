using System;

namespace app.Tabaldi.PACT.LibraryModels.AttendanceRecurrenceModule.Models
{
    public class AttendancesCurrentWeekModel : IAttendancesModelBase
    {
        public int ClientID { get; set; }
        public string ClientName { get; set; }
        public string DayOfAttendance { get; set; }
        public DateTime StartAttendance { get; set; }
        public DateTime EndAttendance { get; set; }
    }
}
