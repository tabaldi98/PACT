using app.Tabaldi.PACT.Domain.AttendanceModule.AttendanceAgg;
using app.Tabaldi.PACT.Domain.AttendanceModule.AttendanceRecurrenceAgg;
using app.Tabaldi.PACT.Domain.Seedwork;
using app.Tabaldi.PACT.Domain.UsersModule.UserAgg;
using app.Tabaldi.PACT.LibraryModels.AttendanceModule.Enums;
using System;
using System.Collections.Generic;

namespace app.Tabaldi.PACT.Domain.ClientsModule.ClientAgg
{
    public class Client : Entity
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public DateTime DateOfBirth { get; set; }
        public decimal Value { get; set; }
        public ChargingType ChargingType { get; set; }
        public string ClinicalDiagnosis { get; set; }
        public string PhysiotherapeuticDiagnosis { get; set; }
        public string Objectives { get; set; }
        public string TreatmentConduct { get; set; }
        public bool Enabled { get; set; }
        public DateTimeOffset RegistrationDate { get; set; }
        public virtual int UserID { get; set; }

        // FK
        public virtual User User { get; set; }

        // Reverse Navigation
        private readonly List<AttendanceRecurrence> _recurrences;
        public virtual ICollection<AttendanceRecurrence> Recurrences => _recurrences;

        private readonly List<Attendance> _attendances;
        public virtual ICollection<Attendance> Attendances => _attendances;

        public Client()
        {
            _attendances = new List<Attendance>();
            _recurrences = new List<AttendanceRecurrence>();
            Enabled = true;
            RegistrationDate = DateTimeOffset.Now;
        }

        public Client(string name, DateTime dateBirth, string phone, string clinicalDiagnosis, string physiotherapeuticDiagnosis, string treatmentConduct, string objective, ChargingType chargingType, decimal value, int userId)
            : this()
        {
            UserID = userId;
            SetName(name);
            SetDiagnosis(clinicalDiagnosis, physiotherapeuticDiagnosis);
            SetDateOfBirth(dateBirth);
            SetPhone(phone);
            SetObjective(objective);
            SetTreatmentConduct(treatmentConduct);
            SetCosts(value, chargingType);
        }

        public void SetName(string name)
        {
            Name = name;
        }

        public void SetDiagnosis(string clinicalDiagnosis, string physiotherapeuticDiagnosis)
        {
            PhysiotherapeuticDiagnosis = physiotherapeuticDiagnosis;
            ClinicalDiagnosis = clinicalDiagnosis;
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
            Objectives = objective;
        }

        public void SetTreatmentConduct(string treatmentConduct)
        {
            TreatmentConduct = treatmentConduct;
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

        public void AddRecurrences(IEnumerable<AttendanceRecurrence> attendancesRecurrences)
        {
            _recurrences.AddRange(attendancesRecurrences);
        }

        public void SetEnabled(bool enabled)
        {
            Enabled = enabled;
        }
    }
}
