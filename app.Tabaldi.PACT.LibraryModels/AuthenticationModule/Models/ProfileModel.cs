using System;

namespace app.Tabaldi.PACT.LibraryModels.AuthenticationModule.Models
{
    public class ProfileModel
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string Mail { get; set; }
        public bool SendAlerts { get; set; }
        public DateTimeOffset RegistrationDate { get; set; }
    }
}
