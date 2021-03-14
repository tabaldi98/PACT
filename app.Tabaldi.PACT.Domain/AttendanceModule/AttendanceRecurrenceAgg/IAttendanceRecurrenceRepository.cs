using app.Tabaldi.PACT.Domain.Seedwork.Contracts.Repositories;
using System.Threading.Tasks;

namespace app.Tabaldi.PACT.Domain.AttendanceModule.AttendanceRecurrenceAgg
{
    public interface IAttendanceRecurrenceRepository :
        IRetrieveMapper<AttendanceRecurrence>
    {
        Task DeleteAsync(int[] ids);
    }
}
