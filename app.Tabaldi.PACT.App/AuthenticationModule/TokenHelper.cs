using System.Configuration;
using System.Windows.Forms;

namespace app.Tabaldi.PACT.App.AuthenticationModule
{
    public static class TokenHelper
    {
        private static string _token;

        public static void SetToken(string token)
        {
            _token = token;

            var configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            configuration.AppSettings.Settings["Token"].Value = token;
            configuration.Save(ConfigurationSaveMode.Modified);
            //ConfigurationManager.RefreshSection("appSettings");
        }

        public static string Token()
        {
            var tokenAppSettings = ConfigurationManager.AppSettings["Token"];
            return string.IsNullOrEmpty(tokenAppSettings) ? _token : tokenAppSettings;
        }
    }
}
