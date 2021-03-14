using app.Tabaldi.PACT.Application.AuthenticationAgg;
using app.Tabaldi.PACT.LibraryModels.AuthenticationModule.Commands;
using app.Tabaldi.PACT.LibraryModels.AuthenticationModule.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace app.Tabaldi.PACT.Api.Controllers.Api.AuthenticationModule
{
    [AllowAnonymous]
    [Route("token")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IAuthenticationAppService _authenticationAppService;

        public TokenController(IAuthenticationAppService authenticationAppService)
        {
            _authenticationAppService = authenticationAppService;
        }

        [HttpPost]
        [Route("")]
        public async Task<JsonWebTokenModel> AuthenticateAsync(AuthenticateCommand command)
        {
            return await _authenticationAppService.AuthenticateAsync(command);
        }
    }
}