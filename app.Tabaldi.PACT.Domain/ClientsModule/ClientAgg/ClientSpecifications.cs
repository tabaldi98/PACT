using app.Tabaldi.PACT.Domain.Seedwork.Specification;
using System;

namespace app.Tabaldi.PACT.Domain.ClientsModule.ClientAgg
{
    public static class ClientSpecifications
    {
        public static ISpecification<Client> RetrieveByID(int id)
        {
            return new DirectSpecification<Client>(p => p.ID == id);
        }

        public static ISpecification<Client> RetrieveByName(string name)
        {
            return new DirectSpecification<Client>(p => p.Name.ToLower() == name.ToLower());
        }

        public static ISpecification<Client> RetrieveAttendanceToCurrentDay(DayOfWeek dayOfWeek)
        {
            switch (dayOfWeek)
            {
                case DayOfWeek.Sunday:
                    return new DirectSpecification<Client>(p => p.HasServiceOnSunday);
                case DayOfWeek.Monday:
                    return new DirectSpecification<Client>(p => p.HasServiceOnMonday);
                case DayOfWeek.Tuesday:
                    return new DirectSpecification<Client>(p => p.HasServiceOnTuesday);
                case DayOfWeek.Wednesday:
                    return new DirectSpecification<Client>(p => p.HasServiceOnWednesday);
                case DayOfWeek.Thursday:
                    return new DirectSpecification<Client>(p => p.HasServiceOnThursday);
                case DayOfWeek.Friday:
                    return new DirectSpecification<Client>(p => p.HasServiceOnFriday);
                case DayOfWeek.Saturday:
                    return new DirectSpecification<Client>(p => p.HasServiceOnSaturday);
                default:
                    return null;
            }
        }
    }
}
