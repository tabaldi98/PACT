using app.Tabaldi.PACT.Domain.AttendanceModule.AttendanceAgg.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace app.Tabaldi.PACT.Infra.Data.HttpClient.AttendanceRecurrenceAgg
{
    public interface IAttendanceRecurrenceRepository
    {
        Task<IList<T>> RetrieveByTypeAsync<T>(ViewPeriodType periodType);
    }

    public class AttendanceRecurrenceRepository : GenericRepositoryBase, IAttendanceRecurrenceRepository
    {
        private readonly string _attendanceBaseAddress = "api/attendance-recurrence";

        public AttendanceRecurrenceRepository()
            : base()
        { }

        public async Task<IList<T>> RetrieveByTypeAsync<T>(ViewPeriodType periodType)
        {
            var response = await HttpClient.GetAsync($"{_attendanceBaseAddress}/{periodType.GetHashCode()}/by-type");

            return await response.Content.ReadAsAsync<IList<T>>();
        }
    }
}
