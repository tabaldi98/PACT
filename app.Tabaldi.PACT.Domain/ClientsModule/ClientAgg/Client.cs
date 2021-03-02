using app.Tabaldi.PACT.Domain.AttendanceModule.AttendanceAgg;
using app.Tabaldi.PACT.Domain.AttendanceModule.AttendanceRecurrenceAgg;
using app.Tabaldi.PACT.Domain.Seedwork;
using System;
using System.Collections.Generic;

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
        public DateTime RegistrationDate { get; set; }

        // Reverse Navigation
        private readonly List<AttendanceRecurrence> _recurrences;
        public virtual ICollection<AttendanceRecurrence> Recurrences => _recurrences;

        public Client()
        {
            _recurrences = new List<AttendanceRecurrence>();
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

        public void AddRecurrence(AttendanceRecurrence attendanceRecurrence)
        {
            Recurrences.Add(attendanceRecurrence);
        }

        public void AddRecurrence(IList<AttendanceRecurrence> attendancesRecurrences)
        {
            foreach (var attendanceRecurrence in attendancesRecurrences)
            {
                AddRecurrence(attendanceRecurrence);
            }
        }
    }
}
