using System;
using System.Collections.Generic;
using app.Tabaldi.PACT.Domain.AttendanceModule.AttendanceAgg;

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
        IEnumerable<string> DaysOffAttendance { get; }
        DateTime StartMonday { get; }
        DateTime EndMonday { get; }
        DateTime StartTuesday { get; }
        DateTime EndTuesday { get; }
        DateTime StartWednesday { get; }
        DateTime EndWednesday { get; }
        DateTime StartThursday { get; }
        DateTime EndThursday { get; }
        DateTime StartFriday { get; }
        DateTime EndFriday { get; }
        DateTime StartSaturday { get; }
        DateTime EndSaturday { get; }
        DateTime StartSunday { get; }
        DateTime EndSunday { get; }
    }
}
