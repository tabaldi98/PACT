using System;

namespace app.Tabaldi.PACT.LibraryModels.ReportsModule.Commands
{
    public class ReportsDefaultFilterCommand
    {
        public DateTime InitialDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
