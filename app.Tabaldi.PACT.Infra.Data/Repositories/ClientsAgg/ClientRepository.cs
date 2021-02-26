using app.Tabaldi.PACT.Domain.ClientsModule.ClientAgg;
using app.Tabaldi.PACT.Domain.Seedwork.Contracts.Mappers;
using app.Tabaldi.PACT.Domain.Seedwork.Specification;
using app.Tabaldi.PACT.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace app.Tabaldi.PACT.Infra.Data.Repositories.ClientsAgg
{
    public class ClientRepository : GenericRepositoryBase<Client, int>, IClientRepository
    {
        public ClientRepository(IDatabaseContext context)
            : base(context)
        { }

        public Task<bool> AnyAsync(ISpecification<Client> specification)
        {
            return GenericRepository.AnyAsync(specification);
        }

        public Client Create(Client entity)
        {
            return GenericRepository.Create(entity);
        }

        public async Task DeleteAsync(int[] ids)
        {
            var list = await GenericRepository.Context.Set<Client>().Where(p => ids.Contains(p.ID)).ToListAsync();

            GenericRepository.Context.Set<Client>().RemoveRange(list.ToArray());
        }

        public IQueryable<TResult> RetrieveMapper<TResult>(IHaveMapper<Client, TResult> mapper)
        {
            return GenericRepository.RetrieveMapper(mapper);
        }

        public Task<TResult> SingleOrDefaultAsync<TResult>(IHaveMapper<Client, TResult> mapper)
        {
            return GenericRepository.SingleOrDefaultAsync(mapper);
        }

        public Task<Client> SingleOrDefaultAsync(ISpecification<Client> specification, bool autoDetectChangesEnabled = false, params Expression<Func<Client, object>>[] includeExpressions)
        {
            return GenericRepository.SingleOrDefaultAsync(specification, autoDetectChangesEnabled, includeExpressions);
        }

        public void Update(Client entity)
        {
            GenericRepository.Update(entity);
        }
    }
}
