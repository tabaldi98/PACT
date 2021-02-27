using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using app.Tabaldi.PACT.Domain.AttendanceModule.AttendanceAgg;
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
        public string Diagnosis { get; set; }
        public string Objective { get; set; }
        public ChargingType ChargingType { get; set; }
        public decimal Value { get; set; }
        public IEnumerable<string> DaysOffAttendance { get; set; }
        public DateTime? StartMonday { get; set; }
        public DateTime? EndMonday { get; set; }
        public DateTime? StartTuesday { get; set; }
        public DateTime? EndTuesday { get; set; }
        public DateTime? StartWednesday { get; set; }
        public DateTime? EndWednesday { get; set; }
        public DateTime? StartThursday { get; set; }
        public DateTime? EndThursday { get; set; }
        public DateTime? StartFriday { get; set; }
        public DateTime? EndFriday { get; set; }
        public DateTime? StartSaturday { get; set; }
        public DateTime? EndSaturday { get; set; }
        public DateTime? StartSunday { get; set; }
        public DateTime? EndSunday { get; set; }
        public DateTime RegistrationDate { get; set; }

        public ClientModel()
        {
            DaysOffAttendance = new List<string>();
        }
    }

    public class ClientModelMapper : IHaveMapper<Client, ClientModel>
    {
        public ClientModelMapper()
        { }

        public Expression<Func<Client, ClientModel>> Selector => client => new ClientModel()
        {
            //

        };

        public ISpecification<Client> Specification => null;
    }
}
