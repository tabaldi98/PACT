using System;

namespace app.Tabaldi.PACT.LibraryModels.ReportsModule.Commands
{
    public class ReportClientIDFilterCommand
    {
        public int ClientID { get; set; }
        public bool UseFilter { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
