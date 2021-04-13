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

    public static class ConfigHelper
    {
        public static void SetValue(string key, string value)
        {
            var configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            configuration.AppSettings.Settings[key].Value = value;
            configuration.Save(ConfigurationSaveMode.Modified);
        }

        public static string GetValue(string key, string defaultValue)
        {
            var value = ConfigurationManager.AppSettings[key];
            return string.IsNullOrEmpty(value) ? defaultValue : value;
        }
    }
}
