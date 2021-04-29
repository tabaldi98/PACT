using app.Tabaldi.PACT.Domain.ClientsModule.ClientAgg;
using app.Tabaldi.PACT.Domain.Seedwork.Contracts.Mappers;
using app.Tabaldi.PACT.Domain.Seedwork.Specification;
using app.Tabaldi.PACT.LibraryModels.AttendanceRecurrenceModule.Models;
using app.Tabaldi.PACT.LibraryModels.ClientsModule.Models;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace app.Tabaldi.PACT.Application.ClientsModule.Models
{
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
            Enabled = client.Enabled,
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
