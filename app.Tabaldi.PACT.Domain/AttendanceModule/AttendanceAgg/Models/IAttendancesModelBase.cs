using System;

namespace app.Tabaldi.PACT.Domain.AttendanceModule.AttendanceAgg.Models
{
    public interface IAttendancesModelBase
    {
        string ClientName { get; }
        DateTime StartAttendance { get; }
        DateTime EndAttendance { get; }
    }
}
