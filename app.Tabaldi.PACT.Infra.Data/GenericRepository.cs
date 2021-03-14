using app.Tabaldi.PACT.Domain.Seedwork.Contracts.Mappers;
using app.Tabaldi.PACT.Domain.Seedwork.Contracts.Repositories;
using app.Tabaldi.PACT.Domain.Seedwork.Specification;
using app.Tabaldi.PACT.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace app.Tabaldi.PACT.Infra.Data
{
    public sealed class GenericRepository<TEntity, KeyType> :
     ICreateRepository<TEntity>,
     IAnyRepository<TEntity>,
     ICountRepository<TEntity>,
     ISingleRepository<TEntity>,
     IRetrieveByIDRepository<TEntity, KeyType>,
     IRetrieveRepository<TEntity>,
     IRetrieveODataRepository<TEntity>,
     IRetrieveMapper<TEntity>,
     IUpdateRepository<TEntity>,
     IUpdatePropertyRepository<TEntity>,
     IDeleteRepository<KeyType> where TEntity : class
    {
        public IDatabaseContext Context { get; private set; }

        public GenericRepository(IDatabaseContext context)
        {
            Context = context;
        }

        public TEntity Create(TEntity entity)
        {
            return Context.Set<TEntity>().Add(entity).Entity;
        }

        public async Task<TEntity> CreateAsync(TEntity entity)
        {
            var entityEntry = await Context.Set<TEntity>().AddAsync(entity);
            return entityEntry.Entity;
        }

        public Task<bool> AnyAsync(ISpecification<TEntity> specification)
        {
            return Context.Set<TEntity>().AsNoTracking().AnyAsync(specification.SatisfiedBy());
        }

        public Task<int> CountAsync(ISpecification<TEntity> specification)
        {
            return Context.Set<TEntity>().AsNoTracking().CountAsync(specification.SatisfiedBy());
        }
        public Task<int> CountAsync()
        {
            return Context.Set<TEntity>().AsNoTracking().CountAsync();
        }

        public async Task<TEntity> RetrieveByIDAsync(KeyType key)
        {
            return await Context.Set<TEntity>().FindAsync(key);
        }

        public Task<TResult> SingleOrDefaultAsync<TResult>(IHaveMapper<TEntity, TResult> mapper)
        {
            return SingleOrDefaultAsync(mapper.Specification, mapper.Selector);
        }

        public Task<TEntity> SingleOrDefaultAsync(
            ISpecification<TEntity> specification,
            bool autoDetectChangesEnabled = false,
            params Expression<Func<TEntity, object>>[] includeExpressions)
        {
            return SingleOrDefaultAsync<TEntity>(specification, null, autoDetectChangesEnabled, includeExpressions);
        }

        private Task<TResult> SingleOrDefaultAsync<TResult>(
            ISpecification<TEntity> specification = null,
            Expression<Func<TEntity, TResult>> selector = null,
            bool autoDetectChangesEnabled = false,
            params Expression<Func<TEntity, object>>[] includeExpressions)
        {
            var entities = autoDetectChangesEnabled ? Context.Set<TEntity>() : Context.Set<TEntity>().AsNoTracking();

            if (includeExpressions.Any())
            {
                entities = includeExpressions
                    .Aggregate<Expression<Func<TEntity, object>>, IQueryable<TEntity>>
                    (Context.Set<TEntity>(), (current, expression) => current.Include(expression));
            }

            var specifiedEntities = specification == null ? entities : entities.Where(specification.SatisfiedBy());

            return selector == null ? specifiedEntities.Cast<TResult>().SingleOrDefaultAsync() : specifiedEntities.Select(selector).SingleOrDefaultAsync();
        }

        public IEnumerable<TEntity> Retrieve(ISpecification<TEntity> specification = null, bool autoDetectChangesEnabled = false, params Expression<Func<TEntity, object>>[] includeExpressions)
        {
            var entities = autoDetectChangesEnabled ? Context.Set<TEntity>() : Context.Set<TEntity>().AsNoTracking();

            if (includeExpressions.Any())
            {
                entities = includeExpressions
                    .Aggregate<Expression<Func<TEntity, object>>, IQueryable<TEntity>>
                    (Context.Set<TEntity>(), (current, expression) => current.Include(expression));
            }

            return specification == null ? entities : entities.Where(specification.SatisfiedBy());
        }

        public Task<List<TEntity>> RetrieveAsync(ISpecification<TEntity> specification = null, bool autoDetectChangesEnabled = false, params Expression<Func<TEntity, object>>[] includeExpressions)
        {
            var entities = autoDetectChangesEnabled ? Context.Set<TEntity>() : Context.Set<TEntity>().AsNoTracking();

            if (includeExpressions.Any())
            {
                entities = includeExpressions
                    .Aggregate<Expression<Func<TEntity, object>>, IQueryable<TEntity>>
                    (Context.Set<TEntity>(), (current, expression) => current.Include(expression));
            }

            return specification == null ? entities.ToListAsync() : entities.Where(specification.SatisfiedBy()).ToListAsync();
        }

        public IQueryable<TResult> RetrieveOData<TResult>(IHaveMapper<TEntity, TResult> mapper)
        {
            var entities = Context.Set<TEntity>().AsNoTracking();

            var specifiedEntities = mapper.Specification == null ? entities : entities.Where(mapper.Specification.SatisfiedBy());

            return specifiedEntities.Select(mapper.Selector);
        }

        public IQueryable<TResult> RetrieveMapper<TResult>(IHaveMapper<TEntity, TResult> mapper)
        {
            var entities = Context.Set<TEntity>().AsNoTracking();

            var specifiedEntities = mapper.Specification == null ? entities : entities.Where(mapper.Specification.SatisfiedBy());

            return specifiedEntities.Select(mapper.Selector);
        }

        public void Update(TEntity entity)
        {
            if (Context.Entry(entity) != null)
            {
                Context.Entry(entity).State = EntityState.Modified;
            }
        }

        public void Update<TProperty>(TEntity entity, Expression<Func<TEntity, TProperty>> property)
        {
            if (Context.Entry(entity) != null)
            {
                Context.Set<TEntity>().Attach(entity);
                Context.Entry(entity).Property(property).IsModified = true;
            }
        }

        public async Task DeleteAsync(KeyType key)
        {
            var entities = Context.Set<TEntity>();
            var entity = await entities.FindAsync(key);
            entities.Remove(entity);
        }

        public void Dispose()
        {
           // Context.Dispose();
        }
    }

}
