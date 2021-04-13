using app.Tabaldi.PACT.LibraryModels.AttendanceRecurrenceModule.Enums;
using System;

namespace app.Tabaldi.PACT.LibraryModels.AttendanceRecurrenceModule.Models
{
    public class AttendanceRecurrenceModel
    {
        public int ID { get; set; }
        public WeekDay WeekDay { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
