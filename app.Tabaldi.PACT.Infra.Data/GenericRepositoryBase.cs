using app.Tabaldi.PACT.Infra.Data.Context;
using System;

namespace app.Tabaldi.PACT.Infra.Data
{
    public abstract class GenericRepositoryBase<TEntity, KeyType> : IDisposable where TEntity : class
    {
        protected GenericRepository<TEntity, KeyType> GenericRepository;

        protected GenericRepositoryBase(IDatabaseContext context)
        {
            GenericRepository = new GenericRepository<TEntity, KeyType>(context);
        }

        public void Dispose()
        {
            GenericRepository.Dispose();
        }
    }
}
