using System;
using System.Linq;
using System.Linq.Expressions;

namespace app.Tabaldi.PACT.Domain.Seedwork.Specification
{
    public sealed class NotSpecification<TEntity> : Specification<TEntity> where TEntity : class
    {
        private readonly Expression<Func<TEntity, bool>> _originalCriteria;

        public override Expression<Func<TEntity, bool>> SatisfiedBy()
        {
            return Expression.Lambda<Func<TEntity, bool>>(Expression.Not(_originalCriteria.Body), _originalCriteria.Parameters.Single());
        }

        public NotSpecification(ISpecification<TEntity> originalSpecification)
        {
            if (originalSpecification == null) { throw new ArgumentNullException(nameof(originalSpecification)); }

            _originalCriteria = originalSpecification.SatisfiedBy();
        }

        public NotSpecification(Expression<Func<TEntity, bool>> originalSpecification)
        {
            _originalCriteria = originalSpecification ?? throw new ArgumentNullException(nameof(originalSpecification));
        }
    }
}