using app.Tabaldi.PACT.LibraryModels.ClientsModule.Commands;
using app.Tabaldi.PACT.LibraryModels.ClientsModule.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace app.Tabaldi.PACT.Infra.Data.HttpClient.ClientAgg
{
    public interface IClientClientRepository
    {
        Task<int> CreateAsync(ClientAddCommand command);
        Task<IList<ClientModel>> GetAllAsync();
        Task<ClientModel> GetByIdAsync(int clientId);
        Task<bool> UpdateAsync(ClientEditCommand command);
        Task<bool> DeleteAsync(ClientRemoveCommand command);
    }

    public class ClientClientRepository : GenericRepositoryBase, IClientClientRepository
    {
        private readonly string _clientBaseAddress = "api/client";
        public ClientClientRepository()
            : base()
        { }

        public async Task<int> CreateAsync(ClientAddCommand command)
        {
            var response = await HttpClient.PostAsync(_clientBaseAddress, command);

            return await response.Content.ReadAsAsync<int>();
        }

        public async Task<bool> DeleteAsync(ClientRemoveCommand command)
        {
            var response = await HttpClient.PostAsync($"{_clientBaseAddress}/remove", command);

            return await response.Content.ReadAsAsync<bool>();
        }

        public async Task<IList<ClientModel>> GetAllAsync()
        {
            var response = await HttpClient.GetAsync(_clientBaseAddress);

            return await response.Content.ReadAsAsync<IList<ClientModel>>();
        }

        public async Task<ClientModel> GetByIdAsync(int clientId)
        {
            var response = await HttpClient.GetAsync($"{_clientBaseAddress}/{clientId}");

            return await response.Content.ReadAsAsync<ClientModel>();
        }

        public async Task<bool> UpdateAsync(ClientEditCommand command)
        {
            var response = await HttpClient.PostAsync($"{_clientBaseAddress}/edit", command);

            return await response.Content.ReadAsAsync<bool>();
        }
    }
}
