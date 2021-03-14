using app.Tabaldi.PACT.Domain.Seedwork.Contracts.Mappers;
using app.Tabaldi.PACT.Domain.Seedwork.Specification;
using app.Tabaldi.PACT.Domain.UsersModule.UserAgg;
using app.Tabaldi.PACT.LibraryModels.AuthenticationModule.Models;
using System;
using System.Linq.Expressions;

namespace app.Tabaldi.PACT.Application.AuthenticationAgg.Models
{
    public class ProfileModelMapper : IHaveMapper<User, ProfileModel>
    {
        public ProfileModelMapper(ISpecification<User> specification = null)
        {
            Specification = specification;
        }

        public Expression<Func<User, ProfileModel>> Selector => user => new ProfileModel()
        {
            ID = user.ID,
            UserName = user.UserName,
            FullName = user.FullName,
            Mail = user.Mail,
            Password = user.Password,
            RegistrationDate = user.RegistrationDate,
        };

        public ISpecification<User> Specification { get; }
    }

}
