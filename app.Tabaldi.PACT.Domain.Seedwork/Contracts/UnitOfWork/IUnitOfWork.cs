using System;
using System.Threading.Tasks;

namespace app.Tabaldi.PACT.Domain.Seedwork.Contracts.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> CommitAsync();
    }
}
