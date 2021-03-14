using app.Tabaldi.PACT.Domain.AttendanceModule.AttendanceRecurrenceAgg;
using app.Tabaldi.PACT.Domain.Seedwork.Contracts.Mappers;
using app.Tabaldi.PACT.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace app.Tabaldi.PACT.Infra.Data.Repositories.AttendanceRecurrenceAgg
{
    public class AttendanceRecurrenceRepository : GenericRepositoryBase<AttendanceRecurrence, int>, IAttendanceRecurrenceRepository
    {
        public AttendanceRecurrenceRepository(IDatabaseContext context)
            : base(context)
        { }

        public async Task DeleteAsync(int[] ids)
        {
            var list = await GenericRepository.Context.Set<AttendanceRecurrence>().Where(p => ids.Contains(p.ClientID)).ToListAsync();

            GenericRepository.Context.Set<AttendanceRecurrence>().RemoveRange(list.ToArray());
        }

        public IQueryable<TResult> RetrieveMapper<TResult>(IHaveMapper<AttendanceRecurrence, TResult> mapper)
        {
            return GenericRepository.RetrieveMapper(mapper);
        }
    }
}
