using app.Tabaldi.PACT.Domain.ClientsModule.ClientAgg;
using app.Tabaldi.PACT.Domain.Seedwork;
using System;

namespace app.Tabaldi.PACT.Domain.AttendanceModule.AttendanceAgg
{
    public class Attendance : Entity
    {
        public virtual int ClientID { get; private set; }
        public DateTime Date { get; private set; }
        public DateTime HourInitial { get; private set; }
        public DateTime HourFinish { get; private set; }
        public string Description { get; private set; }
        public bool AlertHasSend { get; private set; }

        // FK
        public virtual Client Client { get; set; }

        public Attendance()
        { }

        public Attendance(int clientId, DateTime date, DateTime hourInitial, DateTime hourFinish, string description = null)
        {
            ClientID = clientId;
            SetDate(date, hourInitial, hourFinish);
            Description = description;
            AlertHasSend = false;
        }

        public void SetDate(DateTime date, DateTime hourInitial, DateTime hourFinish)
        {
            Date = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);
            HourInitial = hourInitial;
            HourFinish = hourFinish;
        }

        public void SetDescription(string description)
        {
            Description = description;
        }

        public void SetAlertSended()
        {
            AlertHasSend = true;
        }
    }
}
