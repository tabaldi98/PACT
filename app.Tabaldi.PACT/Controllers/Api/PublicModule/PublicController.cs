using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace app.Tabaldi.PACT.Api.Controllers.Api.PublicModule
{
    [Authorize]
    [Route("api/public")]
    [ApiController]
    public class PublicController : ControllerBase
    {
        [HttpGet]
        [Route("is-alive")]
        public bool IsAlive()
        {
            return true;
        }
    }
}