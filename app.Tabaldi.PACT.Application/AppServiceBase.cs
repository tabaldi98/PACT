using app.Tabaldi.PACT.Domain.Seedwork.Contracts.UnitOfWork;
using System;
using System.Threading.Tasks;

namespace app.Tabaldi.PACT.Application
{
    public abstract class AppServiceBase<TRepository> : IDisposable
       where TRepository : IDisposable
    {
        private readonly IUnitOfWork _unitOfWork;
        protected readonly TRepository Repository;

        protected AppServiceBase(TRepository repository)
        {
            Repository = repository;
        }

        protected AppServiceBase(TRepository repository, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            Repository = repository;
        }

        public async Task<bool> CommitAsync()
        {
            return await _unitOfWork.CommitAsync() > 0;
        }

        public void Dispose()
        {
            if (_unitOfWork != null) { _unitOfWork.Dispose(); }
            Repository.Dispose();
        }
    }
}
