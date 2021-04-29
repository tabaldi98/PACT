using app.Tabaldi.PACT.LibraryModels.AttendanceModule.Enums;
using app.Tabaldi.PACT.LibraryModels.AttendanceRecurrenceModule.Models;
using System;
using System.Collections.Generic;

namespace app.Tabaldi.PACT.LibraryModels.ClientsModule.Models
{
    public class ClientModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public DateTime DateOfBirth { get; set; }
        public decimal Value { get; set; }
        public ChargingType ChargingType { get; set; }
        public string ClinicalDiagnosis { get; set; }
        public string PhysiotherapeuticDiagnosis { get; set; }
        public string Objectives { get; set; }
        public string TreatmentConduct { get; set; }
        public IEnumerable<AttendanceRecurrenceModel> Recurrences { get; set; }
        public bool Enabled { get; set; }
        public DateTimeOffset RegistrationDate { get; set; }
    }
}
