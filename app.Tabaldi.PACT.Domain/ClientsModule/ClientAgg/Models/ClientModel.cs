using app.Tabaldi.PACT.Domain.ClientsModule.ClientAgg;
using app.Tabaldi.PACT.Domain.Seedwork.Contracts.Mappers;
using app.Tabaldi.PACT.Domain.Seedwork.Specification;
using System;
using System.Linq.Expressions;

namespace app.Tabaldi.PACT.Domain.ClientsModule.ClientAgg.Models
{
    public class ClientModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Diagnosis { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Phone { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string Objective { get; set; }
    }

    public class ClientModelMapper : IHaveMapper<Client, ClientModel>
    {
        public ClientModelMapper()
        { }

        public Expression<Func<Client, ClientModel>> Selector => client => new ClientModel()
        {
            ID = client.ID,
            Name = client.Name,
            DateOfBirth = client.DateOfBirth,
            Diagnosis = client.Diagnosis,
            Phone = client.Phone,
            RegistrationDate = client.RegistrationDate,
            Objective = client.Objective,
        };

        public ISpecification<Client> Specification => null;
    }
}
