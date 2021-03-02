using System;

namespace app.Tabaldi.PACT.Domain.AttendanceModule.AttendanceRecurrenceAgg.Commands
{
    public class AttendanceRecurrenceCommand
    {
        public WeekDay WeekDay { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
