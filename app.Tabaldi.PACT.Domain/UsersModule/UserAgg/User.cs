using app.Tabaldi.PACT.Domain.ClientsModule.ClientAgg;
using app.Tabaldi.PACT.Domain.Seedwork;
using System;
using System.Collections.Generic;

namespace app.Tabaldi.PACT.Domain.UsersModule.UserAgg
{
    public class User : Entity
    {
        public string UserName { get; private set; }
        public string Password { get; private set; }
        public string FullName { get; private set; }
        public string Mail { get; private set; }
        public bool SendAlerts { get; private set; }
        public DateTimeOffset RegistrationDate { get; private set; }

        // Reverse Navigation
        private readonly List<Client> _clients;
        public virtual ICollection<Client> Clients => _clients;

        public User()
        {
            RegistrationDate = DateTimeOffset.UtcNow;
            _clients = new List<Client>();
        }

        public User(string userName, string password, string fullName = null, string email = null)
            : this()
        {
            SetData(userName, password, fullName, email);
        }

        public void SetData(string userName, string password, string fullName = null, string email = null, bool sendAlerts = false)
        {
            UserName = userName;
            Password = password;
            FullName = fullName;
            Mail = email;
            SendAlerts = sendAlerts;
        }
    }
}
