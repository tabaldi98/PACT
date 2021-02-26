using app.Tabaldi.PACT.Domain.ClientsModule.ClientAgg;
using app.Tabaldi.PACT.Domain.ClientsModule.ClientAgg.Commands;
using app.Tabaldi.PACT.Domain.ClientsModule.ClientAgg.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace app.Tabaldi.PACT.Infra.Data.HttpClient.ClientAgg
{
    public interface IClientClientRepository
    {
        Task<int> CreateAsync(ClientAddCommand command);
        Task<IList<ClientModel>> GetAllAsync();
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

        public async Task<bool> UpdateAsync(ClientEditCommand command)
        {
            var response = await HttpClient.PostAsync($"{_clientBaseAddress}/edit", command);

            return await response.Content.ReadAsAsync<bool>();
        }
    }
}
