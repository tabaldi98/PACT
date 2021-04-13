using app.Tabaldi.PACT.LibraryModels.AttendanceRecurrenceModule.Enums;
using System;

namespace app.Tabaldi.PACT.LibraryModels.AttendanceRecurrenceModule.Commands
{
    public class AttendanceRecurrenceCommand
    {
        public WeekDay WeekDay { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
