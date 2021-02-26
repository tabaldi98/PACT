using app.Tabaldi.PACT.Domain.Seedwork;
using System;

namespace app.Tabaldi.PACT.Domain.ClientsModule.ClientAgg
{
    public class Client : Entity
    {
        public string Name { get; private set; }
        public string Diagnosis { get; private set; }
        public DateTime DateOfBirth { get; private set; }
        public string Phone { get; private set; }
        public DateTime RegistrationDate { get; set; }
        public string Objective { get; set; }

        public Client()
        {
            RegistrationDate = DateTime.UtcNow;
        }

        public Client(string name, string diagnosis, DateTime dateBirth, string phone, string objective)
            : this()
        {
            SetName(name);
            SetDiagnosis(diagnosis);
            SetDateOfBirth(dateBirth);
            SetPhone(phone);
            SetObjective(objective);
        }

        public void SetName(string name)
        {
            Name = name;
        }

        public void SetDiagnosis(string diagnosis)
        {
            Diagnosis = diagnosis;
        }

        public void SetDateOfBirth(DateTime dateBirth)
        {
            DateOfBirth = dateBirth;
        }

        public void SetPhone(string phone)
        {
            Phone = phone;
        }

        public void SetObjective(string objective)
        {
            Objective = objective;
        }
    }
}
