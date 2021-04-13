using System;

namespace app.Tabaldi.PACT.LibraryModels.AttendanceModule.Commands
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
