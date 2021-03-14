using app.Tabaldi.PACT.Domain.Seedwork.Specification;

namespace app.Tabaldi.PACT.Domain.UsersModule.UserAgg
{
    public static class UserSpecification
    {
        public static ISpecification<User> RetrieveByID(int id)
        {
            return new DirectSpecification<User>(p => p.ID == id);
        }

        public static ISpecification<User> RetrieveByUserNameAndPassword(string userName, string password)
        {
            return new DirectSpecification<User>(p => p.UserName.ToLower().Equals(userName.ToLower()) && p.Password.Equals(password));
        }
    }
}
