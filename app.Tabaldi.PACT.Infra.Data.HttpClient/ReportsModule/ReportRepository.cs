using app.Tabaldi.PACT.LibraryModels.ReportsModule.Commands;
using app.Tabaldi.PACT.LibraryModels.ReportsModule.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace app.Tabaldi.PACT.Infra.Data.HttpClient.ReportsModule
{
    public interface IReportRepository
    {
        Task<IEnumerable<ReportsFinancialModel>> RetrieveFinancialReportAsync(ReportsDefaultFilterCommand command);
        Task<IEnumerable<ReportsAttendancesModel>> RetrieveAttendanceReportAsync(ReportClientIDFilterCommand command);
    }

    public class ReportRepository : GenericRepositoryBase, IReportRepository
    {
        private readonly string _reportsBaseAddress = "api/reports";
        public ReportRepository()
            : base()
        { }

        public async Task<IEnumerable<ReportsAttendancesModel>> RetrieveAttendanceReportAsync(ReportClientIDFilterCommand command)
        {
            var response = await HttpClient.PostAsync($"{_reportsBaseAddress}/attendance", command);

            return await response.Content.ReadAsAsync<IEnumerable<ReportsAttendancesModel>>();
        }

        public async Task<IEnumerable<ReportsFinancialModel>> RetrieveFinancialReportAsync(ReportsDefaultFilterCommand command)
        {
            var response = await HttpClient.PostAsync($"{_reportsBaseAddress}/financial", command);

            return await response.Content.ReadAsAsync<IEnumerable<ReportsFinancialModel>>();
        }
    }
}
