using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using app.Tabaldi.PACT.Domain.AttendanceModule.AttendanceAgg;
using app.Tabaldi.PACT.Domain.AttendanceModule.AttendanceRecurrenceAgg.Models;
using app.Tabaldi.PACT.Domain.Seedwork.Contracts.Mappers;
using app.Tabaldi.PACT.Domain.Seedwork.Specification;

namespace app.Tabaldi.PACT.Domain.ClientsModule.ClientAgg.Models
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
        public DateTimeOffset RegistrationDate { get; set; }
    }

    public class ClientModelMapper : IHaveMapper<Client, ClientModel>
    {
        public ClientModelMapper(ISpecification<Client> specification = null)
        {
            Specification = specification;
        }

        public Expression<Func<Client, ClientModel>> Selector => client => new ClientModel()
        {
            ID = client.ID,
            Name = client.Name,
            Phone = client.Phone,
            DateOfBirth = client.DateOfBirth,
            Objectives = client.Objectives,
            ClinicalDiagnosis = client.ClinicalDiagnosis,
            PhysiotherapeuticDiagnosis = client.PhysiotherapeuticDiagnosis,
            RegistrationDate = client.RegistrationDate,
            TreatmentConduct = client.TreatmentConduct,
            ChargingType = client.ChargingType,
            Value = client.Value,
            Recurrences = client.Recurrences.Select(p => new AttendanceRecurrenceModel()
            {
                ID = p.ID,
                WeekDay = p.WeekDay,
                StartTime = p.StartTime,
                EndTime = p.EndTime,
            }),
        };

        public ISpecification<Client> Specification { get; }
    }
}
