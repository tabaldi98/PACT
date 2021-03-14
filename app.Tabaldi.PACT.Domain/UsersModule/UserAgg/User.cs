using app.Tabaldi.PACT.Domain.Seedwork;
using System;

namespace app.Tabaldi.PACT.Domain.UsersModule.UserAgg
{
    public class User : Entity
    {
        public string UserName { get; private set; }
        public string Password { get; private set; }
        public string FullName { get; private set; }
        public string Mail { get; private set; }
        public DateTimeOffset RegistrationDate { get; private set; }

        public User()
        {
            RegistrationDate = DateTimeOffset.UtcNow;
        }

        public User(string userName, string password, string fullName = null, string email = null)
            : this()
        {
            SetData(userName, password, fullName, email);
        }

        public void SetData(string userName, string password, string fullName = null, string email = null)
        {
            UserName = userName;
            Password = password;
            FullName = fullName;
            Mail = email;
        }
    }
}
