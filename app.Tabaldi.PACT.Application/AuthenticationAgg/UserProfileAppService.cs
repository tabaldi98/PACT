using app.Tabaldi.PACT.Application.AuthenticationAgg.Models;
using app.Tabaldi.PACT.Crosscutting.NetCore.AuthenticatedUser;
using app.Tabaldi.PACT.Domain.Seedwork.Contracts.UnitOfWork;
using app.Tabaldi.PACT.Domain.UsersModule.UserAgg;
using app.Tabaldi.PACT.LibraryModels.AuthenticationModule.Commands;
using app.Tabaldi.PACT.LibraryModels.AuthenticationModule.Models;
using System;
using System.Threading.Tasks;

namespace app.Tabaldi.PACT.Application.AuthenticationAgg
{
    public interface IUserProfileAppService
    {
        Task<ProfileModel> RetrieveProfileAsync();
        Task<bool> UpdateProfileAsync(ProfileCommand command);
    }

    public class UserProfileAppService : AppServiceBase<IUserRepository>, IUserProfileAppService
    {
        private readonly Lazy<IAuthenticatedUser> _authenticatedUser;

        public UserProfileAppService(
            IUserRepository repository,
            IUnitOfWork unitOfWork,
            Lazy<IAuthenticatedUser> authenticatedUser)
            : base(repository, unitOfWork)
        {
            _authenticatedUser = authenticatedUser;
        }

        public async Task<ProfileModel> RetrieveProfileAsync()
        {
            return await Repository.SingleOrDefaultAsync(new ProfileModelMapper(UserSpecification.RetrieveByID(_authenticatedUser.Value.User.ID)));
        }

        public async Task<bool> UpdateProfileAsync(ProfileCommand command)
        {
            var user = await Repository.SingleOrDefaultAsync(UserSpecification.RetrieveByID(_authenticatedUser.Value.User.ID), true);
            user.SetData(command.UserName, command.Password, command.FullName, command.Mail);

            return await CommitAsync();
        }
    }
}
