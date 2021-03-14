using System;

namespace app.Tabaldi.PACT.LibraryModels.ReportsModule.Models
{
    public class ReportsAttendancesModel
    {
        public string ClientName { get; set; }
        public DateTime AttendanceDate { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Description { get; set; }
    }
}
