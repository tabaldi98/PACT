namespace app.Tabaldi.PACT.LibraryModels.ReportsModule.Models
{
    public class ReportsFinancialModel
    {
        public int ClientID { get; set; }
        public string ClientName { get; set; }
        public decimal TotalValue { get; set; }
        public int TotalAttendances { get; set; }
    }
}
