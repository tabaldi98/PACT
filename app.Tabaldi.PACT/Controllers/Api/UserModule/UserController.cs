using app.Tabaldi.PACT.Application.AuthenticationAgg;
using app.Tabaldi.PACT.LibraryModels.AuthenticationModule.Commands;
using app.Tabaldi.PACT.LibraryModels.AuthenticationModule.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace app.Tabaldi.PACT.Api.Controllers.Api.UserModule
{
    [Authorize]
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly Lazy<IUserProfileAppService> _userProfileAppService;

        public UserController(Lazy<IUserProfileAppService> userProfileAppService)
        {
            _userProfileAppService = userProfileAppService;
        }

        [HttpGet]
        [Route("profile")]
        public async Task<ProfileModel> RetrieveProfileAsync()
        {
            return await _userProfileAppService.Value.RetrieveProfileAsync();
        }

        [HttpPost]
        [Route("profile")]
        public async Task<bool> UpdateProfileAsync(ProfileCommand command)
        {
            return await _userProfileAppService.Value.UpdateProfileAsync(command);
        }
    }
}