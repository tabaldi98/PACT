using System;

namespace app.Tabaldi.PACT.Domain.Seedwork.Specification
{
    public abstract class LogicalSpecificationBase<TEntity> : CompositeSpecification<TEntity> where TEntity : class
    {
        protected readonly ISpecification<TEntity> _rightSideSpecification = null;
        protected readonly ISpecification<TEntity> _leftSideSpecification = null;

        protected LogicalSpecificationBase(ISpecification<TEntity> leftSide, ISpecification<TEntity> rightSide)
        {
            _leftSideSpecification = leftSide ?? throw new ArgumentNullException("leftSide");
            _rightSideSpecification = rightSide ?? throw new ArgumentNullException("rightSide");
        }

        public override ISpecification<TEntity> LeftSideSpecification => _leftSideSpecification;

        public override ISpecification<TEntity> RightSideSpecification => _rightSideSpecification;
    }
}