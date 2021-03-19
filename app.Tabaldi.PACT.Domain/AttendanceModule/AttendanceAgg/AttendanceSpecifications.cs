using app.Tabaldi.PACT.Domain.Seedwork.Specification;
using System;

namespace app.Tabaldi.PACT.Domain.AttendanceModule.AttendanceAgg
{
    public static class AttendanceSpecifications
    {
        public static ISpecification<Attendance> RetrieveByClientID(int clientId)
        {
            return new DirectSpecification<Attendance>(p => p.ClientID == clientId);
        }

        public static ISpecification<Attendance> RetrieveByClientID(int clientId, bool useFilter, DateTime startDate, DateTime endDate)
        {
            if (useFilter)
            {
                startDate = new DateTime(startDate.Year, startDate.Month, startDate.Day, 0, 0, 0, 0);
                endDate = new DateTime(endDate.Year, endDate.Month, endDate.Day, 23, 59, 59, 59);
                return new DirectSpecification<Attendance>(p => p.ClientID == clientId && (p.Date >= startDate && p.Date <= endDate));
            }

            return new DirectSpecification<Attendance>(p => p.ClientID == clientId);
        }

        public static ISpecification<Attendance> RetrieveByID(int id)
        {
            return new DirectSpecification<Attendance>(p => p.ID == id);
        }

        public static ISpecification<Attendance> RetrieveByClientIDAndDate(DateTime day, int clientId)
        {
            var initDate = new DateTime(day.Year, day.Month, day.Day, 0, 0, 0, 0);
            var endDate = new DateTime(day.Year, day.Month, day.Day, 23, 59, 59, 59);
            return new DirectSpecification<Attendance>(p => p.ClientID == clientId && (p.Date >= initDate && p.Date <= endDate));
        }

        public static ISpecification<Attendance> RetrieveByDate(int userId, DateTime startDate, DateTime endDate)
        {
            startDate = new DateTime(startDate.Year, startDate.Month, startDate.Day, 0, 0, 0, 0);
            endDate = new DateTime(endDate.Year, endDate.Month, endDate.Day, 23, 59, 59, 59);
            return new DirectSpecification<Attendance>(p => p.Client.UserID == userId && (p.Date >= startDate && p.Date <= endDate));
        }
    }
}
