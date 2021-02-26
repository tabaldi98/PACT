using app.Tabaldi.PACT.Domain.Seedwork.Specification;
using System;
using System.Linq.Expressions;

namespace app.Tabaldi.PACT.Domain.Seedwork.Contracts.Mappers
{
    public interface IHaveMapper<TEntity, TResult> where TEntity : class
    {
        Expression<Func<TEntity, TResult>> Selector { get; }
        ISpecification<TEntity> Specification { get; }
    }
}
