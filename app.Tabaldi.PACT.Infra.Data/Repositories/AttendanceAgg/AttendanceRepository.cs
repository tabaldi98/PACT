using app.Tabaldi.PACT.Domain.AttendanceModule.AttendanceAgg;
using app.Tabaldi.PACT.Domain.Seedwork.Contracts.Mappers;
using app.Tabaldi.PACT.Domain.Seedwork.Specification;
using app.Tabaldi.PACT.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace app.Tabaldi.PACT.Infra.Data.Repositories.AttendanceAgg
{
    public class AttendanceRepository : GenericRepositoryBase<Attendance, int>, IAttendanceRepository
    {
        public AttendanceRepository(IDatabaseContext context)
            : base(context)
        { }

        public Attendance Create(Attendance entity)
        {
            return GenericRepository.Create(entity);
        }

        public IQueryable<TResult> RetrieveMapper<TResult>(IHaveMapper<Attendance, TResult> mapper)
        {
            return GenericRepository.RetrieveMapper(mapper);
        }

        public async Task DeleteAsync(int[] ids)
        {
            var list = await GenericRepository.Context.Set<Attendance>().Where(p => ids.Contains(p.ID)).ToListAsync();

            GenericRepository.Context.Set<Attendance>().RemoveRange(list.ToArray());
        }

        public Task<TResult> SingleOrDefaultAsync<TResult>(IHaveMapper<Attendance, TResult> mapper)
        {
            return GenericRepository.SingleOrDefaultAsync(mapper);
        }

        public Task<Attendance> SingleOrDefaultAsync(ISpecification<Attendance> specification, bool autoDetectChangesEnabled = false, params Expression<Func<Attendance, object>>[] includeExpressions)
        {
            return GenericRepository.SingleOrDefaultAsync(specification, autoDetectChangesEnabled, includeExpressions);
        }

        public Task<List<Attendance>> RetrieveAsync(ISpecification<Attendance> specification = null, bool autoDetectChangesEnabled = false, params Expression<Func<Attendance, object>>[] includeExpressions)
        {
            return GenericRepository.RetrieveAsync(specification, autoDetectChangesEnabled, includeExpressions);
        }

        public Task<bool> AnyAsync(ISpecification<Attendance> specification)
        {
            return GenericRepository.AnyAsync(specification);
        }
    }
}
