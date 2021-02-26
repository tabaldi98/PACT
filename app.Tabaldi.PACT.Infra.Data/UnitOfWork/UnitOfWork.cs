using app.Tabaldi.PACT.Domain.Seedwork.Contracts.UnitOfWork;
using app.Tabaldi.PACT.Infra.Data.Context;
using System.Threading.Tasks;

namespace app.Tabaldi.PACT.Infra.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDatabaseContext _context;
        private bool _disposed;

        public UnitOfWork(IDatabaseContext context)
        {
            _context = context;
        }

        public Task<int> CommitAsync()
        {
            return _context.SaveChangesAsync();
        }

        #region IDisposable Support

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
            {
                _context.Dispose();
            }

            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
        }

        #endregion IDisposable Support
    }
}
