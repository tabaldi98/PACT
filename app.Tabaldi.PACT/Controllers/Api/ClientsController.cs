using app.Tabaldi.PACT.Application.ClientsModule;
using app.Tabaldi.PACT.Domain.ClientsModule.ClientAgg.Commands;
using app.Tabaldi.PACT.Domain.ClientsModule.ClientAgg.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace app.Tabaldi.PACT.Api.Controllers.Api
{
    [Route("api/client")]
    [ApiController]
    public class ClientsController
    {
        private readonly IClientAppService _clientAppService;

        public ClientsController(IClientAppService clientAppService)
        {
            _clientAppService = clientAppService;
        }

        [HttpPost]
        public async Task<int> CreateAsync(ClientAddCommand command)
        {
            return await _clientAppService.CreateAsync(command);
        }

        [HttpGet]
        public IQueryable<ClientModel> RetrieveAll()
        {
            return _clientAppService.RetrieveAll();
        }

        [HttpPost]
        [Route("edit")]
        public async Task<bool> EditAsync(ClientEditCommand command)
        {
            return await _clientAppService.UpdateAsync(command);
        }

        [HttpPost]
        [Route("remove")]
        public async Task<bool> RemoveAsync(ClientRemoveCommand command)
        {
            return await _clientAppService.RemoveAsync(command);
        }
    }
}
