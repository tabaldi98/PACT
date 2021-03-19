using app.Tabaldi.PACT.LibraryModels.AuthenticationModule.Commands;
using app.Tabaldi.PACT.LibraryModels.AuthenticationModule.Models;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace app.Tabaldi.PACT.Infra.Data.HttpClient.AuthenticationModule
{
    public interface IUserRepository
    {
        Task<JsonWebTokenModel> AuthenticateAsync(AuthenticateCommand command);
        Task<ProfileModel> GetProfileAsync(string token);
        Task<bool> UpdateProfileAsync(ProfileCommand command);
    }

    public class UserRepository : GenericRepositoryBase, IUserRepository
    {
        private readonly string _baseAddress;
        private readonly string _tokenBaseAddress = "token";
        private readonly string _userBaseAddress = "api/user";

        public UserRepository()
            : base()
        {
            _baseAddress = System.Configuration.ConfigurationManager.AppSettings["ServerBaseAddress"];
        }

        public async Task<JsonWebTokenModel> AuthenticateAsync(AuthenticateCommand command)
        {
            var response = await HttpClient.PostAsync(_tokenBaseAddress, command);

            return await response.Content.ReadAsAsync<JsonWebTokenModel>();
        }

        public async Task<ProfileModel> GetProfileAsync(string token)
        {
            using (var httpClient = new System.Net.Http.HttpClient())
            {
                httpClient.BaseAddress = new Uri(_baseAddress);
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await httpClient.GetAsync($"{_userBaseAddress}/profile");

                return await response.Content.ReadAsAsync<ProfileModel>(); ;
            }
        }

        public async Task<bool> UpdateProfileAsync(ProfileCommand command)
        {
            var response = await HttpClient.PostAsync($"{_userBaseAddress}/profile", command);

            return await response.Content.ReadAsAsync<bool>();
        }
    }
}
