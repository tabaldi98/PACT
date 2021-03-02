using System;

namespace app.Tabaldi.PACT.Domain.AttendanceModule.AttendanceRecurrenceAgg.Models
{
    public class AttendanceRecurrenceModel
    {
        public int ID { get; set; }
        public WeekDay WeekDay { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
