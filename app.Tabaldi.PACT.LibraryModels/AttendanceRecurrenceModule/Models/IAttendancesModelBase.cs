using System;

namespace app.Tabaldi.PACT.LibraryModels.AttendanceRecurrenceModule.Models
{
    public interface IAttendancesModelBase
    {
        string ClientName { get; }
        DateTime StartAttendance { get; }
        DateTime EndAttendance { get; }
    }
}
