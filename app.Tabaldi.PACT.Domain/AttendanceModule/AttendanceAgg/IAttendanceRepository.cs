using app.Tabaldi.PACT.Domain.Seedwork.Contracts.Repositories;
using System.Threading.Tasks;

namespace app.Tabaldi.PACT.Domain.AttendanceModule.AttendanceAgg
{
    public interface IAttendanceRepository :
        ICreateRepository<Attendance>,
        IRetrieveMapper<Attendance>,
        ISingleRepository<Attendance>,
        IRetrieveRepository<Attendance>
    {
        Task DeleteAsync(int[] ids);
    }
}
