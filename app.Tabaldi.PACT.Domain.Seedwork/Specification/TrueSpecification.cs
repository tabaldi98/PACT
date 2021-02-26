using System;
using System.Linq.Expressions;

namespace app.Tabaldi.PACT.Domain.Seedwork.Specification
{
    public sealed class TrueSpecification<TEntity> : Specification<TEntity> where TEntity : class
    {
        public override Expression<Func<TEntity, bool>> SatisfiedBy()
        {
            var result = true;
            Expression<Func<TEntity, bool>> trueExpression = t => result;
            return trueExpression;
        }
    }
}