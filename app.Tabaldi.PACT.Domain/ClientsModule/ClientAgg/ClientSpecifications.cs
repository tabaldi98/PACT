using app.Tabaldi.PACT.Domain.Seedwork.Specification;

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

        public static ISpecification<Client> RetrieveByUserID(int userId)
        {
            return new DirectSpecification<Client>(p => p.UserID == userId);
        }
    }
}
