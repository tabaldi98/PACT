using System.Threading.Tasks;

namespace app.Tabaldi.PACT.Infra.Data.HttpClient.PublicModule
{
    public interface IPublicRepository
    {
        Task<bool> IsAliveAsync();
    }

    public class PublicRepository : GenericRepositoryBase, IPublicRepository
    {
        private readonly string _publicBaseAddress = "api/public";
        public PublicRepository()
            : base()
        { }

        public async Task<bool> IsAliveAsync()
        {
            var response = await HttpClient.GetAsync($"{_publicBaseAddress}/is-alive", false);

            return response.IsSuccessStatusCode;
        }
    }
}
