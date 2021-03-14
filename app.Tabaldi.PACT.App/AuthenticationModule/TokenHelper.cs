using System.Configuration;

namespace app.Tabaldi.PACT.App.AuthenticationModule
{
    public static class TokenHelper
    {
        private static string _token;

        public static void SetToken(string token)
        {
            _token = token;
            ConfigurationManager.AppSettings["Token"] = token;
        }

        public static string Token()
        {
            var tokenAppSettings = ConfigurationManager.AppSettings["Token"];
            return string.IsNullOrEmpty(tokenAppSettings) ? _token : tokenAppSettings;
        }
    }
}
