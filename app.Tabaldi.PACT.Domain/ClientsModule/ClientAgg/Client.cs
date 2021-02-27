using app.Tabaldi.PACT.Domain.AttendanceModule.AttendanceAgg;
using app.Tabaldi.PACT.Domain.Seedwork;
using System;

namespace app.Tabaldi.PACT.Domain.ClientsModule.ClientAgg
{
    public class Client : Entity
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Diagnosis { get; set; }
        public string Objective { get; set; }
        public ChargingType ChargingType { get; set; }
        public decimal Value { get; set; }
        public bool HasServiceOnMonday { get; set; }
        public DateTime? StartMonday { get; set; }
        public DateTime? EndMonday { get; set; }
        public bool HasServiceOnTuesday { get; set; }
        public DateTime? StartTuesday { get; set; }
        public DateTime? EndTuesday { get; set; }
        public bool HasServiceOnWednesday { get; set; }
        public DateTime? StartWednesday { get; set; }
        public DateTime? EndWednesday { get; set; }
        public bool HasServiceOnThursday { get; set; }
        public DateTime? StartThursday { get; set; }
        public DateTime? EndThursday { get; set; }
        public bool HasServiceOnFriday { get; set; }
        public DateTime? StartFriday { get; set; }
        public DateTime? EndFriday { get; set; }
        public bool HasServiceOnSaturday { get; set; }
        public DateTime? StartSaturday { get; set; }
        public DateTime? EndSaturday { get; set; }
        public bool HasServiceOnSunday { get; set; }
        public DateTime? StartSunday { get; set; }
        public DateTime? EndSunday { get; set; }
        public DateTime RegistrationDate { get; set; }

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

        public void SetCosts(decimal value, ChargingType chargingType)
        {
            Value = value;
            ChargingType = chargingType;
        }

        #region Segunda-feira
        public void SetServiceAsMonday(DateTime startDate, DateTime endDate)
        {
            HasServiceOnMonday = true;
            StartMonday = startDate;
            EndMonday = endDate;
        }

        public void SetNoServiceAsMonday()
        {
            HasServiceOnMonday = false;
            StartMonday = null;
            EndMonday = null;
        }
        #endregion

        #region Terça-feira
        public void SetServiceAsTuesday(DateTime startDate, DateTime endDate)
        {
            HasServiceOnTuesday = true;
            StartTuesday = startDate;
            EndTuesday = endDate;
        }

        public void SetNoServiceAsTuesday()
        {
            HasServiceOnTuesday = false;
            StartTuesday = null;
            EndTuesday = null;
        }
        #endregion

        #region Quarta-feira
        public void SetServiceAsWednesday(DateTime startDate, DateTime endDate)
        {
            HasServiceOnWednesday = true;
            StartWednesday = startDate;
            EndWednesday = endDate;
        }

        public void SetNoServiceAsWednesday()
        {
            HasServiceOnWednesday = false;
            StartWednesday = null;
            EndWednesday = null;
        }
        #endregion

        #region Quinta-feira
        public void SetServiceAsThursday(DateTime startDate, DateTime endDate)
        {
            HasServiceOnThursday = true;
            StartThursday = startDate;
            EndThursday = endDate;
        }

        public void SetNoServiceAsThursday()
        {
            HasServiceOnThursday = false;
            StartThursday = null;
            EndThursday = null;
        }
        #endregion

        #region Sexta-feira
        public void SetServiceAsFriday(DateTime startDate, DateTime endDate)
        {
            HasServiceOnFriday = true;
            StartFriday = startDate;
            EndFriday = endDate;
        }

        public void SetNoServiceAsFriday()
        {
            HasServiceOnFriday = false;
            StartFriday = null;
            EndFriday = null;
        }
        #endregion

        #region Sábado
        public void SetServiceAsSaturday(DateTime startDate, DateTime endDate)
        {
            HasServiceOnSaturday = true;
            StartSaturday = startDate;
            EndSaturday = endDate;
        }

        public void SetNoServiceAsSaturday()
        {
            HasServiceOnSaturday = false;
            StartSaturday = null;
            EndSaturday = null;
        }
        #endregion

        #region Domingo
        public void SetServiceAsSunday(DateTime startDate, DateTime endDate)
        {
            HasServiceOnSunday = true;
            StartSunday = startDate;
            EndSunday = endDate;
        }

        public void SetNoServiceAsSunday()
        {
            HasServiceOnSunday = false;
            StartSunday = null;
            EndSunday = null;
        }
        #endregion
    }
}
