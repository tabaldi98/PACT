using app.Tabaldi.PACT.Domain.AttendanceModule.AttendanceAgg;
using System;

namespace app.Tabaldi.PACT.Domain.ClientsModule.ClientAgg.Commands
{
    public interface IClientCommandBase
    {
        string Phone { get; }
        DateTime DateOfBirth { get; }
        string Diagnosis { get; }
        string Objective { get; }
        ChargingType ChargingType { get; }
        decimal Value { get; }
    }
}
