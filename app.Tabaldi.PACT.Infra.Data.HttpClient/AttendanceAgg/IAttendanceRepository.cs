using app.Tabaldi.PACT.LibraryModels.AttendanceModule.Commands;
using app.Tabaldi.PACT.LibraryModels.AttendanceModule.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace app.Tabaldi.PACT.Infra.Data.HttpClient.ClientAgg
{
    public interface IAttendanceRepository
    {
        Task<int> CreateAsync(AttendanceAddCommand command);
        Task<IList<AttendanceModel>> GetAllAsync(int clientId);
        Task<bool> UpdateAsync(AttendanceEditCommand command);
        Task<bool> DeleteAsync(AttendanceRemoveCommand command);
    }

    public class AttendanceRepository : GenericRepositoryBase, IAttendanceRepository
    {
        private readonly string _attendanceBaseAddress = "api/attendance";
        public AttendanceRepository()
            : base()
        { }

        public async Task<int> CreateAsync(AttendanceAddCommand command)
        {
            var response = await HttpClient.PostAsync(_attendanceBaseAddress, command);

            return await response.Content.ReadAsAsync<int>();
        }

        public async Task<IList<AttendanceModel>> GetAllAsync(int clientId)
        {
            var response = await HttpClient.GetAsync($"{_attendanceBaseAddress}?clientId={clientId}");

            return await response.Content.ReadAsAsync<IList<AttendanceModel>>();
        }

        public async Task<bool> DeleteAsync(AttendanceRemoveCommand command)
        {
            var response = await HttpClient.PostAsync($"{_attendanceBaseAddress}/remove", command);

            return await response.Content.ReadAsAsync<bool>();
        }

        public async Task<bool> UpdateAsync(AttendanceEditCommand command)
        {
            var response = await HttpClient.PostAsync($"{_attendanceBaseAddress}/edit", command);

            return await response.Content.ReadAsAsync<bool>();
        }
    }
}
