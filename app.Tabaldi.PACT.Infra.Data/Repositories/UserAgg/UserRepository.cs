using app.Tabaldi.PACT.Domain.Seedwork.Contracts.Mappers;
using app.Tabaldi.PACT.Domain.Seedwork.Specification;
using app.Tabaldi.PACT.Domain.UsersModule.UserAgg;
using app.Tabaldi.PACT.Infra.Data.Context;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace app.Tabaldi.PACT.Infra.Data.Repositories.UserAgg
{
    public class UserRepository : GenericRepositoryBase<User, int>, IUserRepository
    {
        public UserRepository(IDatabaseContext context)
            : base(context)
        { }

        public Task<TResult> SingleOrDefaultAsync<TResult>(IHaveMapper<User, TResult> mapper)
        {
            return GenericRepository.SingleOrDefaultAsync(mapper);
        }

        public Task<User> SingleOrDefaultAsync(ISpecification<User> specification, bool autoDetectChangesEnabled = false, params Expression<Func<User, object>>[] includeExpressions)
        {
            return GenericRepository.SingleOrDefaultAsync(specification, autoDetectChangesEnabled, includeExpressions);
        }
    }
}
