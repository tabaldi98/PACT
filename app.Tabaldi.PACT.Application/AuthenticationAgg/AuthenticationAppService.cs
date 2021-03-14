using app.Tabaldi.PACT.Application.AttendanceRecurrenceAgg;
using app.Tabaldi.PACT.Crosscutting.NetCore.Exceptions;
using app.Tabaldi.PACT.Domain.Seedwork.Contracts.UnitOfWork;
using app.Tabaldi.PACT.Domain.UsersModule.UserAgg;
using app.Tabaldi.PACT.LibraryModels.AuthenticationModule.Commands;
using app.Tabaldi.PACT.LibraryModels.AuthenticationModule.Enums;
using app.Tabaldi.PACT.LibraryModels.AuthenticationModule.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace app.Tabaldi.PACT.Application.AuthenticationAgg
{
    public interface IAuthenticationAppService
    {
        Task<JsonWebTokenModel> AuthenticateAsync(AuthenticateCommand command);
    }

    public class AuthenticationAppService : AppServiceBase<IUserRepository>, IAuthenticationAppService
    {
        private readonly IJwtOptions _jwtOptions;

        public AuthenticationAppService(
            IUserRepository repository,
            IUnitOfWork unitOfWork,
            IJwtOptions jwtOptions)
            : base(repository, unitOfWork)
        {
            _jwtOptions = jwtOptions;
        }

        public async Task<JsonWebTokenModel> AuthenticateAsync(AuthenticateCommand command)
        {
            var user = await Repository.SingleOrDefaultAsync(UserSpecification.RetrieveByUserNameAndPassword(command.UserName, command.Password));
            Guard.ObjectNotFound(user);

            var claims = GetClaimsIdentity(user);

            var handler = new JwtSecurityTokenHandler();
            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Subject = claims,
                Issuer = _jwtOptions.Issuer,
                Audience = _jwtOptions.Audience,
                IssuedAt = _jwtOptions.IssuedAt,
                NotBefore = _jwtOptions.NotBefore,
                Expires = _jwtOptions.AccessTokenExpiration,
                SigningCredentials = _jwtOptions.SigningCredentials
            });

            var accessToken = handler.WriteToken(securityToken);
            var expiresIn = CalculateExpiresIn(securityToken.ValidFrom);

            return new JsonWebTokenModel(accessToken, expiresIn);
        }

        private ClaimsIdentity GetClaimsIdentity(User user)
        {
            return new ClaimsIdentity
            (
                new GenericIdentity(user.UserName),
                new[] {
                    new Claim(CustomClaimTypes.UserID, user.ID.ToString()),
                    new Claim(CustomClaimTypes.TokenID, Guid.NewGuid().ToString()),
                    new Claim(CustomClaimTypes.Logon, user.UserName),
                    new Claim(CustomClaimTypes.FullName, user.FullName),
                    new Claim(CustomClaimTypes.Email, user.Mail),
                }
            );
        }

        private long CalculateExpiresIn(DateTime securityTokenValidFrom)
        {
            var differenceInSecondsBetweenUtcNowAndValidFrom = (DateTimeOffset.UtcNow - securityTokenValidFrom).TotalSeconds;
            var expirationSeconds = TimeSpan.FromMinutes(_jwtOptions.ExpirationMinutes).TotalSeconds;

            return Convert.ToInt64(expirationSeconds - differenceInSecondsBetweenUtcNowAndValidFrom);
        }
    }
}
