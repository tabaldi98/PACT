using app.Tabaldi.PACT.Domain.Seedwork.Contracts.Repositories;
using System.Threading.Tasks;

namespace app.Tabaldi.PACT.Domain.ClientsModule.ClientAgg
{
    public interface IClientRepository :
        ICreateRepository<Client>,
        IRetrieveRepository<Client>,
        ISingleRepository<Client>,
        IAnyRepository<Client>,
        IUpdateRepository<Client>,
        IRetrieveMapper<Client>
    {
        Task DeleteAsync(int[] ids);
    }
}
