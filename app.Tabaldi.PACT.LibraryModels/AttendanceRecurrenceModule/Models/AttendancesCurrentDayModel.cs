using System;

namespace app.Tabaldi.PACT.LibraryModels.AttendanceRecurrenceModule.Models
{
    public class AttendancesCurrentDayModel : IAttendancesModelBase
    {
        public int ClientID { get; set; }
        public string ClientName { get; set; }
        public DateTime StartAttendance { get; set; }
        public DateTime EndAttendance { get; set; }
    }
}
