using app.Tabaldi.PACT.Domain.ClientsModule.ClientAgg;
using app.Tabaldi.PACT.Domain.Seedwork;
using app.Tabaldi.PACT.LibraryModels.AttendanceRecurrenceModule.Enums;
using System;

namespace app.Tabaldi.PACT.Domain.AttendanceModule.AttendanceRecurrenceAgg
{
    public class AttendanceRecurrence : Entity
    {
        public WeekDay WeekDay { get; private set; }
        public DateTime StartTime { get; private set; }
        public DateTime EndTime { get; private set; }
        public virtual int ClientID { get; private set; }

        // FK
        public virtual Client Client { get; private set; }

        public AttendanceRecurrence()
        { }

        public AttendanceRecurrence(WeekDay weekDay, DateTime startTime, DateTime endTime, Client client)
        {
            WeekDay = weekDay;
            StartTime = startTime;
            EndTime = endTime;
            Client = client;
            ClientID = client.ID;
        }
    }
}
