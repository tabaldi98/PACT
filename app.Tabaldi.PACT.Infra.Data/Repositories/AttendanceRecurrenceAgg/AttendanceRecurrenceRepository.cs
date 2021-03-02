using app.Tabaldi.PACT.Domain.AttendanceModule.AttendanceRecurrenceAgg;
using app.Tabaldi.PACT.Domain.Seedwork.Contracts.Mappers;
using app.Tabaldi.PACT.Infra.Data.Context;
using System.Linq;

namespace app.Tabaldi.PACT.Infra.Data.Repositories.AttendanceRecurrenceAgg
{
    public class AttendanceRecurrenceRepository : GenericRepositoryBase<AttendanceRecurrence, int>, IAttendanceRecurrenceRepository
    {
        public AttendanceRecurrenceRepository(IDatabaseContext context)
            : base(context)
        { }

        public IQueryable<TResult> RetrieveMapper<TResult>(IHaveMapper<AttendanceRecurrence, TResult> mapper)
        {
            return GenericRepository.RetrieveMapper(mapper);
        }
    }
}
