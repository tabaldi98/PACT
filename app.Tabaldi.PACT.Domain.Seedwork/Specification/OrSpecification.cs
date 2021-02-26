using System;
using System.Linq.Expressions;

namespace app.Tabaldi.PACT.Domain.Seedwork.Specification
{
    public sealed class OrSpecification<TEntity> : LogicalSpecificationBase<TEntity> where TEntity : class
    {
        public OrSpecification(ISpecification<TEntity> leftSide, ISpecification<TEntity> rightSide)
            : base(leftSide, rightSide)
        { }

        public override Expression<Func<TEntity, bool>> SatisfiedBy()
        {
            var left = _leftSideSpecification.SatisfiedBy();
            var right = _rightSideSpecification.SatisfiedBy();

            return (left.Or(right));
        }
    }
}