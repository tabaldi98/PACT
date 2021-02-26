using System;

namespace app.Tabaldi.PACT.Domain.ClientsModule.ClientAgg.Commands
{
    public class ClientEditCommand
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Diagnosis { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Phone { get; set; }
        public string Objective { get; set; }
    }
}
