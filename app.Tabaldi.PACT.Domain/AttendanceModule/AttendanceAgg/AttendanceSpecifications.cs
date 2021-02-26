using app.Tabaldi.PACT.Domain.Seedwork.Specification;

namespace app.Tabaldi.PACT.Domain.AttendanceModule.AttendanceAgg
{
    public static class AttendanceSpecifications
    {
        public static ISpecification<Attendance> RetrieveByClientID(int clientId)
        {
            return new DirectSpecification<Attendance>(p => p.ClientID == clientId);
        }

        public static ISpecification<Attendance> RetrieveByID(int id)
        {
            return new DirectSpecification<Attendance>(p => p.ID == id);
        }
    }
}
