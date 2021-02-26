using System;

namespace app.Tabaldi.PACT.Domain.AttendanceModule.AttendanceAgg.Commands
{
    public class AttendanceAddCommand
    {
        public int ClientID { get; set; }
        public DateTime Date { get; set; }
        public DateTime HourInitial { get; set; }
        public DateTime HourFinish { get; set; }
        public string Description { get; set; }
    }
}
