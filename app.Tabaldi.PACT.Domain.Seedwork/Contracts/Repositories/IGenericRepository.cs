using app.Tabaldi.PACT.Domain.Seedwork.Contracts.Mappers;
using app.Tabaldi.PACT.Domain.Seedwork.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace app.Tabaldi.PACT.Domain.Seedwork.Contracts.Repositories
{
    public interface ICreateRepository<TEntity> : IDisposable where TEntity : class
    {
        TEntity Create(TEntity entity);
    }

    public interface IAnyRepository<TEntity> : IDisposable where TEntity : class
    {
        Task<bool> AnyAsync(ISpecification<TEntity> specification);
    }

    public interface ICountRepository<TEntity> : IDisposable where TEntity : class
    {
        Task<int> CountAsync(ISpecification<TEntity> specification);
        Task<int> CountAsync();
    }

    public interface IRetrieveByIDRepository<TEntity, KeyType> : IDisposable where TEntity : class
    {
        Task<TEntity> RetrieveByIDAsync(KeyType id);
    }

    public interface IRetrieveRepository<TEntity> : IDisposable where TEntity : class
    {
        Task<List<TEntity>> RetrieveAsync(ISpecification<TEntity> specification = null, bool autoDetectChangesEnabled = false, params Expression<Func<TEntity, object>>[] includeExpressions);
    }

    public interface ISingleRepository<TEntity> : IDisposable where TEntity : class
    {
        Task<TResult> SingleOrDefaultAsync<TResult>(IHaveMapper<TEntity, TResult> mapper);
        Task<TEntity> SingleOrDefaultAsync(ISpecification<TEntity> specification, bool autoDetectChangesEnabled = false, params Expression<Func<TEntity, object>>[] includeExpressions);
    }

    public interface IRetrieveODataRepository<TEntity> : IDisposable where TEntity : class
    {
        IQueryable<TResult> RetrieveOData<TResult>(IHaveMapper<TEntity, TResult> mapper);
    }

    public interface IUpdateRepository<in TEntity> : IDisposable where TEntity : class
    {
        void Update(TEntity entity);
    }

    public interface IUpdatePropertyRepository<TEntity> : IDisposable where TEntity : class
    {
        void Update<TProperty>(TEntity entity, Expression<Func<TEntity, TProperty>> property);
    }

    public interface IDeleteRepository<in KeyType> : IDisposable
    {
        Task DeleteAsync(KeyType id);
    }

    public interface IRetrieveMapper<TEntity> : IDisposable where TEntity : class
    {
        IQueryable<TResult> RetrieveMapper<TResult>(IHaveMapper<TEntity, TResult> mapper);
    }
}
