using app.Tabaldi.PACT.Application.ReportsModule.Models;
using app.Tabaldi.PACT.Domain.AttendanceModule.AttendanceAgg;
using app.Tabaldi.PACT.LibraryModels.ReportsModule.Commands;
using app.Tabaldi.PACT.LibraryModels.ReportsModule.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace app.Tabaldi.PACT.Application.ReportsModule
{
    public interface IReportsAppService
    {
        Task<IEnumerable<ReportsFinancialModel>> RetrieveFinancialReportAsync(ReportsDefaultFilterCommand command);
        IQueryable<ReportsAttendancesModel> RetrieveAttendanceReport(ReportClientIDFilterCommand command);
    }

    public class ReportsAppService : AppServiceBase<IAttendanceRepository>, IReportsAppService
    {
        public ReportsAppService(IAttendanceRepository repository)
            : base(repository)
        { }

        public IQueryable<ReportsAttendancesModel> RetrieveAttendanceReport(ReportClientIDFilterCommand command)
        {
            return Repository.RetrieveMapper(new ReportsAttendancesModelMapper(AttendanceSpecifications.RetrieveByClientID(command.ClientID)));
        }

        public async Task<IEnumerable<ReportsFinancialModel>> RetrieveFinancialReportAsync(ReportsDefaultFilterCommand command)
        {
            var attendances = await Repository.RetrieveAsync(AttendanceSpecifications.RetrieveByDate(command.InitialDate, command.EndDate), false, p => p.Client);

            return attendances.GroupBy(p => p.Client).Select(p => new ReportsFinancialModel()
            {
                ClientID = p.Key.ID,
                ClientName = p.Key.Name,
                TotalAttendances = attendances.Where(x => x.ClientID == p.Key.ID).Count(),
                TotalValue = p.Key.ChargingType == ChargingType.Day
                    ? attendances.Where(x => x.ClientID == p.Key.ID).Sum(p => p.Client.Value)
                    : p.Key.Value,
            });
        }
    }
}
