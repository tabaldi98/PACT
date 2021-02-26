using System;
using System.Linq.Expressions;

namespace app.Tabaldi.PACT.Domain.Seedwork.Specification
{
    public interface ISpecification<TEntity> where TEntity : class
    {
        Expression<Func<TEntity, bool>> SatisfiedBy();
    }
}