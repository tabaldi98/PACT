using System;
using System.Collections.Generic;
using app.Tabaldi.PACT.Domain.AttendanceModule.AttendanceAgg;
using app.Tabaldi.PACT.Domain.AttendanceModule.AttendanceRecurrenceAgg.Commands;

namespace app.Tabaldi.PACT.Domain.ClientsModule.ClientAgg.Commands
{
    public class ClientAddCommand : IClientCommandBase
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Diagnosis { get; set; }
        public string Objective { get; set; }
        public ChargingType ChargingType { get; set; }
        public decimal Value { get; set; }
        public IEnumerable<AttendanceRecurrenceCommand> Recurrences { get; set; }
    }
}
