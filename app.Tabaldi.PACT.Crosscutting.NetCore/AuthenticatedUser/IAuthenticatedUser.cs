using app.Tabaldi.PACT.LibraryModels.AuthenticationModule.Enums;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace app.Tabaldi.PACT.Crosscutting.NetCore.AuthenticatedUser
{
    public interface IUser
    {
        int ID { get; }
        string UserName { get; }
    }

    public class User : IUser
    {
        public int ID { get; private set; }
        public string UserName { get; private set; }

        public User(int id, string userName)
        {
            ID = id;
            UserName = userName;
        }
    }

    public interface IAuthenticatedUser
    {
        IUser User { get; }
    }

    public class AuthenticatedUser : IAuthenticatedUser
    {
        public IUser User => CreateUser();

        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthenticatedUser(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public IUser CreateUser()
        {
            var claims = _httpContextAccessor.HttpContext.User.Claims;
            var claimId = claims.SingleOrDefault(p => p.Type == CustomClaimTypes.UserID).Value;
            var claimLogon = claims.SingleOrDefault(p => p.Type == CustomClaimTypes.Logon).Value;

            return new User(int.Parse(claimId), claimLogon);
        }
    }
}
